using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Mocking
{

    #region Models

    public class Order
    {
        public DateTime OrderedDate { get; set; }
        public OrderStatus Status { get; set; }

        public Order()
        {
            Details = new List<OrderDetail>();
        }

        public List<OrderDetail> Details { get; set; }

        public decimal Total => Details.Sum(d => d.Total);
    }

    public enum OrderStatus
    {
        Boxing,
        Sent,
    }

    public class OrderDetail
    {
        public OrderDetail(decimal unitPrice, short quantity = 1)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public short Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => Quantity * UnitPrice;
    }

    public abstract class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }


    public class Employee : User
    {
        public bool IsBoss { get; set; }
    }

    public class Bot : User
    {

    }

    public abstract class Report
    {
        public DateTime CreatedOn { get; set; }

        public string Name { get; }

        public Report()
        {
            CreatedOn = DateTime.Now;
        }
    }



    public class SalesReport : Report
    {
        public string Title { get; set; }
        public TimeSpan TotalTime { get; set; }

        public decimal TotalAmount { get; set; }


        public override string ToString()
        {
            return $"Report created on {CreatedOn} \r\n TotalAmount: {TotalAmount}";
        }

        public string ToHtml()
        {
            return $"<html>Report created on <b>{CreatedOn}</b> <p>TotalAmount: {TotalAmount}<p></html>";
        }
    }

    #endregion


    public interface ISalesReportBuilder
    {
        void Add(string title);
        void Add(IEnumerable<Order> orders);
        SalesReport Build();
    }

    public class SalesReportBuilder : ISalesReportBuilder
    {
        private string title;
        private IEnumerable<Order> orders;

        public void Add(string title)
        {
            this.title = title;
        }

        public void Add(IEnumerable<Order> orders)
        {
            this.orders = orders;
        }

        public SalesReport Build()
        {
            SalesReport salesReport = Create(orders);
            salesReport.Title = title;

            return salesReport;
        }

        private static SalesReport Create(IEnumerable<Order> orders)
        {
            SalesReport salesReport = new SalesReport();

            salesReport.TotalAmount = orders.Sum(o => o.Total);

            return salesReport;
        }
    }

    public interface IReportService
    {
        Task SendSalesReportEmailAsync(DateTime date);
    }

    public interface IOrderService
    {
        IEnumerable<Order> Get(DateTime from, DateTime to);
    }

    public interface IUserService
    {
        IEnumerable<User> GetBosses();
        User GetBot();
    }

    public class DbUserService : IUserService
    {
        private readonly SalesContext salesContext;

        public DbUserService(SalesContext salesContext)
        {
            this.salesContext = salesContext;
        }

        public IEnumerable<User> GetBosses()
        {
           return salesContext.Users.OfType<Employee>().Where(e => e.IsBoss).ToList();
        }

        public User GetBot()
        {
            return salesContext.Users.OfType<Bot>().Single();
        }
    }

    public interface ILogger
    {
        void Info(string message);
        void Error(string message);
    }

    public class NLogLogger : ILogger
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public void Error(string message)
        {
            Logger.Error(message);
        }

        public void Info(string message)
        {
            Logger.Info(message);
        }
    }

    public class ReportService : IReportService
    {
        private const string apikey = "your_secret_key";

        public delegate void ReportSentHandler(object sender, ReportSentEventArgs e);
        public event ReportSentHandler ReportSent;

        private readonly IOrderService orderService;
        private readonly ISendGridClient client;
        private readonly IUserService userService;
        private readonly ILogger logger;
        private readonly ISalesReportBuilder salesReportBuilder;

        public ReportService(
            IOrderService orderService, 
            ISendGridClient sendGridClient, 
            IUserService userService, 
            ILogger logger,
            ISalesReportBuilder salesReportBuilder)
        {
            this.orderService = orderService;
            this.client = sendGridClient;
            this.userService = userService;
            this.logger = logger;
            this.salesReportBuilder = salesReportBuilder;
        }

        public async Task SendSalesReportEmailAsync(DateTime date)
        {
            var orders = orderService.Get(date.AddDays(-7), date);

            if (!orders.Any())
            {
                return;
            }

            salesReportBuilder.Add(orders);
            salesReportBuilder.Add("Raport sprzedaży");

            SalesReport report = salesReportBuilder.Build();

            var recipients = userService.GetBosses();

            var sender = userService.GetBot();

            foreach (var recipient in recipients)
            {
                if (recipient.Email == null)
                    continue;

                var message = MailHelper.CreateSingleEmail(
                    new EmailAddress(sender.Email, $"{sender.FirstName} {sender.LastName}"), 
                    new EmailAddress(recipient.Email, $"{recipient.FirstName} {recipient.LastName}"), 
                    "Raport sprzedaży",
                    report.ToString(),
                    report.ToHtml());


                logger.Info($"Wysyłanie raportu do {recipient.FirstName} {recipient.LastName} <{recipient.Email}>...");

                var response = await client.SendEmailAsync(message);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    ReportSent?.Invoke(this, new ReportSentEventArgs(DateTime.Now));

                    logger.Info($"Raport został wysłany.");
                }
                else
                {
                    logger.Error($"Błąd podczas wysyłania raportu.");

                    throw new ApplicationException("Błąd podczas wysyłania raportu.");
                }
            }
        }

        
    }

    public class ReportSentEventArgs : EventArgs
    {
        public readonly DateTime SentDate;

        public ReportSentEventArgs(DateTime sentDate)
        {
            this.SentDate = sentDate;
        }
    }

    public class OrderService : IOrderService
    {
        private readonly SalesContext context;

        public OrderService(SalesContext context)
        {
            this.context = context;
        }

        public IEnumerable<Order> Get(DateTime from, DateTime to)
        {
            return context.Orders.Where(o => o.OrderedDate > from && o.OrderedDate < to).ToList();
        }
    }

    public class SalesContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
    }


   

}
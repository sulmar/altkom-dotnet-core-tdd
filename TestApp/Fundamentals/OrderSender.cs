using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace TestApp.Fundamentals
{
    #region Models

    public abstract class Base
    {

    }

    public class Order : Base
    {
        public string Number { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }

    #endregion

    /*
    public class OrderSender
    {
        public void Send(Order order)
        {
            decimal shippingCost = CalculateShippingCost(order);

            SendEmail($"Koszt dostawy {shippingCost}");
        }

        private void SendEmail(string messsage)
        {
            Console.WriteLine($"Sending {messsage}...");
        }

        private decimal CalculateShippingCost(Order order)
        {
            if (order.TotalAmount > 1000)
                return 0;
            else
                return 9.99m;
        }
    }

    */

    public interface IOrderShippingCalculator
    {
        decimal CalculateShippingCost(Order order);
    }

    public interface IOrderTotalCalculator
    {
        decimal CalculateTotalCost(Order order);
    }

    public class OrderCalculator : IOrderShippingCalculator, IOrderTotalCalculator
    {
        public decimal CalculateShippingCost(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(Order));

            if (order.TotalAmount >= 1000)
                return 0;
            else
                return 9.99m;
        }

        public decimal CalculateTotalCost(Order order)
        {
            return order.TotalAmount * 0.1m;
        }
    }

    public class Message
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
    }

    public interface IMessageService
    {
        void Send(Message message);
    }

    public class EmailMessageService : IMessageService
    {
        public DateTime LastSentDate { get; private set; }

        public void Send(Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            Validate(message);

            SendEmail(message.From, message.To, message.Content);
        }

        private static void Validate(Message message)
        {
            if (string.IsNullOrEmpty(message.From))
            {
                throw new ArgumentException(nameof(Message.From));
            }

            if (string.IsNullOrEmpty(message.To))
            {
                throw new ArgumentException(nameof(Message.To));
            }

            if (string.IsNullOrEmpty(message.Content))
            {
                throw new ArgumentException(nameof(Message.Content));
            }
        }

        private void SendEmail(string from, string to, string message)
        {
            Console.WriteLine($"Sending {message}...");

            LastSentDate = DateTime.UtcNow;
        }
    }

    public class OrderSender
    {
        private readonly IOrderShippingCalculator orderShippingCalculator;
        private readonly IMessageService messageService;

        public OrderSender(IOrderShippingCalculator orderShippingCalculator, IMessageService messageService)
        {
            this.orderShippingCalculator = orderShippingCalculator ?? throw new ArgumentNullException(nameof(orderShippingCalculator));
            this.messageService = messageService;
        }

        public void Send(Order order)
        {
            decimal shippingCost = orderShippingCalculator.CalculateShippingCost(order);

            Message message = new Message { Content = $"Koszt dostawy {shippingCost}" };

            messageService.Send(message);
        }

     
       
    }
}

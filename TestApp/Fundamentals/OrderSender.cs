using System;
using System.Collections.Generic;
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
}

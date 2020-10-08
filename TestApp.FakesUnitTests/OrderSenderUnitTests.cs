using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TestApp.Fundamentals;
using Xunit;

namespace TestApp.FakesUnitTests
{

    public class FakeMessageService : IMessageService
    {
        public DateTime LastSentDate { get; private set; }

        public void Send(Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            Validate(message);

            SendEmail(message.From, message.To, message.Content);
        }

        private void SendEmail(string from, string to, string message)
        {
            Trace.WriteLine($"Sending {message}...");

            LastSentDate = DateTime.UtcNow;
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
    }

    public class OrderSenderUnitTests
    {
        [Fact]
        public void Send_ValidOrder_RaiseOrderSentEvent()
        {
            // Arrange
            OrderSender orderSender = new OrderSender(new OrderCalculator(), new FakeMessageService());

            Order id = null;

            orderSender.OrderSent += (sender, args) => { id = args; };

            // Act
            orderSender.Send(new Order());

            // Asserts
            Assert.NotNull(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Fundamentals;
using Xunit;

namespace TestApp.xUnitTests
{
    public class OrderSenderUnitTests
    {
        [Fact]
        public void Send_ValidOrder_RaiseOrderSentEvent()
        {
            // Arrange
            OrderSender orderSender = new OrderSender(new OrderCalculator(), new EmailMessageService());

            Order id = null;

            orderSender.OrderSent += (sender, args) => { id = args; };

            // Act
            orderSender.Send(new Order());

            // Asserts
            Assert.NotNull(id);
        }
    }
}

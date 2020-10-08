using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Fundamentals;
using Xunit;

namespace TestApp.MocksUnitTests
{
    public class OrderSenderUnitTests
    {
        [Fact]
        public void Send_ValidOrder_RaiseOrderSentEvent()
        {
            // Arrange

            Mock<IMessageService> mockMessageService = new Mock<IMessageService>();

            mockMessageService
                .Setup(ms => ms.Send(It.IsAny<Message>()))
                .Verifiable();

            IMessageService messageService = mockMessageService.Object;

            OrderSender orderSender = new OrderSender(new OrderCalculator(), messageService);

            Order id = null;

            orderSender.OrderSent += (sender, args) => { id = args; };

            // Act
            orderSender.Send(new Order());

            // Asserts
            Assert.NotNull(id);
        }


        // mock.Setup().Verifiable();

        // mock.Verify();

    }
}

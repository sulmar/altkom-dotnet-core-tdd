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
        private Mock<IMessageService> mockMessageService;
        private IMessageService messageService;

        public OrderSenderUnitTests()
        {
            mockMessageService = new Mock<IMessageService>();
            messageService = mockMessageService.Object;
        }

        [Fact]
        public void Send_ValidOrder_RaiseOrderSentEvent()
        {
            // Arrange
            mockMessageService
                .Setup(ms => ms.Send(It.IsAny<Message>()))
                .Verifiable();

            OrderSender orderSender = new OrderSender(new OrderCalculator(), messageService);

            Order id = null;

            orderSender.OrderSent += (sender, args) => { id = args; };

            // Act
            orderSender.Send(new Order());

            // Asserts
            Assert.NotNull(id);
        }


        [Fact]
        public void Send_ValidOrder_ShouldSendMessage()
        {
            // Arrange
            OrderSender orderSender = new OrderSender(new OrderCalculator(), messageService);

            // Act
            orderSender.Send(new Order() { TotalAmount = 1m });

            // Assert
            mockMessageService.Verify(ms => ms.Send(It.IsAny<Message>()), Times.Once);
        }

        [Fact]
        public void Send_InvalidOrder_ShouldntSendMessage()
        {
            // Arrange
            OrderSender orderSender = new OrderSender(new OrderCalculator(), messageService);

            // Act
            orderSender.Send(new Order() { TotalAmount = 0m });

            // Assert
            mockMessageService.Verify(ms => ms.Send(It.IsAny<Message>()), Times.Never);
        }

    }
}

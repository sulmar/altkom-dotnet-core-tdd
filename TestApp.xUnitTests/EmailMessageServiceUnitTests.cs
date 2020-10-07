using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Fundamentals;
using Xunit;

namespace TestApp.xUnitTests
{
    public class EmailMessageServiceUnitTests
    {
        private EmailMessageService messageService;

        public EmailMessageServiceUnitTests()
        {
            messageService = new EmailMessageService();
        }

        [Fact]
        public void Send_ValidMessage_SetLastSentDate()
        {
            // Arrange
            Message message = new Message { From = "a", To = "b", Content = "abc" };

            // Act
            messageService.Send(message);

            // Assert
            Assert.NotEqual(DateTime.MinValue, messageService.LastSentDate);
        }

        [Fact]
        public void Send_InvalidMessage_ThrowArgumentException()
        {
            // Arrange
            Message message = new Message();

            // Act
            Action act = ()=> messageService.Send(message);

            // Assert
            Assert.Throws<ArgumentException>(act);



        }
    }
}

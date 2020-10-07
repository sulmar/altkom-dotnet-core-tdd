using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestApp.xUnitTests
{
    public class LoggerUnitTests
    {
        [Fact]
        public void Log_ValidMessage_RaiseMessageLoggedEvent()
        {
            // Arrange
            var logger = new Logger();

            var id = DateTime.MinValue;

            logger.MessageLogged += (sender, args) => { id = args; };

            // Act
            logger.Log("a");

            // Asserts
            Assert.NotEqual(DateTime.MinValue, id);

        }
    }
}

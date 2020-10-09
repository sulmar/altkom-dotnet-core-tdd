using FluentAssertions;
using Moq;
using RabbitMQ.Client;
using RpcConsoleClient;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestApp.MocksUnitTests
{
    public class RpcClientUnitTests
    {
        [Fact]
        public void Call_ValidMessage_ReturnsString()
        {
            // Arrange
           IRpcClient client = Mock.Of<IRpcClient>(c => c.Call("ping") == "pong");

            //var mockClient = new Mock<IRpcClient>();
            //mockClient
            //  .Setup(c => c.Call("ping"))
            //  .Returns("pong");
            //var client = mockClient.Object;

            // Act
            var response = client.Call("ping");

            // Assert
            response.Should().Be("pong");
        }
    }



    public class MyClient : IMyClient
    {
        public virtual string Call(string message)
        {
            return message;
        }
    }
}

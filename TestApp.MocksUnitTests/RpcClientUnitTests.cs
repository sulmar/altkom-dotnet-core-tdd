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

            //    var factory = new ConnectionFactory() { HostName = "localhost" };

            IModel model = Mock.Of<IModel>(channel => channel.QueueDeclare().QueueName == "abc"));

            IConnection connection = Mock.Of<IConnection>(c=>c.CreateModel() == )

            var connection = factory.CreateConnection();

            var rpcClient = new RpcClient(connection);
        }
    }
}

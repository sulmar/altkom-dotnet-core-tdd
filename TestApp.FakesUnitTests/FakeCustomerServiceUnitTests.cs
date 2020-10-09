using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Fakers;
using Xunit;

namespace TestApp.FakesUnitTests
{
    public class FakeCustomerServiceUnitTests
    {

        [Fact]
        public void Get_WhenCalled_ReturnCustomers()
        {
            // Arrange
            Faker<Customer> faker = new CustomerFaker();
            ICustomerService customerService = new FakeCustomerService(faker);

            // Act
            var customers = customerService.Get();

            // Assert
            Assert.NotNull(customers);
            
        }
    }
}

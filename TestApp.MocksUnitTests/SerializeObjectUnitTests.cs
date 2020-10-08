using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestApp.MocksUnitTests
{
    #region Models

    public class Invoice
    {
        public int Id { get; set; }

        public Customer Customer { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }

        public Invoice LastInvoice { get; set; }
    }

    #endregion

    public class SerializeObjectUnitTests
    {
        [Fact]
        public void SerializeObject_Json_ThrowsException()
        {
            // Arrange
            Customer customer = new Customer();
            Invoice invoice = new Invoice { Customer = customer };
            customer.LastInvoice = invoice;

            // Act
            // Self referencing loop detected for property 'Customer' with type 
            Action act = () => JsonConvert.SerializeObject(customer);

            // Assert
            act.Should().ThrowExactly<JsonSerializationException>();

        }

        [Fact]
        public void SerializeObject_JsonSettings_ShouldReturnsJson()
        {
            // Arrange
            Customer customer = new Customer();
            Invoice invoice = new Invoice { Customer = customer };
            customer.LastInvoice = invoice;


            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            // Act
            // Self referencing loop detected for property 'Customer' with type 
            string json = JsonConvert.SerializeObject(customer, settings);

            // Assert
            json.Should().NotBeEmpty();




        }

    }
}

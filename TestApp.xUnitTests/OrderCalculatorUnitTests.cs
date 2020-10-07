using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Fundamentals;
using Xunit;
using Xunit.Sdk;

namespace TestApp.xUnitTests
{
    public class OrderCalculatorUnitTests
    {
        private IOrderShippingCalculator orderShippingCalculator;

        public OrderCalculatorUnitTests()
        {
            orderShippingCalculator = new OrderCalculator();
        }

        private const decimal shippingCost = 9.99m;
        private const decimal shippingFree = 0m;
        private const decimal totalAmountLimit = 1000m;

        [Fact]
        public void CalculateShippingCost_TotalAmountBelow1000_ReturnsNotZero()
        {
            // Arrange
            Order order = new Order { TotalAmount = totalAmountLimit - 0.01m };

            // Act
            var result = orderShippingCalculator.CalculateShippingCost(order);

            // Assert
            Assert.Equal(shippingCost, result);
        }


        [Fact]
        public void CalculateShippingCost_TotalAmountEqualOrAbove1000_ReturnsZero()
        {
            // Arrange
            Order order = new Order { TotalAmount = totalAmountLimit };

            // Act
            var result = orderShippingCalculator.CalculateShippingCost(order);

            // Assert
            Assert.Equal(shippingFree, result);
        }

        [Fact]
        public void CalculateShippingCost_OrderNull_ThrowsArgumentNullException()
        {
           // Act

            Action act = () => orderShippingCalculator.CalculateShippingCost(null);

            //
            Assert.Throws<ArgumentNullException>(act);
        }


    }
}

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

        [Fact]
        public void CalculateShippingCost_TotalAmountBelow1000_ReturnsNotZero()
        {
            // Arrange
            Order order = new Order { TotalAmount = 999 };

            // Act
            var result = orderShippingCalculator.CalculateShippingCost(order);

            // Assert
            Assert.Equal(9.99m, result);
        }


        [Fact]
        public void CalculateShippingCost_TotalAmountEqualOrAbove1000_ReturnsZero()
        {
            // Arrange
            Order order = new Order { TotalAmount = 1000 };

            // Act
            var result = orderShippingCalculator.CalculateShippingCost(order);

            // Assert
            Assert.Equal(0, result);
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

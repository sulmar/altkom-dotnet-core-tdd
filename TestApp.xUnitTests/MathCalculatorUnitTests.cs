using System;
using Xunit;

namespace TestApp.xUnitTests
{
    public class MathCalculatorUnitTests
    {
        private MathCalculator mathCalculator;

        // Method_Scenario_Behavior
        public MathCalculatorUnitTests()
        {
            mathCalculator = new MathCalculator();
        }

        [Fact]
        public void Add_WhenCalled_ReturnsTheSumOfArguments()
        {
            // Arrange

            // Act
            var result = mathCalculator.Add(1, 2);

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void Max_FirstArgumentIsGreater_ReturnsTheFirstArgument()
        {
            // Arrange

            // Act
            var result = mathCalculator.Max(2, 1);

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void Max_SecondsArgumentIsGreater_ReturnsTheSecondArgument()
        {
            // Arrange

            // Act
            var result = mathCalculator.Max(1, 2);

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void Max_ArgumentsAreEqual_ReturnsTheSameArgument()
        {
            // Arrange

            // Act
            var result = mathCalculator.Max(1, 1);

            // Assert
            Assert.Equal(1, result);
        }

        [Theory]
        [InlineData(2, 1, 2)]
        [InlineData(1, 2, 2)]
        [InlineData(1, 1, 1)]
        public void Max_Arguments_ReturnsValidArgument(int first, int second, int expected)
        {
            // Arrange

            // Act
            var result = mathCalculator.Max(first, second);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}

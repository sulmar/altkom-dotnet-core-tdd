using FluentAssertions;
using System;
using System.Collections;
using Xunit;

namespace TestApp.FluentAssertionsUnitTests
{


    // dotnet add package FluentAssertions

    public class MathCalculatorUnitTests
    {
        private MathCalculator mathCalculator;

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
            result.Should().Be(3);
        }

        [Fact]
        public void Max_FirstArgumentIsGreater_ReturnsTheFirstArgument()
        {
            // Arrange

            // Act
            var result = mathCalculator.Max(2, 1);

            // Assert
            result.Should().Be(2);
        }

        [Fact]
        public void Max_SecondsArgumentIsGreater_ReturnsTheSecondArgument()
        {
            // Arrange

            // Act
            var result = mathCalculator.Max(1, 2);

            // Assert
            result.Should().Be(2);
        }

        [Fact]
        public void Max_ArgumentsAreEqual_ReturnsTheSameArgument()
        {
            // Arrange

            // Act
            var result = mathCalculator.Max(1, 1);

            // Assert
            result.Should().Be(1);
        }
    }
}

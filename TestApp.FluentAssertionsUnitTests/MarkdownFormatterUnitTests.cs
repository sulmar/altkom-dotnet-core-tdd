using FluentAssertions;
using FluentAssertions.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestApp.FluentAssertionsUnitTests
{

    // konkurencja Shouldly 
    // https://github.com/shouldly/shouldly

    public class MarkdownFormatterUnitTests
    {
        // abc -> **abc**

        private MarkdownFormatter markdownFormatter;

        public MarkdownFormatterUnitTests()
        {
            markdownFormatter = new MarkdownFormatter();
        }

        private const string encloseStringWithDoubleAsterix = @"^\*\*.*\*\*$";

        [Fact]
        public void FormatAsBold_WhenCalled_ShouldReturnEncloseStringWithDoubleAsterix()
        {
            // Arrange

            // Act
            var result = markdownFormatter.FormatAsBold("abc");

            // Assert
            // Assert.Equal("**abc**", result);

            result.Should()
                .StartWith("**")
                .And
                .Contain("abc")
                .And
                .EndWith("**");

            // Regex
            result.Should().MatchRegex(encloseStringWithDoubleAsterix);
        }

        [Fact]
        public void FormatAsBold_StringIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange

            // Act
            Action act = () => markdownFormatter.FormatAsBold(null);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void FormatAsBold_StringIsEmpty_ShouldThrowArgumentException()
        {
            // Arrange

            // Act
            Action act = () => markdownFormatter.FormatAsBold(string.Empty);

            // Assert
            act.Should().Throw<ArgumentException>();
        }


        [Fact]
        public void FormatAsBold_WhenCalled_ShouldReturnBeLessOrEqual100ms()
        {
            // Arrange

            // Act
            Action act = ()=> markdownFormatter.FormatAsBold("abc");

            // Assert
            act.ExecutionTime().Should().BeLessOrEqualTo(TimeSpan.FromMilliseconds(100));
        }

        // Pomiar czasu operacji asynchronicznych
        [Fact]
        public void FormatAsBoldAsync_WhenCalled_ShouldReturnBeLessOrEqual100ms()
        {
            // Arrange

            // Act
            Func<Task> act = () => markdownFormatter.FormatAsBoldAsync("abc");

            // Assert
            act.Should().CompleteWithinAsync(TimeSpan.FromMilliseconds(100));
        }

        [Fact]
        public async void FormatAsBoldAsync_WhenCalled_ShouldReturnEncloseStringWithDoubleAsterix()
        {
            // Arrange

            // Act
            string result = await markdownFormatter.FormatAsBoldAsync("abc");

            result.Should()
                .StartWith("**")
                .And
                .Contain("abc")
                .And
                .EndWith("**");

            // Regex
            result.Should().MatchRegex(encloseStringWithDoubleAsterix);
        }
    }
}

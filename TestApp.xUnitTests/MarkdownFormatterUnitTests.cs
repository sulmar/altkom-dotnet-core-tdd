using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace TestApp.xUnitTests
{
  

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

            Assert.StartsWith("**", result);
            Assert.Contains("abc", result);
            Assert.EndsWith("**", result);

            // Regex
            Assert.Matches(encloseStringWithDoubleAsterix, result);

        }

        [Fact]
        public void FormatAsBold_StringIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange

            // Act
            Action act = () => markdownFormatter.FormatAsBold(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void FormatAsBold_StringIsEmpty_ShouldThrowArgumentException()
        {
            // Arrange

            // Act
            Action act = () => markdownFormatter.FormatAsBold(string.Empty);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }
    }
}

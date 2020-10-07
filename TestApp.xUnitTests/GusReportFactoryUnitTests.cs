using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Fundamentals.Gus;
using Xunit;
using Xunit.Sdk;

namespace TestApp.xUnitTests
{
    public class GusReportFactoryUnitTests
    {
        [Fact]
        public void Create_TypeIsP_ReturnsLegalPersonalityType()
        {
            // Arrange

            // Act
            var result = ReportFactory.Create("P");

            // Assert
            Assert.IsType<LegalPersonality>(result);
            Assert.IsAssignableFrom<Report>(result);
        }

        [Fact]
        public void Create_TypeIsLP_ReturnsLegalPersonalityType()
        {
            // Arrange

            // Act
            var result = ReportFactory.Create("LP");

            // Assert
            Assert.IsType<LegalPersonality>(result);
            Assert.IsAssignableFrom<Report>(result);
        }

        [Fact]
        public void Create_TypeIsLF_ReturnsLegalPersonalityType()
        {
            // Arrange

            // Act
            var result = ReportFactory.Create("LF");

            // Assert
            Assert.IsType<LegalPersonality>(result);
            Assert.IsAssignableFrom<Report>(result);
        }

        [Fact]
        public void Create_TypeIsF_ReturnsSoleTraderReportType()
        {
            // Arrange

            // Act
            var result = ReportFactory.Create("F");

            // Assert
            Assert.IsType<SoleTraderReport>(result);
            Assert.IsAssignableFrom<Report>(result);
        }

        [Fact]
        public void Create_TypeIsUnknown_ThrowsNotSupportedException()
        {
            // Act
            Action act = () => ReportFactory.Create("X");

            Assert.Throws<NotSupportedException>(act);
        }

    }
}

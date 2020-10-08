using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using System;
using TestApp.Mocking;
using Xunit;

namespace TestApp.MocksUnitTests
{
    // dotnet add package Moq
    // dotnet add package FluentAssertions
    public class TrackingServiceUnitTests
    {
        private ITrackingService trackingService;

        private Mock<IFileReader> mockFileReader;

        public TrackingServiceUnitTests()
        {
            mockFileReader = new Mock<IFileReader>();
            trackingService = new TrackingService(mockFileReader.Object);
        }

        [Fact]
        public void Get_ValidJson_ShouldReturnsLocation()
        {
            // Arrange
            mockFileReader
                .Setup(fr => fr.ReadAllText(It.IsAny<string>()))
                .Returns(JsonConvert.SerializeObject(new Location(52.00, 18.01)));

            //IFileReader fileReader = mockFileReader.Object;
            //trackingService = new TrackingService(fileReader);

            // Act
            var result = trackingService.Get();

            // Assert
            result.Latitude.Should().Be(52.00);
            result.Longitude.Should().Be(18.01);
        }

        [Fact]
        public void Get_InvalidJson_ShouldThrowFormatException()
        {
            // Arrange
            mockFileReader
                .Setup(fr => fr.ReadAllText(It.IsAny<string>()))
                .Returns("a");

            // Act
            Action act = () => trackingService.Get();

            // Assert
            act.Should().Throw<FormatException>();

        }

        [Fact]
        public void Get_FileEmpty_ShouldThrowApplicationException()
        {
            // Arrange
            mockFileReader
                .Setup(fr => fr.ReadAllText(It.IsAny<string>()))
                .Returns(string.Empty);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            act.Should().Throw<ApplicationException>();
        }
    }

}

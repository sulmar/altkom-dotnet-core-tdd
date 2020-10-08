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

    // Linq To Moq
    // zapis deklarytywny

    public class TrackingServiceUnitTestsLinqToMoq
    {
        [Fact]
        public void Location_Test()
        {
            // Arrange
            Location location = Mock.Of<Location>(l => l.Latitude == 52.00 && l.Longitude == 18.01);

            // Act
            var result = location;

            // Assert
            result.Latitude.Should().Be(52.00);
            result.Longitude.Should().Be(18.01);

        }     

        [Fact]
        public void Get_ValidJson_ShouldReturnsLocation()
        {
            // Arrange
            IFileReader fileReader = Mock.Of<IFileReader>(
                       fr =>
                          fr.ReadAllText(It.IsAny<string>()) == JsonConvert.SerializeObject(new Location { Latitude = 52.00, Longitude = 18.01 })
                          && fr.ReadAllText("invalid.json") == "a"
                          );

            ITrackingService trackingService = new TrackingService(fileReader);

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
            IFileReader fileReader = Mock.Of<IFileReader>(fr => fr.ReadAllText(It.IsAny<string>()) == "a");

            ITrackingService trackingService = new TrackingService(fileReader);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            act.Should().Throw<FormatException>();
        }


        [Fact]
        public void Get_FileEmpty_ShouldThrowApplicationException()
        {
            // Arrange
            IFileReader fileReader = Mock.Of<IFileReader>(fr => fr.ReadAllText(It.IsAny<string>()) == string.Empty);

            ITrackingService trackingService = new TrackingService(fileReader);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            act.Should().Throw<ApplicationException>();
        }
    }
}

using FluentAssertions;
using Newtonsoft.Json;
using System;
using TestApp.Mocking;
using Xunit;

namespace TestApp.FakesUnitTests
{
    #region Fakes
    public class FakeValidFileReader : IFileReader
    {
        public string ReadAllText(string path)
        {
            Location location = new Location(52.00, 18.01);

            return JsonConvert.SerializeObject(location);
        }
    }

    public class FakeEmptyFileReader : IFileReader
    {
        public string ReadAllText(string path)
        {
            return string.Empty;
        }
    }

    public class FakeInvalidFileReader : IFileReader
    {
        public string ReadAllText(string path)
        {
            return "a";
        }
    }

    #endregion

    public class TrackingServiceUnitTests
    {
        [Fact]
        public void Get_ValidJson_ShouldReturnsLocation()
        {
            // Arrange
            IFileReader fileReader = new FakeValidFileReader();
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
            IFileReader fileReader = new FakeInvalidFileReader();
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
            IFileReader fileReader = new FakeEmptyFileReader();
            ITrackingService trackingService = new TrackingService(fileReader);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            act.Should().Throw<ApplicationException>();
        }
    }
}

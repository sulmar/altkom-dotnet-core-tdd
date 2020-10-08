using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TestApp.Mocking;
using Xunit;

namespace TestApp.MocksUnitTests
{
    // dotnet add package Moq
    // dotnet add package FluentAssertions


    public class TrackingServiceAsyncUnitTests
    {
        private ITrackingServiceAsync trackingService;

        [Fact]
        public async void GetAsync_ValidJson_ShouldReturnsLocation()
        {
            // Arrange
            Mock<IFileReaderAsync> mockFileReader = new Mock<IFileReaderAsync>();

            mockFileReader
                .Setup(fr => fr.ReadAllTextAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(JsonConvert.SerializeObject(new Location(52.00, 18.01))));

            IFileReaderAsync fileReader = mockFileReader.Object;

            trackingService = new TrackingServiceAsync(fileReader);


            // Act
            var result = await trackingService.GetAsync();

            // Assert
            result.Latitude.Should().Be(52.00);
            result.Longitude.Should().Be(18.01);
        }
    }

}

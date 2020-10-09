using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using VehiclesApi.IServices;
using VehiclesApi.Models;
using Xunit;

namespace VehiclesApi.IntegrationTests
{
    // https://docs.microsoft.com/pl-pl/aspnet/core/test/integration-tests?view=aspnetcore-3.1#sut-environment
    public class BasicTests : IClassFixture<WebApplicationFactory<VehiclesApi.Startup>>
    {
        private readonly WebApplicationFactory<VehiclesApi.Startup> _factory;

        public BasicTests(WebApplicationFactory<VehiclesApi.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Get_WhenCalled_ReturnsVehicles()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("api/vehicles");
            var content = await response.Content.ReadAsStringAsync();

            IEnumerable<Vehicle> vehicles = JsonConvert.DeserializeObject<IEnumerable<Vehicle>>(content);

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Should().Contain("model");
            vehicles.Should().NotBeEmpty();
        }

        [Fact]
        public async void Get_WhenPassId_ReturnsVehicle()
        {
            // Arrange
            // var client = _factory.CreateClient();

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IVehicleService, MyFakeVehicleService>();
                });
            })
            .CreateClient();


            int vehicleId = 1;

            // Act
            var response = await client.GetAsync($"api/vehicles/{vehicleId}");
            var content = await response.Content.ReadAsStringAsync();

            Vehicle vehicle = JsonConvert.DeserializeObject<Vehicle>(content);

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            vehicle.Should().NotBeNull();

        }
    }
}

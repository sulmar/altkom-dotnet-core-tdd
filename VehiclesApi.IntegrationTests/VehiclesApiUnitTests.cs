using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using Xunit;
using FluentAssertions;
using System.Net;
using Newtonsoft.Json;
using VehiclesApi.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using VehiclesApi.IServices;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using VehiclesApi.IntegrationTests;

namespace VehiclesApi.IntegrationTests
{
    // dotnet add package Microsoft.AspNetCore.Mvc.Testing


    public class MockStartup
    {
        public MockStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IVehicleService, MyFakeVehicleService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }


    public static class ServicesExtensions
    {

        public static IServiceCollection Replace<TService, TImplementation>(
                this IServiceCollection services,
                ServiceLifetime lifetime)
                where TService : class
                where TImplementation : class, TService
        {
            var descriptorToRemove = services.FirstOrDefault(d => d.ServiceType == typeof(TService));

            services.Remove(descriptorToRemove);

            var descriptorToAdd = new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime);

            services.Add(descriptorToAdd);

            return services;
        }
    }

    public class MyFakeVehicleService : IVehicleService
    {
        public IEnumerable<Vehicle> Get()
        {
            return null;
        }

        public Vehicle Get(int id)
        {
            return null;
        }
    }

    public class VehiclesApiUnitTests
    {
        private TestServer server;
        private HttpClient client;

        public VehiclesApiUnitTests()
        { 
           server = new TestServer(new WebHostBuilder()
               .ConfigureServices(services=>
               {
                   // services.Replace<IVehicleService, MyFakeVehicleService>(ServiceLifetime.Singleton);
               })
               .UseStartup<Startup>());

           client = server.CreateClient();
        }

        [Fact]
        public async void Get_WhenCalled_ReturnsVehicles()
        {
            // Arrange

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

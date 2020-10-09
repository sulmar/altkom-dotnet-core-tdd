using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehiclesApi.IServices;
using VehiclesApi.Models;

namespace VehiclesApi.FakeServices
{

    // dotnet add package Bogus
    public class FakeVehicleService : IVehicleService
    {
        private readonly IEnumerable<Vehicle> vehicles;

        public FakeVehicleService()
        {
            vehicles = new Faker<Vehicle>()
                .RuleFor(p => p.Id, f => f.IndexFaker)
                .RuleFor(p => p.Model, f => f.Vehicle.Model())
                .Generate(100);
        }

        public IEnumerable<Vehicle> Get()
        {
            return vehicles;
        }

        public Vehicle Get(int id)
        {
            return vehicles.SingleOrDefault(v => v.Id == id);
        }
    }
}

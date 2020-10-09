using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehiclesApi.Models;

namespace VehiclesApi.IServices
{
    public interface IVehicleService
    {
        IEnumerable<Vehicle> Get();
    }
}

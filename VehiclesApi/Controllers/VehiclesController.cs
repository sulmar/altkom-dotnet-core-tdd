using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using VehiclesApi.IServices;

namespace VehiclesApi.Controllers
{
    [Route("api/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        // GET api/vehicles

        [HttpGet]
        public IActionResult Get()
        {
            var vehicles = vehicleService.Get();

            return Ok(vehicles);
        }

        // GET api/vehicles/{id}

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var vehicle = vehicleService.Get(id);

            return Ok(vehicle);
        }
    }
}

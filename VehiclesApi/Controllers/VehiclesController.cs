using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using VehiclesApi.IServices;
using VehiclesApi.UseCases;

namespace VehiclesApi.Controllers
{
    [Route("api/vehicles")]
    public class VehiclesController : ControllerBase
    {
        // private readonly IVehicleService vehicleService;

        private readonly GetCustomerUseCase getCustomerUseCase;
        private readonly RemoveCustomerUseCase removeCustomerUseCase;

        public VehiclesController(GetCustomerUseCase getCustomerUseCase, RemoveCustomerUseCase removeCustomerUseCase)
        {
            this.getCustomerUseCase = getCustomerUseCase;
            this.removeCustomerUseCase = removeCustomerUseCase;
        }

        // GET api/vehicles

        [HttpGet]
        public IActionResult Get()
        {
            // var vehicles = vehicleService.Get();
            // return Ok(vehicles);

            throw new NotImplementedException();

        }

        // GET api/vehicles/{id}

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var vehicle = getCustomerUseCase.Execute(id);

            return Ok(vehicle);
        }


        public IActionResult Remove(Guid id)
        {
            throw new NotImplementedException();
           
        }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehiclesApi.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Model { get; set; }
    }
}

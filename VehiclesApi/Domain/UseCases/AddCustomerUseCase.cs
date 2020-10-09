using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehiclesApi.Domain;

namespace VehiclesApi.UseCases
{

    public class AddCustomerUseCase : IUseCase<Guid, Customer>
    {
        private readonly ICustomerService customerService;

        public AddCustomerUseCase(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public Customer Execute(Guid id)
        {
            return customerService.Get(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehiclesApi.Domain;

namespace VehiclesApi.UseCases
{

    public class GetCustomerUseCase : IUseCase<Guid, Customer>
    {
        private readonly ICustomerService customerService;

        public GetCustomerUseCase(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public Customer Execute(Guid id)
        {
            return customerService.Get(id);
        }
    }
}

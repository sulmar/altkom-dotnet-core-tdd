using System;
using VehiclesApi.Domain;

namespace VehiclesApi.UseCases
{
    public class RemoveCustomerUseCase : IUseCase<Guid, NoContent>
    {
        private readonly ICustomerService customerService;

        public RemoveCustomerUseCase(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public NoContent Execute(Guid id)
        {
            // TODO: remove customer
            this.customerService.Remove(id);

            return NoContent.Return;
        }
    }
}

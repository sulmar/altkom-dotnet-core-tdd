using System;
using System.Collections.Generic;

namespace TestApp.Fakers
{
    public interface ICustomerService
    {
        IEnumerable<Customer> Get();
        Customer Get(Guid id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Remove(Guid id);
    }
}

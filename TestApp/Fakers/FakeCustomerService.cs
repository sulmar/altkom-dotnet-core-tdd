using Bogus;
using Bogus.Extensions.Poland;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TestApp.Fakers
{


    // dotnet add package Bogus
    // dotnet add package Sulmar.Bogus.Extensions.Poland
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker()
        {
            StrictMode(true);
            UseSeed(0); 
            RuleFor(p => p.Id, f => f.Random.Guid());
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.Gender, f => (Gender)f.Person.Gender);
            RuleFor(p => p.Email, (f, customer) => $"{customer.FirstName}.{customer.LastName}@domain.com");   // {firstname}.{lastname}@domain.com
            RuleFor(p => p.DateOfBirth, f => f.Person.DateOfBirth);
            RuleFor(p => p.CreatedDate, f => f.Date.Past(2));
            RuleFor(p => p.Salary, f => Math.Round(f.Random.Decimal(100, 1000), 0));
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f));
            RuleFor(p => p.IsVip, f => f.Random.Bool(0.1f));
            RuleFor(p => p.PhoneNumber, f => f.Person.Phone);
            Ignore(p => p.IsSelected);
            RuleFor(p => p.Pesel, f => f.Person.Pesel());

        }
    }


    public class FakeCustomerService : ICustomerService
    {
        private readonly ICollection<Customer> customers;

        public FakeCustomerService(Faker<Customer> faker)
        {
            customers = faker.Generate(100);
        }

        public void Add(Customer customer)
        {
            customers.Add(customer);
        }

        public IEnumerable<Customer> Get()
        {
            return customers;
        }

        public Customer Get(Guid id)
        {
            return customers.SingleOrDefault(c => c.Id == id);
        }

        public void Remove(Guid id)
        {
            customers.Remove(Get(id));
        }

        public void Update(Customer customer)
        {
            Remove(customer.Id);
            Add(customer);
        }
    }
}

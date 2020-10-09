using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestApp.Fakers
{
    public abstract class Base
    {

    }

    public class Customer : Base
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public bool IsRemoved { get; set; }
        public decimal Salary { get; set; }
    }

    public enum Gender
    {
        Woman,
        Man
    }

    public interface ICustomerService
    {
        IEnumerable<Customer> Get();
        Customer Get(Guid id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Remove(Guid id);
    }

    //public class DbCustomerService : ICustomerService
    //{

    //    public IEnumerable<Customer> Get()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class FakeCustomerService : ICustomerService
    {
        private readonly ICollection<Customer> customers;

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

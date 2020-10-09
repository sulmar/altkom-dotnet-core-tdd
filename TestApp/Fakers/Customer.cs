using System;
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
        public string Pesel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedDate { get; set; }
        public Gender Gender { get; set; }
        public bool IsRemoved { get; set; }
        public decimal Salary { get; set; }
        public bool IsVip { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsSelected { get; set; }
    }

    public enum Gender
    {
        Woman = 1,
        Man = 0
    }
}

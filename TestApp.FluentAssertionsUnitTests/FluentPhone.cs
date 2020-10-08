using System;
using System.Collections.Generic;

namespace TestApp.FluentAssertionsUnitTests
{
    public class Phone
    {
        public void Call(string fromNumber, string toNumber, string subject = "")
        {
            Console.WriteLine($"Calling {fromNumber} to {toNumber}");
        }

        public void Hangout()
        {

        }
    }

    public interface IFrom
    {
        ITo From(string number);
    }

    public interface ITo : ISubject, ICall
    {
        ITo To(string number);
    }

    public interface ISubject : ICall
    {
        ICall WithSubject(string subject);
    }

    public interface ICall
    {
        void Call();
    }


    public class FluentPhone : IFrom, ITo, ICall, ISubject
    {
        private string fromNumber;
        private ICollection<string> toNumbers;
        private string subject;

        public static IFrom Hangup()
        {
            return new FluentPhone();            
        }

        public ITo From(string number)
        {
            this.fromNumber = number;
            return this;
        }

        public ITo To(string number)
        {
            this.toNumbers.Add(number);
            return this;
        }

        public ICall WithSubject(string subject)
        {
            this.subject = subject;
            return this;
        }

        public void Call()
        {
            foreach (var toNumber in toNumbers)
            {
                if (string.IsNullOrEmpty(subject))
                    Console.WriteLine($"Calling {fromNumber} to {toNumber}");
                else
                    Console.WriteLine($"Calling {fromNumber} to {toNumber} subject {subject}");
            }
        }
    }
}

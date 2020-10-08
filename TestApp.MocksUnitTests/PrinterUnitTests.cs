using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestApp.MocksUnitTests
{
    public interface IDateTime
    {
        DateTime GetNow { get; }
    }

    public class Printer
    {
        public void Print(string content, IDateTime dateTime)
        {
            Console.WriteLine($"{dateTime.GetNow} {content}");
        }

        public void Print(string content)
        {
            Console.WriteLine($"{DateTime.Now} {content}");
        }
    }


    public class PrinterUnitTests
    {
        [Fact]
        public void Print_NotEmpty_Printed()
        {

        }
    }
}

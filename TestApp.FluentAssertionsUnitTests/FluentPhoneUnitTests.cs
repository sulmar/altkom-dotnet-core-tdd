using Xunit;

namespace TestApp.FluentAssertionsUnitTests
{
    public class FluentPhoneUnitTests
    {
        
        [Fact(Skip = "no bo tak")]
        public void Call_WhenCalled_ReturnsCall()
        {
            FluentPhone
                .Hangup()
                .From("555-666-777")
                .To("555-888-999")
                .To("555-777-888")
                .WithSubject("TDD")
                .Call();
        }
    }
}

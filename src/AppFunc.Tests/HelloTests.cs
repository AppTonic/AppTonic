using Shouldly;
using Xunit;

namespace AppFunc.Tests
{
    public class HelloTests
    {
        [Fact]
        public void ShouldAlwaysPass()
        {
            true.ShouldBe(true);
        }
    }
}

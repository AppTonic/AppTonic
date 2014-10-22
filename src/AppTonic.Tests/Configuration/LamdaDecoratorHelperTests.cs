using System;
using AppTonic.Configuration;
using Shouldly;
using Xunit;

namespace AppTonic.Tests.Configuration
{
    public class LamdaDecoratorHelperTests
    {
        [Fact]
        public void DecorateShouldWrapAction()
        {
            var innerEffect = "";
            var outerEffect = "";
            Action test = () =>
            {
                innerEffect = "test";
            };

            var decorated = test.Decorate(inner =>
            {
                outerEffect = "test";
                inner();
            });

            decorated();

            innerEffect.ShouldBe("test");
            outerEffect.ShouldBe("test");
        }
    }
}

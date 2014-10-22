using System.Threading.Tasks;
using AppTonic.Configuration;
using Shouldly;
using Xunit;

namespace AppTonic.Tests.Handlers
{
    public class PartialApplicationRegistrations
    {
        [Fact]
        public void ShouldBeAbleToHandleVoidRequest()
        {
            var testEffect = "";

            var dispatcher = AppDispatcherFactory.Create(app =>
            {
                app.RegisterHandler<TestRequestMessage>(message =>
                {
                    testEffect = message.Data;
                });
            });

            dispatcher.Handle(new TestRequestMessage { Data = "success" });

            testEffect.ShouldBe("success");
        }

        [Fact]
        public void ShouldBeAbleToHandleRequestResponse()
        {
            var dispatcher = AppDispatcherFactory.Create(app =>
            {
                app.RegisterHandler<TestRequestResponseMessage, string>(message => message.Data + "-handled");

            });

            dispatcher.Handle<TestRequestResponseMessage, string>(new TestRequestResponseMessage { Data = "success" })
                .ShouldBe("success-handled");
        }

        [Fact]
        public void ShouldHandleAsyncRequest()
        {
            var testEffect = "";

            var dispatcher = AppDispatcherFactory.Create(app =>
            {
                app.RegisterHandler<TestAsyncRequestMessage>(message =>
                {
                    testEffect = message.Data;
                    return Task.FromResult(0);
                });
            });

            dispatcher.HandleAsync(new TestAsyncRequestMessage { Data = "success" });

            testEffect.ShouldBe("success");
        }

        [Fact]
        public void ShouldHandleAsyncRequestResponse()
        {
            var dispatcher = AppDispatcherFactory.Create(app =>
            {
                app.RegisterHandler<TestAsyncRequestResponseMessage, string>(m => Task.FromResult(m.Data + "-handled"));
            });


            dispatcher.HandleAsync<TestAsyncRequestResponseMessage, string>(new TestAsyncRequestResponseMessage { Data = "success" })
                .Result.ShouldBe("success-handled");
        }
    }
}
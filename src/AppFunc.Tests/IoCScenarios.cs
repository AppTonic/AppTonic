using System;
using AppFunc.CommonServiceLocator;
using AppFunc.Configuration;
using CommonServiceLocator.StructureMapAdapter.Unofficial;
using Shouldly;
using StructureMap;
using StructureMap.Graph;
using Xunit;

namespace AppFunc.Tests
{
    public class IoCScenarios
    {
        private readonly IAppDispatcher _dispatcher;

        public IoCScenarios()
        {
            var serviceLocator = new StructureMapServiceLocator(new Container(c => c.Scan(s =>
            {
                s.TheCallingAssembly();
                s.AddAllTypesOf(typeof(IHandle<>));
                s.AddAllTypesOf(typeof(IHandle<,>));
                s.AddAllTypesOf(typeof(IHandleAsync<>));
                s.AddAllTypesOf(typeof(IHandleAsync<,>));
            })));

            _dispatcher = AppDispatcherFactory.Create(app =>
            {
                app.UseCommonServiceLocator(serviceLocator);
            });
        }

        [Fact]
        public void ShouldBeAbleToHandleVoidRequest()
        {
            var message = new TestRequestMessage { Data = "before" };
            _dispatcher.Handle(message);
            message.Data.ShouldBe("handled");
        }

        [Fact]
        public void ShouldBeAbleToHandleRequestResponse()
        {
            _dispatcher.Handle<TestRequestResponseMessage, string>(new TestRequestResponseMessage { Data = "before" })
                .ShouldBe("handled");
        }

        [Fact]
        public void ShouldHandleAsyncRequest()
        {
            var message = new TestAsyncRequestMessage { Data = "before" };
            _dispatcher.HandleAsync(message);
            message.Data.ShouldBe("handled");
        }

        [Fact]
        public void ShouldHandleAsyncRequestResponse()
        {
            var message = new TestAsyncRequestResponsetMessage { Data = "before" };
            _dispatcher.HandleAsync<TestAsyncRequestResponsetMessage, string>(message).Result.ShouldBe("handled");
        }

        [Fact]
        public void ShouldThrowIfNoHandlerForRequestMessage()
        {
            Should.Throw<InvalidOperationException>(() => _dispatcher.Handle(new TestRequestMessageNoHandler()));
        }

        [Fact]
        public void ShouldThrowIfNoHandlerForRequestResponseMessage()
        {
            Should.Throw<InvalidOperationException>(() => _dispatcher.Handle<TestRequestResponseMessageNoHandler, string>(new TestRequestResponseMessageNoHandler()));
        }

        [Fact]
        public void ShouldThrowIfNoHandlerForAsyncRequestMessage()
        {
            Should.Throw<InvalidOperationException>(() => _dispatcher.HandleAsync(new TestAsyncRequestMessageNoHandler()));
        }

        [Fact]
        public void ShouldThrowIfNoHandlerForAsyncRequestResponseMessage()
        {
            Should.Throw<InvalidOperationException>(() => _dispatcher.HandleAsync<TestAsyncRequestResponseMessageNoHandler, string>(new TestAsyncRequestResponseMessageNoHandler()));
        }
    }
}
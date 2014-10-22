using System.Threading.Tasks;
using AppTonic.CommonServiceLocator;
using AppTonic.Configuration;
using AppTonic.Pipeline;
using CommonServiceLocator.StructureMapAdapter.Unofficial;
using Shouldly;
using StructureMap;
using StructureMap.Graph;
using Xunit;

namespace AppTonic.Tests.Pipeline
{
    public class PipelineTests
    {
        private readonly IAppDispatcher _dispatcher;

        public PipelineTests()
        {
            var container = new Container(c =>
             {
                 c.Scan(s =>
                 {
                     s.AssemblyContainingType(typeof(RequestPipelineHandler<>));
                     s.ConnectImplementationsToTypesClosing(typeof(IHandle<>));
                     s.ConnectImplementationsToTypesClosing(typeof(IHandle<,>));
                     s.ConnectImplementationsToTypesClosing(typeof(IHandleAsync<>));
                     s.ConnectImplementationsToTypesClosing(typeof(IHandleAsync<,>));
                     s.TheCallingAssembly();
                     s.WithDefaultConventions();
                     s.AddAllTypesOf(typeof(IPreRequestHandler<>));
                     s.AddAllTypesOf(typeof(IPostRequestHandler<>));
                     s.AddAllTypesOf(typeof(IPostRequestResponseHandler<,>));
                 });

                 c.For(typeof(IHandle<>)).DecorateAllWith(typeof(RequestPipelineHandler<>));
                 c.For(typeof(IHandle<,>)).DecorateAllWith(typeof(RequestResponsePipelineHandler<,>));
                 c.For(typeof(IHandleAsync<>)).DecorateAllWith(typeof(AsyncRequestPipelineHandler<>));
                 c.For(typeof(IHandleAsync<,>)).DecorateAllWith(typeof(AsyncRequestResponsePipelineHandler<,>));
             });

            container.AssertConfigurationIsValid();

            _dispatcher = AppDispatcherFactory.Create(app => app.UseCommonServiceLocator(new StructureMapServiceLocator(container)));


        }


        [Fact]
        public void RequestMessagePipelineShouldFireHandlers()
        {
            var preRequestHandledCount = 0;
            var postRequestHandledCount = 0;
            var handled = false;
            var message = new TestRequestMessage
            {
                PreRequestAction = () =>
                {
                    preRequestHandledCount++;
                },
                HandlerAction = () =>
                {
                    handled = true;
                },
                PostRequestAction = () =>
                {
                    postRequestHandledCount++;
                }
            };

            _dispatcher.Handle(message);
            preRequestHandledCount.ShouldBe(3);
            postRequestHandledCount.ShouldBe(3);
            handled.ShouldBe(true);
        }


        [Fact]
        public void RequestResponseMessagePipelineShouldFireHandlers()
        {
            var preRequestHandledCount = 0;
            var postRequestHandledCount = 0;
            var handled = false;
            var message = new TestRequestResponseMessage
            {
                PreRequestAction = () =>
                {
                    preRequestHandledCount++;
                },
                HandlerAction = () =>
                {
                    handled = true;
                },
                PostRequestAction = () =>
                {
                    postRequestHandledCount++;
                }
            };

            _dispatcher.Handle<TestRequestResponseMessage, string>(message);
            preRequestHandledCount.ShouldBe(3);
            postRequestHandledCount.ShouldBe(3);
            handled.ShouldBe(true);
        }

        [Fact]
        public async Task AsyncRequestMessagePipelineShouldFireHandlers()
        {
            var preRequestHandledCount = 0;
            var postRequestHandledCount = 0;
            var handled = false;
            var message = new TestAsyncRequestMessage
            {
                PreRequestAction = () =>
                {
                    preRequestHandledCount++;
                },
                HandlerAction = () =>
                {
                    handled = true;
                },
                PostRequestAction = () =>
                {
                    postRequestHandledCount++;
                }
            };

            await _dispatcher.HandleAsync(message);
            preRequestHandledCount.ShouldBe(3);
            postRequestHandledCount.ShouldBe(3);
            handled.ShouldBe(true);
        }

        [Fact]
        public async Task AsyncRequestResponseMessagePipelineShouldFireHandlers()
        {
            var preRequestHandledCount = 0;
            var postRequestHandledCount = 0;
            var handled = false;
            var message = new TestAsyncRequestResponseMessage
            {
                PreRequestAction = () =>
                {
                    preRequestHandledCount++;
                },
                HandlerAction = () =>
                {
                    handled = true;
                },
                PostRequestAction = () =>
                {
                    postRequestHandledCount++;
                }
            };

            await _dispatcher.HandleAsync<TestAsyncRequestResponseMessage, string>(message);
            preRequestHandledCount.ShouldBe(3);
            postRequestHandledCount.ShouldBe(3);
            handled.ShouldBe(true);
        }
    }
}
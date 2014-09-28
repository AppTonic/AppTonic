using System.Threading.Tasks;

namespace AppFunc.Tests
{
    public class TestRequestHandler :
        IHandle<TestRequestMessage>,
        IHandle<TestRequestResponseMessage, string>,
        IHandleAsync<TestAsyncRequestMessage>,
        IHandleAsync<TestAsyncRequestResponseMessage, string>
    {
        public void Handle(TestRequestMessage request)
        {
            request.HandlerAction();
            request.Data = "handled";
        }

        public string Handle(TestRequestResponseMessage request)
        {
            request.HandlerAction();
            return "handled";
        }

        public Task HandleAsync(TestAsyncRequestMessage request)
        {
            request.Data = "handled";
            request.HandlerAction();
            return Task.FromResult(0);
        }

        public Task<string> HandleAsync(TestAsyncRequestResponseMessage request)
        {
            request.HandlerAction();
            return Task.FromResult("handled");
        }
    }
}
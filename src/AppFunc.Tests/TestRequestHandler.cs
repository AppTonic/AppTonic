using System.Threading.Tasks;

namespace AppFunc.Tests
{
    public class TestRequestHandler :
        IHandle<TestRequestMessage>,
        IHandle<TestRequestResponseMessage, string>,
        IHandleAsync<TestAsyncRequestMessage>,
        IHandleAsync<TestAsyncRequestResponsetMessage, string>
    {
        public void Handle(TestRequestMessage request)
        {
            request.Data = "handled";
        }

        public string Handle(TestRequestResponseMessage request)
        {
            return "handled";
        }

        public Task HandleAsync(TestAsyncRequestMessage request)
        {
            request.Data = "handled";
            return Task.FromResult(0);
        }

        public Task<string> HandleAsync(TestAsyncRequestResponsetMessage request)
        {
            return Task.FromResult("handled");
        }
    }
}
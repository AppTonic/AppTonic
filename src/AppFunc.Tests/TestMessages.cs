namespace AppFunc.Tests
{
    public class TestRequestMessage : IRequest
    {
        public string Data { get; set; }
    }

    public class TestRequestResponseMessage : IRequest<string>, IRequest
    {
        public string Data { get; set; }
    }

    public class TestAsyncRequestResponsetMessage : IAsyncRequest<string>
    {

        public string Data { get; set; }
    }

    public class TestAsyncRequestMessage : IAsyncRequest
    {
        public string Data { get; set; }
    }

    public class TestRequestMessageNoHandler : IRequest { }
    public class TestRequestResponseMessageNoHandler : IRequest<string> { }
    public class TestAsyncRequestMessageNoHandler : IAsyncRequest { }
    public class TestAsyncRequestResponseMessageNoHandler : IAsyncRequest<string> { }

}

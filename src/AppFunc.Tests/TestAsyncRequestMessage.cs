using System;

namespace AppFunc.Tests
{
    public class TestAsyncRequestMessage : IAsyncRequest
    {
        public TestAsyncRequestMessage()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public string Data { get; set; }
        public Action PreRequestAction { get; set; }
        public Action HandlerAction { get; set; }
        public Action PostRequestAction { get; set; }
    }
}
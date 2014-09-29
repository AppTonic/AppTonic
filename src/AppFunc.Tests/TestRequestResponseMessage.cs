using System;

namespace AppFunc.Tests
{
    public class TestRequestResponseMessage : IRequest<string>
    {
        public TestRequestResponseMessage()
        {
            Id = Guid.NewGuid();
            PreRequestAction = () => { };
            HandlerAction = () => { };
            PostRequestAction = () => { };
        }

        public Guid Id { get; private set; }
        public string Data { get; set; }
        public Action PreRequestAction { get; set; }
        public Action HandlerAction { get; set; }
        public Action PostRequestAction { get; set; }
    }
}
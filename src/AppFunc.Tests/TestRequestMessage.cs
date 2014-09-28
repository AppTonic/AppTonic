﻿using System;

namespace AppFunc.Tests
{
    public class TestRequestMessage : IRequest
    {
        public TestRequestMessage()
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
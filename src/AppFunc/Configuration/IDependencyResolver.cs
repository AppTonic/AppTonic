using System;
using System.Collections.Generic;

namespace AppFunc.Configuration
{
    public interface IDependencyResolver
    {
        bool TryGetInstance<TService>(out TService handlerInstance) where TService : class;
    }
}

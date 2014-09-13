using System;

namespace AppFunc.Configuration
{
    public static class LambdaDecoratorHelpers
    {
        public static Func<TRequest, TResponse> Decorate<TRequest, TResponse>(this Func<TRequest, TResponse> handler,
            Func<TRequest, Func<TRequest, TResponse>, TResponse> decorator)
        {
            return request => decorator(request, handler);
        }

        public static Action<TRequest> Decorate<TRequest>(this Action<TRequest> handler,
            Action<TRequest, Action<TRequest>> decorator)
        {
            return request => decorator(request, handler);
        }

        public static Action Decorate(this Action handler,
            Action<Action> decorator)
        {
            return () => decorator(handler);
        }
    }
}
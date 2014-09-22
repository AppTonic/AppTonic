using System;

namespace AppFunc.Configuration
{
    public static class LambdaDecoratorHelpers
    {
        /// <summary>
        /// Decorates a request/response handler function
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="inner"></param>
        /// <param name="decorator"></param>
        /// <returns></returns>
        public static Func<TRequest, TResponse> Decorate<TRequest, TResponse>(this Func<TRequest, TResponse> inner,
            Func<TRequest, Func<TRequest, TResponse>, TResponse> decorator)
        {
            return request => decorator(request, inner);
        }


        /// <summary>
        /// Decorates a request handler function
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="decorated"></param>
        /// <param name="inner"></param>
        /// <returns></returns>
        public static Action<TRequest> Decorate<TRequest>(this Action<TRequest> decorated,
            Action<TRequest, Action<TRequest>> inner)
        {
            return request => inner(request, decorated);
        }

        /// <summary>
        /// Decorates an inner action
        /// </summary>
        /// <param name="decorated"></param>
        /// <param name="decorator"></param>
        /// <returns></returns>
        public static Action Decorate(this Action decorated,
            Action<Action> decorator)
        {
            return () => decorator(decorated);
        }
    }
}
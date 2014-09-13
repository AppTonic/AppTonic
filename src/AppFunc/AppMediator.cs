using System;
using AppFunc.Configuration;

namespace AppFunc
{
    public static class AppMediator
    {
        private static IAppMediator _instance;

        public static void Initialize(Action<IAppFuncConfigurator> config = null)
        {
            _instance = AppMediatorFactory.Create(config);
        }

        public static IAppMediator Instance
        {
            get
            {
                if (_instance == null)
                    throw new InvalidOperationException("You must call AppMediator.Initialize before accessing AppMediator.Instance");

                return _instance;
            }
        }
    }
}
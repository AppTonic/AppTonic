using System;

namespace AppFunc.Configuration
{
    public static class AppFunc
    {
        private static IMediator _instance;

        public static void Initialize(Action<IAppFuncConfigurator> config = null)
        {
            _instance = AppFuncFactory.Create(config);
        }

        public static IMediator Instance
        {
            get
            {
                if (_instance == null)
                    throw new InvalidOperationException("You must call AppFunc.Initialize before accessing AppFunc.Instance");

                return _instance;
            }
        }
    }
}
using System;

namespace AppFunc.Configuration
{
    public static class AppMediatorFactory
    {
        public static IAppDispatcher Create(Action<IAppDispatcherConfigurator> config = null)
        {
            var configurator = new AppDispatcherConfigurator();
            if (config != null)
                config(configurator);
            return configurator.BuildRequestDispatcher();
        }
    }
}
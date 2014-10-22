using System;

namespace AppTonic.Configuration
{
    public static class AppDispatcherFactory
    {
        public static IAppDispatcher Create(Action<IAppDispatcherConfigurator> config)
        {
            if (config == null) throw new ArgumentNullException("config");
            var configurator = new AppDispatcherConfigurator();
            config(configurator);
            return configurator.BuildRequestDispatcher();
        }
    }
}
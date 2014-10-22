using System;
using AppTonic.CommonServiceLocator;
using AppTonic.Configuration;
using AppTonic.Examples.Shared;
using AppTonic.Examples.Shared.Data;
using AppTonic.Examples.Shared.Domain;
using CommonServiceLocator.SimpleInjectorAdapter;
using SimpleInjector;
using SimpleInjector.Extensions;

namespace AppTonic.Examples.SimpleInjector
{
    class Program
    {
        static void Main()
        {
            var container = new Container();
            container.Register<IUserRepository, InMemoryUserRepository>();
            container.Register<ILogger, ConsoleLogger>();
            container.RegisterManyForOpenGeneric(typeof(IHandle<>), AppDomain.CurrentDomain.GetAssemblies());
            container.RegisterDecorator(typeof(IHandle<>), typeof(LoggingDecorator<>));

            var simpleInjectorServiceLocatorAdapter = new SimpleInjectorServiceLocatorAdapter(container);

            AppDispatcher.Initialize(app =>
            {
                // Decorating the pipline AND using IoC: you will see
                // the action pipleine below wraps any IoC decorators
                app.GlobalPipeline(pipeline => pipeline.Decorate(handler =>
                {
                    Console.WriteLine("before a");
                    handler();
                    Console.WriteLine("after a");
                }).Decorate(handler =>
                {
                    Console.WriteLine("before b");
                    handler();
                    Console.WriteLine("after b");
                }), true);

                app.UseCommonServiceLocator(simpleInjectorServiceLocatorAdapter);

            });

            var request = new CreateUser { Name = "Jane Smith" };

            AppDispatcher.Handle(request);

            Console.ReadLine();
        }
    }
}

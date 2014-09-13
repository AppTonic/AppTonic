using System;
using AppFunc.CommonServiceLocator;
using AppFunc.Examples.Shared.Domain;
using AppFunc.Examples.Shared.Services;
using Microsoft.Practices.Unity;

namespace AppFunc.Examples.Unity
{
    class Program
    {
        static void Main()
        {
            var container = new UnityContainer();
            container.RegisterTypes(AllClasses.FromAssemblies(typeof(UserService).Assembly), WithMappings.FromAllInterfaces);
            var serviceLocator = new UnityServiceLocator(container);

            AppMediator.Initialize(app =>
            {
                app.UseCommonServiceLocator(serviceLocator);
            });

            var request = new CreateUserRequest
            {
                EmailAddress = "jane.smith@example.com",
                FullName = "Jane Smith",
                Username = "jsmith",
                WebsiteUrl = "jsmith.co"
            };

            AppMediator.Handle(request);

            Console.ReadLine();
        }
    }
}
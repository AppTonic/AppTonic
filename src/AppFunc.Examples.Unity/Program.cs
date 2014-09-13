using System;
using System.Diagnostics;
using AppFunc.CommonServiceLocator;
using AppFunc.Configuration;
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
            var unityServiceLocator = new UnityServiceLocator(container);

            AppDispatcher.Initialize(app =>
            {
                app.UseCommonServiceLocator(unityServiceLocator);
            });

            var request = new CreateUserRequest
            {
                EmailAddress = "jane.smith@example.com",
                FullName = "Jane Smith",
                Username = "jsmith",
                WebsiteUrl = "jsmith.co"
            };

            AppDispatcher.Handle(request);

            Console.ReadLine();
        }
    }
}
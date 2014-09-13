using System;
using System.Diagnostics;
using AppFunc.Configuration;
using AppFunc.Examples.Shared;
using AppFunc.Examples.Shared.Data;
using AppFunc.Examples.Shared.Domain;
using AppFunc.Examples.Shared.Services;

namespace AppFunc.Examples.PartialApplicationComposition
{
    public class TestRequestResponse : IRequest<string> { }


    class Program
    {
        static void Main()
        {
            var logger = new ConsoleLogger();
            var stopwatch = new Stopwatch();

            AppDispatcher.Initialize(app =>
            {
                app.DecorateHandlers(pipeline => pipeline.Decorate(handler =>
                {
                    stopwatch.Start();
                    handler();
                    stopwatch.Stop();
                    Console.WriteLine("Handling took {0}ms", stopwatch.ElapsedMilliseconds);
                }));

                app.RegisterHandler<CreateUserRequest>(req =>
                {
                    UserServiceModule.CreateUser(req, () => new InMemoryUserRepository(), logger);
                }, handler => handler
                    .Decorate((req, inner) =>
                    {
                        Console.WriteLine("Before: Inner");
                        inner(req);
                        Console.WriteLine("After: Inner");
                    })
                    .Decorate((req, inner) =>
                    {
                        Console.WriteLine("Before: Outer");
                        inner(req);
                        Console.WriteLine("After: Outer");
                    }));

                app.RegisterHandler<TestRequestResponse, string>(req =>
                {
                    Console.WriteLine("Message Handled!");
                    return "success";
                }, handler =>
                    handler.Decorate((req, inner) =>
                    {
                        Console.WriteLine("Before: Inner");
                        var result = inner(req);
                        Console.WriteLine("After: Inner");
                        return result;

                    }).Decorate((req, inner) =>
                    {
                        Console.WriteLine("Before: Outer");
                        var result = inner(req);
                        Console.WriteLine("After: Outer");
                        return result;
                    })
                );
            });

            var request = new CreateUserRequest
            {
                EmailAddress = "jane.smith@example.com",
                FullName = "Jane Smith",
                Username = "jsmith",
                WebsiteUrl = "jsmith.co"
            };

            AppDispatcher.Handle(request);
            var response = AppDispatcher.Handle<TestRequestResponse, string>(new TestRequestResponse());
            Console.WriteLine(response);

            Console.ReadLine();
        }
    }
}

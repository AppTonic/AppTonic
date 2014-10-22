using System;

namespace AppTonic.Examples.Shared
{
    public class ConsoleLogger : ILogger
    {
        public void Info(string message)
        {
            Console.WriteLine(message);
        }
    }
}
using System;
using Faker.UI;
using Microsoft.Owin.Hosting;

namespace Faker.CommandLine
{
    internal static class Program
    {
        internal static void Main()
        {
            using (WebApp.Start<Startup>("http://localhost:12345"))
            {
                Console.ReadLine();
            }
        }
    }
}

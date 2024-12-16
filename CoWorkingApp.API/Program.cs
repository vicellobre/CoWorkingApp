using CoWorkingApp.API.Extensions.HostBuilder;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static void Main(string[] args)
    {
        Host.CreateDefaultBuilder(args)
            .ConfigureHost()
            .Build()
            .Run();
    }
}

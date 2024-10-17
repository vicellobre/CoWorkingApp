using CoWorkingApp.API;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static void Main(string[] args)
    {

        HostBuilderHelper.CreateHostBuilder(args).Build().Run();
    }
}
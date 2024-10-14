using CoWorkingApp.API;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static void Main(string[] args)
    {
        var app = Startup.Initialize(args);
        app.Run();
    }
}
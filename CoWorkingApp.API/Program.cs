using CoWorkingApp.API;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static void Main(string[] args)
    {

        HostBuilderHelper.CreateHostBuilder(args).Build().Run();
    }
}
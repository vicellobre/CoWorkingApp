using CoWorkingApp.API.Extensions.ApplicationBuilder;
using CoWorkingApp.API.Extensions.ServiceCollection;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Diagnostics.CodeAnalysis;

namespace CoWorkingApp.API;

/// <summary>
/// Clase estática Startup que contiene los métodos para inicializar y configurar la aplicación.
/// </summary>
[ExcludeFromCodeCoverage]
public class Startup
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="Startup"/>.
    /// </summary>
    /// <param name="configuration">Instancia de <see cref="IConfiguration"/> que contiene la configuración de la aplicación.</param>
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    /// <summary>
    /// Configura los servicios que serán utilizados por la aplicación, como la base de datos, autenticación, CORS, Swagger, entre otros.
    /// </summary>
    /// <param name="services">La colección de servicios que se utilizará para registrar los servicios.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddDatabaseService(_configuration)            // Configura la base de datos
            .AddDependencyService()                        // Configura las dependencias necesarias
            .AddMediatWithValidationService()              // Configura MediatR y FluentValidation
            .AddCorsService()                              // Configura políticas de CORS
            .AddTokenAuthenticationService(_configuration) // Configura la autenticación mediante tokens
            .AddDataProtectionService()                    // Configura la protección de datos
            .AddProblemDetailsService()                    // Configura ProblemDetails
            .AddResponseCaching()                          // Configura la caché de respuestas
            .AddSwaggerService(_configuration)             // Configura Swagger
            .AddODataServices()                            // Configura OData
            .AddControllersService()                       // Configura los controladores
            .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme); // Agrega el esquema de autenticación OpenID Connect
    }

    /// <summary>
    /// Configura la aplicación y define cómo se procesarán las solicitudes HTTP.
    /// </summary>
    /// <param name="app">El constructor para configurar la aplicación.</param>
    /// <param name="env">El entorno de alojamiento web.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app
            .UseExceptionApp(env)        // Configura el manejo de excepciones
            .UseStatusCodePages()        // Configura las páginas de código de estado
            .UseSwaggerApp()             // Habilita Swagger
            .UseHttpsRedirection()       // Redirige HTTP a HTTPS
            .UseRouting()                // Configura el enrutamiento
            .UseResponseCaching()        // Habilita la caché de respuestas
            .UseCors("MyPolicy")         // Habilita CORS con la política "MyPolicy"
            .UseAuthentication()         // Habilita la autenticación
            .UseAuthorization()          // Habilita la autorización
            .UseEndpointsApp();          // Configura los endpoints
    }
}

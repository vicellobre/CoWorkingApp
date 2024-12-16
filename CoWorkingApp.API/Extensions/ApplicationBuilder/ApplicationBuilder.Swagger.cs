using System.Diagnostics.CodeAnalysis;

namespace CoWorkingApp.API.Extensions.ApplicationBuilder;

/// <summary>
/// Clase de extensión para configurar Swagger en la aplicación.
/// </summary>
[ExcludeFromCodeCoverage]
public static partial class ApplicationBuilderExtensions
{
    /// <summary>
    /// Método para habilitar Swagger.
    /// </summary>
    /// <param name="app">Constructor para configurar la aplicación.</param>
    /// <returns>El constructor de la aplicación con Swagger habilitado.</returns>
    public static IApplicationBuilder UseSwaggerApp(this IApplicationBuilder app)
    {
        app.UseSwagger(options =>
        {
            options.SerializeAsV2 = true;
        });

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Services v1.0");
            //options.SwaggerEndpoint("/swagger/v2/swagger.json", "Services v2.0");
            //options.SwaggerEndpoint("/swagger/latest/swagger.json", "Services ultimate");
        });

        return app;
    }
}

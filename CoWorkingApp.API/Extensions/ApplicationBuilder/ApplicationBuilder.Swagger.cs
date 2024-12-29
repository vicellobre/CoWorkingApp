using Asp.Versioning.ApiExplorer;
using CoWorkingApp.Infrastructure.Options.Swagger;
using Microsoft.Extensions.Options;
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
            var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
            var swaggerOptions = app.ApplicationServices.GetRequiredService<IOptions<SwaggerOptions>>().Value;

            foreach (var description in provider.ApiVersionDescriptions)
            {
                var versionOption = swaggerOptions.Versions.FirstOrDefault(v => v.Version!.Contains(description.ApiVersion.ToString()));
                if (versionOption != null)
                {
                    var url = $"/swagger/{description.GroupName}/swagger.json";
                    var name = versionOption.DisplayName;
                    options.SwaggerEndpoint(url, name);
                }
            }
        });

        return app;
    }
}

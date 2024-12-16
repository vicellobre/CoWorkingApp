namespace CoWorkingApp.API.Extensions.ApplicationBuilder;

/// <summary>
/// Contiene métodos de extensión para configurar el enrutamiento de la aplicación.
/// </summary>
public static partial class ApplicationBuilderExtensions
{
    /// <summary>
    /// Configura los puntos finales de la aplicación para mapear los controladores.
    /// </summary>
    /// <param name="app">El constructor de la aplicación.</param>
    /// <returns>El constructor de la aplicación con los puntos finales configurados.</returns>
    public static IApplicationBuilder UseEndpointsApp(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }
}

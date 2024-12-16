namespace CoWorkingApp.API.Extensions.ApplicationBuilder;

/// <summary>
/// Contiene métodos de extensión para configurar el manejo de excepciones en la aplicación.
/// </summary>
public static partial class ApplicationBuilderExtensions
{
    /// <summary>
    /// Método para configurar el manejo de excepciones según el entorno de la aplicación.
    /// </summary>
    /// <param name="app">Constructor para configurar la aplicación.</param>
    /// <param name="env">Entorno de alojamiento web.</param>
    /// <returns>El constructor de la aplicación con el manejo de excepciones configurado.</returns>
    public static IApplicationBuilder UseExceptionApp(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseExceptionHandler();

        return app;
    }
}

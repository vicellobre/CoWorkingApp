using System.Diagnostics.CodeAnalysis;

namespace CoWorkingApp.API
{
    /// <summary>
    /// Clase de ayuda para crear y configurar el host de la aplicación.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class HostBuilderHelper
    {
        /// <summary>
        /// Crea y configura un <see cref="IHostBuilder"/> para la aplicación.
        /// </summary>
        /// <param name="args">Argumentos de la línea de comandos.</param>
        /// <returns>Un <see cref="IHostBuilder"/> configurado para usar <see cref="Startup"/>.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

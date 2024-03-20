using Microsoft.Extensions.Configuration;

namespace CoWorkingApp.Tests
{
    /// <summary>
    /// Clase para la creación de instancias de IConfiguration utilizadas en las pruebas.
    /// </summary>
    public class TestConfigurationFactory
    {
        /// <summary>
        /// Crea y devuelve una instancia de IConfiguration configurada con los valores proporcionados en un diccionario.
        /// </summary>
        /// <param name="configValues">Diccionario de valores de configuración.</param>
        /// <returns>Instancia de IConfiguration configurada con los valores proporcionados.</returns>
        public static IConfiguration CreateConfiguration(Dictionary<string, string> configValues)
        {
            // Configura la configuración en memoria
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configValues)
                .Build();

            // Devuelve la configuración
            return configuration;
        }
    }
}

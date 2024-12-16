using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection;

namespace CoWorkingApp.API.Extensions.ServiceCollection;

/// <summary>
/// Contiene métodos de extensión para la configuración de servicios de la colección.
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Método para agregar la configuración de protección de datos a la colección de servicios.
    /// </summary>
    /// <param name="services">Colección de servicios de la aplicación.</param>
    /// <returns>Colección de servicios con la configuración de protección de datos agregada.</returns>
    public static IServiceCollection AddDataProtectionService(this IServiceCollection services)
    {
        services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(@"/app/DataProtectionKeys"))
            .SetApplicationName("CoWorkingApp")
            .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
            {
                EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
            });

        return services;
    }
}

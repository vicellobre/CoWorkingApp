using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CoWorkingApp.Infrastructure.Options.Swagger;

/// <summary>
/// Clase que representa la configuración de Swagger obtenida desde el archivo de configuración.
/// </summary>
[ExcludeFromCodeCoverage]
public class SwaggerOptions
{
    /// <summary>
    /// Lista de configuraciones de versiones de Swagger.
    /// </summary>
    [Required]
    public required List<SwaggerVersionOptions> Versions { get; init; }

    /// <summary>
    /// Información de contacto que se mostrará en la documentación de Swagger.
    /// </summary>
    [Required]
    public required ContactOptions Contact { get; init; }

    /// <summary>
    /// Información sobre la licencia que se mostrará en Swagger.
    /// </summary>
    [Required]
    public required LicenseOptions License { get; init; }

    /// <summary>
    /// Clase que representa la configuración de una versión específica de Swagger.
    /// </summary>
    public class SwaggerVersionOptions
    {
        [Required]
        public string? Version { get; init; }

        [Required]
        public string? Title { get; init; }

        [Required]
        public string? Description { get; init; }

        [Required]
        public string? DisplayName { get; init; }
    }

    public class ContactOptions
    {
        [Required]
        public string? Name { get; init; }

        [Required]
        public string? Email { get; init; }

        [Required]
        public string? Url { get; init; }
    }

    public class LicenseOptions
    {
        [Required]
        public string? Name { get; init; }

        [Required]
        public string? Url { get; init; }
    }
}

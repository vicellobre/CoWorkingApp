using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace CoWorkingApp.API.Extensions.ServiceCollection;

/// <summary>
/// Contiene métodos de extensión para la configuración de servicios de la colección.
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Método de extensión para configurar OData.
    /// </summary>
    /// <param name="services">La colección de servicios.</param>
    /// <returns>La colección de servicios con la configuración de OData agregada.</returns>
    public static IServiceCollection AddODataServices(this IServiceCollection services)
    {
        services.AddODataQueryFilter();
        services.AddControllers().AddOData(options =>
        {
            options.AddRouteComponents("api/odata", GetEdmModel())
                   .Select()
                   .Filter()
                   .OrderBy()
                   .Count()
                   .Expand()
                   .SetMaxTop(100);
        });

        return services;
    }

    /// <summary>
    /// Método para configurar el modelo de OData.
    /// </summary>
    /// <returns>El modelo de OData configurado.</returns>
    private static IEdmModel GetEdmModel()
    {
        var odataBuilder = new ODataConventionModelBuilder();
        // Configura tu modelo OData aquí
        return odataBuilder.GetEdmModel();
    }
}

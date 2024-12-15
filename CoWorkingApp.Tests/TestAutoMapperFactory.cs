using AutoMapper;
using CoWorkingApp.Infrastructure.Adapters;

namespace CoWorkingApp.Tests
{
    /// <summary>
    /// Clase para la creación de objetos IMapper utilizados en las pruebas.
    /// </summary>
    public class TestAutoMapperFactory
    {
        /// <summary>
        /// Crea y devuelve un objeto IMapper configurado con los perfiles de mapeo necesarios.
        /// </summary>
        /// <returns>Objeto IMapper configurado.</returns>
        public static IMapper CreateMapper()
        {
            // Configura AutoMapper con los perfiles de mapeo requeridos
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
                // Agrega otros perfiles de mapeo si es necesario
            });

            // Crea un objeto IMapper utilizando la configuración
            return config.CreateMapper();
        }
    }
}

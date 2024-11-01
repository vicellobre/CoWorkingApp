using CoWorkingApp.API.Infrastructure.Adapters;
using CoWorkingApp.API.Infrastructure.Context;
using CoWorkingApp.API.Infrastructure.Extensions;
using CoWorkingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using CoWorkingApp.API.Configurations;

namespace CoWorkingApp.API
{
    /// <summary>
    /// Clase estática Startup que contiene los métodos para inicializar y configurar la aplicación.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Startup"/>.
        /// </summary>
        /// <param name="configuration">Instancia de <see cref="IConfiguration"/> que contiene la configuración de la aplicación.</param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Configura los servicios que serán utilizados por la aplicación, como la base de datos, autenticación, CORS, Swagger, entre otros.
        /// </summary>
        /// <param name="services">La colección de servicios que se utilizará para registrar los servicios.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Detectar si se ejecuta en un contenedor Docker
            var isRunningInContainer = Environment.GetEnvironmentVariable("RUNNING_IN_CONTAINER") == "true";

            // Seleccionar la cadena de conexión adecuada
            var connectionString = isRunningInContainer
                ? _configuration.GetConnectionString("ConnectionCoWorking_Container")
                : _configuration.GetConnectionString("ConnectionCoWorking");

            // Verificar si la cadena de conexión es null
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("The connection string is not configured correctly.");
            }

            // Configuración del contexto de la base de datos
            services.AddDbContext<CoWorkingContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Agrega dependencias adicionales
            services.AddDependency();

            // Configuración CORS
            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                           .AllowAnyOrigin()
                           .AllowAnyMethod();
                });
            });

            // Configuración de autenticación JWT
            services.AddTokenAuthentication(_configuration);

            // Configuración de AutoMapper y registro del perfil de mapeo
            services.AddAutoMapper(typeof(MappingProfile));

            // Configuración de protección de datos con encriptador
            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(@"/app/DataProtectionKeys"))
                .SetApplicationName("CoWorkingApp")
                .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
                {
                     EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                     ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                });

            // Configuración de los controladores (API Endpoints)
            services.AddControllers(options =>
            {
                // Requiere que los usuarios estén autenticados
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            // Configuración para habilitar la caché de respuestas
            services.AddResponseCaching();

            // Configuración de Swagger/OpenAPI (documentación de la API)
            services.AddEndpointsApiExplorer();

            // Obtener la sección Swagger desde appsettings
            services.AddSwaggerGen(options =>
            {
                // Información del archivo XML de documentación
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                // Configurar Swagger usando los valores desde appsettings
                var swaggerConfig = _configuration.GetSection("Swagger").Get<SwaggerConfig>();
                if (swaggerConfig is null)
                {
                    throw new ArgumentNullException(nameof(swaggerConfig));
                }

                // Documentación para la versión 1
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = swaggerConfig.Version,  // Ahora usará el valor desde el appsettings
                    Title = swaggerConfig.Title,
                    Description = swaggerConfig.Description,
                    Contact = new OpenApiContact
                    {
                        Name = swaggerConfig.Contact.Name,
                        Email = swaggerConfig.Contact.Email,
                        Url = new Uri(swaggerConfig.Contact.Url!)
                    },
                    License = new OpenApiLicense
                    {
                        Name = swaggerConfig.License.Name,
                        Url = new Uri(swaggerConfig.License.Url!)
                    }
                });

                // Documentación para la versión 2
                //options.SwaggerDoc("v2", new OpenApiInfo { Title = "CoWorkng API 2", Version = "v2" });
                //options.SwaggerDoc("lastest", new OpenApiInfo { Title = "CoWorkng API full", Version = "lastest neto" });
            });

            // Configuración para OData
            services.AddODataQueryFilter();
            services.AddControllers().AddOData(options =>
            {
                // Configuración de rutas y opciones de OData
                options.AddRouteComponents("api/odata", GetEdmModel())
                    .Select()
                    .Filter()
                    .OrderBy()
                    .Count()
                    .Expand()
                    .SetMaxTop(100);
            });
        }

        /// <summary>
        /// Define el modelo EDM para OData con las entidades disponibles.
        /// </summary>
        /// <returns>El modelo EDM configurado con las entidades de la aplicación.</returns>
        private IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();

            // Agrega entidades al modelo EDM
            odataBuilder.EntitySet<User>("Users");
            odataBuilder.EntitySet<Seat>("Seats");
            odataBuilder.EntitySet<Reservation>("Reservations");

            return odataBuilder.GetEdmModel();
        }

        /// <summary>
        /// Configura la aplicación y define cómo se procesarán las solicitudes HTTP.
        /// </summary>
        /// <param name="app">El constructor para configurar la aplicación.</param>
        /// <param name="env">El entorno de alojamiento web.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configuración específica para entornos de desarrollo
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error");
            }

            // Habilita Swagger
            app.UseSwagger(options =>
            {
                options.SerializeAsV2 = true;
            });

            // Configura la interfaz de usuario de Swagger
            app.UseSwaggerUI(options =>
            {
                //// Endpoint para la versión 1 de la API
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Services v1.0");
                // Endpoint para la versión 2 de la API
                //options.SwaggerEndpoint("/swagger/v2/swagger.json", "Services v2.0");
                //options.SwaggerEndpoint("/swagger/lastest/swagger.json", "Services ultimate");
            });

            // Redirige el tráfico HTTP a HTTPS
            app.UseHttpsRedirection();

            // Configuración del enrutamiento de solicitudes
            app.UseRouting();

            // Habilita la caché de respuestas
            app.UseResponseCaching();

            // Habilita CORS
            app.UseCors("MyPolicy");

            // Configuración de autenticación y autorización
            app.UseAuthentication();
            app.UseAuthorization();

            // Asigna los controladores para procesar las solicitudes HTTP
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

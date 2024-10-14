using CoWorkingApp.API.Infrastructure.Adapters;
using CoWorkingApp.API.Infrastructure.Context;
using CoWorkingApp.API.Infrastructure.Extensions;
using CoWorkingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CoWorkingApp.API
{
    /// <summary>
    /// Clase estática Startup que contiene los métodos para inicializar y configurar la aplicación.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Startup
    {
        /// <summary>
        /// Inicializa la aplicación configurando los servicios y devolviendo la instancia de WebApplication.
        /// </summary>
        /// <param name="args">Parámetros de entrada de la aplicación.</param>
        /// <returns>Una instancia de WebApplication lista para ejecutarse.</returns>
        public static WebApplication Initialize(string[] args)
        {
            // Crea una instancia del constructor de la aplicación
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);

            // Construye la aplicación
            var app = builder.Build();
            Configure(app);

            // Ejecuta la aplicación
            return app;
        }

        /// <summary>
        /// Configura los servicios que serán utilizados por la aplicación, como la base de datos, autenticación, CORS, Swagger, entre otros.
        /// </summary>
        /// <param name="builder">El constructor de la aplicación que se utiliza para registrar servicios.</param>
        static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Configuración del contexto de la base de datos
            builder.Services.AddDbContext<CoWorkingContext>(options =>
            {
                options .UseSqlServer(builder.Configuration.GetConnectionString("ConnectionCoWorking"));
            });

            // Agrega dependencias adicionales
            builder.Services.AddDependency();

            // Configuración CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                           .AllowAnyOrigin()
                           .AllowAnyMethod();
                });
            });

            // Configuración de autenticación JWT
            builder.Services.AddTokenAuthentication(builder.Configuration);

            // Configuración de AutoMapper y registro del perfil de mapeo
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Configuración de los controladores (API Endpoints)
            builder.Services.AddControllers(options =>
            {
                // Requiere que los usuarios estén autenticados
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            // Configuración para habilitar la caché de respuestas
            builder.Services.AddResponseCaching();

            // Configuración de Swagger/OpenAPI (documentación de la API)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                // Información del archivo XML de documentación
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                // Configuración de Swagger
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CoWorking API",
                    Description = "Este proyecto de API ASP.NET Core, denominado CoWorkingApp, tiene como objetivo demostrar la evolución en el diseño y desarrollo de software a través de la implementación gradual de diferentes patrones arquitectónicos." +
                    "\n\n La aplicación está centrada en la gestión de espacios de trabajo compartidos, con un enfoque educativo para que los desarrolladores comprendan la adopción de las buenas prácticas de desarrollo de software y los patrones de diseño como Repository, Adapter, Factory, Service, UseCase y Controller .\n\n" +
                    "## Repositorio\nEste repositorio se presenta como un recurso educativo y de referencia para aquellos interesados en aprender sobre la evolución de un proyecto desde un diseño basado en servicios hasta la adopción de Casos de Uso. Siéntete libre de explorar y contribuir al proyecto para enriquecer aún más este aprendizaje.\n\n¡Gracias por ser parte de CoWorkingApp y explorar la evolución de las arquitecturas de software!",
                    Contact = new OpenApiContact
                    {
                        Name = "Llobregat Vicente",
                        Email = "vicente.llobregat94@gmail.com",
                        Url = new Uri("https://github.com/vicellobre/CoWorkingApp"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT"),
                    }
                });
            });

            // Configuración para OData
            builder.Services.AddODataQueryFilter();
            builder.Services.AddControllers().AddOData(options =>
            {
                // Configuración de rutas y opciones de OData
                options.AddRouteComponents("odata", GetEdmModel()).Select().Filter().OrderBy().Count().Expand().SetMaxTop(100);
            });
        }

        /// <summary>
        /// Define el modelo EDM para OData con las entidades disponibles.
        /// </summary>
        /// <returns>El modelo EDM configurado con las entidades de la aplicación.</returns>
        static IEdmModel GetEdmModel()
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
        /// <param name="app">La instancia de la aplicación web.</param>
        static void Configure(WebApplication app)
        {
            // Configuración específica para entornos de desarrollo
            if (app.Environment.IsDevelopment())
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
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Services v1.0");
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
            app.MapControllers();
        }
    }
}

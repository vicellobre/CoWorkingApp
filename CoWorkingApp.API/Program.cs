using CoWorkingApp.API.Infrastructure.Adapters;
using CoWorkingApp.API.Infrastructure.Context;
using CoWorkingApp.API.Infrastructure.Extensions;
using CoWorkingApp.API.Infrastructure.Adapters;
using CoWorkingApp.API.Infrastructure.Context;
using CoWorkingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static void Main(string[] args)
    {
        // Crea una instancia del constructor de la aplicación
        var builder = WebApplication.CreateBuilder(args);

        // Configura los servicios que serán utilizados por la aplicación
        ConfigureServices(builder.Services, builder.Configuration);

        // Construye la aplicación
        var app = builder.Build();

        // Configura la aplicación y define cómo se procesarán las solicitudes HTTP
        Configure(app, app.Environment);

        // Ejecuta la aplicación
        app.Run();

        // Configura los servicios que serán utilizados por la aplicación
        static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Configuración del contexto de la base de datos
            services.AddDbContext<CoWorkingContext>(builder =>
            {
                builder.UseSqlServer(configuration.GetConnectionString("ConnectionCoWorking"));
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
            services.AddTokenAuthentication(configuration);

            // Configuración de AutoMapper y registro del perfil de mapeo
            services.AddAutoMapper(typeof(MappingProfile));

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
            services.AddSwaggerGen(options =>
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
        "\n\n La aplicación está centrada en la gestión de espacios de trabajo compartidos, con un enfoque educativo para que los desarrolladores comprendan la adopción de las buenas prácticas de desarrollo de software y los patrones de diseño como Repository, Adapter, Factory, Service, UseCase, CQRS y Controller .\n\n" +
        "## Repositorio\nEste repositorio se presenta como un recurso educativo y de referencia para aquellos interesados en aprender sobre la evolución de un proyecto desde un diseño basado en servicios hasta la adopción de Casos de Uso y CQRS. Siéntete libre de explorar y contribuir al proyecto para enriquecer aún más este aprendizaje.\n\n¡Gracias por ser parte de CoWorkingApp y explorar la evolución de las arquitecturas de software!",
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
            services.AddODataQueryFilter();
            services.AddControllers().AddOData(options =>
            {
                // Configuración de rutas y opciones de OData
                options.AddRouteComponents("odata", GetEdmModel()).Select().Filter().OrderBy().Count().Expand().SetMaxTop(100);
            });
        }

        // Configura la aplicación y define cómo se procesarán las solicitudes HTTP
        static void Configure(WebApplication app, IWebHostEnvironment env)
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

        // Obtiene el modelo EDM (Entity Data Model) para OData
        static IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();

            // Agrega entidades al modelo EDM
            odataBuilder.EntitySet<User>("Users");
            odataBuilder.EntitySet<Seat>("Seats");
            odataBuilder.EntitySet<Reservation>("Reservations");

            return odataBuilder.GetEdmModel();
        }
    }
}
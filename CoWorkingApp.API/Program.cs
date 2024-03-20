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
        // Crea una instancia del constructor de la aplicaci�n
        var builder = WebApplication.CreateBuilder(args);

        // Configura los servicios que ser�n utilizados por la aplicaci�n
        ConfigureServices(builder.Services, builder.Configuration);

        // Construye la aplicaci�n
        var app = builder.Build();

        // Configura la aplicaci�n y define c�mo se procesar�n las solicitudes HTTP
        Configure(app, app.Environment);

        // Ejecuta la aplicaci�n
        app.Run();

        // Configura los servicios que ser�n utilizados por la aplicaci�n
        static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Configuraci�n del contexto de la base de datos
            services.AddDbContext<CoWorkingContext>(builder =>
            {
                builder.UseSqlServer(configuration.GetConnectionString("ConnectionCoWorking"));
            });

            // Agrega dependencias adicionales
            services.AddDependency();

            // Configuraci�n CORS
            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                           .AllowAnyOrigin()
                           .AllowAnyMethod();
                });
            });

            // Configuraci�n de autenticaci�n JWT
            services.AddTokenAuthentication(configuration);

            // Configuraci�n de AutoMapper y registro del perfil de mapeo
            services.AddAutoMapper(typeof(MappingProfile));

            // Configuraci�n de los controladores (API Endpoints)
            services.AddControllers(options =>
            {
                // Requiere que los usuarios est�n autenticados
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            // Configuraci�n para habilitar la cach� de respuestas
            services.AddResponseCaching();

            // Configuraci�n de Swagger/OpenAPI (documentaci�n de la API)
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                // Informaci�n del archivo XML de documentaci�n
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                // Configuraci�n de Swagger
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CoWorking API",
                    Description = "Este proyecto de API ASP.NET Core, denominado CoWorkingApp, tiene como objetivo demostrar la evoluci�n en el dise�o y desarrollo de software a trav�s de la implementaci�n gradual de diferentes patrones arquitect�nicos." +
        "\n\n La aplicaci�n est� centrada en la gesti�n de espacios de trabajo compartidos, con un enfoque educativo para que los desarrolladores comprendan la adopci�n de las buenas pr�cticas de desarrollo de software y los patrones de dise�o como Repository, Adapter, Factory, Service, UseCase, CQRS y Controller .\n\n" +
        "## Repositorio\nEste repositorio se presenta como un recurso educativo y de referencia para aquellos interesados en aprender sobre la evoluci�n de un proyecto desde un dise�o basado en servicios hasta la adopci�n de Casos de Uso y CQRS. Si�ntete libre de explorar y contribuir al proyecto para enriquecer a�n m�s este aprendizaje.\n\n�Gracias por ser parte de CoWorkingApp y explorar la evoluci�n de las arquitecturas de software!",
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

            // Configuraci�n para OData
            services.AddODataQueryFilter();
            services.AddControllers().AddOData(options =>
            {
                // Configuraci�n de rutas y opciones de OData
                options.AddRouteComponents("odata", GetEdmModel()).Select().Filter().OrderBy().Count().Expand().SetMaxTop(100);
            });
        }

        // Configura la aplicaci�n y define c�mo se procesar�n las solicitudes HTTP
        static void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configuraci�n espec�fica para entornos de desarrollo
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

            // Redirige el tr�fico HTTP a HTTPS
            app.UseHttpsRedirection();

            // Configuraci�n del enrutamiento de solicitudes
            app.UseRouting();

            // Habilita la cach� de respuestas
            app.UseResponseCaching();

            // Habilita CORS
            app.UseCors("MyPolicy");

            // Configuraci�n de autenticaci�n y autorizaci�n
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
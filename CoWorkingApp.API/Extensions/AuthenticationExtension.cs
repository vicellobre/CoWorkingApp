using CoWorkingApp.Application.Behaviors;
using CoWorkingApp.Persistence.Context;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CoWorkingApp.API.Extensions;

/// <summary>
/// Clase de extensión para configurar la autenticación mediante token en la aplicación.
/// </summary>
[ExcludeFromCodeCoverage]
public static class AuthenticationExtension
{
    /// <summary>
    /// Método para agregar la autenticación mediante token a la colección de servicios.
    /// </summary>
    /// <param name="services">Colección de servicios de la aplicación.</param>
    /// <param name="configuration">Configuración de la aplicación.</param>
    /// <returns>Colección de servicios con la autenticación mediante token agregada.</returns>
    public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // Clave secreta para firmar y validar el token
        var secretKey = configuration["Auth:Jwt:SecretKey"] ?? throw new ArgumentNullException();
        var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

        // Configuración de la autenticación mediante token
        services
        .AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            // Parámetros de validación del token
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = signingKey,
                ValidIssuer = configuration["Auth:Jwt:Issuer"], // Emisor del token
                ValidAudience = configuration["Auth:Jwt:Audience"], // Consumidores válidos del token
                ValidateIssuer = false, // Validar el emisor o origen del token (desactivado)
                ValidateAudience = false, // Validar el destinatario o consumidor del token (desactivado)
                ValidateIssuerSigningKey = true, // Validar la clave de firma (activado)

                ValidateLifetime = true, // Validar el tiempo de validez del token
                ClockSkew = TimeSpan.Zero, // Sin margen de tiempo adicional
                RequireExpirationTime = true, // Exigir tiempo de expiración en el token
            };

            // Eventos para gestionar la autenticación mediante token
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    // Manejar el evento de falla de autenticación
                    Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    // Manejar el evento de token validado
                    Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
    {
        // Detectar si se ejecuta en un contenedor Docker
        //var isRunningInContainer = Environment.GetEnvironmentVariable("RUNNING_IN_CONTAINER") == "true";

        // Seleccionar la cadena de conexión adecuada
        //var connectionString = isRunningInContainer
        //? _configuration.GetConnectionString("ConnectionCoWorking_Container")
        //: _configuration.GetConnectionString("ConnectionCoWorking");


        var connectionString = configuration.GetConnectionString("ConnectionCoWorking");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("The connection string is not configured correctly.");
        }

        // Configuración del contexto de la base de datos
        services.AddDbContext<CoWorkingContext>(options =>
            options.UseSqlServer(connectionString,
            sqlOptions => sqlOptions.MigrationsAssembly("CoWorkingApp.Persistence")));

        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddFeaturesWithValidations(this IServiceCollection services)
    {
        // Configuración de MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Application.AssemblyReference.Assembly));

        // MediatR with FluentValidation
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(InputFilterBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        // Configuración de FluentValidation
        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly, includeInternalTypes: true);

        return services;
    }
}

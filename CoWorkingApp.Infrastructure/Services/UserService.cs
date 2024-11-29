using CoWorkingApp.Application.Contracts.Adapters;
using CoWorkingApp.Application.Contracts.Services;
using CoWorkingApp.Application.DTOs;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Contracts.UnitOfWork;
using CoWorkingApp.Core.Entities;
using CoWorkingApp.Infrastructure.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace CoWorkingApp.Infrastructure.Services;

/// <summary>
/// Implementación concreta del servicio para la entidad <see cref="User"/>.
/// </summary>
public class UserService : ServiceGeneric<IUserRepository, User, UserRequest, UserResponse>, IUserService
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UserService"/> utilizando las dependencias necesarias.
    /// </summary>
    /// <param name="unitOfWork">La unidad de trabajo para manejar transacciones.</param>
    /// <param name="repository">El repositorio de tipo <see cref="IUserRepository"/> asociado al servicio.</param>
    /// <param name="mapper">El adaptador de mapeo para convertir entre entidades y DTOs.</param>
    public UserService(IUnitOfWork? unitOfWork, IUserRepository? repository, IMapperAdapter? mapper) : base(unitOfWork, repository, mapper) { }

    // Implementación de métodos específicos de IUserService

    /// <summary>
    /// Obtiene una entidad <see cref="UserResponse"/> por su dirección de correo electrónico.
    /// </summary>
    /// <param name="email">La dirección de correo electrónico del usuario.</param>
    /// <returns>Un objeto <see cref="UserResponse"/> que representa al usuario encontrado.</returns>
    public async Task<UserResponse> GetByEmailAsync(string? email)
    {
        try
        {
            // Verificar si el correo electrónico es nulo o vacío
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email), "The email cannot be null or empty");
            }

            // Validar el formato del correo electrónico
            if (!EmailIsValid(email))
            {
                throw new ValidationException("Argument is invalid.");
            }

            // Obtener el usuario por su correo electrónico desde el repositorio. Si no, lanzar una excepción
            var user = await _repository.GetByEmailAsync(email) ?? throw new ArgumentException($"Email {email} not found");

            // Mapear el usuario a una respuesta de usuario y establecer el éxito en verdadero
            var response = _mapper.Map<User, UserResponse>(user);
            response.Success = true;
            return response;
        }
        catch (Exception ex)
        {
            // Manejar excepciones inesperadas y generar una respuesta de error
            return HandleException(ex);
        }
    }

    /// <summary>
    /// Autentica a una entidad <see cref="UserResponse"/> utilizando su dirección de correo electrónico y contraseña.
    /// </summary>
    /// <param name="request">La solicitud de autenticación que contiene la dirección de correo electrónico y la contraseña del usuario.</param>
    /// <returns>Un objeto <see cref="UserResponse"/> que representa al usuario autenticado.</returns>
    public async Task<UserResponse> AuthenticateAsync(UserRequest? request)
    {
        try
        {
            // Verificar si la solicitud es nula
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request), "The request object cannot be null or empty");
            }

            // Verificar si el correo electrónico o la contraseña son nulos o vacíos
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                throw new ArgumentNullException(nameof(request), "The email or password cannot be null or empty");
            }

            // Validar el formato del correo electrónico y la contraseña
            if (!CredentialsIsValid(request.Email, request.Password))
            {
                throw new ValidationException("Argument is invalid.");
            }

            // Autenticar al usuario utilizando el correo electrónico y la contraseña proporcionados
            var user = await _repository.AuthenticateAsync(request.Email, request.Password) ?? throw new ArgumentException("Credentials incorrect");

            // Mapear el usuario autenticado a una respuesta de usuario y establecer el éxito en verdadero
            var response = _mapper.Map<User, UserResponse>(user);
            response.Success = true;
            return response;
        }
        catch (Exception ex)
        {
            // Manejar excepciones inesperadas y generar una respuesta de error
            return HandleException(ex);
        }
    }

    // Implementación de métodos abstractos de ServiceGeneric

    /// <summary>
    /// Actualiza las propiedades de una entidad <see cref="User"/> existente con los valores de una solicitud de tipo <see cref="UserRequest"/>.
    /// </summary>
    /// <param name="existingEntity">La entidad existente que se va a actualizar.</param>
    /// <param name="request">La solicitud de usuario con los nuevos valores.</param>
    /// <returns>La entidad <see cref="User"/> actualizada.</returns>
    protected override User UpdateProperties(User existingEntity, UserRequest request)
    {
        // Actualizar las propiedades del usuario existente con los valores de la solicitud
        if (!string.IsNullOrEmpty(request.Name))
        {
            existingEntity.Name = request.Name;
        }

        if (!string.IsNullOrEmpty(request.LastName))
        {
            existingEntity.LastName = request.LastName;
        }

        if (!string.IsNullOrEmpty(request.Email))
        {
            existingEntity.Email = request.Email;
        }

        if (!string.IsNullOrEmpty(request.Password))
        {
            existingEntity.Password = request.Password;
        }

        // Retornar la entidad existente que ahora está actualizada
        return existingEntity;
    }

    /// <summary>
    /// Verifica si una entidad <see cref="User"/> es válida.
    /// </summary>
    /// <param name="entity">La entidad <see cref="User"/> que se va a validar.</param>
    /// <returns>True si la entidad <see cref="User"/> es válida, de lo contrario False.</returns>
    protected override bool IsValid(User entity) =>
        // Verificar si el usuario es válido (ninguna propiedad debe ser nula o vacía)
        entity is not null
               && !string.IsNullOrEmpty(entity.Name)
               && !string.IsNullOrEmpty(entity.LastName)
               && !string.IsNullOrEmpty(entity.Email)
               && !string.IsNullOrEmpty(entity.Password);

    // Métodos privados

    /// <summary>
    /// Verifica si el formato de una dirección de correo electrónico es válido.
    /// </summary>
    /// <param name="email">La dirección de correo electrónico a validar.</param>
    /// <returns>True si el formato del correo electrónico es válido, de lo contrario False.</returns>
    private static bool EmailIsValid(string email) =>
        // Verificar si el formato del correo electrónico es válido
        email.Length > 1 && email.Contains('@');

    /// <summary>
    /// Verifica si una contraseña tiene una longitud mayor que 1.
    /// </summary>
    /// <param name="password">La contraseña a validar.</param>
    /// <returns>True si la contraseña tiene una longitud mayor que 1, de lo contrario False.</returns>
    private static bool PasswordIsValid(string password) =>
        // Verificar si la contraseña tiene una longitud mayor que 1
        password.Length > 1;

    /// <summary>
    /// Verifica si las credenciales de correo electrónico y contraseña son válidas.
    /// </summary>
    /// <param name="email">El correo electrónico a validar.</param>
    /// <param name="password">La contraseña a validar.</param>
    /// <returns>True si tanto el correo electrónico como la contraseña son válidos, de lo contrario False.</returns>
    private static bool CredentialsIsValid(string email, string password) =>
        EmailIsValid(email) && PasswordIsValid(password);
}

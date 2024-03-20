using CoWorkingApp.Core.Application.Abstracts;
using CoWorkingApp.Core.Application.Contracts.Adapters;
using CoWorkingApp.Core.Application.Contracts.Repositories;
using CoWorkingApp.Core.Application.Contracts.Services;
using CoWorkingApp.Core.Domain.DTOs;
using CoWorkingApp.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace CoWorkingApp.Core.Application.Services
{
    /// <summary>
    /// Implementación concreta del servicio para la entidad User.
    /// </summary>
    public class UserService : ServiceGeneric<IUserRepository, User, UserRequest, UserResponse>, IUserService
    {
        /// <summary>
        /// Constructor de la clase UserService.
        /// </summary>
        /// <param name="repository">El repositorio de usuarios asociado al servicio.</param>
        public UserService(IUserRepository repository, IMapperAdapter mapper) : base(repository, mapper) { }

        // Implementación de métodos específicos de IUserService

        /// <summary>
        /// Obtiene un usuario por su dirección de correo electrónico.
        /// </summary>
        /// <param name="email">La dirección de correo electrónico del usuario.</param>
        /// <returns>Un objeto UserResponse que representa al usuario encontrado.</returns>
        public async Task<UserResponse> GetByEmailAsync(string email)
        {
            try
            {
                // Verificar si el correo electrónico es nulo o vacío
                if (string.IsNullOrEmpty(email))
                {
                    // Si es nulo o vacío, lanzar una excepción
                    throw new ArgumentNullException("The email cannot be null or empty");
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
            catch (ArgumentNullException ex)
            {
                // Manejar la excepción de argumento nulo y generar una respuesta de error
                return HandleException(ex);
            }
            catch (ValidationException ex)
            {
                // Manejar la excepción de validación y generar una respuesta de error
                return HandleException(ex);
            }
            catch (ArgumentException ex)
            {
                // Manejar la excepción de argumento inválido y generar una respuesta de error
                return HandleException(ex);
            }
            catch (Exception ex)
            {
                // Manejar excepciones inesperadas y generar una respuesta de error
                var response = new UserResponse { Success = false, Message = ex.Message };
                response.Errors.Add(ex.Message);
                return response;
            }
        }

        /// <summary>
        /// Autentica a un usuario utilizando su dirección de correo electrónico y contraseña.
        /// </summary>
        /// <param name="request">La solicitud de autenticación que contiene la dirección de correo electrónico y la contraseña del usuario.</param>
        /// <returns>Un objeto UserResponse que representa al usuario autenticado.</returns>
        public async Task<UserResponse> AuthenticateAsync(UserRequest request)
        {
            try
            {
                // Verificar si la solicitud es nula
                if (request is null)
                {
                    // Si es nula, lanzar una excepción
                    throw new ArgumentNullException("The request object cannot be null or empty");
                }

                // Verificar si el correo electrónico o la contraseña son nulos o vacíos
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                {
                    // Si lo son, lanzar una excepción
                    throw new ArgumentNullException("The email or password cannot be null or empty");
                }

                // Validar el formato del correo electrónico y la contraseña
                if (!CredentialsIsValid(request.Email, request.Password))
                {
                    throw new ValidationException("Argument is invalid.");
                }

                // Autenticar al usuario utilizando el correo electrónico y la contraseña proporcionados
                var user = await _repository.AuthenticateAsync(request.Email, request.Password);

                // Verificar si el usuario fue autenticado correctamente
                if (user is null)
                {
                    // Si no se autentica correctamente, lanzar una excepción
                    throw new ArgumentException($"Credentials incorrect");
                }

                // Mapear el usuario autenticado a una respuesta de usuario y establecer el éxito en verdadero
                var response = _mapper.Map<User, UserResponse>(user);
                response.Success = true;
                return response;
            }
            catch (ArgumentNullException ex)
            {
                // Manejar la excepción de argumento nulo y generar una respuesta de error
                return HandleException(ex);
            }
            catch (ValidationException ex)
            {
                // Manejar la excepción de validación y generar una respuesta de error
                return HandleException(ex);
            }
            catch (ArgumentException ex)
            {
                // Manejar la excepción de argumento inválido y generar una respuesta de error
                return HandleException(ex);
            }
            catch (Exception ex)
            {
                // Manejar excepciones inesperadas y generar una respuesta de error
                return HandleException(ex);
            }
        }

        // Implementación de métodos abstractos de ServiceGeneric

        /// <summary>
        /// ACTualiza las propiedades de un usuario existente con los valores de una solicitud de usuario.
        /// </summary>
        /// <param name="existingEntity">El usuario existente que se va a actualizar.</param>
        /// <param name="request">La solicitud de usuario con los nuevos valores.</param>
        /// <returns>El usuario actualizado.</returns>
        protected override User UpdateProperties(User existingEntity, UserRequest request)
        {
            // ACTualizar las propiedades del usuario existente con los valores de la solicitud
            return new User
            {
                Name = string.IsNullOrEmpty(request.Name) ? existingEntity.Name : request.Name,
                LastName = string.IsNullOrEmpty(request.LastName) ? existingEntity.LastName : request.LastName,
                Email = string.IsNullOrEmpty(request.Email) ? existingEntity.Email : request.Email,
                Password = string.IsNullOrEmpty(request.Password) ? existingEntity.Password : request.Password,
            };
        }

        /// <summary>
        /// Verifica si un usuario es válido.
        /// </summary>
        /// <param name="entity">El usuario que se va a validar.</param>
        /// <returns>True si el usuario es válido, de lo contrario False.</returns>
        protected override bool IsValid(User entity)
        {
            // Verificar si el usuario es válido (ninguna propiedad debe ser nula o vacía)
            return entity is not null
                   && !string.IsNullOrEmpty(entity.Name)
                   && !string.IsNullOrEmpty(entity.LastName)
                   && !string.IsNullOrEmpty(entity.Email)
                   && !string.IsNullOrEmpty(entity.Password);
        }

        // Métodos privados

        /// <summary>
        /// Verifica si el formato de una dirección de correo electrónico es válido.
        /// </summary>
        /// <param name="email">La dirección de correo electrónico a validar.</param>
        /// <returns>True si el formato del correo electrónico es válido, de lo contrario False.</returns>
        private bool EmailIsValid(string email)
        {
            // Verificar si el formato del correo electrónico es válido
            return email.Length > 1 && email.Contains('@');
        }

        /// <summary>
        /// Verifica si una contraseña tiene una longitud mayor que 1.
        /// </summary>
        /// <param name="password">La contraseña a validar.</param>
        /// <returns>True si la contraseña tiene una longitud mayor que 1, de lo contrario False.</returns>
        private bool PasswordIsValid(string password)
        {
            // Verificar si la contraseña tiene una longitud mayor que 1
            return password.Length > 1;
        }

        /// <summary>
        /// Verifica si las credenciales de correo electrónico y contraseña son válidas.
        /// </summary>
        /// <param name="email">El correo electrónico a validar.</param>
        /// <param name="password">La contraseña a validar.</param>
        /// <returns>True si tanto el correo electrónico como la contraseña son válidos, de lo contrario False.</returns>
        private bool CredentialsIsValid(string email, string password)
        {
            return EmailIsValid(email) && PasswordIsValid(password);
        }
    }
}

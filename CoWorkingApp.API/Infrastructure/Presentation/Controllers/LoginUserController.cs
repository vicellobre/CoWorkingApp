using CoWorkingApp.Core.Application.Abstracts;
using CoWorkingApp.Core.Application.Contracts.Services;
using CoWorkingApp.Core.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoWorkingApp.API.Infrastructure.Presentation.Controllers
{
    /// <summary>
    /// Controlador para operaciones relacionadas con la autenticación de usuarios.
    /// </summary>
    [EnableCors("MyPolicy")] // Habilita CORS para este controlador específico
    [ApiController]
    [Route("[controller]s")] // Ruta del controlador, en plural por convención RESTful
    public class LoginUserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor de la clase LoginUserController.
        /// </summary>
        /// <param name="service">Instancia del servicio de usuarios.</param>
        /// <param name="configuration">Instancia de IConfiguration para acceder a la configuración de la aplicación.</param>
        public LoginUserController(IUserService? service, IConfiguration configuration)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Método para realizar la autenticación de un usuario.
        /// </summary>
        /// <param name="user">Datos de usuario (correo y contraseña) para autenticación.</param>
        /// <returns>ActionResult con el token generado o un mensaje de error.</returns>
        [HttpPost]
        [AllowAnonymous] // Permite el acceso a este método sin autenticación
        [Route("validateuser")]
        public async Task<IActionResult> Login([FromBody] UserRequest user)
        {
            try
            {
                // Autenticar al usuario utilizando el servicio
                var authenticatedUser = await _service.AuthenticateAsync(user);

                if (!authenticatedUser.Success)
                {
                    // Usuario no autenticado, devuelve un Unauthorized con un mensaje de error
                    authenticatedUser.Message = "Invalid email or password";
                    authenticatedUser.Errors.Add(authenticatedUser.Message);
                    return Unauthorized(authenticatedUser);
                }

                // Usuario autenticado, generamos un token JWT
                var token = BuildToken(authenticatedUser);

                // Retorna el token en la respuesta
                return Ok(new { Response = authenticatedUser, Token = token });
            }
            catch (Exception)
            {
                // Manejar cualquier error y devolver un mensaje de error
                var exception = new Exception("An unexpected error occurred while retrieving all entities");
                var response = ResponseMessage.HandleException<UserResponse>(exception);
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Método para construir el token JWT.
        /// </summary>
        /// <param name="user">Usuario autenticado para el cual se genera el token.</param>
        /// <returns>JsonResult con el token generado.</returns>
        private JsonResult BuildToken(UserResponse user)
        {
            // Verifica que el usuario no sea nulo
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            // Verifica que los campos del usuario no sean nulos
            if (string.IsNullOrEmpty(user.Name))
            {
                throw new ArgumentNullException(nameof(user.Name), "User name cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(user.LastName))
            {
                throw new ArgumentNullException(nameof(user.LastName), "User last name cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                throw new ArgumentNullException(nameof(user.Email), "User email cannot be null or empty.");
            }

            // Obtener el origen del emisor y la audiencia desde la configuración
            string issuer = _configuration["Auth:Jwt:Issuer"] ?? throw new ArgumentNullException(nameof(issuer), "Issuer cannot be null.");
            string audience = _configuration["Auth:Jwt:Audience"] ?? throw new ArgumentNullException(nameof(audience), "Audience cannot be null.");
            string secretKey = _configuration["Auth:Jwt:SecretKey"] ?? throw new ArgumentNullException(nameof(secretKey), "SecretKey cannot be null.");

            // Datos a incluir en el token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Name, user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            // Generar la clave secreta para firmar el token
            var key = Encoding.UTF8.GetBytes(secretKey);
            var symmetricSecurityKey = new SymmetricSecurityKey(key);
            var creds = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            // Calcular el tiempo de validez del token
            DateTime now = DateTime.Now;
            double minutes = Convert.ToDouble(_configuration["Auth:Jwt:TokenExpirationInMinutes"]);
            DateTime expiredDateTime = now.AddMinutes(minutes);

            // Generar el token JWT
            var token = new JwtSecurityToken(issuer,
                                             audience,
                                             claims,
                                             expires: expiredDateTime,
                                             signingCredentials: creds
            );

            // Escribir el token como una cadena
            var tokenSecurity = new JwtSecurityTokenHandler();
            var tokenString = tokenSecurity.WriteToken(token);

            // Retornar el token en un JsonResult
            return new JsonResult(new { Token = tokenString });
        }
    }
}

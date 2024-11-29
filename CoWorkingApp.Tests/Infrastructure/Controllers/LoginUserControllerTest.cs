using CoWorkingApp.Application.Contracts.Services;
using CoWorkingApp.Application.DTOs;
using CoWorkingApp.Presentation.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace CoWorkingApp.Tests.Infrastructure.Controllers
{
    public class LoginUserControllerTest
    {
        public class Constructor
        {
            /// <summary>
            /// Verifica que el constructor de la clase LoginUserController cree una instancia correctamente cuando los parámetros no son nulos.
            /// </summary>
            [Fact]
            public void Constructor_When_ParametersAreNotNull()
            {
                // ARRANGE
                // Se crea un mock de IUserService y IConfiguration
                var mockService = new Mock<IUserService>();
                var mockConfiguration = new Mock<IConfiguration>();

                // ACT
                // Se llama al constructor de LoginUserController con los mocks configurados
                var result = new LoginUserController(mockService.Object, mockConfiguration.Object);

                // ASSERT
                // Se verifica que el resultado no sea nulo y sea del tipo LoginUserController
                Assert.NotNull(result);
                Assert.IsType<LoginUserController>(result);
            }

            /// <summary>
            /// Verifica que el constructor de la clase LoginUserController lance una excepción cuando el servicio es nulo.
            /// </summary>
            [Fact]
            public void Constructor_Throws_ArgumentNullException_When_NullService()
            {
                // ARRANGE
                // Se establece el servicio como nulo y se crea un mock de IConfiguration
                IUserService? service = null;
                var mockConfiguration = new Mock<IConfiguration>();

                // ACT
                // Se intenta llamar al constructor de LoginUserController con el servicio nulo
                var result = () => new LoginUserController(service, mockConfiguration.Object);

                // ASSERT
                // Se verifica que el resultado no sea nulo y que lance una excepción del tipo ArgumentNullException
                Assert.NotNull(result);
                Assert.Throws<ArgumentNullException>(result);
            }

            /// <summary>
            /// Verifica que el constructor de la clase LoginUserController lance una excepción cuando la configuración es nula.
            /// </summary>
            [Fact]
            public void Constructor_Throws_ArgumentNullException_When_NullConfiguration()
            {
                // ARRANGE
                // Se crea un mock de IUserService y se establece la configuración como nula
                var mockService = new Mock<IUserService>();
                IConfiguration? configuration = null;

                // ACT
                // Se intenta llamar al constructor de LoginUserController con la configuración nula
                var result = () => new LoginUserController(mockService.Object, configuration);

                // ASSERT
                // Se verifica que el resultado no sea nulo y que lance una excepción del tipo ArgumentNullException
                Assert.NotNull(result);
                Assert.Throws<ArgumentNullException>(result);
            }
        }

        public class Login
        {
            /// <summary>
            /// Verifica que el método Login del controlador LoginUserController devuelva una respuesta de usuario exitosa en caso de una respuesta exitosa.
            /// </summary>
            [Fact]
            public void Login_Returns_UserResponse_When_SuccessfulResponse()
            {
                // ARRANGE
                // Se crean los valores de configuración necesarios para la generación del token
                var configValues = new Dictionary<string, string?>
                {
                    { "Auth:Jwt:Issuer", "" },
                    { "Auth:Jwt:Audience", "" },
                    { "Auth:Jwt:SecretKey", "995a2f5bffed19e5ea33ad4e5d6fa2120f956a8df4449ee77030a52fca245f4b" },
                    { "Auth:Jwt:TokenExpirationInMinutes", "20" }
                    // Se pueden agregar otros valores de configuración simulados si es necesario
                };

                // Se crea una configuración de prueba utilizando los valores de configuración simulados
                var configuration = TestConfigurationFactory.CreateConfiguration(configValues);

                // Se crea una solicitud de usuario simulada
                var userRequest = new UserRequest
                {
                    Name = "John",
                    LastName = "Doe",
                    Email = "john@example.com",
                    Password = "1234"
                };

                // Se crea una respuesta de usuario simulada exitosa
                var userResponse = new UserResponse
                {
                    Success = true,
                    Name = userRequest.Name,
                    LastName = userRequest.LastName,
                    Email = userRequest.Email
                };

                // Se crea un mock de IConfiguration que devuelve la configuración simulada
                var mockConfiguration = new Mock<IConfiguration>();
                mockConfiguration.Setup(c => c[It.IsAny<string>()]).Returns((string key) => configuration[key]);

                // Se crea un mock de IUserService que devuelve la respuesta de usuario simulada
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.AuthenticateAsync(It.IsAny<UserRequest>())).ReturnsAsync(userResponse);

                // Se crea una instancia del controlador LoginUserController con los mocks configurados
                var controller = new LoginUserController(mockService.Object, mockConfiguration.Object);

                // ACT
                // Se llama al método Login del controlador y se obtiene la respuesta de usuario resultante
                var loginTask = controller.Login(userRequest);
                var loginResult = loginTask?.Result;
                var loginObjectResult = loginResult as ObjectResult;

                var loginValue = loginObjectResult?.Value;
                var loginValueType = loginValue?.GetType();

                var responseProperty = loginValueType?.GetProperty("Response");
                var userResponseValue = responseProperty?.GetValue(loginValue);
                var resultUserResponse = userResponseValue as UserResponse;

                // ASSERT
                // Se verifica que la respuesta de usuario obtenida no sea nula y sea exitosa
                Assert.NotNull(resultUserResponse);
                Assert.True(resultUserResponse.Success);
                // Verifica que el StatusCode sea 200 (OK)
                Assert.Equal(StatusCodes.Status200OK, loginObjectResult?.StatusCode);
            }

            /// <summary>
            /// Verifica que el método Login del controlador LoginUserController devuelva un token en caso de una respuesta exitosa.
            /// </summary>
            [Fact]
            public void Login_Returns_Token_When_SuccessfulResponse()
            {
                // ARRANGE
                // Se crean los valores de configuración necesarios para la generación del token
                var configValues = new Dictionary<string, string?>
                {
                    { "Auth:Jwt:Issuer", "" },
                    { "Auth:Jwt:Audience", "" },
                    { "Auth:Jwt:SecretKey", "995a2f5bffed19e5ea33ad4e5d6fa2120f956a8df4449ee77030a52fca245f4b" },
                    { "Auth:Jwt:TokenExpirationInMinutes", "20" }
                    // Se pueden agregar otros valores de configuración simulados si es necesario
                };

                // Se crea una configuración de prueba utilizando los valores de configuración simulados
                var configuration = TestConfigurationFactory.CreateConfiguration(configValues);

                // Se crea una solicitud de usuario simulada
                var userRequest = new UserRequest
                {
                    Name = "John",
                    LastName = "Doe",
                    Email = "john@example.com",
                    Password = "1234"
                };

                // Se crea una respuesta de usuario simulada exitosa
                var userResponse = new UserResponse
                {
                    Success = true,
                    Name = userRequest.Name,
                    LastName = userRequest.LastName,
                    Email = userRequest.Email
                };

                // Se crea un mock de IConfiguration que devuelve la configuración simulada
                var mockConfiguration = new Mock<IConfiguration>();
                mockConfiguration.Setup(c => c[It.IsAny<string>()]).Returns((string key) => configuration[key]);

                // Se crea un mock de IUserService que devuelve la respuesta de usuario simulada
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.AuthenticateAsync(It.IsAny<UserRequest>())).ReturnsAsync(userResponse);

                // Se crea una instancia del controlador LoginUserController con los mocks configurados
                var controller = new LoginUserController(mockService.Object, mockConfiguration.Object);

                // ACT
                // Se llama al método Login del controlador y se obtiene el token resultante
                var loginTask = controller.Login(userRequest);
                var loginResult = loginTask?.Result;
                var loginObjectResult = loginResult as ObjectResult;

                var loginValue = loginObjectResult?.Value;
                var loginValueType = loginValue?.GetType();

                var tokenProperty = loginValueType?.GetProperty("Token");
                var tokenValue = tokenProperty?.GetValue(loginValue);
                var tokenJsonResult = tokenValue as JsonResult;

                var tokenObject = tokenJsonResult?.Value;
                var tokenType = tokenObject?.GetType();

                var tokenPropertyValue = tokenType?.GetProperty("Token");
                var token = tokenPropertyValue?.GetValue(tokenObject);
                var tokenString = token as string;

                // ASSERT
                // Se verifica que el token obtenido no sea nulo, no esté vacío y tenga un valor
                Assert.NotNull(token);
                Assert.NotNull(tokenString);
                Assert.NotEmpty(tokenString);
                // Verificar el código de estado 200 OK
                Assert.Equal(StatusCodes.Status200OK, loginObjectResult?.StatusCode);
            }

            [Fact]
            public void Login_Returns_StatusCode401_When_FailureResponse()
            {
                // ARRANGE
                var errorMessage = "Invalid email or password";

                var userResponse = new UserResponse { Success = false };

                var mockConfiguration = new Mock<IConfiguration>();

                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.AuthenticateAsync(It.IsAny<UserRequest>())).ReturnsAsync(userResponse);

                // Se crea una instancia del controlador LoginUserController con los mocks configurados
                var controller = new LoginUserController(mockService.Object, mockConfiguration.Object);

                // ACT
                var result_Task = controller.Login(new UserRequest());
                var result_ActionResult = result_Task?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                Assert.NotNull(result);
                Assert.False(result.Success);
                Assert.Equal(errorMessage, result.Message);
                Assert.Equal(userResponse, result);
                Assert.Equal(StatusCodes.Status401Unauthorized, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Verifica que el método Login retorne un código de estado 500 cuando se produce una excepción.
            /// </summary>
            [Fact]
            public void Login_Returns_StatusCode500_When_ExceptionCaught_BecauseService()
            {
                // ARRANGE
                // Se crea una excepción simulada con un mensaje de error
                var exception = new Exception("An unexpected error occurred while retrieving all entities");
                var errorMessage = exception.Message;

                // Se crea un mock de IConfiguration
                var mockConfiguration = new Mock<IConfiguration>();

                // Se crea un mock de IUserService y se configura para que lance una excepción cuando se llame al método AuthenticateUserAsync
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.AuthenticateAsync(It.IsAny<UserRequest>())).ThrowsAsync(new Exception());

                // Se crea una instancia del controlador LoginUserController con los mocks configurados
                var controller = new LoginUserController(mockService.Object, mockConfiguration.Object);

                // ACT
                // Se llama al método Login del controlador
                var result_Task = controller.Login(new UserRequest());
                var result_ActionResult = result_Task?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                // Se verifica que el resultado no sea nulo y que corresponda a una respuesta de fallo con el mensaje de error de la excepción y el código de estado 500
                Assert.NotNull(result);
                Assert.False(result.Success);
                Assert.Equal(errorMessage, result.Message);
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }

            [Theory]
            [InlineData(null, "Doe", "john@example.com")]
            [InlineData("", "Doe", "john@example.com")]
            [InlineData("   ", "Doe", "john@example.com")]
            [InlineData("John", null, "john@example.com")]
            [InlineData("John", "", "john@example.com")]
            [InlineData("John", "   ", "john@example.com")]
            [InlineData("John", "Doe", null)]
            [InlineData("John", "Doe", "")]
            [InlineData("John", "Doe", " ")]
            public void Login_Returns_StatusCode500_When_ExceptionCaught_With_IncorrectUserProperties(string? name, string? lastname, string? email)
            {
                // ARRANGE
                var user = new UserResponse
                { 
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Success = true
                };

                // Se crea una excepción simulada con un mensaje de error
                var exception = new Exception("An unexpected error occurred while retrieving all entities");
                var errorMessage = exception.Message;

                // Se crea un mock de IConfiguration
                var mockConfiguration = new Mock<IConfiguration>();

                // Se crea un mock de IUserService y se configura para que lance una excepción cuando se llame al método AuthenticateUserAsync
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.AuthenticateAsync(It.IsAny<UserRequest>())).ReturnsAsync(user);

                // Se crea una instancia del controlador LoginUserController con los mocks configurados
                var controller = new LoginUserController(mockService.Object, mockConfiguration.Object);

                // ACT
                // Se llama al método Login del controlador
                var result_Task = controller.Login(new UserRequest());
                var result_ActionResult = result_Task?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                // Se verifica que el resultado no sea nulo y que corresponda a una respuesta de fallo con el mensaje de error de la excepción y el código de estado 500
                Assert.NotNull(result);
                Assert.False(result.Success);
                Assert.Equal(errorMessage, result.Message);
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }

            [Theory]
            [InlineData(null, "", "")]
            [InlineData("", null, "")]
            [InlineData("", "", null)]
            public void Login_Returns_StatusCode500_When_ExceptionCaught_With_BadConfigurations(string? issuer, string? audience, string? secretKey)
            {
                // ARRANGE
                var user = new UserResponse
                {
                    Name = "John",
                    LastName = "Doe",
                    Email = "john@example.com",
                    Success = true
                };

                // Se crean los valores de configuración necesarios para la generación del token
                var configValues = new Dictionary<string, string?>
                {
                    { "Auth:Jwt:Issuer", issuer },
                    { "Auth:Jwt:Audience", audience },
                    { "Auth:Jwt:SecretKey", secretKey },
                    // Se pueden agregar otros valores de configuración simulados si es necesario
                };

                // Se crea una configuración de prueba utilizando los valores de configuración simulados
                var configuration = TestConfigurationFactory.CreateConfiguration(configValues);

                // Se crea una excepción simulada con un mensaje de error
                var exception = new Exception("An unexpected error occurred while retrieving all entities");
                var errorMessage = exception.Message;

                // Se crea un mock de IConfiguration que devuelve la configuración simulada
                var mockConfiguration = new Mock<IConfiguration>();
                mockConfiguration.Setup(c => c[It.IsAny<string>()]).Returns((string key) => configuration[key]);

                // Se crea un mock de IUserService y se configura para que lance una excepción cuando se llame al método AuthenticateUserAsync
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.AuthenticateAsync(It.IsAny<UserRequest>())).ReturnsAsync(user);

                // Se crea una instancia del controlador LoginUserController con los mocks configurados
                var controller = new LoginUserController(mockService.Object, mockConfiguration.Object);

                // ACT
                // Se llama al método Login del controlador
                var result_Task = controller.Login(new UserRequest());
                var result_ActionResult = result_Task?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                // Se verifica que el resultado no sea nulo y que corresponda a una respuesta de fallo con el mensaje de error de la excepción y el código de estado 500
                Assert.NotNull(result);
                Assert.False(result.Success);
                Assert.Equal(errorMessage, result.Message);
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }
        }
    }
}

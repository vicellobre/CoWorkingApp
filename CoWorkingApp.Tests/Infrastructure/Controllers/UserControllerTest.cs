using CoWorkingApp.Application.Contracts.Services;
using CoWorkingApp.Application.DTOs;
using CoWorkingApp.Presentation.Abstracts;
using CoWorkingApp.Presentation.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoWorkingApp.Tests.Infrastructure.Controllers
{
    /// <summary>
    /// Clase de pruebas para el controlador UserController.
    /// </summary>
    public class UserControllerTest
    {
        public class Constructor
        {
            /// <summary>
            /// Prueba para verificar que el constructor de UserController inicializa correctamente cuando los parámetros no son nulos.
            /// </summary>
            [Fact]
            public void Constructor_When_ParametersAreNotNull()
            {
                // ARRANGE 
                // Crear mocks para IUserService y ILogger
                var mockService = new Mock<IUserService>();
                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                // ACT
                // Crear una instancia de UserController con los mocks
                var result = new UserController(mockService.Object, mockLogger.Object);

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
            }

            /// <summary>
            /// Prueba para verificar que el constructor de UserController lance una excepción cuando el logger es nulo.
            /// </summary>
            [Fact]
            public void Constructor_Throws_ArgumentNullException_When_NullLogger()
            {
                // ARRANGE 
                // Crear un mock para IUserService
                var mockService = new Mock<IUserService>();
                // Inicializar el logger como nulo
                ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>? logger = null;

                // ACT
                // Utilizar una expresión lambda para intentar crear una instancia de UserController con el logger nulo
                var result = () => new UserController(mockService.Object, logger);

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que la creación del controlador lance una excepción de argumento nulo
                Assert.Throws<ArgumentNullException>(result);
            }

            /// <summary>
            /// Prueba para verificar que el constructor de UserController lance una excepción cuando el servicio es nulo.
            /// </summary>
            [Fact]
            public void Constructor_Throws_ArgumentNullException_When_NullService()
            {
                // ARRANGE 
                // Inicializar el servicio como nulo
                IUserService? service = null;
                // Crear un mock para ILogger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                // ACT
                // Utilizar una expresión lambda para intentar crear una instancia de UserController con el servicio nulo
                var result = () => new UserController(service, mockLogger.Object);

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que la creación del controlador lance una excepción de argumento nulo
                Assert.Throws<ArgumentNullException>(result);
            }
        }

        public class GetAll
        {
            /// <summary>
            /// Comprueba que el método GetAll del controlador UserController devuelva correctamente todos los usuarios.
            /// </summary>
            [Fact]
            public void GetAll_Returns_StatusCode200_When_SuccessfulResponse()
            {
                // ARRANGE

                // Crear una lista de UserResponse
                var usersResponses = new List<UserResponse>
                {
                    new UserResponse { Success = true, Name = "John", LastName = "Doe", Email = "john@example.com" },
                    new UserResponse { Success = true, Name = "Jane", LastName = "Smith", Email = "jane@example.com" }
                };

                // Configurar el servicio mock para devolver la lista de usuarios
                var mockService = new Mock<IUserService>();
                mockService.Setup(u => u.GetAllAsync()).ReturnsAsync(usersResponses);

                // Mockear el logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                // Crear una instancia del controlador con el servicio y el logger mockeados
                var controller = new UserController(mockService.Object, mockLogger.Object);

                // ACT
                // Invocar el método GetAll del controlador
                var result_Task = controller.GetAll();
                // Obtener el resultado de la tarea
                var result_TaskResult = result_Task?.Result;
                // Obtener el resultado de la acción
                var result_ActionResult = result_TaskResult?.Result;
                // Verificar si el resultado es un OkObjectResult
                var result_ObjectResult = result_ActionResult as OkObjectResult;
                // Obtener el valor del resultado como una lista de UserResponse
                var result = result_ObjectResult?.Value as IEnumerable<UserResponse>;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que el número de usuarios en la respuesta coincida con el número de usuarios esperados
                Assert.Equal(usersResponses.Count, result.Count());
                // Verificar que todos los usuarios en la respuesta tengan éxito
                Assert.True(result.All(u => u.Success));
                // Verificar el código de estado 200 OK
                Assert.Equal(StatusCodes.Status200OK, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Verifica que GetAll maneje correctamente el caso en que al menos una respuesta sea un fallo.
            /// </summary>
            [Theory]
            [InlineData(true, true, false)]
            [InlineData(true, false, true)]
            [InlineData(false, true, true)]
            [InlineData(false, false, true)]
            [InlineData(false, false, false)]
            public void GetAll_Returns_StatusCode500_When_FailureResponse(bool success1, bool success2, bool success3)
            {
                // ARRANGE

                // Mensaje de error esperado
                var errorMessage = "Error occurred while retrieving entity";

                // Lista de respuestas de usuario con diferentes combinaciones de éxito/fallo
                var usersResponses = new List<UserResponse>
                {
                    new UserResponse { Success = success1, Name = "John", LastName = "Doe", Email = "john@example.com" },
                    new UserResponse { Success = success2, Name = "Jane", LastName = "Smith", Email = "jane@email.com" },
                    new UserResponse { Success = success3, Name = "Alice", LastName = "Johnson", Email = "alice@example.com" }
                };
                var userResponseFailureCount = usersResponses.Where(e => !e.Success).Count();

                var mockService = new Mock<IUserService>();
                mockService.Setup(u => u.GetAllAsync()).ReturnsAsync(usersResponses);

                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                var controller = new UserController(mockService.Object, mockLogger.Object);

                // ACT
                var result_Task = controller.GetAll();// Invocar el método GetAll del controlador
                var result_TaskResult = result_Task?.Result; // Obtener el resultado de la tarea
                var result_ActionResult = result_TaskResult?.Result; // Obtener el resultado de la acción
                var result_ObjectResult = result_ActionResult as ObjectResult; // Verificar si el resultado es un ObjectResult
                var result = result_ObjectResult?.Value as IEnumerable<UserResponse>; // Obtener el valor del resultado como una lista de UserResponse

                // ASSERT
                Assert.NotNull(result);  // Verifica que el resultado no sea nulo
                Assert.Equal(userResponseFailureCount, result.Count()); // Verifica que el número de respuestas sea el esperado
                Assert.Contains(result, u => !u.Success); // Verifica que al menos una respuesta sea un fallo
                Assert.True(result.All(u => u.Message == errorMessage)); // Verifica que todas las respuestas tengan el mismo mensaje de error
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode); // Verifica que el código de estado sea 500 (Error interno del servidor)
            }

            /// <summary>
            /// Prueba que GetAll devuelva un StatusCode 500 cuando se captura una excepción.
            /// </summary>
            [Fact]
            public void GetAll_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE
                // Crea una excepción simulada
                var exception = new Exception("An unexpected error occurred while retrieving all entities");
                var errorMessage = exception.Message;

                // Configura el servicio simulado para lanzar la excepción
                var mockService = new Mock<IUserService>();
                mockService.Setup(u => u.GetAllAsync()).ThrowsAsync(new Exception());

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new UserController(mockService.Object, mockLogger.Object);

                // ACT
                var result_Task = controller.GetAll();
                var result_TaskResult = result_Task?.Result;
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que el resultado indique un fallo
                Assert.False(result.Success);
                // Verifica que el resultado contenga un solo error
                Assert.Single(result.Errors);
                // Verifica que el mensaje del error sea el mismo que el de la excepción simulada
                Assert.Equal(errorMessage, result.Message);
                // Verifica que el StatusCode sea 500
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }

            ///// <summary>
            ///// Prueba asincrónica que verifica que el método GetAll del controlador UserController devuelve la respuesta correcta.
            ///// </summary>
            //[Fact]
            //public async Task GetAll_ReturnsCorrectResponse()
            //{
            //    // ARRANGE
            //    // Crear una instancia de CoWorkingContext utilizando una base de datos en memoria
            //    var context = TestContextFactory.CreateContext();

            //    // Agregar algunos datos de ejemplo a la base de datos en memoria
            //    var users = new List<User>
            //    {
            //        new User { Id = Guid.NewGuid(), Name = "User1", LastName = "LastName1", Email = "user1@example.com", Password = "password1" },
            //        new User { Id = Guid.NewGuid(), Name = "User2", LastName = "LastName2", Email = "user2@example.com", Password = "password2" },
            //        new User { Id = Guid.NewGuid(), Name = "User3", LastName = "LastName3", Email = "user3@example.com", Password = "password3" }
            //    };
            //    await context.Users.AddRangeAsync(users);
            //    await context.SaveChangesAsync();

            //    // Mock del servicio IUserService
            //    var userResponses = new List<UserResponse>
            //    {
            //        new UserResponse { Name = "User1", LastName = "LastName1", Email = "user1@example.com" },
            //        new UserResponse { Name = "User2", LastName = "LastName2", Email = "user2@example.com" },
            //        new UserResponse { Name = "User3", LastName = "LastName3", Email = "user3@example.com" }
            //    };

            //    var userServiceMock = new Mock<IUserService>();
            //    // Configurar el comportamiento esperado del servicio IUserService
            //    userServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(userResponses); // Devolver una lista de usuarios de prueba

            //    // Crear una instancia del controlador UserController utilizando el servicio IUserService mockeado
            //    var controller = new UserController(userServiceMock.Object, Mock.Of<ILogger<ControllerGeneric<IUserService, User, UserResponse>>>());

            //    // ACT
            //    // Llamar al método GetAll del controlador
            //    var result = await controller.GetAll();

            //    // ASSERT
            //    // Verificar que el resultado sea del tipo OkObjectResult
            //    Assert.IsType<OkObjectResult>(result.Result);

            //    // Obtener los datos devueltos por el controlador
            //    var okResult = result.Result as OkObjectResult;
            //    var usersResult = okResult?.Value as IEnumerable<UserResponse>;

            //    // Verificar que la cantidad de usuarios devueltos sea la esperada
            //    Assert.NotNull(usersResult);
            //    Assert.Equal(userResponses.Count, ((List<UserResponse>)usersResult).Count);
            //}
        }

        public class GetById
        {
            /// <summary>
            /// Verifica que el método GetById del controlador UserController devuelva un código de estado 200 en caso de una respuesta exitosa.
            /// </summary>
            [Fact]
            public void GetById_Returns_StatusCode200_When_SuccessfulResponse()
            {
                // ARRANGE
                // Se crea un usuario existente simulado
                var existingUser = new UserResponse
                {
                    Success = true,
                    Name = "John",
                    LastName = "Doe",
                    Email = "john@example.com",
                };

                // Se configura el mock de IUserService para que devuelva el usuario existente simulado
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingUser);

                // Se crea un mock de ILogger para el controlador UserController
                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                // Se crea una instancia del controlador UserController con los mocks configurados
                var controller = new UserController(mockService.Object, mockLogger.Object);

                // ACT
                // Se llama al método GetById del controlador y se obtiene el resultado
                var result_Task = controller.GetById(Guid.NewGuid());
                var result_TaskResult = result_Task?.Result;
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as OkObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                // Se verifica que el resultado no sea nulo, sea exitoso, coincida con el usuario existente simulado y tenga el código de estado 200
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.Equal(existingUser, result);
                Assert.Equal(StatusCodes.Status200OK, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba que GetById devuelva un StatusCode 500 cuando la respuesta presenta una falla.
            /// </summary>
            [Fact]
            public void GetById_Returns_StatusCode500_When_FailureResponse()
            {
                // ARRANGE
                // Mensaje de error esperado cuando la entidad no se encuentra
                var errorMessage = "Entity not found";

                // Simula una respuesta de usuario con indicación de fallo
                UserResponse user = new UserResponse { Success = false };

                // Configura el servicio simulado para devolver la respuesta de usuario creada anteriormente
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new UserController(mockService.Object, mockLogger.Object);

                // ACT
                var result_Task = controller.GetById(Guid.NewGuid());
                var result_TaskResult = result_Task?.Result;
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as NotFoundObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que el resultado indique un fallo
                Assert.False(result.Success);
                // Verifica que el resultado sea igual al usuario creado anteriormente
                Assert.Equal(user, result);
                // Verifica que el mensaje del error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verifica que el StatusCode sea 404 (Not Found)
                Assert.Equal(StatusCodes.Status404NotFound, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba que GetById devuelva un StatusCode 500 cuando se produce una excepción.
            /// </summary>
            [Fact]
            public void GetById_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE
                // Mensaje de error esperado cuando se produce una excepción
                var errorMessage = "Error retrieving entity by ID";

                // Simula una excepción al obtener una entidad por su ID
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new Exception());

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new UserController(mockService.Object, mockLogger.Object);

                // ACT
                var result_Task = controller.GetById(Guid.NewGuid());
                var result_TaskResult = result_Task?.Result;
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que el resultado indique un fallo
                Assert.False(result.Success);
                // Verifica que el mensaje del error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verifica que el StatusCode sea 500 (Internal Server Error)
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }
        }

        public class Create
        {
            /// <summary>
            /// Prueba que Create devuelva un StatusCode 201 (Created) en una respuesta exitosa.
            /// </summary>
            [Theory]
            [InlineData("John", "Doe", "john@example.com", "1234")]
            [InlineData("Jane", "Smith", "jane@example.com", "456")]
            public void Create_Returns_StatusCode201_When_SuccessfulResponse(string name, string lastname, string email, string password)
            {
                // ARRANGE
                // Crea una solicitud de usuario con los datos proporcionados
                var user = new UserRequest
                {
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Password = password
                };

                // Crea una respuesta de usuario exitosa con los mismos datos de la solicitud
                var userResponse = new UserResponse
                {
                    Success = true,
                    Name = user.Name,
                    LastName = user.LastName,
                    Email = user.Email
                };

                // Configura el servicio simulado para que devuelva la respuesta de usuario creada
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.CreateAsync(It.IsAny<UserRequest>())).ReturnsAsync(userResponse);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new UserController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método Create con la solicitud de usuario y obtiene el resultado
                var result_Task = controller.Create(user);
                var result_TaskResult = result_Task?.Result;
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que la operación haya tenido éxito
                Assert.True(result.Success);
                // Verifica que la respuesta sea igual a la esperada
                Assert.Equal(userResponse, result);
                // Verifica que el StatusCode sea 201 (Created)
                Assert.Equal(StatusCodes.Status201Created, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba que Create devuelva un StatusCode 400 (BadRequest) en una respuesta de fallo.
            /// </summary>
            [Fact]
            public void Create_Returns_StatusCode400_When_FailureResponse()
            {
                // ARRANGE
                // Mensaje de error esperado
                var errorMessage = "Error occurred while creating the entity.";

                // Crea una respuesta de usuario con fallo
                var userResponse = new UserResponse { Success = false };

                // Configura el servicio simulado para que devuelva la respuesta de usuario con fallo
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.CreateAsync(It.IsAny<UserRequest>())).ReturnsAsync(userResponse);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new UserController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método Create con una solicitud de usuario vacía y obtiene el resultado
                var result_Task = controller.Create(new UserRequest());
                var result_TaskResult = result_Task?.Result;
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que la operación haya fallado
                Assert.False(result.Success);
                // Verifica que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verifica que la respuesta sea igual a la esperada
                Assert.Equal(userResponse, result);
                // Verifica que el StatusCode sea 400 (BadRequest)
                Assert.Equal(StatusCodes.Status400BadRequest, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba que Create devuelva un StatusCode 500 (InternalServerError) cuando se captura una excepción.
            /// </summary>
            [Fact]
            public void Create_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE
                // Crea una excepción simulada
                var exception = new Exception("An unexpected error occurred while creating the entity.");
                // Mensaje de error esperado
                var errorMessage = exception.Message;

                // Configura el servicio simulado para que lance una excepción al llamar a CreateAsync
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.CreateAsync(It.IsAny<UserRequest>())).ThrowsAsync(exception);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new UserController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método Create con una solicitud de usuario vacía y obtiene el resultado
                var result_Task = controller.Create(new UserRequest());
                var result_TaskResult = result_Task?.Result;
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que la operación haya fallado
                Assert.False(result.Success);
                // Verifica que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verifica que el StatusCode sea 500 (InternalServerError)
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }
        }

        public class Update
        {
            /// <summary>
            /// Prueba que Update devuelva un StatusCode 200 (OK) cuando la actualización se realiza correctamente.
            /// </summary>
            [Theory]
            [InlineData("John", "Doe", "john@example.com", "1234")]
            [InlineData("Jane", "Smith", "jane@example.com", "456")]
            public void Update_Returns_StatusCode200_When_SuccessfulResponse(string name, string lastname, string email, string password)
            {
                // ARRANGE
                // Crea una solicitud de usuario con los datos proporcionados en los argumentos de la teoría
                var userRequest = new UserRequest
                {
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Password = password
                };

                // Crea una respuesta de usuario simulada con los mismos datos de la solicitud
                var userResponse = new UserResponse
                {
                    Success = true,
                    Name = userRequest.Name,
                    LastName = userRequest.LastName,
                    Email = userRequest.Email
                };

                // Configura el servicio simulado para que devuelva la respuesta de usuario simulada al llamar a UpdateAsync
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.UpdateAsync(It.IsAny<Guid>(), It.IsAny<UserRequest>())).ReturnsAsync(userResponse);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new UserController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método Update con un ID de usuario generado aleatoriamente y la solicitud de usuario creada, y obtiene el resultado
                var result_Task = controller.Update(Guid.NewGuid(), userRequest);
                var result_TaskResult = result_Task?.Result;
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as OkObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que la operación se haya realizado correctamente
                Assert.True(result.Success);
                // Verifica que la respuesta sea igual a la respuesta esperada
                Assert.Equal(userResponse, result);
                // Verifica que el StatusCode sea 200 (OK)
                Assert.Equal(StatusCodes.Status200OK, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba que Update devuelva un StatusCode 400 (BadRequest) cuando la actualización no se realiza correctamente.
            /// </summary>
            [Theory]
            [InlineData("John", "Doe", "john@example.com", "1234")]
            [InlineData("Jane", "Smith", "jane@example.com", "456")]
            public void Update_Returns_StatusCode400_When_FailureResponse(string name, string lastname, string email, string password)
            {
                // ARRANGE
                // Mensaje de error esperado en caso de falla
                var errorMessage = "Error occurred while updating the entity.";

                // Crea una solicitud de usuario con los datos proporcionados en los argumentos de la teoría
                var userRequest = new UserRequest
                {
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Password = password
                };

                // Crea una respuesta de usuario simulada con los mismos datos de la solicitud y establece Success en false para indicar una falla
                var userResponse = new UserResponse
                {
                    Success = false,
                    Name = userRequest.Name,
                    LastName = userRequest.LastName,
                    Email = userRequest.Email
                };

                // Configura el servicio simulado para que devuelva la respuesta de usuario simulada al llamar a UpdateAsync
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.UpdateAsync(It.IsAny<Guid>(), It.IsAny<UserRequest>())).ReturnsAsync(userResponse);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new UserController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método Update con un ID de usuario generado aleatoriamente y la solicitud de usuario creada, y obtiene el resultado
                var result_Task = controller.Update(Guid.NewGuid(), userRequest);
                var result_TaskResult = result_Task?.Result;
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as BadRequestObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que la operación haya fallado
                Assert.False(result.Success);
                // Verifica que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verifica que la respuesta sea igual a la respuesta esperada
                Assert.Equal(userResponse, result);
                // Verifica que el StatusCode sea 400 (BadRequest)
                Assert.Equal(StatusCodes.Status400BadRequest, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba que Update devuelva un StatusCode 500 (InternalServerError) cuando se produce una excepción.
            /// </summary>
            [Fact]
            public void Update_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE
                // Mensaje de error esperado en caso de excepción
                var errorMessage = "An unexpected error occurred while updating the entity.";

                // Simula una excepción
                var exception = new Exception(errorMessage);

                // Configura el servicio simulado para que lance una excepción al llamar a UpdateAsync
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.UpdateAsync(It.IsAny<Guid>(), It.IsAny<UserRequest>())).ThrowsAsync(exception);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new UserController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método Update con un ID de usuario generado aleatoriamente y una solicitud de usuario vacía, y obtiene el resultado
                var result_Task = controller.Update(Guid.NewGuid(), new UserRequest());
                var result_TaskResult = result_Task?.Result;
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que la operación haya fallado
                Assert.False(result.Success);
                // Verifica que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verifica que el StatusCode sea 500 (InternalServerError)
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }
        }

        public class Delete
        {
            /// <summary>
            /// Prueba que Delete devuelva un StatusCode 204 (NoContent) en caso de una respuesta exitosa.
            /// </summary>
            [Fact]
            public void Delete_Returns_StatusCode204_When_SuccessfulResponse()
            {
                // ARRANGE
                // Crea una respuesta de usuario exitosa
                var userResponse = new UserResponse { Success = true };

                // Configura el servicio simulado para que devuelva la respuesta de usuario al llamar a DeleteAsync
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(userResponse);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new UserController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método Delete con un ID de usuario generado aleatoriamente y obtiene el resultado
                var result_Task = controller.Delete(Guid.NewGuid());
                var result_TaskResult = result_Task?.Result;
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que la operación haya sido exitosa
                Assert.True(result.Success);
                // Verifica que la respuesta sea la esperada
                Assert.Equal(userResponse, result);
                // Verifica que el StatusCode sea 204 (NoContent)
                Assert.Equal(StatusCodes.Status204NoContent, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba que Delete devuelva un StatusCode 400 (BadRequest) en caso de una respuesta fallida.
            /// </summary>
            [Fact]
            public void Delete_Returns_StatusCode400_When_FailureResponse()
            {
                // ARRANGE
                // Mensaje de error esperado
                var errorMessage = "Error occurred while deleting the entity.";

                // Crea una respuesta de usuario fallida
                var userResponse = new UserResponse { Success = false };

                // Configura el servicio simulado para que devuelva la respuesta de usuario al llamar a DeleteAsync
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(userResponse);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new UserController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método Delete con un ID de usuario generado aleatoriamente y obtiene el resultado
                var result_Task = controller.Delete(Guid.NewGuid());
                var result_TaskResult = result_Task?.Result;
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que la operación haya fallado
                Assert.False(result.Success);
                // Verifica que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verifica que la respuesta sea la esperada
                Assert.Equal(userResponse, result);
                // Verifica que el StatusCode sea 400 (BadRequest)
                Assert.Equal(StatusCodes.Status400BadRequest, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba que Delete devuelva un StatusCode 500 (InternalServerError) cuando se captura una excepción.
            /// </summary>
            [Fact]
            public void Delete_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE
                // Mensaje de error esperado
                var errorMessage = "An unexpected error occurred while deleting the entity.";

                // Crea una excepción simulada
                var exception = new Exception(errorMessage);

                // Configura el servicio simulado para que lance una excepción al llamar a DeleteAsync
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.DeleteAsync(It.IsAny<Guid>())).ThrowsAsync(exception);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new UserController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método Delete con un ID de usuario generado aleatoriamente y obtiene el resultado
                var result_Task = controller.Delete(Guid.NewGuid());
                var result_TaskResult = result_Task?.Result;
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que la operación haya fallado
                Assert.False(result.Success);
                // Verifica que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verifica que el StatusCode sea 500 (InternalServerError)
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }
        }

        public class GetByEmail
        {
            /// <summary>
            /// Prueba que GetByEmail devuelva un StatusCode 200 (OK) cuando la respuesta es exitosa.
            /// </summary>
            [Fact]
            public void GetByEmail_Returns_StatusCode200_When_SuccessfulResponse()
            {
                // ARRANGE
                // Crea una respuesta de usuario exitosa
                var userResponse = new UserResponse { Success = true };

                // Configura el servicio simulado para que devuelva la respuesta de usuario exitosa al llamar a GetUserByEmailAsync
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(userResponse);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new UserController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método GetUserByEmail con cualquier dirección de correo electrónico y obtiene el resultado
                var result_Task = controller.GetByEmail(It.IsAny<string>());
                var result_TaskResult = result_Task?.Result;
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as OkObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que la operación haya tenido éxito
                Assert.True(result.Success);
                // Verifica que el resultado sea igual al esperado
                Assert.Equal(userResponse, result);
                // Verifica que el StatusCode sea 200 (OK)
                Assert.Equal(StatusCodes.Status200OK, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba que GetByEmail devuelva un StatusCode 400 (BadRequest) cuando la respuesta es fallida.
            /// </summary>
            [Fact]
            public void GetByEmail_Returns_StatusCode400_When_SuccessfulResponse()
            {
                // ARRANGE
                // Mensaje de error esperado
                var errorMessage = "Error occurred while retrieving the user by email.";

                // Respuesta de usuario fallida
                var userResponse = new UserResponse { Success = false };

                // Configura el servicio simulado para que devuelva la respuesta de usuario fallida al llamar a GetUserByEmailAsync
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(userResponse);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new UserController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método GetUserByEmail con cualquier dirección de correo electrónico y obtiene el resultado
                var result_Task = controller.GetByEmail(It.IsAny<string>());
                var result_TaskResult = result_Task?.Result;
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as BadRequestObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que la operación haya fallado
                Assert.False(result.Success);
                // Verifica que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verifica que el resultado sea igual al esperado
                Assert.Equal(userResponse, result);
                // Verifica que el StatusCode sea 400 (BadRequest)
                Assert.Equal(StatusCodes.Status400BadRequest, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba que GetByEmail devuelva un StatusCode 500 (InternalServerError) cuando se produce una excepción.
            /// </summary>
            [Fact]
            public void GetByEmail_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE
                // Mensaje de error de excepción esperado
                var exception = new Exception("An unexpected error occurred while getting the user by email.");
                var errorMessage = exception.Message;

                // Configura el servicio simulado para que lance una excepción al llamar a GetUserByEmailAsync
                var mockService = new Mock<IUserService>();
                mockService.Setup(s => s.GetByEmailAsync(It.IsAny<string>())).ThrowsAsync(exception);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IUserService, UserRequest, UserResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new UserController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método GetUserByEmail con cualquier dirección de correo electrónico y obtiene el resultado
                var result_Task = controller.GetByEmail(It.IsAny<string>());
                var result_TaskResult = result_Task?.Result;
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as UserResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que la operación haya fallado
                Assert.False(result.Success);
                // Verifica que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verifica que el StatusCode sea 500 (InternalServerError)
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }
        }
    }
}

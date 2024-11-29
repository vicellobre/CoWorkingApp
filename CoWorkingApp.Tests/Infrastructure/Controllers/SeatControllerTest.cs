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
    public class SeatControllerTest
    {
        public class Constructor
        {
            /// <summary>
            /// Prueba para verificar que el constructor de SeatController inicializa correctamente cuando los parámetros no son nulos.
            /// </summary>
            [Fact]
            public void Constructor_When_ParametersAreNotNull()
            {
                // ARRANGE 
                // Crear mocks para ISeatService y ILogger
                var mockService = new Mock<ISeatService>();
                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                // ACT
                // Crear una instancia de SeatController con los mocks
                var result = new SeatController(mockService.Object, mockLogger.Object);

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
            }

            /// <summary>
            /// Prueba para verificar que el constructor de SeatController lance una excepción cuando el logger es nulo.
            /// </summary>
            [Fact]
            public void Constructor_Throws_ArgumentNullException_When_NullLogger()
            {
                // ARRANGE 
                // Crear un mock para ISeatService
                var mockService = new Mock<ISeatService>();
                // Inicializar el logger como nulo
                ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>? logger = null;

                // ACT
                // Utilizar una expresión lambda para intentar crear una instancia de SeatController con el logger nulo
                var result = () => new SeatController(mockService.Object, logger);

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que la creación del controlador lance una excepción de argumento nulo
                Assert.Throws<ArgumentNullException>(result);
            }

            /// <summary>
            /// Prueba para verificar que el constructor de SeatController lance una excepción cuando el servicio es nulo.
            /// </summary>
            [Fact]
            public void Constructor_Throws_ArgumentNullException_When_NullService()
            {
                // ARRANGE 
                // Inicializar el servicio como nulo
                ISeatService? service = null;
                // Crear un mock para ILogger
                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                // ACT
                // Utilizar una expresión lambda para intentar crear una instancia de SeatController con el servicio nulo
                var result = () => new SeatController(service, mockLogger.Object);

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
            /// Comprueba que el método GetAll del controlador SeatController devuelva correctamente todos los asientos.
            /// </summary>
            [Fact]
            public async void GetAll_Returns_StatusCode200_When_SuccessfulResponse()
            {
                // ARRANGE

                // Crear una lista de SeatResponse
                var seatsResponses = new List<SeatResponse>
                {
                    new SeatResponse { Success = true, Name = "Q-1", IsBlocked = false, Description = "This seat is located near the window." },
                    new SeatResponse { Success = true, Name = "M-2", IsBlocked = true, Description = "This seat is reserved for VIP guests." }
                };

                // Configurar el servicio mock para devolver la lista de asientos
                var mockService = new Mock<ISeatService>();
                mockService.Setup(u => u.GetAllAsync()).ReturnsAsync(seatsResponses);

                // Mockear el logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                // Crear una instancia del controlador con el servicio y el logger mockeados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT
                // Invocar el método GetAll del controlador
                var result_TaskResult = await controller.GetAll();
                // Obtener el resultado de la acción
                var result_ActionResult = result_TaskResult?.Result;
                // Verificar si el resultado es un OkObjectResult
                var result_ObjectResult = result_ActionResult as OkObjectResult;
                // Obtener el valor del resultado como una lista de SeatResponse
                var result = result_ObjectResult?.Value as IEnumerable<SeatResponse>;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que el número de asientos en la respuesta coincida con el número de asientos esperados
                Assert.Equal(seatsResponses.Count, result.Count());
                // Verificar que todos los asientos en la respuesta tengan éxito
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
            public async void GetAll_Returns_StatusCode500_When_FailureResponse(bool success1, bool success2, bool success3)
            {
                // ARRANGE

                // Mensaje de error esperado
                var errorMessage = "Error occurred while retrieving entity";

                // Lista de respuestas de asiento con diferentes combinaciones de éxito/fallo
                var seatsResponses = new List<SeatResponse>
                {
                    new SeatResponse { Success = success1, Name = "Q-1", IsBlocked = false, Description = "This seat is located near the window." },
                    new SeatResponse { Success = success2, Name = "M-2", IsBlocked = true, Description = "This seat is reserved for VIP guests." },
                    new SeatResponse { Success = success3, Name = "M-1", IsBlocked = false, Description = "This seat is in the back row." },
                };
                var seatResponseFailureCount = seatsResponses.Where(e => !e.Success).Count();

                var mockService = new Mock<ISeatService>();
                mockService.Setup(u => u.GetAllAsync()).ReturnsAsync(seatsResponses);

                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT
                var result_TaskResult = await controller.GetAll();// Invocar el método GetAll del controlador
                var result_ActionResult = result_TaskResult?.Result; // Obtener el resultado de la acción
                var result_ObjectResult = result_ActionResult as ObjectResult; // Verificar si el resultado es un ObjectResult
                var result = result_ObjectResult?.Value as IEnumerable<SeatResponse>; // Obtener el valor del resultado como una lista de SeatResponse

                // ASSERT
                Assert.NotNull(result);  // Verifica que el resultado no sea nulo
                Assert.Equal(seatResponseFailureCount, result.Count()); // Verifica que el número de respuestas sea el esperado
                Assert.Contains(result, u => !u.Success); // Verifica que al menos una respuesta sea un fallo
                Assert.True(result.All(u => u.Message == errorMessage)); // Verifica que todas las respuestas tengan el mismo mensaje de error
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode); // Verifica que el código de estado sea 500 (Error interno del servidor)
            }

            /// <summary>
            /// Prueba que GetAll devuelva un StatusCode 500 cuando se captura una excepción.
            /// </summary>
            [Fact]
            public async void GetAll_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE
                // Crea una excepción simulada
                var exception = new Exception("An unexpected error occurred while retrieving all entities");
                var errorMessage = exception.Message;

                // Configura el servicio simulado para lanzar la excepción
                var mockService = new Mock<ISeatService>();
                mockService.Setup(u => u.GetAllAsync()).ThrowsAsync(new Exception());

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT
                var result_TaskResult = await controller.GetAll();
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

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
            ///// Prueba asincrónica que verifica que el método GetAll del controlador SeatController devuelve la respuesta correcta.
            ///// </summary>
            //[Fact]
            //public async Task GetAll_ReturnsCorrectResponse()
            //{
            //    // ARRANGE
            //    // Crear una instancia de CoWorkingContext utilizando una base de datos en memoria
            //    var context = TestContextFactory.CreateContext();

            //    // Agregar algunos datos de ejemplo a la base de datos en memoria
            //    var seats = new List<Seat>
            //    {
            //        new Seat { Id = Guid.NewGuid(), Name = "Seat1", LastName = "LastName1", Email = "seat1@example.com", Password = "password1" },
            //        new Seat { Id = Guid.NewGuid(), Name = "Seat2", LastName = "LastName2", Email = "seat2@example.com", Password = "password2" },
            //        new Seat { Id = Guid.NewGuid(), Name = "Seat3", LastName = "LastName3", Email = "seat3@example.com", Password = "password3" }
            //    };
            //    await context.Seats.AddRangeAsync(seats);
            //    await context.SaveChangesAsync();

            //    // Mock del servicio ISeatService
            //    var seatResponses = new List<SeatResponse>
            //    {
            //        new SeatResponse { Name = "Seat1", LastName = "LastName1", Email = "seat1@example.com" },
            //        new SeatResponse { Name = "Seat2", LastName = "LastName2", Email = "seat2@example.com" },
            //        new SeatResponse { Name = "Seat3", LastName = "LastName3", Email = "seat3@example.com" }
            //    };

            //    var seatServiceMock = new Mock<ISeatService>();
            //    // Configurar el comportamiento esperado del servicio ISeatService
            //    seatServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(seatResponses); // Devolver una lista de asientos de prueba

            //    // Crear una instancia del controlador SeatController utilizando el servicio ISeatService mockeado
            //    var controller = new SeatController(seatServiceMock.Object, Mock.Of<ILogger<ControllerGeneric<ISeatService, Seat, SeatResponse>>>());

            //    // ACT
            //    // Llamar al método GetAll del controlador
            //    var result = await controller.GetAll();

            //    // ASSERT
            //    // Verificar que el resultado sea del tipo OkObjectResult
            //    Assert.IsType<OkObjectResult>(result.Result);

            //    // Obtener los datos devueltos por el controlador
            //    var okResult = result.Result as OkObjectResult;
            //    var seatsResult = okResult?.Value as IEnumerable<SeatResponse>;

            //    // Verificar que la cantidad de asientos devueltos sea la esperada
            //    Assert.NotNull(seatsResult);
            //    Assert.Equal(seatResponses.Count, ((List<SeatResponse>)seatsResult).Count);
            //}
        }

        public class GetById
        {
            /// <summary>
            /// Verifica que el método GetById del controlador SeatController devuelva un código de estado 200 en caso de una respuesta exitosa.
            /// </summary>
            [Fact]
            public async void GetById_Returns_StatusCode200_When_SuccessfulResponse()
            {
                // ARRANGE
                // Se crea un asiento existente simulado
                var existingSeat = new SeatResponse
                {
                    Success = true,
                    Name = "Q-1",
                    IsBlocked = false,
                    Description = "This seat is located near the window."
                };

                // Se configura el mock de ISeatService para que devuelva el asiento existente simulado
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingSeat);

                // Se crea un mock de ILogger para el controlador SeatController
                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                // Se crea una instancia del controlador SeatController con los mocks configurados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT
                // Se llama al método GetById del controlador y se obtiene el resultado
                var result_TaskResult = await controller.GetById(Guid.NewGuid());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as OkObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

                // ASSERT
                // Se verifica que el resultado no sea nulo, sea exitoso, coincida con el asiento existente simulado y tenga el código de estado 200
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.Equal(existingSeat, result);
                Assert.Equal(StatusCodes.Status200OK, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba que GetById devuelva un StatusCode 500 cuando la respuesta presenta una falla.
            /// </summary>
            [Fact]
            public async void GetById_Returns_StatusCode500_When_FailureResponse()
            {
                // ARRANGE
                // Mensaje de error esperado cuando la entidad no se encuentra
                var errorMessage = "Entity not found";

                // Simula una respuesta de asiento con indicación de fallo
                SeatResponse seat = new SeatResponse { Success = false };

                // Configura el servicio simulado para devolver la respuesta de asiento creada anteriormente
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(seat);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT
                var result_TaskResult = await controller.GetById(Guid.NewGuid());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as NotFoundObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que el resultado indique un fallo
                Assert.False(result.Success);
                // Verifica que el resultado sea igual al asiento creado anteriormente
                Assert.Equal(seat, result);
                // Verifica que el mensaje del error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verifica que el StatusCode sea 404 (Not Found)
                Assert.Equal(StatusCodes.Status404NotFound, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba que GetById devuelva un StatusCode 500 cuando se produce una excepción.
            /// </summary>
            [Fact]
            public async void GetById_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE
                // Mensaje de error esperado cuando se produce una excepción
                var errorMessage = "Error retrieving entity by ID";

                // Simula una excepción al obtener una entidad por su ID
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new Exception());

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT
                var result_TaskResult = await controller.GetById(Guid.NewGuid());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

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
            [InlineData("Q-1", true, "This seat is located near the window.")]
            [InlineData("M-2", false, "This seat is reserved for VIP guests.")]
            public async void Create_Returns_StatusCode201_When_SuccessfulResponse(string name, bool isBlcoked, string? description)
            {
                // ARRANGE
                // Crea una solicitud de asiento con los datos proporcionados
                var seat = new SeatRequest
                {
                    Name = name,
                    IsBlocked = isBlcoked,
                    Description = description
                };

                // Crea una respuesta de asiento exitosa con los mismos datos de la solicitud
                var seatResponse = new SeatResponse
                {
                    Success = true,
                    Name = seat.Name,
                    IsBlocked = seat.IsBlocked,
                    Description = seat.Description
                };

                // Configura el servicio simulado para que devuelva la respuesta de asiento creada
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.CreateAsync(It.IsAny<SeatRequest>())).ReturnsAsync(seatResponse);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método Create con la solicitud de asiento y obtiene el resultado
                var result_TaskResult = await controller.Create(seat);
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que la operación haya tenido éxito
                Assert.True(result.Success);
                // Verifica que la respuesta sea igual a la esperada
                Assert.Equal(seatResponse, result);
                // Verifica que el StatusCode sea 201 (Created)
                Assert.Equal(StatusCodes.Status201Created, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba que Create devuelva un StatusCode 400 (BadRequest) en una respuesta de fallo.
            /// </summary>
            [Fact]
            public async void Create_Returns_StatusCode400_When_FailureResponse()
            {
                // ARRANGE
                // Mensaje de error esperado
                var errorMessage = "Error occurred while creating the entity.";

                // Crea una respuesta de asiento con fallo
                var seatResponse = new SeatResponse { Success = false };

                // Configura el servicio simulado para que devuelva la respuesta de asiento con fallo
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.CreateAsync(It.IsAny<SeatRequest>())).ReturnsAsync(seatResponse);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método Create con una solicitud de asiento vacía y obtiene el resultado
                var result_TaskResult = await controller.Create(new SeatRequest());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que la operación haya fallado
                Assert.False(result.Success);
                // Verifica que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verifica que la respuesta sea igual a la esperada
                Assert.Equal(seatResponse, result);
                // Verifica que el StatusCode sea 400 (BadRequest)
                Assert.Equal(StatusCodes.Status400BadRequest, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba que Create devuelva un StatusCode 500 (InternalServerError) cuando se captura una excepción.
            /// </summary>
            [Fact]
            public async void Create_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE
                // Crea una excepción simulada
                var exception = new Exception("An unexpected error occurred while creating the entity.");
                // Mensaje de error esperado
                var errorMessage = exception.Message;

                // Configura el servicio simulado para que lance una excepción al llamar a CreateAsync
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.CreateAsync(It.IsAny<SeatRequest>())).ThrowsAsync(exception);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método Create con una solicitud de asiento vacía y obtiene el resultado
                var result_TaskResult = await controller.Create(new SeatRequest());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

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
            [InlineData("Q-1", true, "This seat is located near the window.")]
            [InlineData("M-2", false, "This seat is reserved for VIP guests.")]
            public async void Update_Returns_StatusCode200_When_SuccessfulResponse(string name, bool isBlcoked, string? description)
            {
                // ARRANGE
                // Crea una solicitud de asiento con los datos proporcionados en los argumentos de la teoría
                var seatRequest = new SeatRequest
                {
                    Name = name,
                    IsBlocked = isBlcoked,
                    Description = description
                };

                // Crea una respuesta de asiento simulada con los mismos datos de la solicitud
                var seatResponse = new SeatResponse
                {
                    Success = true,
                    Name = seatRequest.Name,
                    IsBlocked = seatRequest.IsBlocked,
                    Description = seatRequest.Description
                };

                // Configura el servicio simulado para que devuelva la respuesta de asiento simulada al llamar a UpdateAsync
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.UpdateAsync(It.IsAny<Guid>(), It.IsAny<SeatRequest>())).ReturnsAsync(seatResponse);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método Update con un ID de asiento generado aleatoriamente y la solicitud de asiento creada, y obtiene el resultado
                var result_TaskResult = await controller.Update(Guid.NewGuid(), seatRequest);
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as OkObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que la operación se haya realizado correctamente
                Assert.True(result.Success);
                // Verifica que la respuesta sea igual a la respuesta esperada
                Assert.Equal(seatResponse, result);
                // Verifica que el StatusCode sea 200 (OK)
                Assert.Equal(StatusCodes.Status200OK, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba que Update devuelva un StatusCode 400 (BadRequest) cuando la actualización no se realiza correctamente.
            /// </summary>
            [Theory]
            [InlineData("Q-1", true, "This seat is located near the window.")]
            [InlineData("M-2", false, "This seat is reserved for VIP guests.")]
            public async void Update_Returns_StatusCode400_When_FailureResponse(string name, bool isBlcoked, string? description)
            {
                // ARRANGE
                // Mensaje de error esperado en caso de falla
                var errorMessage = "Error occurred while updating the entity.";

                // Crea una solicitud de asiento con los datos proporcionados en los argumentos de la teoría
                var seatRequest = new SeatRequest
                {
                    Name = name,
                    IsBlocked = isBlcoked,
                    Description = description
                };

                // Crea una respuesta de asiento simulada con los mismos datos de la solicitud y establece Success en false para indicar una falla
                var seatResponse = new SeatResponse
                {
                    Success = false,
                    Name = seatRequest.Name,
                    IsBlocked = seatRequest.IsBlocked,
                    Description = seatRequest.Description
                };

                // Configura el servicio simulado para que devuelva la respuesta de asiento simulada al llamar a UpdateAsync
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.UpdateAsync(It.IsAny<Guid>(), It.IsAny<SeatRequest>())).ReturnsAsync(seatResponse);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método Update con un ID de asiento generado aleatoriamente y la solicitud de asiento creada, y obtiene el resultado
                var result_TaskResult = await controller.Update(Guid.NewGuid(), seatRequest);
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as BadRequestObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que la operación haya fallado
                Assert.False(result.Success);
                // Verifica que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verifica que la respuesta sea igual a la respuesta esperada
                Assert.Equal(seatResponse, result);
                // Verifica que el StatusCode sea 400 (BadRequest)
                Assert.Equal(StatusCodes.Status400BadRequest, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba que Update devuelva un StatusCode 500 (InternalServerError) cuando se produce una excepción.
            /// </summary>
            [Fact]
            public async void Update_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE
                // Mensaje de error esperado en caso de excepción
                var errorMessage = "An unexpected error occurred while updating the entity.";

                // Simula una excepción
                var exception = new Exception(errorMessage);

                // Configura el servicio simulado para que lance una excepción al llamar a UpdateAsync
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.UpdateAsync(It.IsAny<Guid>(), It.IsAny<SeatRequest>())).ThrowsAsync(exception);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método Update con un ID de asiento generado aleatoriamente y una solicitud de asiento vacía, y obtiene el resultado
                var result_TaskResult = await controller.Update(Guid.NewGuid(), new SeatRequest());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

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
            public async void Delete_Returns_StatusCode204_When_SuccessfulResponse()
            {
                // ARRANGE
                // Crea una respuesta de asiento exitosa
                var seatResponse = new SeatResponse { Success = true };

                // Configura el servicio simulado para que devuelva la respuesta de asiento al llamar a DeleteAsync
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(seatResponse);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método Delete con un ID de asiento generado aleatoriamente y obtiene el resultado
                var result_TaskResult = await controller.Delete(Guid.NewGuid());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que la operación haya sido exitosa
                Assert.True(result.Success);
                // Verifica que la respuesta sea la esperada
                Assert.Equal(seatResponse, result);
                // Verifica que el StatusCode sea 204 (NoContent)
                Assert.Equal(StatusCodes.Status204NoContent, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba que Delete devuelva un StatusCode 400 (BadRequest) en caso de una respuesta fallida.
            /// </summary>
            [Fact]
            public async void Delete_Returns_StatusCode400_When_FailureResponse()
            {
                // ARRANGE
                // Mensaje de error esperado
                var errorMessage = "Error occurred while deleting the entity.";

                // Crea una respuesta de asiento fallida
                var seatResponse = new SeatResponse { Success = false };

                // Configura el servicio simulado para que devuelva la respuesta de asiento al llamar a DeleteAsync
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(seatResponse);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método Delete con un ID de asiento generado aleatoriamente y obtiene el resultado
                var result_TaskResult = await controller.Delete(Guid.NewGuid());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que la operación haya fallado
                Assert.False(result.Success);
                // Verifica que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verifica que la respuesta sea la esperada
                Assert.Equal(seatResponse, result);
                // Verifica que el StatusCode sea 400 (BadRequest)
                Assert.Equal(StatusCodes.Status400BadRequest, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba que Delete devuelva un StatusCode 500 (InternalServerError) cuando se captura una excepción.
            /// </summary>
            [Fact]
            public async void Delete_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE
                // Mensaje de error esperado
                var errorMessage = "An unexpected error occurred while deleting the entity.";

                // Crea una excepción simulada
                var exception = new Exception(errorMessage);

                // Configura el servicio simulado para que lance una excepción al llamar a DeleteAsync
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.DeleteAsync(It.IsAny<Guid>())).ThrowsAsync(exception);

                // Crea un simulacro de logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                // Crea una instancia del controlador bajo prueba con los mocks configurados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT
                // Llama al método Delete con un ID de asiento generado aleatoriamente y obtiene el resultado
                var result_TaskResult = await controller.Delete(Guid.NewGuid());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

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

        public class GetAvailables
        {
            /// <summary>
            /// Prueba para verificar que se devuelva el código de estado 200 en una respuesta exitosa al obtener todos los asientos disponibles.
            /// </summary>
            [Fact]
            public async void GetAvailables_Returns_StatusCode200_When_SuccessfulResponse()
            {
                // ARRANGE

                // Lista simulada de respuestas de asientos disponibles
                var seats = new List<SeatResponse>
                {
                    new SeatResponse { Success = true, Name = "Q-1", IsBlocked = false, Description = "This seat is located near the window." },
                    new SeatResponse { Success = true, Name = "M-2", IsBlocked = false, Description = "This seat is reserved for VIP guests." },
                    new SeatResponse { Success = true, Name = "M-1", IsBlocked = false, Description = "This seat is in the back row." },
                    new SeatResponse { Success = true, Name = "M-3", IsBlocked = false, Description = "This seat has extra legroom." }
                };

                // Configurar un mock para el servicio de asiento (ISeatService) y establecer su comportamiento para devolver las respuestas simuladas
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.GetAvailablesAsync()).ReturnsAsync(seats);

                // Configurar un mock para el registrador ILogger
                var mockLogger = new Mock<ILogger<SeatController>>();

                // Crear una instancia del controlador de asiento (SeatController) con los mocks configurados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT

                // Llamar al método GetAvailableSeats del controlador y obtener el resultado
                var result_TaskResult = await controller.GetAvailables();
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as IEnumerable<SeatResponse>;

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que todas las respuestas tengan la propiedad Success establecida en true
                Assert.True(result.All(e => e.Success));

                // Verificar que el número de respuestas devueltas sea igual al número de respuestas simuladas
                Assert.Equal(seats.Count, result.Count());

                // Verificar que el código de estado de la respuesta sea 200 (OK)
                Assert.Equal(StatusCodes.Status200OK, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba para verificar que se devuelva el código de estado 404 en una respuesta de fallo al obtener todos los asientos disponibles.
            /// </summary>
            [Fact]
            public async void GetAvailables_Returns_StatusCode404_When_FailureResponse()
            {
                // ARRANGE

                // Lista simulada de respuestas de asientos disponibles
                var seats = new List<SeatResponse>
                {
                    new SeatResponse { Success = true, Name = "Q-1", IsBlocked = false, Description = "This seat is located near the window." },
                    new SeatResponse { Success = false, Name = "M-2", IsBlocked = false, Description = "This seat is reserved for VIP guests." },
                    new SeatResponse { Success = true, Name = "M-1", IsBlocked = false, Description = "This seat is in the back row." },
                    new SeatResponse { Success = true, Name = "M-3", IsBlocked = false, Description = "This seat has extra legroom." }
                };

                // Contador de respuestas incorrectas
                var incorrectSeatsCount = seats.Count(e => !e.Success);

                // Configurar un mock para el servicio de asiento (ISeatService) y establecer su comportamiento para devolver las respuestas simuladas
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.GetAvailablesAsync()).ReturnsAsync(seats);

                // Configurar un mock para el registrador ILogger
                var mockLogger = new Mock<ILogger<SeatController>>();

                // Crear una instancia del controlador de asiento (SeatController) con los mocks configurados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT

                // Llamar al método GetAvailableSeats del controlador y obtener el resultado
                var result_TaskResult = await controller.GetAvailables();
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as IEnumerable<SeatResponse>;

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que no todas las respuestas tengan la propiedad Success establecida en true
                Assert.False(result.All(e => e.Success));

                // Verificar que el número de respuestas incorrectas devueltas sea igual al número de respuestas incorrectas simuladas
                Assert.Equal(incorrectSeatsCount, result.Count());

                // Verificar que el código de estado de la respuesta sea 404 (Not Found)
                Assert.Equal(StatusCodes.Status404NotFound, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba para verificar que se devuelva el código de estado 500 en una respuesta de fallo cuando se produce una excepción al obtener todos los asientos disponibles.
            /// </summary>
            [Fact]
            public async void GetAvailables_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE

                // Crear una excepción simulada
                var exception = new Exception("An unexpected error occurred while getting available seats.");
                var errorMessage = exception.Message;

                // Configurar un mock para el servicio de asiento (ISeatService) y establecer su comportamiento para lanzar una excepción al llamar a GetAvailableSeatsAsync
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.GetAvailablesAsync()).ThrowsAsync(exception);

                // Configurar un mock para el registrador ILogger
                var mockLogger = new Mock<ILogger<SeatController>>();

                // Crear una instancia del controlador de asiento (SeatController) con los mocks configurados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT

                // Llamar al método GetAvailableSeats del controlador y obtener el resultado
                var result_TaskResult = await controller.GetAvailables();
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);

                // Verificar que el mensaje de la respuesta coincida con el mensaje de la excepción simulada
                Assert.Equal(errorMessage, result.Message);

                // Verificar que el código de estado de la respuesta sea 500 (Internal Server Error)
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }
        }

        public class GetByName
        {
            /// <summary>
            /// Prueba para verificar que se devuelva el código de estado 200 en una respuesta exitosa al buscar un asiento por nombre.
            /// </summary>
            /// <param name="name">El nombre del asiento a buscar.</param>
            [Theory]
            [InlineData("Q-1")]
            [InlineData("M-2")]
            public async void GetByName_Returns_StatusCode200_When_SuccessfulResponse(string name)
            {
                // ARRANGE

                // Crear una respuesta exitosa para el asiento
                var seat = new SeatResponse { Success = true };

                // Configurar un mock para el servicio de asiento (ISeatService) y establecer su comportamiento para devolver la respuesta del asiento cuando se llame a GetSeatByNameAsync
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(seat);

                // Configurar un mock para el registrador ILogger
                var mockLogger = new Mock<ILogger<SeatController>>();

                // Crear una instancia del controlador de asiento (SeatController) con los mocks configurados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT

                // Llamar al método GetSeatByName del controlador y obtener el resultado
                var result_TaskResult = await controller.GetByName(name);
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea true
                Assert.True(result.Success);

                // Verificar que el código de estado de la respuesta sea 200 (OK)
                Assert.Equal(StatusCodes.Status200OK, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba para verificar que se devuelva el código de estado 404 en una respuesta de error al buscar un asiento por nombre.
            /// </summary>
            /// <param name="name">El nombre del asiento a buscar.</param>
            [Theory]
            [InlineData("Q-1")]
            [InlineData("M-2")]
            public async void GetByName_Returns_StatusCode200_When_FailureResponse(string name)
            {
                // ARRANGE

                // Mensaje de error esperado
                var errorMessage = "Entity not found by name";

                // Crear una respuesta de error para el asiento
                var seat = new SeatResponse { Success = false };

                // Configurar un mock para el servicio de asiento (ISeatService) y establecer su comportamiento para devolver la respuesta del asiento cuando se llame a GetSeatByNameAsync
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(seat);

                // Configurar un mock para el registrador ILogger
                var mockLogger = new Mock<ILogger<SeatController>>();

                // Crear una instancia del controlador de asiento (SeatController) con los mocks configurados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT

                // Llamar al método GetSeatByName del controlador y obtener el resultado
                var result_TaskResult = await controller.GetByName(name);
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);

                // Verificar que el mensaje de error en la respuesta sea el esperado
                Assert.Equal(errorMessage, result.Message);

                // Verificar que el código de estado de la respuesta sea 404 (NotFound)
                Assert.Equal(StatusCodes.Status404NotFound, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba para verificar que se devuelva el código de estado 500 en caso de que ocurra una excepción al buscar un asiento por nombre.
            /// </summary>
            [Fact]
            public async void GetByName_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE

                // Mensaje de error esperado
                var errorMessage = "An unexpected error occurred while getting seat by name.";

                // Configurar un mock para el servicio de asiento (ISeatService) y establecer su comportamiento para lanzar una excepción al llamar a GetSeatByNameAsync
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.GetByNameAsync(It.IsAny<string>())).ThrowsAsync(new Exception());

                // Configurar un mock para el registrador ILogger
                var mockLogger = new Mock<ILogger<SeatController>>();

                // Crear una instancia del controlador de asiento (SeatController) con los mocks configurados
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT

                // Llamar al método GetSeatByName del controlador y obtener el resultado
                var result_TaskResult = await controller.GetByName(It.IsAny<string>());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);

                // Verificar que el mensaje de error en la respuesta sea el esperado
                Assert.Equal(errorMessage, result.Message);

                // Verificar que el código de estado de la respuesta sea 500 (InternalServerError)
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }
        }
    }
}

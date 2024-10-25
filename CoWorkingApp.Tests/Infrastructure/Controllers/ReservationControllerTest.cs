using CoWorkingApp.API.Infrastructure.Presentation.Controllers;
using CoWorkingApp.Core.Application.Contracts.Services;
using CoWorkingApp.Core.Domain.DTOs;
using CoWorkingApp.Tests.ClassData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoWorkingApp.Tests.Infrastructure.Controllers
{
    public class ReservationControllerTest
    {
        public class Constructor
        {
            /// <summary>
            /// Prueba unitaria para verificar que el constructor de ReservationController se inicializa correctamente cuando los parámetros no son nulos.
            /// </summary>
            [Fact]
            public void Constructor_When_ParametersAreNotNull()
            {
                // ARRANGE 
                // Crear mocks para IReservationService y ILogger
                var mockService = new Mock<IReservationService>();
                var mockLogger = new Mock<ILogger<ControllerGeneric<IReservationService, ReservationRequest, ReservationResponse>>>();

                // ACT
                // Crear una instancia de ReservationController con los mocks
                var result = new ReservationController(mockService.Object, mockLogger.Object);

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
            }

            /// <summary>
            /// Prueba unitaria para verificar que el constructor de ReservationController lanza una excepción cuando el logger es nulo.
            /// </summary>
            [Fact]
            public void Constructor_Throws_ArgumentNullException_When_NullLogger()
            {
                // ARRANGE 
                // Crear un mock para IReservationService
                var mockService = new Mock<IReservationService>();
                // Declarar el logger como nulo
                ILogger<ControllerGeneric<IReservationService, ReservationRequest, ReservationResponse>>? logger = null;

                // ACT
                // Intentar crear una instancia de ReservationController con el logger nulo
                var result = () => new ReservationController(mockService.Object, logger);

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que crear una instancia con un logger nulo lance una excepción de tipo ArgumentNullException
                Assert.Throws<ArgumentNullException>(result);
            }

            /// <summary>
            /// Prueba unitaria para verificar que el constructor de ReservationController lanza una excepción cuando el servicio es nulo.
            /// </summary>
            [Fact]
            public void Constructor_Throws_ArgumentNullException_When_NullService()
            {
                // ARRANGE 
                // Declarar el servicio como nulo
                IReservationService? service = null;
                // Crear un mock para ILogger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IReservationService, ReservationRequest, ReservationResponse>>>();

                // ACT
                // Intentar crear una instancia de ReservationController con el servicio nulo
                var result = () => new ReservationController(service, mockLogger.Object);

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que crear una instancia con un servicio nulo lance una excepción de tipo ArgumentNullException
                Assert.Throws<ArgumentNullException>(result);
            }
        }

        public class GetAll
        {
            /// <summary>
            /// Prueba unitaria para verificar que GetAll devuelve el código de estado 200 en una respuesta exitosa.
            /// </summary>
            [Fact]
            public async Task GetAll_Returns_StatusCode200_When_SuccessfulResponse()
            {
                // ARRANGE

                // Lista de respuestas de reservas
                var reservationsResponses = new List<ReservationResponse>
                {
                    new ReservationResponse { Success = true },
                    new ReservationResponse { Success = true }
                };

                // Mock del servicio de reservas
                var mockService = new Mock<IReservationService>();
                mockService.Setup(u => u.GetAllAsync()).ReturnsAsync(reservationsResponses);

                // Mock del registrador
                var mockLogger = new Mock<ILogger<ControllerGeneric<IReservationService, ReservationRequest, ReservationResponse>>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamada al método GetAll del controlador
                var result_TaskResult = await controller.GetAll();
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as OkObjectResult;
                var result = result_ObjectResult?.Value as IEnumerable<ReservationResponse>;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que el número de respuestas devueltas sea igual al número de respuestas esperadas
                Assert.Equal(reservationsResponses.Count, result.Count());
                // Verificar que todas las respuestas sean exitosas
                Assert.True(result.All(u => u.Success));
                // Verificar que el código de estado sea 200 (OK)
                Assert.Equal(StatusCodes.Status200OK, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba unitaria para verificar que GetAll maneje correctamente el caso en que al menos una respuesta sea un fallo.
            /// </summary>
            [Theory]
            [ClassData(typeof(InvalidSuccessResponsesClassData))]
            public async Task GetAll_Returns_StatusCode500_When_FailureResponse(bool success1, bool success2, bool success3)
            {
                // ARRANGE

                // Mensaje de error esperado
                var errorMessage = "Error occurred while retrieving entity";

                // Lista de respuestas de reservas con diferentes combinaciones de éxito/fallo
                var reservationsResponses = new List<ReservationResponse>
                {
                    new ReservationResponse { Success = success1 },
                    new ReservationResponse { Success = success2 },
                    new ReservationResponse { Success = success3 },
                };

                // Contar la cantidad de respuestas de reservas que no tienen éxito
                var reservationResponseFailureCount = reservationsResponses.Count(e => !e.Success);

                // Mock del servicio de reservas
                var mockService = new Mock<IReservationService>();
                mockService.Setup(u => u.GetAllAsync()).ReturnsAsync(reservationsResponses);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IReservationService, ReservationRequest, ReservationResponse>>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                var result_TaskResult = await controller.GetAll();
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as IEnumerable<ReservationResponse>;

                // ASSERT
                Assert.NotNull(result);
                Assert.Equal(reservationResponseFailureCount, result.Count());
                Assert.Contains(result, u => !u.Success);
                Assert.True(result.All(u => u.Message == errorMessage));
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba unitaria para verificar que GetAll maneje correctamente el caso en que se capture una excepción.
            /// </summary>
            [Fact]
            public async Task GetAll_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE
                // Crear una excepción simulada
                var exception = new Exception("An unexpected error occurred while retrieving all entities");
                var errorMessage = exception.Message;

                // Mock del servicio de reservas que arroja una excepción al llamar a GetAllAsync
                var mockService = new Mock<IReservationService>();
                mockService.Setup(u => u.GetAllAsync()).ThrowsAsync(exception);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IReservationService, ReservationRequest, ReservationResponse>>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método GetAll del controlador
                var result_TaskResult = await controller.GetAll();
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as ReservationResponse;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que la operación no fue exitosa
                Assert.False(result.Success);
                // Verificar que solo haya un mensaje de error
                Assert.Single(result.Errors);
                // Verificar que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verificar que el código de estado sea 500 (Error interno del servidor)
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }
        }

        public class GetById
        {
            /// <summary>
            /// Prueba unitaria para verificar que GetById devuelva el código de estado 200 en una respuesta exitosa.
            /// </summary>
            [Fact]
            public async Task GetById_Returns_StatusCode200_When_SuccessfulResponse()
            {
                // ARRANGE
                // Reserva existente simulada
                var existingReservation = new ReservationResponse
                {
                    Success = true,
                };

                // Mock del servicio de reservas que devuelve la reserva existente al llamar a GetByIdAsync
                var mockService = new Mock<IReservationService>();
                mockService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingReservation);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IReservationService, ReservationRequest, ReservationResponse>>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método GetById del controlador con un ID de reserva
                var result_TaskResult = await controller.GetById(Guid.NewGuid());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as OkObjectResult;
                var result = result_ObjectResult?.Value as ReservationResponse;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que la operación fue exitosa
                Assert.True(result.Success);
                // Verificar que la reserva devuelta sea la misma que la simulada
                Assert.Equal(existingReservation, result);
                // Verificar que el código de estado sea 200 (OK)
                Assert.Equal(StatusCodes.Status200OK, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba unitaria para verificar que GetById devuelva el código de estado 500 en una respuesta de fallo.
            /// </summary>
            [Fact]
            public async Task GetById_Returns_StatusCode500_When_FailureResponse()
            {
                // ARRANGE
                // Mensaje de error esperado
                var errorMessage = "Entity not found";

                // Simular una respuesta de reserva que indica un fallo
                var reservation = new ReservationResponse { Success = false };

                // Mock del servicio de reservas que devuelve la reserva al llamar a GetByIdAsync
                var mockService = new Mock<IReservationService>();
                mockService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(reservation);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IReservationService, ReservationRequest, ReservationResponse>>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método GetById del controlador con un ID de reserva
                var result_TaskResult = await controller.GetById(Guid.NewGuid());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as NotFoundObjectResult;
                var result = result_ObjectResult?.Value as ReservationResponse;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que la operación fue un fallo
                Assert.False(result.Success);
                // Verificar que la reserva devuelta sea la misma que la simulada
                Assert.Equal(reservation, result);
                // Verificar que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verificar que el código de estado sea 404 (No encontrado)
                Assert.Equal(StatusCodes.Status404NotFound, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba unitaria para verificar que GetById devuelve el código de estado 500 en caso de excepción.
            /// </summary>
            [Fact]
            public async Task GetById_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE
                // Mensaje de error esperado
                var errorMessage = "Error retrieving entity by ID";

                // Mock del servicio de reservas que arroja una excepción al llamar a GetByIdAsync
                var mockService = new Mock<IReservationService>();
                mockService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new Exception());

                // Mock del logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IReservationService, ReservationRequest, ReservationResponse>>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método GetById del controlador con un ID de reserva
                var result_TaskResult = await controller.GetById(Guid.NewGuid());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as ReservationResponse;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que la operación fue un fallo
                Assert.False(result.Success);
                // Verificar que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verificar que el código de estado sea 500 (Error interno del servidor)
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }
        }

        public class Create
        {
            /// <summary>
            /// Prueba unitaria para verificar que el método Create devuelve el código de estado 201 en caso de respuesta exitosa.
            /// </summary>
            [Fact]
            public async Task Create_Returns_StatusCode201_When_SuccessfulResponse()
            {
                // ARRANGE
                // Crear una solicitud de reserva
                var reservation = new ReservationRequest();

                // Crear una respuesta de reserva exitosa
                var reservationResponse = new ReservationResponse { Success = true };

                // Mock del servicio de reservas que devuelve la respuesta exitosa al llamar a CreateAsync
                var mockService = new Mock<IReservationService>();
                mockService.Setup(s => s.CreateAsync(It.IsAny<ReservationRequest>())).ReturnsAsync(reservationResponse);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IReservationService, ReservationRequest, ReservationResponse>>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método Create del controlador con la solicitud de reserva
                var result_TaskResult = await controller.Create(reservation);
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as ReservationResponse;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que la operación fue exitosa
                Assert.True(result.Success);
                // Verificar que la respuesta sea igual a la respuesta esperada
                Assert.Equal(reservationResponse, result);
                // Verificar que el código de estado sea 201 (Creado)
                Assert.Equal(StatusCodes.Status201Created, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba unitaria para verificar que el método Create devuelve el código de estado 400 en caso de respuesta de error.
            /// </summary>
            [Fact]
            public async Task Create_Returns_StatusCode400_When_FailureResponse()
            {
                // ARRANGE
                // Mensaje de error esperado
                var errorMessage = "Error occurred while creating the entity.";

                // Respuesta de reserva de fallo
                var reservationResponse = new ReservationResponse { Success = false };

                // Mock del servicio de reservas que devuelve la respuesta de fallo al llamar a CreateAsync
                var mockService = new Mock<IReservationService>();
                mockService.Setup(s => s.CreateAsync(It.IsAny<ReservationRequest>())).ReturnsAsync(reservationResponse);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IReservationService, ReservationRequest, ReservationResponse>>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método Create del controlador con una solicitud de reserva
                var result_TaskResult = await controller.Create(new ReservationRequest());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as ReservationResponse;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que la operación fue un fallo
                Assert.False(result.Success);
                // Verificar que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verificar que la respuesta sea igual a la respuesta esperada
                Assert.Equal(reservationResponse, result);
                // Verificar que el código de estado sea 400 (Solicitud incorrecta)
                Assert.Equal(StatusCodes.Status400BadRequest, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba unitaria para verificar que el método Create devuelve el código de estado 500 en caso de excepción capturada.
            /// </summary>
            [Fact]
            public async Task Create_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE
                // Mensaje de error esperado
                var errorMessage = "An unexpected error occurred while creating the entity.";

                // Excepción simulada
                var exception = new Exception(errorMessage);

                // Mock del servicio de reservas que lanza una excepción al llamar a CreateAsync
                var mockService = new Mock<IReservationService>();
                mockService.Setup(s => s.CreateAsync(It.IsAny<ReservationRequest>())).ThrowsAsync(exception);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IReservationService, ReservationRequest, ReservationResponse>>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método Create del controlador con una solicitud de reserva
                var result_TaskResult = await controller.Create(new ReservationRequest());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as ReservationResponse;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que la operación fue un fallo
                Assert.False(result.Success);
                // Verificar que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verificar que el código de estado sea 500 (Error interno del servidor)
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }
        }

        public class Update
        {
            /// <summary>
            /// Prueba unitaria para verificar que el método Update devuelve el código de estado 200 en caso de una respuesta exitosa.
            /// </summary>
            [Fact]
            public async Task Update_Returns_StatusCode200_When_SuccessfulResponse()
            {
                // ARRANGE
                // Solicitud de asiento
                var seatRequest = new SeatRequest();

                // Respuesta de asiento exitosa
                var seatResponse = new SeatResponse { Success = true };

                // Mock del servicio de asientos que devuelve la respuesta de asiento al llamar a UpdateAsync
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.UpdateAsync(It.IsAny<Guid>(), It.IsAny<SeatRequest>())).ReturnsAsync(seatResponse);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                // Controlador de asientos
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método Update del controlador con un ID de asiento y una solicitud de asiento
                var result_TaskResult = await controller.Update(Guid.NewGuid(), seatRequest);
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as OkObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que la operación fue un éxito
                Assert.True(result.Success);
                // Verificar que la respuesta sea igual a la respuesta esperada
                Assert.Equal(seatResponse, result);
                // Verificar que el código de estado sea 200 (OK)
                Assert.Equal(StatusCodes.Status200OK, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba unitaria para verificar que el método Update devuelve el código de estado 400 en caso de una respuesta de fallo.
            /// </summary>
            [Fact]
            public async Task Update_Returns_StatusCode400_When_FailureResponse()
            {
                // ARRANGE
                // Mensaje de error esperado
                var errorMessage = "Error occurred while updating the entity.";

                // Solicitud de asiento
                var seatRequest = new SeatRequest();

                // Respuesta de asiento de fallo
                var seatResponse = new SeatResponse { Success = false };

                // Mock del servicio de asientos que devuelve la respuesta de asiento al llamar a UpdateAsync
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.UpdateAsync(It.IsAny<Guid>(), It.IsAny<SeatRequest>())).ReturnsAsync(seatResponse);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                // Controlador de asientos
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método Update del controlador con un ID de asiento y una solicitud de asiento
                var result_TaskResult = await controller.Update(Guid.NewGuid(), seatRequest);
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as BadRequestObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que la operación fue un fallo
                Assert.False(result.Success);
                // Verificar que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verificar que la respuesta sea igual a la respuesta esperada
                Assert.Equal(seatResponse, result);
                // Verificar que el código de estado sea 400 (BadRequest)
                Assert.Equal(StatusCodes.Status400BadRequest, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba unitaria para verificar que el método Update devuelve el código de estado 500 en caso de una excepción.
            /// </summary>
            [Fact]
            public async Task Update_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE
                // Mensaje de error esperado
                var errorMessage = "An unexpected error occurred while updating the entity.";

                // Excepción simulada
                var exception = new Exception(errorMessage);

                // Mock del servicio de asientos que lanza una excepción al llamar a UpdateAsync
                var mockService = new Mock<ISeatService>();
                mockService.Setup(s => s.UpdateAsync(It.IsAny<Guid>(), It.IsAny<SeatRequest>())).ThrowsAsync(exception);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<ISeatService, SeatRequest, SeatResponse>>>();

                // Controlador de asientos
                var controller = new SeatController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método Update del controlador con un ID de asiento y una solicitud de asiento
                var result_TaskResult = await controller.Update(Guid.NewGuid(), new SeatRequest());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as SeatResponse;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que la operación fue un fallo
                Assert.False(result.Success);
                // Verificar que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verificar que el código de estado sea 500 (InternalServerError)
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }
        }

        public class Delete
        {
            /// <summary>
            /// Prueba unitaria para verificar que el método Delete devuelve el código de estado 204 en caso de una respuesta exitosa.
            /// </summary>
            [Fact]
            public async Task Delete_Returns_StatusCode204_When_SuccessfulResponse()
            {
                // ARRANGE
                // Respuesta de éxito esperada
                var reservationResponse = new ReservationResponse { Success = true };

                // Mock del servicio de reservas que devuelve la respuesta de éxito al llamar a DeleteAsync
                var mockService = new Mock<IReservationService>();
                mockService.Setup(s => s.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(reservationResponse);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IReservationService, ReservationRequest, ReservationResponse>>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método Delete del controlador con un ID de reserva
                var result_TaskResult = await controller.Delete(Guid.NewGuid());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as ReservationResponse;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que la operación fue un éxito
                Assert.True(result.Success);
                // Verificar que la respuesta es igual a la respuesta esperada
                Assert.Equal(reservationResponse, result);
                // Verificar que el código de estado sea 204 (NoContent)
                Assert.Equal(StatusCodes.Status204NoContent, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba unitaria para verificar que el método Delete devuelve el código de estado 400 en caso de una respuesta de error.
            /// </summary>
            [Fact]
            public async Task Delete_Returns_StatusCode400_When_FailureResponse()
            {
                // ARRANGE
                // Mensaje de error esperado
                var errorMessage = "Error occurred while deleting the entity.";

                // Respuesta de error esperada
                var reservationResponse = new ReservationResponse { Success = false };

                // Mock del servicio de reservas que devuelve la respuesta de error al llamar a DeleteAsync
                var mockService = new Mock<IReservationService>();
                mockService.Setup(s => s.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(reservationResponse);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IReservationService, ReservationRequest, ReservationResponse>>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método Delete del controlador con un ID de reserva
                var result_TaskResult = await controller.Delete(Guid.NewGuid());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as ReservationResponse;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que la operación fue un fallo
                Assert.False(result.Success);
                // Verificar que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verificar que la respuesta es igual a la respuesta esperada
                Assert.Equal(reservationResponse, result);
                // Verificar que el código de estado sea 400 (BadRequest)
                Assert.Equal(StatusCodes.Status400BadRequest, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba unitaria para verificar que el método Delete devuelve el código de estado 500 en caso de excepción.
            /// </summary>
            [Fact]
            public async Task Delete_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE
                // Mensaje de error esperado
                var errorMessage = "An unexpected error occurred while deleting the entity.";

                // Excepción simulada
                var exception = new Exception(errorMessage);

                // Mock del servicio de reservas que lanza una excepción al llamar a DeleteAsync
                var mockService = new Mock<IReservationService>();
                mockService.Setup(s => s.DeleteAsync(It.IsAny<Guid>())).ThrowsAsync(exception);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ControllerGeneric<IReservationService, ReservationRequest, ReservationResponse>>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método Delete del controlador con un ID de reserva
                var result_TaskResult = await controller.Delete(Guid.NewGuid());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as ReservationResponse;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que la operación fue un fallo
                Assert.False(result.Success);
                // Verificar que el mensaje de error sea el esperado
                Assert.Equal(errorMessage, result.Message);
                // Verificar que el código de estado sea 500 (InternalServerError)
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }
        }

        public class GetByDate
        {
            /// <summary>
            /// Prueba unitaria para verificar que el método GetReservationsByDate devuelve el código de estado 200 en caso de respuesta exitosa.
            /// </summary>
            [Fact]
            public async Task GetByDate_Returns_StatusCode200_When_SuccessfulResponse()
            {
                // ARRANGE
                // Lista simulada de reservas
                var reservations = new List<ReservationResponse>
                {
                    new ReservationResponse { Success = true },
                    new ReservationResponse { Success = true },
                    new ReservationResponse { Success = true },
                    new ReservationResponse { Success = true }
                };

                // Mock del servicio de reservas que devuelve las reservas por fecha
                var mockService = new Mock<IReservationService>();
                mockService.Setup(s => s.GetByDateAsync(It.IsAny<DateTime>())).ReturnsAsync(reservations);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ReservationController>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método GetReservationsByDate del controlador con una fecha de reserva
                var result_TaskResult = await controller.GetByDate(It.IsAny<DateTime>());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as IEnumerable<ReservationResponse>;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que todas las reservas en el resultado tengan éxito
                Assert.True(result.All(e => e.Success));
                // Verificar que el número de reservas en el resultado sea igual al número esperado de reservas
                Assert.Equal(reservations.Count, result.Count());
                // Verificar que el código de estado sea 200 (OK)
                Assert.Equal(StatusCodes.Status200OK, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba unitaria para verificar que el método GetReservationsByDate devuelve el código de estado 404 en caso de respuesta fallida.
            /// </summary>
            [Fact]
            public async void GetByDate_Returns_StatusCode404_When_FailureResponse()
            {
                // ARRANGE
                // Lista simulada de reservas con al menos una reserva fallida
                var reservations = new List<ReservationResponse>
                {
                    new ReservationResponse { Success = true },
                    new ReservationResponse { Success = false },
                    new ReservationResponse { Success = true },
                    new ReservationResponse { Success = true }
                };

                // Contar el número de reservas incorrectas en la lista
                var incorrectReservationsCount = reservations.Count(e => !e.Success);

                // Mock del servicio de reservas que devuelve las reservas por fecha
                var mockService = new Mock<IReservationService>();
                mockService.Setup(s => s.GetByDateAsync(It.IsAny<DateTime>())).ReturnsAsync(reservations);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ReservationController>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método GetReservationsByDate del controlador con una fecha de reserva
                var result_TaskResult = await controller.GetByDate(It.IsAny<DateTime>());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as IEnumerable<ReservationResponse>;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que no todas las reservas en el resultado tengan éxito
                Assert.False(result.All(e => e.Success));
                // Verificar que el número de reservas incorrectas en el resultado sea igual al número esperado de reservas incorrectas
                Assert.Equal(incorrectReservationsCount, result.Count());
                // Verificar que el código de estado sea 404 (No encontrado)
                Assert.Equal(StatusCodes.Status404NotFound, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba unitaria para verificar que el método GetReservationsByDate devuelve el código de estado 500 en caso de excepción.
            /// </summary>
            [Fact]
            public async void GetByDate_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE
                // Mensaje de error de la excepción
                var errorMessage = "An unexpected error occurred while getting reservations.";

                // Excepción simulada
                var exception = new Exception(errorMessage);

                // Mock del servicio de reservas que lanza una excepción al obtener reservas por fecha
                var mockService = new Mock<IReservationService>();
                mockService.Setup(s => s.GetByDateAsync(It.IsAny<DateTime>())).ThrowsAsync(exception);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ReservationController>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método GetReservationsByDate del controlador con una fecha de reserva
                var result_TaskResult = await controller.GetByDate(It.IsAny<DateTime>());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as ReservationResponse;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que la operación no fue exitosa
                Assert.False(result.Success);
                // Verificar que el mensaje de error en el resultado coincida con el mensaje de la excepción
                Assert.Equal(errorMessage, result.Message);
                // Verificar que el código de estado sea 500 (Error interno del servidor)
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }
        }

        public class GetByUserId
        {
            /// <summary>
            /// Prueba unitaria para verificar que el método GetReservationsByUserId devuelve el código de estado 200 en caso de respuesta exitosa.
            /// </summary>
            [Fact]
            public async Task GetByUserId_Returns_StatusCode200_When_SuccessfulResponse()
            {
                // ARRANGE
                // Lista simulada de reservas
                var reservations = new List<ReservationResponse>
                {
                    new ReservationResponse { Success = true },
                    new ReservationResponse { Success = true },
                    new ReservationResponse { Success = true },
                    new ReservationResponse { Success = true }
                };

                // Mock del servicio de reservas que devuelve las reservas por identificador de usuario
                var mockService = new Mock<IReservationService>();
                mockService.Setup(s => s.GetByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(reservations);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ReservationController>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método GetReservationsByUserId del controlador con un identificador de usuario
                var result_TaskResult = await controller.GetByUserId(It.IsAny<Guid>());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as IEnumerable<ReservationResponse>;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que todas las reservas en el resultado tengan éxito
                Assert.True(result.All(e => e.Success));
                // Verificar que el número de reservas en el resultado sea igual al número esperado de reservas
                Assert.Equal(reservations.Count, result.Count());
                // Verificar que el código de estado sea 200 (OK)
                Assert.Equal(StatusCodes.Status200OK, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba unitaria para verificar que el método GetReservationsByUserId devuelve el código de estado 404 en caso de respuesta fallida.
            /// </summary>
            [Fact]
            public async void GetByUserId_Returns_StatusCode404_When_FailureResponse()
            {
                // ARRANGE
                // Lista simulada de reservas con al menos una reserva fallida
                var reservations = new List<ReservationResponse>
                {
                    new ReservationResponse { Success = true },
                    new ReservationResponse { Success = false },
                    new ReservationResponse { Success = true },
                    new ReservationResponse { Success = true }
                };

                // Contar el número de reservas incorrectas en la lista
                var incorrectReservationsCount = reservations.Count(e => !e.Success);

                // Mock del servicio de reservas que devuelve las reservas por identificador de usuario
                var mockService = new Mock<IReservationService>();
                mockService.Setup(s => s.GetByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(reservations);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ReservationController>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método GetReservationsByUserId del controlador con un identificador de usuario
                var result_TaskResult = await controller.GetByUserId(It.IsAny<Guid>());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as IEnumerable<ReservationResponse>;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que no todas las reservas en el resultado tengan éxito
                Assert.False(result.All(e => e.Success));
                // Verificar que el número de reservas incorrectas en el resultado sea igual al número esperado de reservas incorrectas
                Assert.Equal(incorrectReservationsCount, result.Count());
                // Verificar que el código de estado sea 404 (No encontrado)
                Assert.Equal(StatusCodes.Status404NotFound, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba unitaria para verificar que el método GetReservationsByUserId devuelve el código de estado 500 en caso de excepción.
            /// </summary>
            [Fact]
            public async void GetByUserId_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE
                // Mensaje de error de la excepción
                var errorMessage = "An unexpected error occurred while getting reservations.";

                // Excepción simulada
                var exception = new Exception(errorMessage);

                // Mock del servicio de reservas que lanza una excepción al obtener reservas por identificador de usuario
                var mockService = new Mock<IReservationService>();
                mockService.Setup(s => s.GetByUserIdAsync(It.IsAny<Guid>())).ThrowsAsync(exception);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ReservationController>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método GetReservationsByUserId del controlador con un identificador de usuario
                var result_TaskResult = await controller.GetByUserId(It.IsAny<Guid>());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as ReservationResponse;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que la operación no fue exitosa
                Assert.False(result.Success);
                // Verificar que el mensaje de error en el resultado coincida con el mensaje de la excepción
                Assert.Equal(errorMessage, result.Message);
                // Verificar que el código de estado sea 500 (Error interno del servidor)
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }
        }

        public class GetBySeatId
        {
            /// <summary>
            /// Prueba unitaria para verificar que el método GetReservationsBySeatId devuelve el código de estado 200 en caso de respuesta exitosa.
            /// </summary>
            [Fact]
            public async Task GetBySeatId_Returns_StatusCode200_When_SuccessfulResponse()
            {
                // ARRANGE
                // Lista simulada de reservas
                var reservations = new List<ReservationResponse>
                {
                    new ReservationResponse { Success = true },
                    new ReservationResponse { Success = true },
                    new ReservationResponse { Success = true },
                    new ReservationResponse { Success = true }
                };

                // Mock del servicio de reservas que devuelve las reservas por identificador de asiento
                var mockService = new Mock<IReservationService>();
                mockService.Setup(s => s.GetBySeatIdAsync(It.IsAny<Guid>())).ReturnsAsync(reservations);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ReservationController>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método GetReservationsBySeatId del controlador con un identificador de asiento
                var result_TaskResult = await controller.GetBySeatId(It.IsAny<Guid>());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as IEnumerable<ReservationResponse>;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que todas las reservas en el resultado tengan éxito
                Assert.True(result.All(e => e.Success));
                // Verificar que el número de reservas en el resultado sea igual al número esperado de reservas
                Assert.Equal(reservations.Count, result.Count());
                // Verificar que el código de estado sea 200 (OK)
                Assert.Equal(StatusCodes.Status200OK, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba unitaria para verificar que el método GetReservationsBySeatId devuelve el código de estado 404 en caso de respuesta fallida.
            /// </summary>
            [Fact]
            public async void GetBySeatId_Returns_StatusCode404_When_FailureResponse()
            {
                // ARRANGE
                // Lista simulada de reservas con al menos una reserva fallida
                var reservations = new List<ReservationResponse>
                {
                    new ReservationResponse { Success = true },
                    new ReservationResponse { Success = false },
                    new ReservationResponse { Success = true },
                    new ReservationResponse { Success = true }
                };

                // Contar el número de reservas incorrectas en la lista
                var incorrectReservationsCount = reservations.Count(e => !e.Success);

                // Mock del servicio de reservas que devuelve las reservas por identificador de asiento
                var mockService = new Mock<IReservationService>();
                mockService.Setup(s => s.GetBySeatIdAsync(It.IsAny<Guid>())).ReturnsAsync(reservations);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ReservationController>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método GetReservationsBySeatId del controlador con un identificador de asiento
                var result_TaskResult = await controller.GetBySeatId(It.IsAny<Guid>());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as IEnumerable<ReservationResponse>;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que no todas las reservas en el resultado tengan éxito
                Assert.False(result.All(e => e.Success));
                // Verificar que el número de reservas incorrectas en el resultado sea igual al número esperado de reservas incorrectas
                Assert.Equal(incorrectReservationsCount, result.Count());
                // Verificar que el código de estado sea 404 (No encontrado)
                Assert.Equal(StatusCodes.Status404NotFound, result_ObjectResult?.StatusCode);
            }

            /// <summary>
            /// Prueba unitaria para verificar que el método GetReservationsBySeatId devuelve el código de estado 500 en caso de excepción.
            /// </summary>
            [Fact]
            public async void GetBySeatId_Returns_StatusCode500_When_ExceptionCaught()
            {
                // ARRANGE
                // Mensaje de error de la excepción
                var errorMessage = "An unexpected error occurred while getting reservations.";

                // Excepción simulada
                var exception = new Exception(errorMessage);

                // Mock del servicio de reservas que lanza una excepción al obtener reservas por identificador de asiento
                var mockService = new Mock<IReservationService>();
                mockService.Setup(s => s.GetBySeatIdAsync(It.IsAny<Guid>())).ThrowsAsync(exception);

                // Mock del logger
                var mockLogger = new Mock<ILogger<ReservationController>>();

                // Controlador de reservas
                var controller = new ReservationController(mockService.Object, mockLogger.Object);

                // ACT
                // Llamar al método GetReservationsBySeatId del controlador con un identificador de asiento
                var result_TaskResult = await controller.GetBySeatId(It.IsAny<Guid>());
                var result_ActionResult = result_TaskResult?.Result;
                var result_ObjectResult = result_ActionResult as ObjectResult;
                var result = result_ObjectResult?.Value as ReservationResponse;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que la operación no fue exitosa
                Assert.False(result.Success);
                // Verificar que el mensaje de error en el resultado coincida con el mensaje de la excepción
                Assert.Equal(errorMessage, result.Message);
                // Verificar que el código de estado sea 500 (Error interno del servidor)
                Assert.Equal(StatusCodes.Status500InternalServerError, result_ObjectResult?.StatusCode);
            }
        }
    }
}

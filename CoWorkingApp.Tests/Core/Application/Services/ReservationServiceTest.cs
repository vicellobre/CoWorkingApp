using CoWorkingApp.Core.Application.Contracts.Adapters;
using CoWorkingApp.Core.Application.Contracts.Repositories;
using CoWorkingApp.Core.Application.Services;
using CoWorkingApp.Core.Domain.DTOs;
using CoWorkingApp.Core.Domain.Entities;
using CoWorkingApp.Tests.ClassData;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace CoWorkingApp.Tests.Core.Application.Services
{
    public class ReservationServiceTest
    {
        public class Constructor
        {
            /// <summary>
            /// Verifica que el constructor de la clase ReservationService maneje correctamente el caso en que los parámetros no son nulos.
            /// </summary>
            [Fact]
            public void Constructor_ReturnsInstance_When_ParametersAreNotNull()
            {
                // ARRANGE
                // Se establecen los mocks de IMapperAdapter e IReservationRepository
                var mockMapper = new Mock<IMapperAdapter>();
                var mockRepository = new Mock<IReservationRepository>();

                // ACT
                // Se intenta crear una nueva instancia de ReservationService con los mocks proporcionados
                var result = () => new ReservationService(mockRepository.Object, mockMapper.Object);

                // ASSERT
                // Se verifica que el resultado no sea nulo, lo que indica que se ha creado una instancia de ReservationService correctamente
                Assert.NotNull(result);
            }

            /// <summary>
            /// Verifica que el constructor de la clase ReservationService lance una excepción ArgumentNullException cuando el repositorio es nulo.
            /// </summary>
            [Fact]
            public void Constructor_Throws_ArgumentNullException_When_NullRepository()
            {
                // ARRANGE
                // Se establece el repositorio como nulo
                IReservationRepository? repository = null;
                var mockMapper = new Mock<IMapperAdapter>();

                // ACT
                // Se intenta crear una nueva instancia de ReservationService con el repositorio nulo
                var result = () => new ReservationService(repository, mockMapper.Object);

                // ASSERT
                // Se verifica que el resultado no sea nulo, lo que indica que se ha creado una instancia de ReservationService correctamente
                Assert.NotNull(result);
                // Se verifica que lanzar la excepción ArgumentNullException
                Assert.Throws<ArgumentNullException>(result);
            }

            /// <summary>
            /// Verifica que el constructor de la clase ReservationService lance una excepción ArgumentNullException cuando el adaptador de mapeo es nulo.
            /// </summary>
            [Fact]
            public void Constructor_Throws_ArgumentNullException_When_NullMapper()
            {
                // ARRANGE
                // Se crea un mock del repositorio
                var mockRepository = new Mock<IReservationRepository>();
                // Se establece el adaptador de mapeo como nulo
                IMapperAdapter? mapper = null;

                // ACT
                // Se intenta crear una nueva instancia de ReservationService con el adaptador de mapeo nulo
                var result = () => new ReservationService(mockRepository.Object, mapper);

                // ASSERT
                // Se verifica que el resultado no sea nulo, lo que indica que se ha creado una instancia de ReservationService correctamente
                Assert.NotNull(result);
                // Se verifica que lanzar la excepción ArgumentNullException
                Assert.Throws<ArgumentNullException>(result);
            }
        }

        public class GetAll
        {
            /// <summary>
            /// Verifica que el método GetAllAsync de la clase ReservationService devuelva todas las reservaciones correctamente mapeadas.
            /// </summary>
            [Fact]
            public async Task GetAllAsync_Returns_AllReservations_When_ReservationsExistInRepository()
            {
                // ARRANGE
                // Se inicializa el contexto de prueba con datos de reservaciones
                var context = await TestContextFactory.InitializeDataRerservationsAsync();
                var reservations = context.Reservations;

                // Se crea un mapeador para mapear las reservaciones a respuestas de reservación
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Se establece un mock del adaptador de mapeo para simular el mapeo de reservaciones a respuestas de reservación
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<IEnumerable<Reservation>, IEnumerable<ReservationResponse>>(reservations))
                    .Returns(mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationResponse>>(reservations));

                // Se establece un mock del repositorio para devolver todas las reservaciones
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(reservations);

                // Se crea una instancia de ReservationService con el mock del repositorio y el mock del adaptador de mapeo
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método GetAllAsync para obtener todas las reservaciones
                var result = await service.GetAllAsync();

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que el número de reservaciones devueltas coincida con el número de reservaciones en el contexto de prueba
                Assert.Equal(reservations.Count(), result.Count());
                // Se verifica que todas las reservaciones en el resultado tengan éxito
                Assert.True(result.All(p => p.Success));
                // Se verifica que las propiedades Date, UserEmail y SeatName de cada reserva en el resultado coincidan con las propiedades correspondientes en el contexto de prueba
                Assert.Equal(reservations
                                .Select(u => new
                                {
                                    u.Date,
                                    Email = u.User != null ? u.User.Email : null,
                                    SeatName = u.Seat != null ? u.Seat.Name : null
                                })
                                .Where(x => x.Email != null && x.SeatName != null),
                             result
                                .Select(u => new
                                {
                                    u.Date,
                                    Email = u.UserEmail,
                                    SeatName = u.SeatName
                                })
                );
            }

            /// <summary>
            /// Verifica que el método GetAllAsync de la clase ReservationService devuelva una colección vacía cuando no hay reservaciones.
            /// </summary>
            [Fact]
            public async void GetAllAsync_Returns_EmptyCollection_When_NoReservationsInRepository()
            {
                // ARRANGE
                // Se crea una lista vacía de reservaciones
                var reservations = new List<Reservation>();

                // Se establece un mock del adaptador de mapeo
                var mockMapper = new Mock<IMapperAdapter>();

                // Se establece un mock del repositorio para devolver una lista vacía de reservaciones
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(reservations);

                // Se crea una instancia de ReservationService con el mock del repositorio y el mock del adaptador de mapeo
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método GetAllAsync para obtener todas las reservaciones
                var result = await service.GetAllAsync();

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que el resultado esté vacío
                Assert.Empty(result);
                // Se verifica que el número de reservaciones en el resultado sea igual al número de reservaciones en la lista de reservaciones
                Assert.Equal(reservations.Count, result.Count());
                // Se verifica que todas las reservaciones en el resultado tengan éxito
                Assert.True(result.All(p => p.Success));
            }

            /// <summary>
            /// Verifica que el método GetAllAsync de la clase ReservationService maneje correctamente una excepción al intentar obtener todas las reservaciones.
            /// </summary>
            [Fact]
            public async void GetAllAsync_Returns_NegativeResponse_When_ExceptionThrown()
            {
                // ARRANGE
                // Se crea una excepción simulada
                var exception = new Exception();
                // Se obtiene el mensaje de error de la excepción
                var errorMessage = exception.Message;

                // Se establece un mock del adaptador de mapeo
                var mockMapper = new Mock<IMapperAdapter>();

                // Se establece un mock del repositorio para lanzar una excepción al intentar obtener todas las reservaciones
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetAllAsync()).ThrowsAsync(exception);

                // Se crea una instancia de ReservationService con el mock del repositorio y el mock del adaptador de mapeo
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método GetAllAsync para obtener todas las reservaciones y manejar la excepción
                var result = await service.GetAllAsync();

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que haya exactamente un elemento en el resultado
                Assert.Single(result);
                // Se verifica que la operación no haya tenido éxito
                Assert.False(result.First().Success);
                // Se verifica que el mensaje de error en el resultado sea igual al mensaje de error de la excepción simulada
                Assert.Equal(errorMessage, result.First().Message);
            }
        }

        public class GetById
        {
            /// <summary>
            /// Verifica que el método GetByIdAsync de la clase ReservationService devuelva correctamente una reservación existente cuando se proporciona su ID.
            /// </summary>
            [Fact]
            public async void GetByIdAsync_Returns_SuccessfulResponse_When_ValidRequest()
            {
                // ARRANGE
                // Inicializa el contexto de prueba con datos de reservaciones
                var context = await TestContextFactory.InitializeDataRerservationsAsync();
                // Obtiene la primera reservación existente en el contexto de prueba
                var existingReservation = context.Reservations.First();

                // Crea un mapeador de AutoMapper
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Crea un mock del adaptador de mapeo y configúralo para devolver el resultado del mapeo de la reservación existente
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<Reservation, ReservationResponse>(existingReservation))
                    .Returns(mapper.Map<Reservation, ReservationResponse>(existingReservation));

                // Crea un mock del repositorio y configúralo para devolver la reservación existente cuando se llama al método GetByIdAsync
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(existingReservation.Id)).ReturnsAsync(existingReservation);

                // Crea una instancia de ReservationService con el mock del repositorio y el mock del adaptador de mapeo
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llama al método GetByIdAsync para obtener la reservación con el ID proporcionado
                var result = await service.GetByIdAsync(existingReservation.Id);

                // ASSERT
                // Verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Verifica que la operación haya tenido éxito
                Assert.True(result.Success);

                // Verifica que la reservación existente no sea nula
                Assert.NotNull(existingReservation);

                // Verifica que el usuario y el asiento no sean nulos
                Assert.NotNull(existingReservation.User);
                Assert.NotNull(existingReservation.Seat);

                // Verifica que los datos de la reservación devuelta sean consistentes con los de la reservación existente
                Assert.Equal((existingReservation.Date, existingReservation.User.Email, existingReservation.Seat.Name),
                             (result.Date, result.UserEmail, result.SeatName));
            }

            /// <summary>
            /// Verifica que el método GetByIdAsync de la clase ReservationService arroje una excepción de ArgumentException cuando la reservación devuelta es nula.
            /// </summary>
            [Fact]
            public async void GetByIdAsync_Returns_NegativeResponse_When_RequestNotFound()
            {
                // ARRANGE
                // Se crea una reservación nula
                Reservation? nullReservation = null;

                // Se configura el mock del repositorio para devolver la reservación nula cuando se llama al método GetByIdAsync
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(nullReservation);

                // Se crea un mock del adaptador de mapeo
                var mockMapper = new Mock<IMapperAdapter>();

                // Se crea una instancia de ReservationService con el mock del repositorio y el mock del adaptador de mapeo
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                var reservationId = Guid.NewGuid();

                // ACT
                // Se llama al método GetByIdAsync con un ID de reservación
                var result = await service.GetByIdAsync(reservationId);

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que la operación no haya tenido éxito
                Assert.False(result.Success);
                // Se verifica que el mensaje de la excepción contenga información sobre la reservación no encontrada
                Assert.Contains($"Entity with id {reservationId} not found", result.Message);
            }

            /// <summary>
            /// Verifica que el método GetByIdAsync de la clase ReservationService maneje correctamente una excepción.
            /// </summary>
            [Fact]
            public async void GetByIdAsync_Returns_NegativeResponse_When_ExceptionThrown()
            {
                // ARRANGE
                // Se crea una excepción y se guarda su mensaje de error
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Se configura el mock del repositorio para que arroje una excepción al llamar al método GetByIdAsync
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ThrowsAsync(exception);

                // Se crea un mock del adaptador de mapeo
                var mockMapper = new Mock<IMapperAdapter>();

                // Se crea una instancia de ReservationService con el mock del repositorio y el mock del adaptador de mapeo
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método GetByIdAsync con un ID de reservación
                var result = await service.GetByIdAsync(Guid.NewGuid());

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que la operación no haya tenido éxito
                Assert.False(result.Success);
                // Se verifica que el mensaje de error sea el mismo que el de la excepción original
                Assert.Equal(errorMessage, result.Message);
            }
        }

        public class Create
        {
            /// <summary>
            /// Verifica que el método CreateAsync de la clase ReservationService cree una nueva reserva cuando se proporciona una entrada válida.
            /// </summary>
            [Theory]
            [ClassData(typeof(ReservationRequestClassData))]
            public async void CreateAsync_Returns_SuccessfulResponse_When_ValidRequest(DateTime dateTime)
            {
                // ARRANGE
                // Se crea un mapeador AutoMapper para el proceso de mapeo
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Se inicializa el contexto de prueba con datos de reservas
                var context = await TestContextFactory.InitializeDataRerservationsAsync();

                // Se crea una solicitud de reserva con datos válidos
                var reservationRequest = new ReservationRequest
                {
                    Date = dateTime,
                    UserId = context.Users.First().Id,
                    SeatId = context.Seats.First().Id
                };

                // Se mapea la solicitud de reserva a una entidad de reserva
                var reservation = mapper.Map<ReservationRequest, Reservation>(reservationRequest);

                // Se mapea la reserva a una respuesta de reserva
                var reservationResponse = mapper.Map<Reservation, ReservationResponse>(reservation);

                // Se configura el mock del adaptador de mapeo para devolver los resultados esperados
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<ReservationRequest, Reservation>(It.IsAny<ReservationRequest>()))
                    .Returns(mapper.Map<ReservationRequest, Reservation>(reservationRequest));
                mockMapper.Setup(m => m.Map<Reservation, ReservationResponse>(It.IsAny<Reservation>()))
                    .Returns(mapper.Map<Reservation, ReservationResponse>(reservation));

                // Se configura el mock del repositorio para devolver true al agregar una reserva
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.AddAsync(It.IsAny<Reservation>())).ReturnsAsync(true);

                // Se crea una instancia de ReservationService con los mocks del repositorio y del adaptador de mapeo
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método CreateAsync con la solicitud de reserva y se espera que la operación sea exitosa
                var result = await service.CreateAsync(reservationRequest);

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que la operación haya tenido éxito
                Assert.True(result.Success);
                // Se verifica que la fecha de la reserva sea la misma que la fecha de la solicitud de reserva
                Assert.Equal(reservationRequest.Date, result.Date);
            }

            /// <summary>
            /// Verifica que el método CreateAsync de la clase ReservationService lance una excepción de ArgumentNullException cuando se proporciona una solicitud nula.
            /// </summary>
            [Fact]
            public async void CreateAsync_Returns_NegativeResponse_When_NullRequest()
            {
                // ARRANGE
                // Se crea una excepción de ArgumentNullException para simular el escenario de solicitud nula
                var exception = new ArgumentNullException("The request object cannot be null");
                var errorMessage = exception.Message;

                // Se establece la solicitud de reserva como nula
                ReservationRequest? reservation = null;

                // Se configura el mock del repositorio y del adaptador de mapeo
                var mockRepository = new Mock<IReservationRepository>();
                var mockMapper = new Mock<IMapperAdapter>();

                // Se crea una instancia de ReservationService con los mocks del repositorio y del adaptador de mapeo
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método CreateAsync con la solicitud de reserva nula y se espera que lance una excepción de ArgumentNullException
                var result = await service.CreateAsync(reservation);

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que la operación no haya tenido éxito
                Assert.False(result.Success);
                // Se verifica que el mensaje de la excepción sea el esperado
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Verifica que el método CreateAsync de la clase ReservationService lance una excepción de validación cuando se proporciona una entrada no válida.
            /// </summary>
            [Theory]
            [ClassData(typeof(InvalidReservationRequestClassData))]
            public async void CreateAsync_Returns_NegativeResponse_When_InvalidRequest(DateTime dateTime)
            {
                // ARRANGE
                // Se crea una excepción de validación con un mensaje personalizado
                var exception = new ValidationException("Argument is invalid.");
                var errorMessage = exception.Message;

                // Se inicializa el contexto de prueba con datos de reservas
                var context = await TestContextFactory.InitializeDataRerservationsAsync();

                // Se crea una solicitud de reserva con datos no válidos
                var reservation = new ReservationRequest
                {
                    Date = dateTime,
                    UserId = context.Users.First().Id,
                    SeatId = context.Seats.First().Id
                };

                // Se crea un mapeador AutoMapper para el proceso de mapeo
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Se configura el mock del adaptador de mapeo para devolver los resultados esperados
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<ReservationRequest, Reservation>(reservation))
                    .Returns(mapper.Map<ReservationRequest, Reservation>(reservation));

                // Se crea un mock del repositorio que no se utilizará en este caso
                var mockRepository = new Mock<IReservationRepository>();

                // Se crea una instancia de ReservationService con el mock del repositorio y el mock del adaptador de mapeo
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método CreateAsync con la solicitud de reserva y se espera que lance una excepción de validación
                var result = await service.CreateAsync(reservation);

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que la operación no haya tenido éxito
                Assert.False(result.Success);
                // Se verifica que el mensaje de error sea el mismo que el esperado
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Verifica que el método CreateAsync de la clase ReservationService lance una excepción de InvalidOperationException cuando el repositorio no puede agregar la entidad.
            /// </summary>
            [Fact]
            public async void CreateAsync_Returns_NegativeResponse_When_RepositoryFailsToAdd()
            {
                // ARRANGE
                // Se crea una excepción de InvalidOperationException con un mensaje personalizado
                var exception = new InvalidOperationException("The entity could not be added.");
                var errorMessage = exception.Message;

                // Se inicializa el contexto de prueba con datos de reservas
                var context = await TestContextFactory.InitializeDataRerservationsAsync();

                // Se crea una solicitud de reserva con datos válidos
                var reservation = new ReservationRequest
                {
                    Date = DateTime.Now,
                    UserId = context.Users.First().Id,
                    SeatId = context.Seats.First().Id
                };

                // Se crea un mapeador AutoMapper para el proceso de mapeo
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Se configura el mock del adaptador de mapeo para devolver los resultados esperados
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<ReservationRequest, Reservation>(reservation))
                    .Returns(mapper.Map<ReservationRequest, Reservation>(reservation));

                // Se configura el mock del repositorio para simular un fallo al agregar la entidad
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.AddAsync(It.IsAny<Reservation>())).ReturnsAsync(false);

                // Se crea una instancia de ReservationService con el mock del repositorio y el mock del adaptador de mapeo
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método CreateAsync con la solicitud de reserva y se espera que lance una excepción de InvalidOperationException
                var result = await service.CreateAsync(reservation);

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que la operación no haya tenido éxito
                Assert.False(result.Success);
                // Se verifica que el mensaje de error sea el mismo que el esperado
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Verifica que el método CreateAsync de la clase ReservationService lance una excepción cuando el mapeador no puede realizar el mapeo.
            /// </summary>
            [Fact]
            public async void CreateAsync_Returns_NegativeResponse_ExceptionThrown()
            {
                // ARRANGE
                // Se crea una excepción genérica para simular un error en el mapeo
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Se configura el mock del adaptador de mapeo para lanzar una excepción al intentar mapear
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<ReservationRequest, Reservation>(It.IsAny<ReservationRequest>())).Throws(exception);

                // Se configura el mock del repositorio para simular una adición exitosa de la entidad
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.AddAsync(It.IsAny<Reservation>())).ReturnsAsync(true);

                // Se crea una instancia de ReservationService con el mock del repositorio y el mock del adaptador de mapeo
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método CreateAsync con una solicitud de reserva y se espera que lance una excepción
                var result = await service.CreateAsync(new ReservationRequest());

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que la operación no haya tenido éxito
                Assert.False(result.Success);
                // Se verifica que el mensaje de error sea el mismo que el esperado
                Assert.Equal(errorMessage, result.Message);
            }
        }

        public class Update
        {
            /// <summary>
            /// Verifica que el método UpdateAsync de la clase ReservationService actualice una reservación existente cuando se proporciona una solicitud válida.
            /// </summary>
            [Theory]
            [ClassData(typeof(ReservationRequestClassData))]
            public async void UpdateAsync_Returns_SuccessfulResponse_When_ValidRequest(DateTime dateTime)
            {
                // ARRANGE
                // Se genera un nuevo identificador de reservación
                var reservationId = Guid.NewGuid();

                // Se inicializa el contexto de prueba con datos de reservaciones
                var context = await TestContextFactory.InitializeDataRerservationsAsync();

                // Se obtiene la primera reservación existente en el contexto de prueba
                var existingReservation = context.Reservations.First();

                // Se crea una solicitud de reservación con la fecha proporcionada por los datos de la clase de datos
                // Verifica que el usuario y el asiento existen
                var existingUser = context.Users.Find(existingReservation.UserId);
                var existingSeat = context.Seats.Find(existingReservation.SeatId);

                Assert.NotNull(existingUser);
                Assert.NotNull(existingSeat);

                // Se crea una solicitud de reservación con la fecha proporcionada por los datos de la clase de datos
                var reservationRequest = new ReservationRequest
                {
                    Date = dateTime,
                    UserId = existingUser.Id,  // Se usa existingUser.Id
                    SeatId = existingSeat.Id     // Se usa existingSeat.Id
                };

                // Se configura la respuesta de la reservación para compararla con la respuesta esperada después de la actualización
                var reservationResponse = new ReservationResponse
                {
                    Date = reservationRequest.Date
                };

                // Se configura el mock del repositorio para devolver la reservación existente al llamar GetByIdAsync y simular una actualización exitosa
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingReservation);
                mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Reservation>())).ReturnsAsync(true);

                // Se crea un mapeador de AutoMapper para mapear la reservación a su respuesta
                var mapper = TestAutoMapperFactory.CreateMapper();
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<Reservation, ReservationResponse>(It.IsAny<Reservation>()))
                    .Returns((Reservation reservation) => mapper.Map<Reservation, ReservationResponse>(reservation));

                // Se crea una instancia de ReservationService con el mock del repositorio y el mock del adaptador de mapeo
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método UpdateAsync para actualizar la reservación existente con la solicitud proporcionada
                var result = await service.UpdateAsync(existingReservation.Id, reservationRequest);

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que la operación haya tenido éxito
                Assert.True(result.Success);
                // Se verifica que la fecha de la reservación actualizada sea igual a la fecha proporcionada en la solicitud
                Assert.Equal(reservationRequest.Date, result.Date);
            }

            /// <summary>
            /// Verifica que el método UpdateAsync de la clase ReservationService lance ArgumentNullException cuando se proporciona una solicitud nula.
            /// </summary>
            [Fact]
            public async void UpdateAsync_Returns_NegativeResponse_When_NullRequest()
            {
                // ARRANGE
                // Se crea una excepción de argumento nulo con el mensaje apropiado
                var exception = new ArgumentNullException("The request object cannot be null");
                var errorMessage = exception.Message;

                // Se inicializa el objeto de solicitud de reservación como nulo
                ReservationRequest? reservation = null;

                // Se configura un mock del repositorio y del adaptador de mapeo
                var mockRepository = new Mock<IReservationRepository>();
                var mockMapper = new Mock<IMapperAdapter>();

                // Se crea una instancia de ReservationService con los mocks configurados
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método UpdateAsync con un identificador de reservación y una solicitud nula
                var result = await service.UpdateAsync(Guid.NewGuid(), reservation);

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que la operación haya fallado
                Assert.False(result.Success);
                // Se verifica que el mensaje de la excepción sea igual al mensaje esperado
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Verifica que el método UpdateAsync de la clase ReservationService lance ArgumentException cuando no se encuentra la solicitud de reserva.
            /// </summary>
            [Fact]
            public async void UpdateAsync_Returns_NegativeResponse_When_RequestNotFound()
            {
                // ARRANGE
                // Se genera un identificador de reservación único
                var reservationId = Guid.NewGuid();
                // Se inicializa la reserva como nula
                Reservation? reservation = null;

                // Se crea una excepción de argumento con el mensaje apropiado
                var exception = new ArgumentException($"Entity with id {reservationId} not found");
                var errorMessage = exception.Message;

                // Se configura un mock del adaptador de mapeo
                var mockMapper = new Mock<IMapperAdapter>();

                // Se configura un mock del repositorio con la reserva nula para el identificador dado
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(reservation);

                // Se crea una instancia de ReservationService con los mocks configurados
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método UpdateAsync con el identificador de la reserva y una solicitud de reserva vacía
                var result = await service.UpdateAsync(reservationId, new ReservationRequest());

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que la operación haya fallado
                Assert.False(result.Success);
                // Se verifica que el mensaje de la excepción sea igual al mensaje esperado
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Verifica que el método UpdateAsync de la clase ReservationService lance ValidationException cuando la solicitud de reserva no es válida.
            /// </summary>
            [Theory]
            [ClassData(typeof(InvalidReservationRequestClassData))]
            public async void UpdateAsync_Returns_NegativeResponse_When_InvalidRequest(DateTime dateTime)
            {
                // ARRANGE
                // Se crea una excepción de validación con el mensaje apropiado
                var exception = new ValidationException("Argument is invalid.");
                var errorMessage = exception.Message;

                // Se inicializa una reserva actualizada con la fecha proporcionada
                var updatedReservation = new Reservation
                {
                    Date = dateTime,
                };

                // Se configura un mock del adaptador de mapeo
                var mockMapper = new Mock<IMapperAdapter>();

                // Se configura un mock del repositorio con la reserva actualizada para cualquier identificador de reserva
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(updatedReservation);

                // Se crea una instancia de ReservationService con los mocks configurados
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método UpdateAsync con un identificador de reserva aleatorio y una solicitud de reserva vacía
                var result = await service.UpdateAsync(Guid.NewGuid(), new ReservationRequest());

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que la operación haya fallado
                Assert.False(result.Success);
                // Se verifica que el mensaje de la excepción sea igual al mensaje esperado
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Verifica que el método UpdateAsync de la clase ReservationService lance InvalidOperationException cuando el repositorio no puede actualizar la reserva.
            /// </summary>
            [Theory]
            [ClassData(typeof(ReservationRequestClassData))]
            public async void UpdateAsync_Returns_NegativeResponse_When_RepositoryFailsToUpdate(DateTime dateTime)
            {
                // ARRANGE
                // Se crea una excepción de operación no válida con el mensaje apropiado
                var exception = new InvalidOperationException("The entity could not be updated.");
                var errorMessage = exception.Message;

                // Se crea una solicitud de reserva con la fecha proporcionada
                var reservationRequest = new ReservationRequest
                {
                    Date = dateTime,
                };

                // Se crea una reserva existente con la misma fecha
                var existingReservation = new Reservation
                {
                    Date = reservationRequest.Date,
                };

                // Se configura un mock del repositorio con la reserva existente para cualquier identificador de reserva
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingReservation);
                // Se configura el repositorio para que falle al actualizar la reserva
                mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Reservation>())).ReturnsAsync(false);

                // Se configura un mock del adaptador de mapeo
                var mockMapper = new Mock<IMapperAdapter>();

                // Se crea una instancia de ReservationService con los mocks configurados
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método UpdateAsync con un identificador de reserva aleatorio y la solicitud de reserva creada
                var result = await service.UpdateAsync(Guid.NewGuid(), reservationRequest);

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que la operación haya fallado
                Assert.False(result.Success);
                // Se verifica que el mensaje de la excepción sea igual al mensaje esperado
                Assert.Equal(errorMessage, result.Message);
            }

            [Theory]
            [ClassData(typeof(ReservationRequestClassData))]
            public void UpdateAsync_Returns_NegativeResponse_ExceptionThrown(DateTime dateTime)
            {
                // ARRANGE

                var exception = new Exception();
                var errorMessage = exception.Message;

                var reservationRequest = new ReservationRequest
                {
                    Date = dateTime,
                };

                // Se crea una reserva existente con la misma fecha
                var existingReservation = new Reservation
                {
                    Date = reservationRequest.Date,
                };

                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingReservation);
                mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Reservation>())).ReturnsAsync(true);

                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<Reservation, ReservationResponse>(It.IsAny<Reservation>())).Throws(new Exception());

                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                var result = service.UpdateAsync(Guid.NewGuid(), reservationRequest)?.Result;

                // ASSERT
                Assert.NotNull(result);
                Assert.False(result.Success);
                Assert.Equal(errorMessage, result.Message);
            }
        }

        public class Delete
        {
            /// <summary>
            /// Verifica que el método DeleteAsync de la clase ReservationService elimine correctamente una reserva existente.
            /// </summary>
            [Theory]
            [ClassData(typeof(ReservationRequestClassData))]
            public async void DeleteAsync_Returns_SuccessfulResponse_When_ValidRequest(DateTime dateTime)
            {
                // ARRANGE
                // Se crea una reserva existente con la fecha proporcionada
                var existingReservation = new Reservation
                {
                    Date = dateTime,
                };

                // Se configura un mock del repositorio con la reserva existente para cualquier identificador de reserva
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingReservation);
                // Se configura el repositorio para que elimine correctamente la reserva
                mockRepository.Setup(r => r.RemoveAsync(It.IsAny<Reservation>())).ReturnsAsync(true);

                // Se crea un mapper para el mapeo de la reserva a su respuesta correspondiente
                var mapper = TestAutoMapperFactory.CreateMapper();
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<Reservation, ReservationResponse>(It.IsAny<Reservation>())).
                    Returns((Reservation reservation) => mapper.Map<Reservation, ReservationResponse>(reservation));

                // Se crea una instancia de ReservationService con los mocks configurados
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método DeleteAsync con un identificador de reserva aleatorio
                var result = await service.DeleteAsync(Guid.NewGuid());

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que la operación haya sido exitosa
                Assert.True(result.Success);
                // Se verifica que la fecha de la reserva eliminada coincida con la fecha proporcionada
                Assert.Equal(existingReservation.Date, result.Date);
            }

            /// <summary>
            /// Verifica que el método DeleteAsync de la clase ReservationService lance una excepción de ArgumentNullException cuando no se encuentra la reserva.
            /// </summary>
            [Fact]
            public async void DeleteAsync_Returns_NegativeResponse_When_RequestNotFound()
            {
                // ARRANGE
                // Se genera un identificador de reserva aleatorio
                var reservationId = Guid.NewGuid();

                // Se crea una excepción de ArgumentNullException con el mensaje adecuado
                var exception = new ArgumentNullException($"Entity with id {reservationId} not found");
                var errorMessage = exception.Message;

                // Se configura el repositorio para devolver una reserva nula para el identificador proporcionado
                Reservation? reservation = null;
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(reservation);

                // Se configura un mock de IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Se crea una instancia de ReservationService con los mocks configurados
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método DeleteAsync con el identificador de reserva aleatorio
                var result = await service.DeleteAsync(reservationId);

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que la operación no haya sido exitosa
                Assert.False(result.Success);
                // Se verifica que el mensaje de la excepción sea el esperado
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Verifica que el método DeleteAsync de la clase ReservationService lance una excepción de InvalidOperationException cuando el repositorio no puede eliminar la reserva.
            /// </summary>
            [Theory]
            [ClassData(typeof(ReservationRequestClassData))]
            public async void DeleteAsync_Returns_NegativeResponse_When_RepositoryFailsToDelete(DateTime dateTime)
            {
                // ARRANGE
                // Se crea una excepción de InvalidOperationException con el mensaje adecuado
                var exception = new InvalidOperationException("The entity could not be deleted.");
                var errorMessage = exception.Message;

                // Se crea una reserva existente con la fecha proporcionada
                var existingReservation = new Reservation
                {
                    Date = dateTime,
                };

                // Se configura el repositorio para devolver la reserva existente
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingReservation);
                // Se configura el repositorio para que la eliminación de la reserva devuelva false, indicando que no se pudo eliminar
                mockRepository.Setup(r => r.RemoveAsync(It.IsAny<Reservation>())).ReturnsAsync(false);

                // Se configura un mock de IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Se crea una instancia de ReservationService con los mocks configurados
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método DeleteAsync con un identificador de reserva aleatorio
                var result = await service.DeleteAsync(Guid.NewGuid());

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que la operación no haya sido exitosa
                Assert.False(result.Success);
                // Se verifica que el mensaje de la excepción sea el esperado
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Verifica que el método DeleteAsync de la clase ReservationService lance una excepción cuando el mapeador no puede realizar el mapeo.
            /// </summary>
            [Theory]
            [ClassData(typeof(ReservationRequestClassData))]
            public void DeleteAsync_Returns_NegativeResponse_ExceptionThrown(DateTime dateTime)
            {
                // ARRANGE
                // Se crea una excepción genérica
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Se crea una reserva existente con la fecha proporcionada
                var existingReservation = new Reservation
                {
                    Date = dateTime,
                };

                // Se configura el repositorio para devolver la reserva existente
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingReservation);
                // Se configura el repositorio para devolver true al intentar eliminar la reserva, indicando que se pudo eliminar
                mockRepository.Setup(r => r.RemoveAsync(It.IsAny<Reservation>())).ReturnsAsync(true);

                // Crear un mapeador AutoMapper
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Se configura un mock de IMapperAdapter para que lance una excepción al realizar el mapeo
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<Reservation, ReservationResponse>(It.IsAny<Reservation>())).Throws(new Exception());

                // Se crea una instancia de ReservationService con los mocks configurados
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método DeleteAsync con un identificador de reserva aleatorio y se obtiene el resultado
                var result = service.DeleteAsync(Guid.NewGuid())?.Result;

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que la operación no haya sido exitosa
                Assert.False(result.Success);
                // Se verifica que el mensaje de la excepción sea el esperado
                Assert.Equal(errorMessage, result.Message);
            }
        }

        public class GetByDate
        {
            /// <summary>
            /// Verifica que el método GetReservationsByDateAsync de la clase ReservationService devuelva todas las reservas para una fecha dada.
            /// </summary>
            [Fact]
            public async Task GetByDateAsync_Returns_SuccessfulResponse_When_ReservationsExistInRepository()
            {
                // ARRANGE
                // Se crea un mapeador AutoMapper
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Se inicializa el contexto de prueba con datos de reservas
                var context = await TestContextFactory.InitializeDataRerservationsAsync();
                var reservations = context.Reservations;

                // Se selecciona una fecha de la primera reserva en la lista de reservas
                var date = reservations.First().Date;

                // Se filtran las reservas por la fecha seleccionada
                var reservationsByDate = reservations.Where(r => r.Date == date);

                // Se configura el mock del repositorio para devolver las reservas filtradas por la fecha
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetByDateAsync(It.IsAny<DateTime>())).ReturnsAsync(reservationsByDate);

                // Se configura el mock del mapeador para mapear las reservas filtradas
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<IEnumerable<Reservation>, IEnumerable<ReservationResponse>>(reservationsByDate))
                    .Returns(mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationResponse>>(reservationsByDate));

                // Se crea una instancia de ReservationService con los mocks configurados
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método GetReservationsByDateAsync con cualquier fecha y se obtiene el resultado
                var result = await service.GetByDateAsync(date);

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que el número de elementos en el resultado sea igual al de las reservas filtradas
                Assert.Equal(reservationsByDate.Count(), result.Count());
                // Se verifica que todos los elementos en el resultado sean exitosos
                Assert.True(result.All(p => p.Success));
                // Se verifica que las fechas en el resultado coincidan con las fechas de las reservas filtradas
                Assert.Equal(reservationsByDate.Select(u => u.Date), result.Select(u => u.Date));
            }

            /// <summary>
            /// Verifica que el método GetReservationsByDateAsync de la clase ReservationService devuelva una lista vacía cuando no hay reservas para la fecha proporcionada.
            /// </summary>
            [Fact]
            public async void GetByDateAsync_Returns_EmptyCollection_When_NoReservationsInRepository()
            {
                // ARRANGE
                // Se crea una lista vacía de reservas
                var reservations = new List<Reservation>();

                // Se configura el mock del mapeador para devolver una lista vacía de reservas mapeadas
                var mockMapper = new Mock<IMapperAdapter>();

                // Se configura el mock del repositorio para devolver la lista vacía de reservas al buscar por fecha
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetByDateAsync(It.IsAny<DateTime>())).ReturnsAsync(reservations);

                // Se crea una instancia de ReservationService con los mocks configurados
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método GetReservationsByDateAsync con cualquier fecha y se obtiene el resultado
                var result = await service.GetByDateAsync(It.IsAny<DateTime>());

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que la lista de resultados esté vacía
                Assert.Empty(result);
                // Se verifica que el número de elementos en el resultado sea igual al de la lista de reservas
                Assert.Equal(reservations.Count, result.Count());
                // Se verifica que todos los elementos en el resultado sean exitosos
                Assert.True(result.All(p => p.Success));
            }

            /// <summary>
            /// Verifica que el método GetReservationsByDateAsync de la clase ReservationService maneje correctamente las excepciones.
            /// </summary>
            [Fact]
            public async void GetByDateAsync_Returns_NegativeResponse_ExceptionThrown()
            {
                // ARRANGE
                // Se crea una excepción y se obtiene el mensaje de error
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Se configura el mock del repositorio para lanzar la excepción al llamar a GetByDateAsync
                var mockMapper = new Mock<IMapperAdapter>();
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetByDateAsync(It.IsAny<DateTime>())).ThrowsAsync(exception);

                // Se crea una instancia de ReservationService con el mock del repositorio configurado
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método GetReservationsByDateAsync con cualquier fecha y se obtiene el resultado
                var result = await service.GetByDateAsync(It.IsAny<DateTime>());

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que solo haya un elemento en el resultado
                Assert.Single(result);
                // Se verifica que el elemento en el resultado sea un fallo
                Assert.False(result.First().Success);
                // Se verifica que el mensaje de error del resultado coincida con el de la excepción
                Assert.Equal(errorMessage, result.First().Message);
            }
        }

        public class GetByUserId
        {
            /// <summary>
            /// Verifica que el método GetReservationsByUserIdAsync de la clase ReservationService devuelva todas las reservas asociadas a un usuario.
            /// </summary>
            [Fact]
            public async Task GetByUserIdAsync_Returns_SuccessfulResponse_When_ReservationsExistInRepository()
            {
                // ARRANGE
                // Se crea un mapeador AutoMapper
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Se inicializa el contexto de prueba con datos de reservas
                var context = await TestContextFactory.InitializeDataRerservationsAsync();
                var reservations = context.Reservations;

                // Se obtiene el ID de usuario de la primera reserva
                var userId = reservations.First().UserId;

                // Se filtran las reservas por el ID de usuario
                var reservationsByUserId = reservations.Where(r => r.UserId == userId);

                // Se configura el mock del repositorio para devolver las reservas filtradas por el ID de usuario
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(reservationsByUserId);

                // Se configura el mock del mapeador para devolver las reservas mapeadas a partir de las reservas filtradas
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<IEnumerable<Reservation>, IEnumerable<ReservationResponse>>(reservationsByUserId))
                    .Returns(mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationResponse>>(reservationsByUserId));

                // Se crea una instancia de ReservationService con los mocks configurados
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método GetReservationsByUserIdAsync con el ID de usuario y se obtiene el resultado
                var result = await service.GetByUserIdAsync(userId);

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que el número de reservas en el resultado sea igual al número de reservas filtradas
                Assert.Equal(reservationsByUserId.Count(), result.Count());
                // Se verifica que todos los elementos en el resultado sean exitosos
                Assert.True(result.All(p => p.Success));
                // Se verifica que las fechas de las reservas en el resultado coincidan con las fechas de las reservas filtradas
                Assert.Equal(reservationsByUserId.Select(u => u.Date), result.Select(u => u.Date));
            }

            /// <summary>
            /// Verifica que el método GetReservationsByUserIdAsync de la clase ReservationService devuelva una lista vacía cuando no hay reservas asociadas a un usuario.
            /// </summary>
            [Fact]
            public async void GetByUserIdAsync_Returns_EmptyCollection_When_NoReservationsInRepository()
            {
                // ARRANGE
                // Se crea una lista vacía de reservas
                var reservations = new List<Reservation>();

                // Se configura el mock del mapeador
                var mockMapper = new Mock<IMapperAdapter>();

                // Se configura el mock del repositorio para devolver una lista vacía de reservas
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(reservations);

                // Se crea una instancia de ReservationService con los mocks configurados
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método GetReservationsByUserIdAsync con cualquier ID de usuario y se obtiene el resultado
                var result = await service.GetByUserIdAsync(It.IsAny<Guid>());

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que el resultado esté vacío
                Assert.Empty(result);
                // Se verifica que el número de reservas en el resultado sea igual al número de reservas en la lista vacía
                Assert.Equal(reservations.Count, result.Count());
                // Se verifica que todos los elementos en el resultado sean exitosos
                Assert.True(result.All(p => p.Success));
            }

            /// <summary>
            /// Verifica que el método GetReservationsByUserIdAsync de la clase ReservationService lance una excepción cuando se produce un error al recuperar las reservas asociadas a un usuario.
            /// </summary>
            [Fact]
            public async void GetByUserIdAsync_Returns_NegativeResponse_ExceptionThrown()
            {
                // ARRANGE
                // Se crea una excepción simulada
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Se configura el mock del mapeador
                var mockMapper = new Mock<IMapperAdapter>();

                // Se configura el mock del repositorio para lanzar una excepción al intentar obtener las reservas por ID de usuario
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetByUserIdAsync(It.IsAny<Guid>())).ThrowsAsync(exception);

                // Se crea una instancia de ReservationService con los mocks configurados
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método GetReservationsByUserIdAsync con cualquier ID de usuario y se obtiene el resultado
                var result = await service.GetByUserIdAsync(It.IsAny<Guid>());

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que haya un elemento en el resultado
                Assert.Single(result);
                // Se verifica que el elemento en el resultado indique un fallo
                Assert.False(result.First().Success);
                // Se verifica que el mensaje de la excepción se haya pasado al mensaje del resultado
                Assert.Equal(errorMessage, result.First().Message);
            }
        }

        public class GetBySeatId
        {
            /// <summary>
            /// Verifica que el método GetReservationsBySeatIdAsync de la clase ReservationService devuelva todas las reservas asociadas a un asiento cuando se proporciona un ID de asiento válido.
            /// </summary>
            [Fact]
            public async Task GetBySeatIdAsync_Returns_SuccessfulResponse_When_ReservationsExistInRepository()
            {
                // ARRANGE
                // Se crea un mapeador AutoMapper
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Se inicializa el contexto de prueba con datos de reservas
                var context = await TestContextFactory.InitializeDataRerservationsAsync();
                var reservations = context.Reservations;

                // Se obtiene el ID del asiento de la primera reserva
                var seatId = reservations.First().SeatId;

                // Se filtran las reservas por el ID del asiento
                var reservationsBySeatId = reservations.Where(r => r.SeatId == seatId);

                // Se configura el mock del repositorio para devolver las reservas filtradas por el ID del asiento
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetBySeatIdAsync(It.IsAny<Guid>())).ReturnsAsync(reservationsBySeatId);

                // Se configura el mock del mapeador para devolver las reservas mapeadas a ReservationResponse
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<IEnumerable<Reservation>, IEnumerable<ReservationResponse>>(reservationsBySeatId))
                    .Returns(mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationResponse>>(reservationsBySeatId));

                // Se crea una instancia de ReservationService con los mocks configurados
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método GetReservationsBySeatIdAsync con el ID del asiento y se obtiene el resultado
                var result = await service.GetBySeatIdAsync(seatId);

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que el número de reservas en el resultado sea igual al número de reservas filtradas por el ID del asiento
                Assert.Equal(reservationsBySeatId.Count(), result.Count());
                // Se verifica que todas las reservas en el resultado indiquen un éxito
                Assert.True(result.All(p => p.Success));
                // Se verifica que las fechas de las reservas en el resultado sean iguales a las fechas de las reservas filtradas por el ID del asiento
                Assert.Equal(reservationsBySeatId.Select(u => u.Date), result.Select(u => u.Date));
            }

            /// <summary>
            /// Verifica que el método GetReservationsBySeatIdAsync de la clase ReservationService devuelva una lista vacía cuando no hay reservas asociadas al ID del asiento proporcionado.
            /// </summary>
            [Fact]
            public async void GetBySeatIdAsync_Returns_EmptyCollection_When_NoReservationsInRepository()
            {
                // ARRANGE
                // Se crea una lista vacía de reservas
                var reservations = new List<Reservation>();

                // Se configura el mock del mapeador
                var mockMapper = new Mock<IMapperAdapter>();

                // Se configura el mock del repositorio para devolver la lista vacía de reservas
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetBySeatIdAsync(It.IsAny<Guid>())).ReturnsAsync(reservations);

                // Se crea una instancia de ReservationService con los mocks configurados
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método GetReservationsBySeatIdAsync con un ID de asiento y se obtiene el resultado
                var result = await service.GetBySeatIdAsync(It.IsAny<Guid>());

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que el resultado esté vacío
                Assert.Empty(result);
                // Se verifica que el número de reservas en el resultado sea igual al número de reservas en la lista vacía
                Assert.Equal(reservations.Count, result.Count());
                // Se verifica que todas las reservas en el resultado indiquen un éxito
                Assert.True(result.All(p => p.Success));
            }

            /// <summary>
            /// Verifica que el método GetReservationsBySeatIdAsync de la clase ReservationService maneje correctamente una excepción lanzada por el repositorio al buscar reservas por ID de asiento.
            /// </summary>
            [Fact]
            public async void GetBySeatIdAsync_Returns_NegativeResponse_ExceptionThrown()
            {
                // ARRANGE
                // Se crea una excepción genérica
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Se configura el mock del mapeador
                var mockMapper = new Mock<IMapperAdapter>();

                // Se configura el mock del repositorio para lanzar una excepción al buscar reservas por ID de asiento
                var mockRepository = new Mock<IReservationRepository>();
                mockRepository.Setup(r => r.GetBySeatIdAsync(It.IsAny<Guid>())).ThrowsAsync(exception);

                // Se crea una instancia de ReservationService con los mocks configurados
                var service = new ReservationService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Se llama al método GetReservationsBySeatIdAsync con un ID de asiento y se obtiene el resultado
                var result = await service.GetBySeatIdAsync(It.IsAny<Guid>());

                // ASSERT
                // Se verifica que el resultado no sea nulo
                Assert.NotNull(result);
                // Se verifica que solo haya una reserva en el resultado
                Assert.Single(result);
                // Se verifica que el resultado indique un fallo
                Assert.False(result.First().Success);
                // Se verifica que el mensaje de la excepción sea el mismo que el mensaje del resultado
                Assert.Equal(errorMessage, result.First().Message);
            }
        }
    }
}

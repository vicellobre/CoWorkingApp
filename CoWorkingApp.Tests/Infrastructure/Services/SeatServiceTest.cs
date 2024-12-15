using CoWorkingApp.Application.Contracts.Adapters;
using CoWorkingApp.Application.DTOs;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Entities;
using CoWorkingApp.Infrastructure.Services;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace CoWorkingApp.Tests.Infrastructure.Services
{
    public class SeatServiceTest
    {
        public class Constructor
        {
            /// <summary>
            /// Prueba para verificar que los parámetros no sean nulos al construir el servicio de asiento.
            /// </summary>
            [Fact]
            public void Constructor_ReturnsInstance_When_ParametersAreNotNull()
            {
                // ARRANGE

                // Crear un mock del adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear un mock del repositorio ISeatRepository
                var mockRepository = new Mock<ISeatRepository>();

                // ACT

                // Crear una instancia del servicio de asiento y capturar cualquier excepción que ocurra durante la creación
                var result = () => new SeatService(mockRepository.Object, mockMapper.Object);

                // ASSERT

                // Verificar que el resultado de la creación del servicio no sea nulo
                Assert.NotNull(result);
            }

            /// <summary>
            /// Prueba para verificar que se lance una excepción cuando el repositorio de asientos es nulo al construir el servicio de asiento.
            /// </summary>
            [Fact]
            public void Constructor_Throws_ArgumentNullException_When_NullRepository()
            {
                // ARRANGE

                // Establecer el repositorio de asientos como nulo
                ISeatRepository? repository = null;

                // Crear un mock del adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // ACT

                // Intentar crear una instancia del servicio de asiento con un repositorio nulo y capturar cualquier excepción que se genere
                var result = () => new SeatService(repository, mockMapper.Object);

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que se haya lanzado una excepción de tipo ArgumentNullException
                Assert.Throws<ArgumentNullException>(result);
            }

            [Fact]
            public void Constructor_Throws_ArgumentNullException_When_NullMapper()
            {
                // ARRANGE

                // Crear un mock del repositorio ISeatRepository
                var mockRepository = new Mock<ISeatRepository>();

                // Establecer el mapeador como nulo
                IMapperAdapter? mapper = null;

                // ACT

                // Crear una instancia del servicio de asiento y capturar cualquier excepción que ocurra durante la creación
                var result = () => new SeatService(mockRepository.Object, mapper);

                // ASSERT

                // Verificar que el resultado de la creación del servicio no sea nulo
                Assert.NotNull(result);

                // Verificar que la creación del servicio lance una excepción de tipo ArgumentNullException
                Assert.Throws<ArgumentNullException>(result);
            }
        }

        public class GetAll
        {
            /// <summary>
            /// Prueba para verificar que se devuelvan todos los asientos correctamente.
            /// </summary>
            [Fact]
            public async void GetAllAsync_Returns_AllSeats_When_SeatsExistInRepository()
            {
                // ARRANGE

                // Lista de asientos simulada
                var seats = new List<Seat>
                {
                    new() { Name = "Q-1", IsBlocked = false, Description = "This seat is located near the window." },
                    new() { Name = "M-2", IsBlocked = true, Description = "This seat is reserved for VIP guests." }
                };

                // Configuración del mapeador
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Mock del adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<IEnumerable<Seat>, IEnumerable<SeatResponse>>(seats))
                    .Returns(mapper.Map<IEnumerable<Seat>, IEnumerable<SeatResponse>>(seats));

                // Mock del repositorio ISeatRepository
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetAllAsNoTrackingAsync()).ReturnsAsync(seats);

                // Crear una instancia del servicio de asiento con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Obtener todos los asientos y esperar el resultado
                var result = await service.GetAllAsync();

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que el número de asientos devueltos sea el mismo que el número de asientos en la lista simulada
                Assert.Equal(seats.Count, result.Count());

                // Verificar que todos los asientos devueltos tengan éxito
                Assert.True(result.All(p => p.Success));

                // Verificar que los detalles de cada asiento devuelto coincidan con los detalles de los asientos simulados
                Assert.Equal(
                    seats.Select(u => new { u.Name, u.IsBlocked, u.Description }),
                    result.Select(u => new { u.Name, u.IsBlocked, u.Description })
                );
            }

            /// <summary>
            /// Prueba para verificar que se devuelva una lista vacía cuando no hay asientos.
            /// </summary>
            [Fact]
            public async void GetAllAsync_Returns_EmptyCollection_When_NoSeatsInRepository()
            {
                // ARRANGE

                // Crear una lista vacía de asientos
                var seats = new List<Seat>();

                // Crear un mock del adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Configurar un mock para el repositorio ISeatRepository y establecer su comportamiento para devolver la lista vacía de asientos
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetAllAsNoTrackingAsync()).ReturnsAsync(seats);

                // Crear una instancia del servicio de asiento (SeatService) con el mock del repositorio y del mapeador
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método GetAllAsync y obtener el resultado
                var result = await service.GetAllAsync();

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que el resultado esté vacío
                Assert.Empty(result);

                // Verificar que el número de elementos en el resultado sea igual al número de asientos en la lista de asientos de prueba
                Assert.Equal(seats.Count, result.Count());

                // Verificar que todas las respuestas tengan la propiedad Success establecida en true
                Assert.True(result.All(p => p.Success));
            }

            /// <summary>
            /// Prueba para verificar el manejo de excepciones al intentar obtener todos los asientos.
            /// </summary>
            [Fact]
            public async void GetAllAsync_Returns_NegativeResponse_When_ExceptionThrown()
            {
                // ARRANGE

                // Crear una excepción simulada y obtener su mensaje de error
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Crear un mock del adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Configurar un mock para el repositorio ISeatRepository y establecer su comportamiento para lanzar una excepción al llamar a GetAllAsync
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetAllAsNoTrackingAsync()).ThrowsAsync(exception);

                // Crear una instancia del servicio de asiento (SeatService) con el mock del repositorio y del mapeador
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método GetAllAsync y obtener el resultado
                var result = await service.GetAllAsync();

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que haya una única respuesta en el resultado
                Assert.Single(result);

                // Verificar que Success en la respuesta sea false
                Assert.False(result.First().Success);

                // Verificar que el mensaje de la excepción coincida con el mensaje de la respuesta
                Assert.Equal(errorMessage, result.First().Message);
            }
        }

        public class GetById
        {
            /// <summary>
            /// Prueba para verificar la obtención de un asiento existente por su ID.
            /// </summary>
            [Fact]
            public async void GetByIdAsync_Returns_SuccessfulResponse_When_ValidRequest()
            {
                // ARRANGE

                // Crear un asiento existente para la prueba
                var existingSeat = new Seat
                {
                    Id = Guid.NewGuid(),
                    Name = "John",
                    IsBlocked = false,
                    Description = "This seat is located near the window.",
                };

                // Crear un mapeador AutoMapper para simular el mapeo de un asiento a una respuesta de asiento
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Configurar un mock para el adaptador IMapperAdapter y establecer su comportamiento
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<Seat, SeatResponse>(existingSeat))
                    .Returns(mapper.Map<Seat, SeatResponse>(existingSeat));

                // Configurar un mock para el repositorio ISeatRepository y establecer su comportamiento para devolver el asiento existente cuando se llame a GetByIdAsync con su ID
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(existingSeat.Id)).ReturnsAsync(existingSeat);

                // Crear una instancia del servicio de asiento (SeatService) con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método GetByIdAsync con el ID del asiento existente y obtener el resultado
                var result = await service.GetByIdAsync(existingSeat.Id);

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea true
                Assert.True(result.Success);

                // Verificar que las propiedades Name, IsBlocked y Description en la respuesta sean iguales a las del asiento existente
                Assert.Equal((existingSeat.Name, existingSeat.IsBlocked, existingSeat.Description),
                             (result.Name, result.IsBlocked, result.Description));
            }

            /// <summary>
            /// Prueba para verificar que se lance una ArgumentException cuando se intenta obtener un asiento por su ID y el asiento no existe.
            /// </summary>
            [Fact]
            public async void GetByIdAsync_Returns_NegativeResponse_When_RequestNotFound()
            {
                // ARRANGE

                // Crear un asiento nulo para simular un asiento que no existe en el repositorio
                Seat? nullSeat = null;

                // Configurar un mock para el repositorio ISeatRepository y establecer su comportamiento para devolver un asiento nulo cuando se llame a GetByIdAsync con cualquier ID
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(nullSeat);

                // Crear un mock del adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear una instancia del servicio de asiento (SeatService) con el mock del repositorio y del mapeador
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // Generar un ID de asiento aleatorio
                var seatId = Guid.NewGuid();

                // ACT

                // Llamar al método GetByIdAsync con un ID de asiento que no existe y obtener el resultado
                var result = await service.GetByIdAsync(seatId);

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);

                // Verificar que el mensaje de la respuesta contenga el ID de asiento no encontrado
                Assert.Contains($"Entity with id {seatId} not found", result.Message);
            }

            /// <summary>
            /// Prueba para verificar que GetByIdAsync devuelve una respuesta negativa cuando se produce una excepción al buscar un asiento por su ID.
            /// </summary>
            [Fact]
            public async void GetByIdAsync_Returns_NegativeResponse_ExceptionThrown()
            {
                // ARRANGE

                // Crear una excepción simulada y obtener su mensaje de error
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Configurar un mock para el repositorio ISeatRepository y establecer su comportamiento para lanzar una excepción al llamar a GetByIdAsync con cualquier ID
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new Exception());

                // Crear un mock del adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear una instancia del servicio de asiento (SeatService) con el mock del repositorio y del mapeador
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método GetByIdAsync con un ID de asiento y obtener el resultado
                var result = await service.GetByIdAsync(Guid.NewGuid());

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);

                // Verificar que el mensaje de la respuesta coincida con el mensaje de la excepción
                Assert.Equal(errorMessage, result.Message);
            }
        }

        public class Create
        {
            /// <summary>
            /// Prueba para verificar que CreateAsync agrega un nuevo asiento cuando se proporciona una entrada válida.
            /// </summary>
            [Fact]
            public async void CreateAsync_Returns_SuccessfulResponse_When_ValidRequest()
            {
                // ARRANGE

                // Crear un mapeador AutoMapper para simular el mapeo de una solicitud de asiento a un asiento y de un asiento a una respuesta de asiento
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Crear una solicitud de asiento válida
                var seatRequest = new SeatRequest
                {
                    Name = "Q-1",
                    IsBlocked = false,
                    Description = "This seat is located near the window."
                };

                // Mapear la solicitud de asiento a un asiento
                var seat = mapper.Map<SeatRequest, Seat>(seatRequest);

                // Mapear el asiento a una respuesta de asiento
                var seatResponse = mapper.Map<Seat, SeatResponse>(seat);

                // Configurar un mock para el adaptador IMapperAdapter y establecer su comportamiento
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<SeatRequest, Seat>(It.IsAny<SeatRequest>()))
                    .Returns(mapper.Map<SeatRequest, Seat>(seatRequest));
                mockMapper.Setup(m => m.Map<Seat, SeatResponse>(It.IsAny<Seat>()))
                    .Returns(mapper.Map<Seat, SeatResponse>(seat));

                // Configurar un mock para el repositorio ISeatRepository y establecer su comportamiento para agregar un asiento correctamente
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.Add(It.IsAny<Seat>())).ReturnsAsync(true);

                // Crear una instancia del servicio de asiento (SeatService) con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método CreateAsync con la solicitud de asiento y obtener el resultado
                var result = await service.CreateAsync(seatRequest);

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea true
                Assert.True(result.Success);

                // Verificar que las propiedades Name, IsBlocked y Description en la respuesta sean iguales a las de la solicitud de asiento
                Assert.Equal((seatRequest.Name, seatRequest.IsBlocked, seatRequest.Description),
                    (result.Name, result.IsBlocked, result.Description));
            }

            /// <summary>
            /// Prueba para verificar que CreateAsync lance una excepción de ArgumentNullException cuando se proporciona una solicitud nula.
            /// </summary>
            [Fact]
            public async void CreateAsync_Returns_NegativeResponse_When_NullRequest()
            {
                // ARRANGE

                // Crear una excepción simulada de ArgumentNullException y obtener su mensaje de error
                var exception = new ArgumentNullException("The request object cannot be null");
                var errorMessage = exception.Message;

                // Crear una solicitud de asiento nula
                SeatRequest? seat = null;

                // Configurar un mock para el repositorio ISeatRepository
                var mockRepository = new Mock<ISeatRepository>();

                // Configurar un mock para el adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear una instancia del servicio de asiento (SeatService) con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método CreateAsync con una solicitud de asiento nula y obtener el resultado
                var result = await service.CreateAsync(seat);

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);

                // Verificar que el mensaje de la respuesta coincida con el mensaje de la excepción de ArgumentNullException
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Prueba para verificar que CreateAsync lance una excepción de ValidationException cuando se proporcionan datos de entrada inválidos.
            /// </summary>
            [Theory]
            [InlineData("", false, "This seat is located near the window.")]
            [InlineData("", false, "")]
            [InlineData("", true, null)]
            [InlineData(null, true, "This seat is located near the window.")]
            [InlineData(null, true, "")]
            [InlineData(null, true, null)]
            public async void CreateAsync_Returns_NegativeResponse_When_InvalidRequest(string? name, bool isBlocked, string? description)
            {
                // ARRANGE

                // Crear una excepción simulada de ValidationException y obtener su mensaje de error
                var exception = new ValidationException("Argument is invalid.");
                var errorMessage = exception.Message;

                // Crear una solicitud de asiento con los datos de entrada proporcionados
                var seat = new SeatRequest
                {
                    Name = name,
                    IsBlocked = isBlocked,
                    Description = description
                };

                // Crear un mapeador AutoMapper para simular el mapeo de la solicitud de asiento a un asiento
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Configurar un mock para el adaptador IMapperAdapter y establecer su comportamiento
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<SeatRequest, Seat>(seat))
                    .Returns(mapper.Map<SeatRequest, Seat>(seat));

                // Configurar un mock para el repositorio ISeatRepository
                var mockRepository = new Mock<ISeatRepository>();

                // Crear una instancia del servicio de asiento (SeatService) con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método CreateAsync con la solicitud de asiento y obtener el resultado
                var result = await service.CreateAsync(seat);

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);

                // Verificar que el mensaje de la respuesta coincida con el mensaje de la excepción de ValidationException
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Verifica que el método CreateAsync lance una InvalidOperationException cuando el repositorio no puede agregar la entidad.
            /// </summary>
            [Fact]
            public async void CreateAsync_Returns_NegativeResponse_When_RepositoryFailsToAdd()
            {
                // ARRANGE

                // Crear una excepción InvalidOperationException simulada para representar la incapacidad del repositorio para agregar la entidad
                var exception = new InvalidOperationException("The entity could not be added.");
                var errorMessage = exception.Message;

                // Crear una solicitud de asiento (SeatRequest) para utilizarla como entrada para la creación de asientos
                var seat = new SeatRequest
                {
                    Name = "Q-1",
                    IsBlocked = false,
                    Description = "This seat is located near the window."
                };

                // Crear un mapeador de AutoMapper para simular la conversión de la solicitud de asiento a un objeto de asiento
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Configurar un mock para el adaptador IMapperAdapter y establecer su comportamiento para mapear la solicitud de asiento a un objeto de asiento
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<SeatRequest, Seat>(seat))
                    .Returns(mapper.Map<SeatRequest, Seat>(seat));

                // Configurar un mock para el repositorio ISeatRepository y establecer su comportamiento para devolver false al intentar agregar una entidad
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.Add(It.IsAny<Seat>())).ReturnsAsync(false);

                // Crear una instancia del servicio de asiento (SeatService) con los mocks del repositorio y del mapeador
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método CreateAsync del servicio con la solicitud de asiento y obtener el resultado
                var result = await service.CreateAsync(seat);

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);

                // Verificar que el mensaje de la excepción coincida con el mensaje esperado
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Verifica que el método CreateAsync lance una excepción cuando el mapeador no puede mapear la solicitud de asiento a un objeto de asiento.
            /// </summary>
            [Fact]
            public async void CreateAsync_Returns_NegativeResponse_ExceptionThrown()
            {
                // ARRANGE

                // Crear una excepción genérica para simular la situación en la que el mapeador no puede mapear la solicitud de asiento a un objeto de asiento
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Configurar un mock para el adaptador IMapperAdapter y establecer su comportamiento para lanzar una excepción cuando se llame al método Map con cualquier solicitud de asiento
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<SeatRequest, Seat>(It.IsAny<SeatRequest>())).Throws(new Exception());

                // Configurar un mock para el repositorio ISeatRepository y establecer su comportamiento para devolver true al intentar agregar una entidad
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.Add(It.IsAny<Seat>())).ReturnsAsync(true);

                // Crear una instancia del servicio de asiento (SeatService) con los mocks del repositorio y del mapeador
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método CreateAsync del servicio con una solicitud de asiento genérica y obtener el resultado
                var result = await service.CreateAsync(new SeatRequest());

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);

                // Verificar que el mensaje de la excepción coincida con el mensaje esperado
                Assert.Equal(errorMessage, result.Message);
            }
        }

        public class Update
        {
            /// <summary>
            /// Prueba para verificar que UpdateAsync actualiza un asiento existente cuando se proporciona una entrada válida o inválida.
            /// </summary>
            [Theory]
            [InlineData("Q-1", false, "This seat is located near the window.")]
            [InlineData("Q-1", false, "")]
            [InlineData("Q-1", true, null)]
            [InlineData("", false, "This seat is located near the window.")]
            [InlineData(null, false, "This seat is located near the window.")]
            [InlineData("", true, "")]
            [InlineData(null, true, null)]
            public async void UpdateAsync_Returns_SuccessfulResponse_When_ValidRequest(string? name, bool isBlocked, string? description)
            {
                // ARRANGE

                // Identificador único para el asiento existente
                var seatId = Guid.NewGuid();

                // Crear una solicitud de asiento con los datos proporcionados en los argumentos
                var seatRequest = new SeatRequest
                {
                    Name = name,
                    IsBlocked = isBlocked,
                    Description = description
                };

                // Asiento existente para ser actualizado
                var existingSeat = new Seat
                {
                    Name = "M-2",
                    IsBlocked = true,
                    Description = "This seat is reserved for VIP guests."
                };

                // Respuesta de asiento esperada después de la actualización
                var seatResponse = new SeatResponse
                {
                    // Se asigna el nombre del asiento existente si el nombre en la solicitud es nulo o vacío
                    Name = string.IsNullOrEmpty(seatRequest.Name) ? existingSeat.Name : seatRequest.Name,
                    // Se asigna el estado de bloqueado
                    IsBlocked = seatRequest.IsBlocked,
                    // Se asigna la descripción del asiento existente si la descripción en la solicitud es nulo o vacío
                    Description = string.IsNullOrEmpty(seatRequest.Description) ? existingSeat.Description : seatRequest.Description,
                };

                // Configuración del mock para el repositorio ISeatRepository y establecimiento de su comportamiento para obtener un asiento por su ID y actualizar un asiento correctamente
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingSeat);
                mockRepository.Setup(r => r.Update(It.IsAny<Seat>())).ReturnsAsync(true);

                // Crear un mapeador AutoMapper para mapear un asiento a una respuesta de asiento
                var mapper = TestAutoMapperFactory.CreateMapper();
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<Seat, SeatResponse>(It.IsAny<Seat>()))
                    .Returns((Seat seat) => mapper.Map<Seat, SeatResponse>(seat));

                // Crear una instancia del servicio de asiento (SeatService) con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método UpdateAsync con el ID del asiento existente y la solicitud de asiento, y obtener el resultado
                var result = await service.UpdateAsync(existingSeat.Id, seatRequest);

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea true
                Assert.True(result.Success);
                // Verificar que las propiedades Name, IsBlocked y Description en la respuesta sean iguales a las de la respuesta de asiento esperada
                Assert.Equal((seatResponse.Name, seatResponse.IsBlocked, seatResponse.Description),
                             (result.Name, result.IsBlocked, result.Description));
            }

            /// <summary>
            /// Prueba para verificar que UpdateAsync lanza ArgumentNullException cuando se proporciona una solicitud nula.
            /// </summary>
            [Fact]
            public async void UpdateAsync_Returns_NegativeResponse_When_NullRequest()
            {
                // ARRANGE

                // Crear una excepción de ArgumentNullException con el mensaje adecuado
                var exception = new ArgumentNullException("The request object cannot be null");
                var errorMessage = exception.Message;

                // Establecer la solicitud de asiento como nula
                SeatRequest? seat = null;

                // Configurar un mock para el repositorio ISeatRepository y el adaptador IMapperAdapter
                var mockRepository = new Mock<ISeatRepository>();
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear una instancia del servicio de asiento (SeatService) con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método UpdateAsync con un ID aleatorio y una solicitud de asiento nula, y obtener el resultado
                var result = await service.UpdateAsync(Guid.NewGuid(), seat);

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción ArgumentNullException
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Prueba para verificar que UpdateAsync lanza ArgumentException cuando no se encuentra el asiento.
            /// </summary>
            [Fact]
            public async void UpdateAsync_Returns_NegativeResponse_When_RequestNotFound()
            {
                // ARRANGE
                // Generar un nuevo ID de asiento
                var seatId = Guid.NewGuid();
                // Establecer el asiento como nulo
                Seat? seat = null;

                // Crear una excepción de ArgumentException con el mensaje adecuado
                var exception = new ArgumentException($"Entity with id {seatId} not found");
                var errorMessage = exception.Message;

                // Configurar un mock para el adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Configurar un mock para el repositorio ISeatRepository y establecer su comportamiento para devolver un asiento nulo cuando se busque por ID
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(seat);

                // Crear una instancia del servicio de asiento (SeatService) con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método UpdateAsync con un ID de asiento y una solicitud de asiento, y obtener el resultado
                var result = await service.UpdateAsync(seatId, new SeatRequest());

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción ArgumentException
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Prueba para verificar que UpdateAsync lanza ValidationException cuando los datos de asiento actualizado son inválidos.
            /// </summary>
            [Theory]
            [InlineData("", true, "This seat is located near the window.")]
            [InlineData(null, true, "This seat is located near the window.")]
            [InlineData("", false, "")]
            [InlineData(null, false, null)]
            public async void UpdateAsync_Returns_NegativeResponse_When_InvalidRequest(string? name, bool isBlocked, string? description)
            {
                // ARRANGE
                // Crear una excepción de ValidationException con el mensaje adecuado
                var exception = new ValidationException("Argument is invalid.");
                var errorMessage = exception.Message;

                // Crear un asiento existente con los datos proporcionados en los argumentos
                var existingSeat = new Seat
                {
                    Name = name,
                    IsBlocked = isBlocked,
                    Description = description
                };

                // Configurar un mock para el adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Configurar un mock para el repositorio ISeatRepository y establecer su comportamiento para devolver un asiento existente cuando se busque por ID
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingSeat);

                // Crear una instancia del servicio de asiento (SeatService) con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método UpdateAsync con un ID aleatorio y una solicitud de asiento vacía, y obtener el resultado
                var result = await service.UpdateAsync(Guid.NewGuid(), new SeatRequest());

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción ValidationException
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Prueba para verificar que UpdateAsync lanza InvalidOperationException cuando el repositorio no puede actualizar la entidad.
            /// </summary>
            [Theory]
            [InlineData("Q-1", true, "This seat is located near the window.")]
            public async void UpdateAsync_Returns_NegativeResponse_When_RepositoryFailsToUpdate(string name, bool isBlcoked, string? description)
            {
                // ARRANGE
                // Crear una excepción de InvalidOperationException con el mensaje adecuado
                var exception = new InvalidOperationException("The entity could not be updated.");
                var errorMessage = exception.Message;

                // Crear un asiento existente con los datos proporcionados en los argumentos
                var existingSeat = new Seat
                {
                    Name = name,
                    IsBlocked = isBlcoked,
                    Description = description
                };

                // Configurar un mock para el repositorio ISeatRepository y establecer su comportamiento para devolver un asiento existente cuando se busque por ID y para devolver false cuando se intente actualizar la entidad
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingSeat);
                mockRepository.Setup(r => r.Update(It.IsAny<Seat>())).ReturnsAsync(false);

                // Configurar un mock para el adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear una instancia del servicio de asiento (SeatService) con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método UpdateAsync con un ID aleatorio y una solicitud de asiento vacía, y obtener el resultado
                var result = await service.UpdateAsync(Guid.NewGuid(), new SeatRequest());

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción InvalidOperationException
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Prueba para verificar que UpdateAsync lance una excepción cuando el mapeador no puede mapear los datos del asiento.
            /// </summary>
            [Theory]
            [InlineData("Q-1", true, "This seat is located near the window.")]
            public void UpdateAsync_Returns_NegativeResponse_ExceptionThrown(string name, bool isBlcoked, string? description)
            {
                // ARRANGE

                // Crear una excepción genérica para simular el escenario en el que el mapeador no puede mapear los datos del asiento
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Crear un asiento existente con los datos proporcionados
                var existingSeat = new Seat
                {
                    Name = name,
                    IsBlocked = isBlcoked,
                    Description = description
                };

                // Configurar mocks para el repositorio ISeatRepository y el adaptador IMapperAdapter
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingSeat);
                mockRepository.Setup(r => r.Update(It.IsAny<Seat>())).ReturnsAsync(true);

                var mockMapper = new Mock<IMapperAdapter>();
                // Configurar el comportamiento del mapeador para lanzar una excepción al intentar mapear un asiento a una respuesta de asiento
                mockMapper.Setup(m => m.Map<Seat, SeatResponse>(It.IsAny<Seat>())).Throws(new Exception());

                // Crear una instancia del servicio de asiento (SeatService) con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método UpdateAsync con un nuevo identificador y una solicitud de asiento vacía y obtener el resultado
                var result = service.UpdateAsync(Guid.NewGuid(), new SeatRequest())?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción
                Assert.Equal(errorMessage, result.Message);
            }
        }

        public class Delete
        {
            /// <summary>
            /// Verifica que DeleteAsync elimine correctamente un asiento existente.
            /// </summary>
            [Theory]
            [InlineData("Q-1", true, "This seat is located near the window.")]
            [InlineData("M-2", false, "This seat is reserved for VIP guests.")]
            public async void DeleteAsync_Returns_SuccessfulResponse_When_ValidRequest(string name, bool isBlcoked, string? description)
            {
                // ARRANGE
                var existingSeat = new Seat
                {
                    Name = name,
                    IsBlocked = isBlcoked,
                    Description = description
                };

                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingSeat);
                mockRepository.Setup(r => r.Remove(It.IsAny<Seat>())).ReturnsAsync(true);

                var mapper = TestAutoMapperFactory.CreateMapper();
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<Seat, SeatResponse>(It.IsAny<Seat>())).
                    Returns((Seat seat) => mapper.Map<Seat, SeatResponse>(seat));

                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT
                var result = await service.DeleteAsync(Guid.NewGuid());

                // ASSERT
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.Equal((existingSeat.Name, existingSeat.IsBlocked, existingSeat.Description),
                            (result.Name, result.IsBlocked, result.Description));
            }

            /// <summary>
            /// Verifica que DeleteAsync lance una excepción al intentar eliminar un asiento inexistente.
            /// </summary>
            [Fact]
            public async void DeleteAsync_Returns_NegativeResponse_When_RequestNotFound()
            {
                // ARRANGE
                // Generar un nuevo ID de asiento
                var seatId = Guid.NewGuid();

                // Crear una excepción de ArgumentNullException con el mensaje adecuado
                var exception = new ArgumentNullException($"Entity with id {seatId} not found");
                var errorMessage = exception.Message;

                // Establecer el asiento como nulo
                Seat? seat = null;

                // Configurar un mock para el repositorio ISeatRepository y establecer su comportamiento para devolver un asiento nulo cuando se busque por ID
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(seat);

                // Configurar un mock para el adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear una instancia del servicio de asiento (SeatService) con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método DeleteAsync con un ID aleatorio y obtener el resultado
                var result = await service.DeleteAsync(seatId);

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción ArgumentNullException
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Verifica que DeleteAsync lance una excepción InvalidOperationException cuando el repositorio no puede eliminar la entidad.
            /// </summary>
            [Theory]
            [InlineData("Q-1", true, "This seat is located near the window.")]
            [InlineData("M-2", false, "This seat is reserved for VIP guests.")]
            public async void DeleteAsync_Returns_NegativeResponse_When_RepositoryFailsToDelete(string name, bool isBlcoked, string? description)
            {
                // ARRANGE
                // Crear una excepción de InvalidOperationException con el mensaje adecuado
                var exception = new InvalidOperationException("The entity could not be deleted.");
                var errorMessage = exception.Message;

                // Crear un asiento existente con los datos proporcionados en los argumentos
                var existingSeat = new Seat
                {
                    Name = name,
                    IsBlocked = isBlcoked,
                    Description = description
                };

                // Configurar un mock para el repositorio ISeatRepository y establecer su comportamiento para devolver un asiento existente cuando se busque por ID y para devolver false cuando se intente eliminar la entidad
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingSeat);
                mockRepository.Setup(r => r.Remove(It.IsAny<Seat>())).ReturnsAsync(false);

                // Configurar un mock para el adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear una instancia del servicio de asiento (SeatService) con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método DeleteAsync con un ID aleatorio y obtener el resultado
                var result = await service.DeleteAsync(Guid.NewGuid());

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción InvalidOperationException
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Verifica que DeleteAsync lance una excepción cuando el mapeador no puede mapear la entidad.
            /// </summary>
            [Theory]
            [InlineData("Q-1", true, "This seat is located near the window.")]
            [InlineData("M-2", false, "This seat is reserved for VIP guests.")]
            public void DeleteAsync_Returns_NegativeResponse_ExceptionThrown(string name, bool isBlcoked, string? description)
            {
                // ARRANGE
                // Crear una excepción genérica
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Crear un asiento existente con los datos proporcionados en los argumentos
                var existingSeat = new Seat
                {
                    Name = name,
                    IsBlocked = isBlcoked,
                    Description = description
                };

                // Configurar un mock para el repositorio ISeatRepository y establecer su comportamiento para devolver un asiento existente cuando se busque por ID y para devolver true cuando se intente eliminar la entidad
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingSeat);
                mockRepository.Setup(r => r.Remove(It.IsAny<Seat>())).ReturnsAsync(true);

                // Crear un mapeador AutoMapper
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Configurar un mock para el adaptador IMapperAdapter y establecer su comportamiento para lanzar una excepción al mapear el asiento a una respuesta de asiento
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<Seat, SeatResponse>(It.IsAny<Seat>())).Throws(new Exception());

                // Crear una instancia del servicio de asiento (SeatService) con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método DeleteAsync con un ID aleatorio y obtener el resultado
                var result = service.DeleteAsync(Guid.NewGuid())?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción genérica
                Assert.Equal(errorMessage, result.Message);
            }
        }

        public class GetAvailabe
        {
            /// <summary>
            /// Prueba para verificar que se devuelven correctamente todos los asientos disponibles.
            /// </summary>
            [Fact]
            public async void GetAvailablesAsync_Returns_SuccessfulResponse_When_AvailabilityExists()
            {
                // ARRANGE

                // Lista de asientos simulada
                var seats = new List<Seat>
                {
                    new() { Name = "Q-1", IsBlocked = false, Description = "This seat is located near the window." },
                    new() { Name = "M-2", IsBlocked = true, Description = "This seat is reserved for VIP guests." },
                    new() { Name = "M-1", IsBlocked = false, Description = "This seat is in the back row." },
                    new() { Name = "M-3", IsBlocked = false, Description = "This seat has extra legroom." }
                };
                var availableSeats = seats.Where(s => !s.IsBlocked);

                // Configuración del mapeador
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Mock del repositorio ISeatRepository
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetAvailablesAsNoTrackingAsync()).ReturnsAsync(availableSeats);

                // Mock del adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<IEnumerable<Seat>, IEnumerable<SeatResponse>>(It.IsAny<IEnumerable<Seat>>()))
                    .Returns(mapper.Map<IEnumerable<Seat>, IEnumerable<SeatResponse>>(availableSeats));

                // Crear una instancia del servicio de asiento con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Obtener todos los asientos disponibles y esperar el resultado
                var result = await service.GetAvailablesAsync();

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que el resultado no sea vacío
                Assert.NotEmpty(result);
                // Verificar que el número de asientos disponibles  devueltos sea el mismo que el número de asientos disponibles en la lista simulada
                Assert.Equal(availableSeats.Count(), result.Count());

                // Verificar que todos los asientos devueltos tengan éxito
                Assert.True(result.All(p => p.Success));
                Assert.False(result.All(p => p.IsBlocked));

                // Verificar que los detalles de cada asiento devuelto coincidan con los detalles de los asientos simulados
                Assert.Equal(
                    availableSeats.Select(u => new { u.Name, u.IsBlocked, u.Description }),
                    result.Select(u => new { u.Name, u.IsBlocked, u.Description })
                );
            }

            /// <summary>
            /// Prueba para verificar que se devuelva una lista vacía cuando no hay asientos disponibles.
            /// </summary>
            [Fact]
            public async void GetAvailablesAsync_Returns_EmptyCollection_When_NoAvailability()
            {
                // ARRANGE

                // Configuración del mapeador
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Mock del adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<IEnumerable<Seat>, IEnumerable<SeatResponse>>(It.IsAny<IEnumerable<Seat>>()))
                    .Returns((IEnumerable<Seat> seats) => mapper.Map<IEnumerable<Seat>, IEnumerable<SeatResponse>>(seats));

                // Mock del repositorio ISeatRepository
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetAvailablesAsNoTrackingAsync()).ReturnsAsync([]);

                // Crear una instancia del servicio de asiento con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Obtener todos los asientos disponibles y esperar el resultado
                var result = await service.GetAvailablesAsync();

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que el resultado sea vacío
                Assert.Empty(result);
            }

            /// <summary>
            /// Prueba para verificar que se maneje correctamente una excepción al obtener los asientos disponibles.
            /// </summary>
            [Fact]
            public async void GetAvailablesAsync_Returns_NegativeResponse_When_ExceptionThrown()
            {
                // ARRANGE
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Mock del adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Mock del repositorio ISeatRepository
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetAvailablesAsNoTrackingAsync()).ThrowsAsync(exception);

                // Crear una instancia del servicio de asiento con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Obtener todos los asientos disponibles y esperar el resultado
                var result = await service.GetAvailablesAsync();

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que haya una única respuesta en el resultado
                Assert.Single(result);

                // Verificar que Success en la respuesta sea false
                Assert.False(result.First().Success);

                // Verificar que el mensaje de la excepción coincida con el mensaje de la respuesta
                Assert.Equal(errorMessage, result.First().Message);
            }
        }

        public class GetByName
        {
            /// <summary>
            /// Prueba para verificar que se obtenga correctamente un asiento existente por su nombre.
            /// </summary>
            [Fact]
            public async void GetByNameAsync_Returns_SuccessfulResponse_When_ValidName()
            {
                // ARRANGE

                // Crear un asiento existente para la prueba
                var existingSeat = new Seat
                {
                    Name = "John",
                    IsBlocked = false,
                    Description = "This seat is located near the window.",
                };

                // Crear un mapeador AutoMapper para simular el mapeo de un asiento a una respuesta de asiento
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Configurar un mock para el repositorio ISeatRepository y establecer su comportamiento para devolver el asiento existente cuando se llame a GetByNameAsync con su nombre
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(existingSeat);

                // Configurar un mock para el adaptador IMapperAdapter y establecer su comportamiento
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<Seat, SeatResponse>(It.IsAny<Seat>()))
                    .Returns(mapper.Map<Seat, SeatResponse>(existingSeat));

                // Crear una instancia del servicio de asiento (SeatService) con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método GetSeatByNameAsync con el nombre del asiento existente y obtener el resultado
                var result = await service.GetByNameAsync(existingSeat.Name);

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea true
                Assert.True(result.Success);

                // Verificar que las propiedades Name, IsBlocked y Description en la respuesta sean iguales a las del asiento existente
                Assert.Equal((existingSeat.Name, existingSeat.IsBlocked, existingSeat.Description),
                             (result.Name, result.IsBlocked, result.Description));
            }

            /// <summary>
            /// Prueba para verificar que se lance una excepción ArgumentNullException cuando el nombre del asiento es nulo o vacío.
            /// </summary>
            /// <param name="name">El nombre del asiento.</param>
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            public async void GetByNameAsync_Returns_NegativeResponse_When_NullorEmptyName(string? name)
            {
                // ARRANGE

                // Crear una excepción simulada de ArgumentNullException
                var exception = new ArgumentNullException(nameof(name), "The name cannot be null or empty");
                var errorMessage = exception.Message;

                // Configurar un mock para el repositorio ISeatRepository
                var mockRepository = new Mock<ISeatRepository>();

                // Configurar un mock para el adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear una instancia del servicio de asiento con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método GetSeatByNameAsync con el nombre del asiento y obtener el resultado
                var result = await service.GetByNameAsync(name);

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);

                // Verificar que el mensaje de la respuesta coincida con el mensaje de la excepción simulada
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Prueba para verificar que se lance una excepción ArgumentException cuando el asiento es nulo al buscar por nombre.
            /// </summary>
            /// <param name="name">El nombre del asiento.</param>
            [Theory]
            [InlineData("Q-1")]
            [InlineData("M-1")]
            [InlineData("M-2")]
            public async void GetByNameAsync_Returns_NegativeResponse_When_SeatNotFound(string name)
            {
                // ARRANGE

                // Crear una excepción simulada de ArgumentException
                var exception = new ArgumentException($"Seat {name} not found");
                var errorMessage = exception.Message;

                // Establecer el asiento como nulo
                Seat? seat = null;

                // Configurar un mock para el repositorio ISeatRepository y establecer su comportamiento para devolver un asiento nulo cuando se llame a GetByNameAsync con cualquier nombre
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(seat);

                // Configurar un mock para el adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear una instancia del servicio de asiento (SeatService) con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método GetSeatByNameAsync con el nombre del asiento y obtener el resultado
                var result = await service.GetByNameAsync(name);

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);

                // Verificar que el mensaje de la respuesta coincida con el mensaje de la excepción simulada
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Prueba para verificar que se lance una excepción genérica cuando ocurre un error al buscar un asiento por nombre.
            /// </summary>
            /// <param name="name">El nombre del asiento.</param>
            [Theory]
            [InlineData("Q-1")]
            [InlineData("M-1")]
            [InlineData("M-2")]
            public async void GetByNameAsync_Returns_NegativeResponse_ExceptionThrown(string name)
            {
                // ARRANGE

                // Crear una excepción simulada de Exception
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Configurar un mock para el repositorio ISeatRepository y establecer su comportamiento para devolver un asiento simulado cuando se llame a GetByNameAsync
                var mockRepository = new Mock<ISeatRepository>();
                mockRepository.Setup(r => r.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(new Seat());

                // Configurar un mock para el adaptador IMapperAdapter y establecer su comportamiento para lanzar una excepción al mapear el asiento
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<Seat, SeatResponse>(It.IsAny<Seat>())).Throws(new Exception());

                // Crear una instancia del servicio de asiento (SeatService) con los mocks configurados
                var service = new SeatService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método GetSeatByNameAsync con el nombre del asiento y obtener el resultado
                var result = await service.GetByNameAsync(name);

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);

                // Verificar que el mensaje de la respuesta coincida con el mensaje de la excepción simulada
                Assert.Equal(errorMessage, result.Message);
            }
        }
    }
}

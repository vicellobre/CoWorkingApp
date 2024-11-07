using CoWorkingApp.Core.Application.Contracts.Adapters;
using CoWorkingApp.Core.Application.Contracts.Repositories;
using CoWorkingApp.Core.Application.Services;
using CoWorkingApp.Core.Domain.DTOs;
using CoWorkingApp.Core.Domain.Entities;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace CoWorkingApp.Tests.Core.Application.Services
{
    /// <summary>
    /// Clase que contiene pruebas unitarias para la clase UserService.
    /// </summary>
    public class UserServiceTest
    {
        
        public class Constructor
        {
            /// <summary>
            /// Prueba para verificar que los parámetros no sean nulos al construir el servicio de usuario.
            /// </summary>
            [Fact]
            public void Constructor_ReturnsInstance_When_ParametersAreNotNull()
            {
                // ARRANGE

                // Crear un mock del adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear un mock del repositorio IUserRepository
                var mockRepository = new Mock<IUserRepository>();

                // ACT

                // Crear una instancia del servicio de usuario y capturar cualquier excepción que ocurra durante la creación
                var result = () => new UserService(mockRepository.Object, mockMapper.Object);

                // ASSERT

                // Verificar que el resultado de la creación del servicio no sea nulo
                Assert.NotNull(result);
            }

            /// <summary>
            /// Prueba para verificar que se lance una excepción cuando se proporciona un repositorio nulo al constructor.
            /// </summary>
            [Fact]
            public void Constructor_Throws_ArgumentNullException_When_NullRepository()
            {
                // ARRANGE

                // Establecer el repositorio como nulo
                IUserRepository? repository = null;

                // Crear un mock del adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // ACT

                // Crear una instancia del servicio de usuario y capturar cualquier excepción que ocurra durante la creación
                var result = () => new UserService(repository, mockMapper.Object);

                // ASSERT

                // Verificar que el resultado de la creación del servicio no sea nulo
                Assert.NotNull(result);

                // Verificar que la creación del servicio lance una excepción de tipo ArgumentNullException
                Assert.Throws<ArgumentNullException>(result);
            }

            /// <summary>
            /// Prueba para verificar que se lance una excepción cuando se proporciona un mapeador nulo al constructor.
            /// </summary>
            [Fact]
            public void Constructor_Throws_ArgumentNullException_When_NullMapper()
            {
                // ARRANGE

                // Crear un mock del repositorio IUserRepository
                var mockRepository = new Mock<IUserRepository>();

                // Establecer el mapeador como nulo
                IMapperAdapter? mapper = null;

                // ACT

                // Crear una instancia del servicio de usuario y capturar cualquier excepción que ocurra durante la creación
                var result = () => new UserService(mockRepository.Object, mapper);

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
            /// Prueba para verificar que el método GetAllAsync devuelve todos los usuarios existentes.
            /// </summary>
            [Fact]
            public void GetAllAsync_Returns_AllUsers_When_UsersExistInRepository()
            {
                // ARRANGE

                // Crear una lista de usuarios de prueba
                var users = new List<User>
                {
                    new() { Id = Guid.NewGuid(), Name = "John", LastName = "Doe", Email = "john@example.com", Password = "123" },
                    new() { Id = Guid.NewGuid(), Name = "Jane", LastName = "Smith", Email = "jane@example.com", Password = "456" }
                };

                // Crear un mapeador AutoMapper para simular el mapeo de entidades de usuario a respuestas de usuario
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Configurar un mock para el adaptador IMapperAdapter y establecer su comportamiento
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<IEnumerable<User>, IEnumerable<UserResponse>>(users))
                    .Returns(mapper.Map<IEnumerable<User>, IEnumerable<UserResponse>>(users));

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método GetAllAsync y obtener el resultado
                var result = service.GetAllAsync()?.Result;

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que el número de elementos en el resultado sea igual al número de usuarios en la lista de usuarios de prueba
                Assert.Equal(users.Count, result.Count());

                // Verificar que todas las respuestas tengan la propiedad Success establecida en true
                Assert.True(result.All(p => p.Success));

                // Verificar que las propiedades Name, LastName y Email de cada usuario en el resultado sean iguales a las de la lista de usuarios de prueba
                Assert.Equal(
                    users.Select(u => new { u.Id, u.Name, u.LastName, u.Email }),
                    result.Select(u => new { u.Id, u.Name, u.LastName, u.Email })
                );
            }

            /// <summary>
            /// Prueba para verificar que GetAllAsync devuelve una lista vacía cuando no hay usuarios.
            /// </summary>
            [Fact]
            public void GetAllAsync_Returns_EmptyCollection_When_NoUsersInRepository()
            {
                // ARRANGE

                // Crear una lista de usuarios vacía
                var users = new List<User>();

                // Crear un mock del adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento para devolver la lista vacía de usuarios
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

                // Crear una instancia del servicio de usuario (UserService) con el mock del repositorio y del mapeador
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método GetAllAsync y obtener el resultado
                var result = service.GetAllAsync()?.Result;

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que el resultado esté vacío
                Assert.Empty(result);

                // Verificar que el número de elementos en el resultado sea igual al número de usuarios en la lista de usuarios de prueba
                Assert.Equal(users.Count, result.Count());

                // Verificar que todas las respuestas tengan la propiedad Success establecida en true
                Assert.True(result.All(p => p.Success));
            }

            [Fact]
            public void GetAllAsync_Returns_NegativeResponse_When_ExceptionThrown()
            {
                // ARRANGE

                // Crear una excepción simulada y obtener su mensaje de error
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Crear un mock del adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento para lanzar una excepción al llamar a GetAllAsync
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.GetAllAsync()).ThrowsAsync(new Exception());

                // Crear una instancia del servicio de usuario (UserService) con el mock del repositorio y del mapeador
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método GetAllAsync y obtener el resultado
                var result = service.GetAllAsync()?.Result;

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
            /// Prueba para verificar que GetByIdAsync devuelve un usuario existente cuando se proporciona un ID válido.
            /// </summary>
            [Fact]
            public void GetByIdAsync_Returns_SuccessfulResponse_When_ValidRequest()
            {
                // ARRANGE

                // Crear un usuario existente para la prueba
                var existingUser = new User
                {
                    Id = Guid.NewGuid(),
                    Name = "John",
                    LastName = "Doe",
                    Email = "john@example.com",
                    Password = "123"
                };

                // Crear un mapeador AutoMapper para simular el mapeo de un usuario a una respuesta de usuario
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Configurar un mock para el adaptador IMapperAdapter y establecer su comportamiento
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<User, UserResponse>(existingUser))
                    .Returns(mapper.Map<User, UserResponse>(existingUser));

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento para devolver el usuario existente cuando se llame a GetByIdAsync con su ID
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método GetByIdAsync con el ID del usuario existente y obtener el resultado
                var result = service.GetByIdAsync(existingUser.Id)?.Result;

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea true
                Assert.True(result.Success);

                // Verificar que las propiedades Name, LastName y Email en la respuesta sean iguales a las del usuario existente
                Assert.Equal((existingUser.Id, existingUser.Name, existingUser.LastName, existingUser.Email),
                             (result.Id, result.Name, result.LastName, result.Email));
            }

            /// <summary>
            /// Prueba para verificar que GetByIdAsync devuelve una excepción cuando se proporciona un ID de usuario que no existe.
            /// </summary>
            [Fact]
            public void GetByIdAsync_Returns_NegativeResponse_When_RequestNotFound()
            {
                // ARRANGE

                // Crear un usuario nulo para simular un usuario que no existe en el repositorio
                User? nullUser = null;

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento para devolver un usuario nulo cuando se llame a GetByIdAsync con cualquier ID
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(nullUser);

                // Crear un mock del adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear una instancia del servicio de usuario (UserService) con el mock del repositorio y del mapeador
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // Generar un ID de usuario aleatorio
                var userId = Guid.NewGuid();

                // ACT

                // Llamar al método GetByIdAsync con un ID de usuario que no existe y obtener el resultado
                var result = service.GetByIdAsync(userId)?.Result;

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);

                // Verificar que el mensaje de la respuesta contenga el ID de usuario no encontrado
                Assert.Contains($"Entity with id {userId} not found", result.Message);
            }

            /// <summary>
            /// Prueba para verificar que GetByIdAsync devuelve una respuesta negativa cuando se produce una excepción al buscar un usuario por su ID.
            /// </summary>
            [Fact]
            public void GetByIdAsync_Returns_NegativeResponse_When_ExceptionThrown()
            {
                // ARRANGE
                // Crear una excepción simulada y obtener su mensaje de error
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento para lanzar una excepción al llamar a GetByIdAsync con cualquier ID
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new Exception());

                // Crear un mock del adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear una instancia del servicio de usuario (UserService) con el mock del repositorio y del mapeador
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método GetByIdAsync con un ID de usuario y obtener el resultado
                var result = service.GetByIdAsync(Guid.NewGuid())?.Result;

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
            /// Prueba para verificar que CreateAsync agrega un nuevo usuario cuando se proporciona una entrada válida.
            /// </summary>
            [Fact]
            public void CreateAsync_Returns_SuccessfulResponse_When_ValidRequest()
            {
                // ARRANGE
                // Crear un mapeador AutoMapper para simular el mapeo de una solicitud de usuario a un usuario y de un usuario a una respuesta de usuario
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Crear una solicitud de usuario válida
                var userRequest = new UserRequest
                {
                    Name = "John",
                    LastName = "Doe",
                    Email = "john@example.com",
                    Password = "123"
                };

                // Mapear la solicitud de usuario a un usuario
                var user = mapper.Map<UserRequest, User>(userRequest);

                // Mapear el usuario a una respuesta de usuario
                var userResponse = mapper.Map<User, UserResponse>(user);

                // Configurar un mock para el adaptador IMapperAdapter y establecer su comportamiento
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<UserRequest, User>(It.IsAny<UserRequest>()))
                    .Returns(mapper.Map<UserRequest, User>(userRequest));
                mockMapper.Setup(m => m.Map<User, UserResponse>(It.IsAny<User>()))
                    .Returns(mapper.Map<User, UserResponse>(user));

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento para agregar un usuario correctamente
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.AddAsync(It.IsAny<User>())).ReturnsAsync(true);

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método CreateAsync con la solicitud de usuario y obtener el resultado
                var result = service.CreateAsync(userRequest)?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea true
                Assert.True(result.Success);

                // Verificar que las propiedades Name, LastName y Email en la respuesta sean iguales a las de la solicitud de usuario
                Assert.Equal((user.Id, userRequest.Name, userRequest.LastName, userRequest.Email),
                    (result.Id, result.Name, result.LastName, result.Email));
            }

            /// <summary>
            /// Prueba para verificar que CreateAsync lance una excepción de ArgumentNullException cuando se proporciona una solicitud nula.
            /// </summary>
            [Fact]
            public void CreateAsync_Returns_NegativeResponse_When_NullRequest()
            {
                // ARRANGE

                // Crear una excepción simulada de ArgumentNullException y obtener su mensaje de error
                var exception = new ArgumentNullException("The request object cannot be null");
                var errorMessage = exception.Message;

                // Crear una solicitud de usuario nula
                UserRequest? user = null;

                // Configurar un mock para el repositorio IUserRepository
                var mockRepository = new Mock<IUserRepository>();

                // Configurar un mock para el adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método CreateAsync con una solicitud de usuario nula y obtener el resultado
                var result = service.CreateAsync(user)?.Result;

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
            [InlineData("", "Doe", "john@example.com", "123")]
            [InlineData("John", "", "john@example.com", "123")]
            [InlineData("John", "Doe", "", "123")]
            [InlineData("John", "Doe", "john@example.com", "")]
            [InlineData("", "", "", "")]
            [InlineData(null, "Doe", "john@example.com", "")]
            [InlineData("John", "", "john@example.com", null)]
            [InlineData("John", "Doe", "john@example.com", null)]
            public void CreateAsync_Returns_NegativeResponse_When_InvalidRequest(string? name, string? lastname, string? email, string? password)
            {
                // ARRANGE

                // Crear una excepción simulada de ValidationException y obtener su mensaje de error
                var exception = new ValidationException("Argument is invalid.");
                var errorMessage = exception.Message;

                // Crear una solicitud de usuario con los datos de entrada proporcionados
                var user = new UserRequest
                {
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Password = password
                };

                // Crear un mapeador AutoMapper para simular el mapeo de la solicitud de usuario a un usuario
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Configurar un mock para el adaptador IMapperAdapter y establecer su comportamiento
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<UserRequest, User>(user))
                    .Returns(mapper.Map<UserRequest, User>(user));

                // Configurar un mock para el repositorio IUserRepository
                var mockRepository = new Mock<IUserRepository>();

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método CreateAsync con la solicitud de usuario y obtener el resultado
                var result = service.CreateAsync(user)?.Result;

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);

                // Verificar que el mensaje de la respuesta coincida con el mensaje de la excepción de ValidationException
                Assert.Equal(errorMessage, result.Message);
            }

            //[Theory]
            //[InlineData("", "Doe", "john@example.com", "123")]
            //[InlineData("John", "", "john@example.com", "123")]
            //[InlineData("John", "Doe", "", "123")]
            //[InlineData("John", "Doe", "john@example.com", "")]
            //[InlineData("", "", "", "")]
            //[InlineData(null, "Doe", "john@example.com", "")]
            //[InlineData("John", "", "john@example.com", null)]
            //[InlineData(null, null, null, null)]
            //public void CreateAsync_Throw_ValidationException_When_InvalidInput(string? name, string? lastname, string? email, string? password)
            //{
            //    // ARRANGE
            //    var user = new UserRequest
            //    {
            //        Name = name,
            //        LastName = lastname,
            //        Email = email,
            //        Password = password
            //    };

            //    var mockMapper = new Mock<IMapperAdapter>();
            //    var mockRepository = new Mock<IUserRepository>();
            //    var service = new UserService(mockRepository.Object, mockMapper.Object);

            //    // ACT
            //    var result = service.CreateAsync(user);
            //    var exception = result?.Exception;
            //    var exceptions = exception?.InnerExceptions;

            //    // ASSERT
            //    Assert.NotNull(user);
            //    Assert.Contains(exceptions, ex => ex.GetType() == typeof(ArgumentException));
            //}

            ///// <summary>
            ///// Verifica que CreateAsync lance una excepción al proporcionar una entrada nula.
            ///// </summary>
            //[Fact]
            //public void CreateAsync_NullInput()
            //{
            //    // ARRANGE
            //    User? user = null;

            //    var mockRepository = new Mock<IUserRepository>();
            //    var service = new UserService(mockRepository.Object);

            //    // ACT
            //    var result = service.CreateAsync(user);
            //    var exception = result?.Exception;
            //    var exceptions = exception?.InnerExceptions;

            //    // ASSERT
            //    Assert.Null(user);
            //    Assert.Contains(exceptions, ex => ex.GetType() == typeof(ArgumentException));
            //}

            /// <summary>
            /// Verifica que el método CreateAsync lance una InvalidOperationException cuando el repositorio no puede agregar la entidad.
            /// </summary>
            [Fact]
            public void CreateAsync_Returns_NegativeResponse_When_RepositoryFailsToAdd()
            {
                // ARRANGE

                // Crear una excepción InvalidOperationException simulada para representar la incapacidad del repositorio para agregar la entidad
                var exception = new InvalidOperationException("The entity could not be added.");
                var errorMessage = exception.Message;

                // Crear una solicitud de usuario (UserRequest) para utilizarla como entrada para la creación de usuarios
                var user = new UserRequest
                {
                    Name = "John",
                    LastName = "Doe",
                    Email = "john@email.com",
                    Password = "1234"
                };

                // Crear un mapeador de AutoMapper para simular la conversión de la solicitud de usuario a un objeto de usuario
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Configurar un mock para el adaptador IMapperAdapter y establecer su comportamiento para mapear la solicitud de usuario a un objeto de usuario
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<UserRequest, User>(user))
                    .Returns(mapper.Map<UserRequest, User>(user));

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento para devolver false al intentar agregar una entidad
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.AddAsync(It.IsAny<User>())).ReturnsAsync(false);

                // Crear una instancia del servicio de usuario (UserService) con los mocks del repositorio y del mapeador
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método CreateAsync del servicio con la solicitud de usuario y obtener el resultado
                var result = service.CreateAsync(user)?.Result;

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);

                // Verificar que el mensaje de la excepción coincida con el mensaje esperado
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Verifica que el método CreateAsync lance una excepción cuando el mapeador no puede mapear la solicitud de usuario a un objeto de usuario.
            /// </summary>
            [Fact]
            public void CreateAsync_Returns_NegativeResponse_ExceptionThrown()
            {
                // ARRANGE

                // Crear una excepción genérica para simular la situación en la que el mapeador no puede mapear la solicitud de usuario a un objeto de usuario
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Configurar un mock para el adaptador IMapperAdapter y establecer su comportamiento para lanzar una excepción cuando se llame al método Map con cualquier solicitud de usuario
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<UserRequest, User>(It.IsAny<UserRequest>())).Throws(new Exception());

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento para devolver true al intentar agregar una entidad
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.AddAsync(It.IsAny<User>())).ReturnsAsync(true);

                // Crear una instancia del servicio de usuario (UserService) con los mocks del repositorio y del mapeador
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método CreateAsync del servicio con una solicitud de usuario genérica y obtener el resultado
                var result = service.CreateAsync(new UserRequest())?.Result;

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
            /// Prueba para verificar que UpdateAsync actualiza un usuario existente cuando se proporciona una entrada válida
            /// </summary>
            [Theory]
            [InlineData("", "Doe", "john@example.com", "123")]
            [InlineData("John", "", "john@example.com", "123")]
            [InlineData(null, "Doe", "john@example.com", "123")]
            [InlineData("John", "Doe", "", "123")]
            [InlineData("John", "Doe", "john@example.com", "")]
            [InlineData("", "", "", "")]
            public void UpdateAsync_Returns_SuccessfulResponse_When_ValidRequest(string? name, string? lastname, string? email, string? password)
            {
                // ARRANGE

                // Crear una solicitud de usuario con los datos proporcionados en los argumentos
                var userRequest = new UserRequest
                {
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Password = password
                };

                // Usuario existente para ser actualizado
                var existingUser = new User
                {
                    Name = "Jane",
                    LastName = "Smith",
                    Email = "jane@example.com",
                    Password = "456"
                };

                // Respuesta de usuario esperada después de la actualización
                var userResponse = new UserResponse
                {
                    Id = existingUser.Id,
                    // Se asigna el nombre del usuario existente si el nombre en la solicitud es nulo o vacío
                    Name = string.IsNullOrEmpty(userRequest.Name) ? existingUser.Name : userRequest.Name,
                    // Se asigna el apellido del usuario existente si el apellido en la solicitud es nulo o vacío
                    LastName = string.IsNullOrEmpty(userRequest.LastName) ? existingUser.LastName : userRequest.LastName,
                    // Se asigna el correo electrónico del usuario existente si el correo electrónico en la solicitud es nulo o vacío
                    Email = string.IsNullOrEmpty(userRequest.Email) ? existingUser.Email : userRequest.Email,
                };

                // Configuración del mock para el repositorio IUserRepository y establecimiento de su comportamiento para obtener un usuario por su ID y actualizar un usuario correctamente
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingUser);
                mockRepository.Setup(r => r.UpdateAsync(It.IsAny<User>())).ReturnsAsync(true);

                // Crear un mapeador AutoMapper para mapear un usuario a una respuesta de usuario
                var mapper = TestAutoMapperFactory.CreateMapper();
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<User, UserResponse>(It.IsAny<User>()))
                    .Returns((User user) => mapper.Map<User, UserResponse>(user));

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT

                // Llamar al método UpdateAsync con el ID del usuario existente y la solicitud de usuario, y obtener el resultado
                var result = service.UpdateAsync(existingUser.Id, userRequest)?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea true
                Assert.True(result.Success);
                // Verificar que las propiedades Name, LastName y Email en la respuesta sean iguales a las de la respuesta de usuario esperada
                Assert.Equal((userResponse.Name, userResponse.LastName, userResponse.Email),
                             (result.Name, result.LastName, result.Email));
            }

            /// <summary>
            /// Prueba para verificar que UpdateAsync lanza ArgumentNullException cuando se proporciona una solicitud nula.
            /// </summary>
            [Fact]
            public void UpdateAsync_Returns_NegativeResponse_When_NullRequest()
            {
                // ARRANGE

                // Crear una excepción de ArgumentNullException con el mensaje adecuado
                var exception = new ArgumentNullException("The request object cannot be null");
                var errorMessage = exception.Message;

                // Establecer la solicitud de usuario como nula
                UserRequest? user = null;

                // Configurar un mock para el repositorio IUserRepository y el adaptador IMapperAdapter
                var mockRepository = new Mock<IUserRepository>();
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método UpdateAsync con un ID aleatorio y una solicitud de usuario nula, y obtener el resultado
                var result = service.UpdateAsync(Guid.NewGuid(), user)?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción ArgumentNullException
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Prueba para verificar que UpdateAsync lanza ArgumentException cuando no se encuentra el usuario.
            /// </summary>
            [Fact]
            public void UpdateAsync_Returns_NegativeResponse_When_RequestNotFound()
            {
                // ARRANGE
                // Generar un nuevo ID de usuario
                var userId = Guid.NewGuid();
                // Establecer el usuario como nulo
                User? user = null;

                // Crear una excepción de ArgumentException con el mensaje adecuado
                var exception = new ArgumentException($"Entity with id {userId} not found");
                var errorMessage = exception.Message;

                // Configurar un mock para el adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento para devolver un usuario nulo cuando se busque por ID
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método UpdateAsync con un ID de usuario y una solicitud de usuario, y obtener el resultado
                var result = service.UpdateAsync(userId, new UserRequest())?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción ArgumentException
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Prueba para verificar que UpdateAsync lanza ValidationException cuando los datos de usuario actualizado son inválidos.
            /// </summary>
            [Theory]
            [InlineData("", "Doe", "john@example.com", "123")]
            [InlineData(null, "Doe", "john@example.com", "123")]
            [InlineData("John", "", "john@example.com", "123")]
            [InlineData("John", null, "john@example.com", "123")]
            [InlineData("John", "Doe", "", "123")]
            [InlineData("John", "Doe", null, "123")]
            [InlineData("John", "Doe", "john@example.com", "")]
            [InlineData("John", "Doe", "john@example.com", null)]
            [InlineData("", "", "", "")]
            [InlineData(null, null, null, null)]
            public void UpdateAsync_Returns_NegativeResponse_When_InvalidRequest(string? name, string? lastname, string? email, string? password)
            {
                // ARRANGE
                // Crear una excepción de ValidationException con el mensaje adecuado
                var exception = new ValidationException("Argument is invalid.");
                var errorMessage = exception.Message;

                // Crear un usuario existente con los datos proporcionados en los argumentos
                var existingUser = new User
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Password = password
                };

                // Configurar un mock para el adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento para devolver un usuario existente cuando se busque por ID
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingUser);

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método UpdateAsync con un ID aleatorio y una solicitud de usuario vacía, y obtener el resultado
                var result = service.UpdateAsync(Guid.NewGuid(), new UserRequest())?.Result;

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
            [InlineData("John", "Doe", "john@example.com", "123")]
            public void UpdateAsync_Returns_NegativeResponse_When_RepositoryFailsToUpdate(string name, string lastname, string email, string password)
            {
                // ARRANGE
                // Crear una excepción de InvalidOperationException con el mensaje adecuado
                var exception = new InvalidOperationException("The entity could not be updated.");
                var errorMessage = exception.Message;

                // Crear un usuario existente con los datos proporcionados en los argumentos
                var existingUser = new User
                {
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Password = password
                };

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento para devolver un usuario existente cuando se busque por ID y para devolver false cuando se intente actualizar la entidad
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingUser);
                mockRepository.Setup(r => r.UpdateAsync(It.IsAny<User>())).ReturnsAsync(false);

                // Configurar un mock para el adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método UpdateAsync con un ID aleatorio y una solicitud de usuario vacía, y obtener el resultado
                var result = service.UpdateAsync(Guid.NewGuid(), new UserRequest())?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción InvalidOperationException
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Prueba para verificar que UpdateAsync lance una excepción cuando el mapeador no puede mapear los datos del usuario.
            /// </summary>
            [Theory]
            [InlineData("John", "Doe", "john@example.com", "123")]
            public void UpdateAsync_Returns_NegativeResponse_ExceptionThrown(string name, string lastname, string email, string password)
            {
                // ARRANGE

                // Crear una excepción genérica para simular el escenario en el que el mapeador no puede mapear los datos del usuario
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Crear un usuario existente con los datos proporcionados
                var existingUser = new User
                {
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Password = password
                };

                // Configurar mocks para el repositorio IUserRepository y el adaptador IMapperAdapter
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingUser);
                mockRepository.Setup(r => r.UpdateAsync(It.IsAny<User>())).ReturnsAsync(true);

                var mockMapper = new Mock<IMapperAdapter>();
                // Configurar el comportamiento del mapeador para lanzar una excepción al intentar mapear un usuario a una respuesta de usuario
                mockMapper.Setup(m => m.Map<User, UserResponse>(It.IsAny<User>())).Throws(new Exception());

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método UpdateAsync con un nuevo identificador y una solicitud de usuario vacía y obtener el resultado
                var result = service.UpdateAsync(Guid.NewGuid(), new UserRequest())?.Result;

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
            /// Verifica que DeleteAsync elimine correctamente un usuario existente.
            /// </summary>
            [Theory]
            [InlineData("John", "Doe", "john@example.com", "1234")]
            [InlineData("Jane", "Smith", "jane@email.com", "456")]
            public void DeleteAsync_Returns_SuccessfulResponse_When_ValidRequest(string name, string lastname, string email, string password)
            {
                // ARRANGE
                var existingUser = new User
                {
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Password = password
                };

                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingUser);
                mockRepository.Setup(r => r.RemoveAsync(It.IsAny<User>())).ReturnsAsync(true);

                var mapper = TestAutoMapperFactory.CreateMapper();
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<User, UserResponse>(It.IsAny<User>())).
                    Returns((User user) => mapper.Map<User, UserResponse>(user));

                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                var result = service.DeleteAsync(Guid.NewGuid())?.Result;

                // ASSERT
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.Equal((existingUser.Name, existingUser.LastName, existingUser.Email),
                            (result.Name, result.LastName, result.Email));
            }

            /// <summary>
            /// Verifica que DeleteAsync lance una excepción al intentar eliminar un usuario inexistente.
            /// </summary>
            [Fact]
            public void DeleteAsync_Returns_NegativeResponse_When_RequestNotFound()
            {
                // ARRANGE
                // Generar un nuevo ID de usuario
                var userId = Guid.NewGuid();

                // Crear una excepción de ArgumentNullException con el mensaje adecuado
                var exception = new ArgumentNullException($"Entity with id {userId} not found");
                var errorMessage = exception.Message;

                // Establecer el usuario como nulo
                User? user = null;

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento para devolver un usuario nulo cuando se busque por ID
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);

                // Configurar un mock para el adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método DeleteAsync con un ID aleatorio y obtener el resultado
                var result = service.DeleteAsync(userId)?.Result;

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
            [InlineData("John", "Doe", "john@example.com", "1234")]
            [InlineData("Jane", "Smith", "jane@email.com", "456")]
            public void DeleteAsync_Returns_NegativeResponse_When_RepositoryFailsToDelete(string name, string lastname, string email, string password)
            {
                // ARRANGE
                // Crear una excepción de InvalidOperationException con el mensaje adecuado
                var exception = new InvalidOperationException("The entity could not be deleted.");
                var errorMessage = exception.Message;

                // Crear un usuario existente con los datos proporcionados en los argumentos
                var existingUser = new User
                {
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Password = password
                };

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento para devolver un usuario existente cuando se busque por ID y para devolver false cuando se intente eliminar la entidad
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingUser);
                mockRepository.Setup(r => r.RemoveAsync(It.IsAny<User>())).ReturnsAsync(false);

                // Configurar un mock para el adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método DeleteAsync con un ID aleatorio y obtener el resultado
                var result = service.DeleteAsync(Guid.NewGuid())?.Result;

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
            [InlineData("John", "Doe", "john@example.com", "1234")]
            [InlineData("Jane", "Smith", "jane@email.com", "456")]
            public void DeleteAsync_Returns_NegativeResponse_ExceptionThrown(string name, string lastname, string email, string password)
            {
                // ARRANGE
                // Crear una excepción genérica
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Crear un usuario existente con los datos proporcionados en los argumentos
                var existingUser = new User
                {
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Password = password
                };

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento para devolver un usuario existente cuando se busque por ID y para devolver true cuando se intente eliminar la entidad
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingUser);
                mockRepository.Setup(r => r.RemoveAsync(It.IsAny<User>())).ReturnsAsync(true);

                // Crear un mapeador AutoMapper
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Configurar un mock para el adaptador IMapperAdapter y establecer su comportamiento para lanzar una excepción al mapear el usuario a una respuesta de usuario
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<User, UserResponse>(It.IsAny<User>())).Throws(new Exception());

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

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

        public class GetByEmail
        {
            /// <summary>
            /// Prueba para verificar que GetUserByEmailAsync devuelve un usuario existente cuando se proporciona un correo electrónico válido.
            /// </summary>
            [Theory]
            [InlineData("John", "Doe", "john@example.com", "1234")]
            [InlineData("Jane", "Smith", "jane@example.com", "456")]
            public void GetByEmailAsync_Returns_SuccessfulResponse_When_ValidEmail(string name, string lastname, string email, string password)
            {
                // ARRANGE
                // Crear un usuario existente con los datos proporcionados en los argumentos
                var existingUser = new User
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Password = password
                };

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento para devolver un usuario existente cuando se busque por correo electrónico
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(existingUser);

                // Crear un mapeador AutoMapper
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Configurar un mock para el adaptador IMapperAdapter y establecer su comportamiento para mapear el usuario a una respuesta de usuario
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<User, UserResponse>(It.IsAny<User>()))
                    .Returns((User user) => mapper.Map<User, UserResponse>(user));

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método GetUserByEmailAsync con el correo electrónico del usuario existente y obtener el resultado
                var result = service.GetByEmailAsync(email)?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea true
                Assert.True(result.Success);
                // Verificar que las propiedades Name, LastName y Email en la respuesta sean iguales a las del usuario existente
                Assert.Equal((existingUser.Name, existingUser.LastName, existingUser.Email),
                    (result.Name, result.LastName, result.Email));
            }

            /// <summary>
            /// Prueba para verificar que GetUserByEmailAsync lance una excepción ArgumentNullException cuando se pasa un correo electrónico nulo.
            /// </summary>
            [Fact]
            public void GetByEmailAsync_Returns_NegativeResponse_When_NullEmail()
            {
                // ARRANGE
                string? email = null;

                // Crear una excepción de ArgumentNullException con el mensaje adecuado
                var exception = new ArgumentNullException(nameof(email), "The email cannot be null or empty");
                var errorMessage = exception.Message;

                // Establecer el usuario como nulo
                User? nullUser = null;

                // Configurar un mock para el adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento para devolver un usuario nulo cuando se busque por correo electrónico
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(nullUser);

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método GetUserByEmailAsync con un correo electrónico nulo y obtener el resultado
                var result = service.GetByEmailAsync(email)?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción ArgumentNullException
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Prueba para verificar que GetUserByEmailAsync lance una excepción ValidationException cuando se proporciona un correo electrónico no válido.
            /// </summary>
            [Theory]
            [InlineData("@")]
            [InlineData("john.com")]
            [InlineData("email.com")]
            public void GetByEmailAsync_Returns_NegativeResponse_When_InvalidEmail(string email)
            {
                // ARRANGE
                // Crear una excepción de ValidationException con el mensaje adecuado
                var exception = new ValidationException("Argument is invalid.");
                var errorMessage = exception.Message;

                // Configurar mocks para el repositorio IUserRepository y el adaptador IMapperAdapter
                var mockRepository = new Mock<IUserRepository>();
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear una instancia del servicio de usuario (UserService)
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método GetUserByEmailAsync con el correo electrónico no válido y obtener el resultado
                var result = service.GetByEmailAsync(email)?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción ValidationException
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Prueba para verificar que GetUserByEmailAsync lance una excepción ArgumentException cuando no se encuentra un usuario con el correo electrónico proporcionado.
            /// </summary>
            [Theory]
            [InlineData("john@example.com")]
            [InlineData("jane@email.com")]
            public void GetByEmailAsync_Returns_NegativeResponse_When_EmailNotFound(string email)
            {
                // ARRANGE
                // Crear una excepción de ArgumentException con el mensaje adecuado
                var exception = new ArgumentException($"Email {email} not found");
                var errorMessage = exception.Message;

                // Establecer el usuario como nulo
                User? nullUser = null;

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento para devolver un usuario nulo cuando se busque por correo electrónico
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(nullUser);

                // Configurar un mock para el adaptador IMapperAdapter
                var mockMapper = new Mock<IMapperAdapter>();

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método GetUserByEmailAsync con el correo electrónico y obtener el resultado
                var result = service.GetByEmailAsync(email)?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción ArgumentException
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Prueba para verificar que GetUserByEmailAsync lance una excepción cuando el mapeo de usuario a respuesta de usuario falla.
            /// </summary>
            [Theory]
            [InlineData("John", "Doe", "john@example.com", "1234")]
            [InlineData("Jane", "Smith", "jane@email.com", "456")]
            public void GetByEmailAsync_Returns_NegativeResponse_ExceptionThrown(string name, string lastname, string email, string password)
            {
                // ARRANGE
                // Crear una excepción de tipo Exception
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Crear un usuario existente con los datos proporcionados
                var existingUser = new User
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Password = password
                };

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento para devolver un usuario existente cuando se busque por correo electrónico
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(existingUser);

                // Crear un mapeador AutoMapper
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Configurar un mock para el adaptador IMapperAdapter y establecer su comportamiento para lanzar una excepción al intentar mapear un usuario a una respuesta de usuario
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<User, UserResponse>(It.IsAny<User>())).Throws(new Exception());

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método GetUserByEmailAsync con el correo electrónico y obtener el resultado
                var result = service.GetByEmailAsync(email)?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción
                Assert.Equal(errorMessage, result.Message);
            }
        }

        public class Authenticate
        {
            /// <summary>
            /// Prueba para verificar que AuthenticateAsync autentique al usuario cuando se proporcionan credenciales existentes.
            /// </summary>
            [Theory]
            [InlineData("John", "Doe", "john@example.com", "1234")]
            [InlineData("Jane", "Smith", "jane@example.com", "456")]
            public void AuthenticateAsync_Returns_SuccesfulResponse_When_CorrectRequest(string name, string lastname, string email, string password)
            {
                // ARRANGE
                // Crear un mapeador AutoMapper
                var mapper = TestAutoMapperFactory.CreateMapper();

                // Crear una solicitud de usuario con los datos proporcionados
                var userRequest = new UserRequest
                {
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Password = password
                };

                // Mapear la solicitud de usuario a un objeto de usuario existente
                var existingUser = mapper.Map<UserRequest, User>(userRequest);

                // Configurar un mock para el adaptador IMapperAdapter y establecer su comportamiento para mapear un usuario a una respuesta de usuario
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<User, UserResponse>(It.IsAny<User>()))
                    .Returns((User user) => mapper.Map<User, UserResponse>(user));

                // Configurar un mock para el repositorio IUserRepository y establecer su comportamiento para autenticar al usuario correctamente
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(existingUser);

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método AuthenticateUserAsync con la solicitud de usuario y obtener el resultado
                var result = service.AuthenticateAsync(userRequest)?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea true
                Assert.True(result.Success);
                // Verificar que las propiedades Name, LastName y Email en la respuesta sean iguales a las del usuario autenticado
                Assert.Equal((existingUser.Name, existingUser.LastName, existingUser.Email),
                    (result.Name, result.LastName, result.Email));
            }

            /// <summary>
            /// Prueba para verificar que AuthenticateAsync lance una excepción ArgumentNullException cuando la solicitud es nula.
            /// </summary>
            [Fact]
            public void AuthenticateAsync_Returns_NegativeResponse_When_NullRequest()
            {
                // ARRANGE
                // Establecer la solicitud como nula
                UserRequest? request = null;
                
                // Crear una excepción de ArgumentNullException con el mensaje adecuado
                var exception = new ArgumentNullException(nameof(request), "The request object cannot be null or empty");
                var errorMessage = exception.Message;

                // Configurar mocks para el adaptador IMapperAdapter y el repositorio IUserRepository
                var mockMapper = new Mock<IMapperAdapter>();
                var mockRepository = new Mock<IUserRepository>();

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método AuthenticateUserAsync con la solicitud nula y obtener el resultado
                var result = service.AuthenticateAsync(request)?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción ArgumentNullException
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Prueba para verificar que AuthenticateAsync lance una excepción ArgumentNullException cuando el correo electrónico o la contraseña son nulos o vacíos.
            /// </summary>
            [Theory]
            [InlineData("", "1234")]
            [InlineData("john@example.com", "")]
            [InlineData(null, "456")]
            [InlineData("jane@example.com", null)]
            [InlineData("", "")]
            [InlineData("", null)]
            [InlineData(null, "")]
            [InlineData(null, null)]
            public void AuthenticateAsync_Returns_NegativeResponse_When_NullCredentials(string? email, string? password)
            {
                // ARRANGE
                // Crear una solicitud de usuario con los datos proporcionados
                var request = new UserRequest
                {
                    Email = email,
                    Password = password
                };

                // Crear una excepción de ArgumentNullException con el mensaje adecuado
                var exception = new ArgumentNullException(nameof(request), "The email or password cannot be null or empty");
                var errorMessage = exception.Message;
                

                // Configurar mocks para el adaptador IMapperAdapter y el repositorio IUserRepository
                var mockMapper = new Mock<IMapperAdapter>();
                var mockRepository = new Mock<IUserRepository>();

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método AuthenticateUserAsync con la solicitud y obtener el resultado
                var result = service.AuthenticateAsync(request)?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción ArgumentNullException
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Prueba para verificar que AuthenticateAsync lance una excepción ValidationException cuando los parámetros no son válidos.
            /// </summary>
            [Theory]
            [InlineData("john", "1234")]
            [InlineData("@", "1234")]
            [InlineData("john@example.com", "1")]
            [InlineData("john", "1")]
            [InlineData("@", "1")]
            public void AuthenticateAsync_Returns_NegativeResponse_When_InvalidCredentials(string? email, string? password)
            {
                // ARRANGE
                // Crear una excepción de ValidationException con el mensaje adecuado
                var exception = new ValidationException("Argument is invalid.");
                var errorMessage = exception.Message;

                // Crear una solicitud de usuario con los datos proporcionados
                var userRequest = new UserRequest
                {
                    Email = email,
                    Password = password
                };

                // Configurar mocks para el adaptador IMapperAdapter y el repositorio IUserRepository
                var mockMapper = new Mock<IMapperAdapter>();
                var mockRepository = new Mock<IUserRepository>();

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método AuthenticateUserAsync con la solicitud y obtener el resultado
                var result = service.AuthenticateAsync(userRequest)?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción ValidationException
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Prueba para verificar que AuthenticateAsync lance una excepción ArgumentException cuando el repositorio no puede autenticar las credenciales.
            /// </summary>
            [Theory]
            [InlineData("john@example.com", "1234")]
            [InlineData("jane@email.com", "456")]
            public void AuthenticateAsync_Returns_NegativeResponse_When_RepositoryFailsToAuthenticate(string email, string password)
            {
                // ARRANGE
                // Crear una excepción de ArgumentException con el mensaje adecuado
                var exception = new ArgumentException($"Credentials incorrect");
                var errorMessage = exception.Message;

                // Crear una solicitud de usuario con los datos proporcionados
                var userRequest = new UserRequest
                {
                    Email = email,
                    Password = password
                };

                // Establecer el usuario como nulo para simular una autenticación fallida
                User? nullUser = null;

                // Configurar mocks para el adaptador IMapperAdapter y el repositorio IUserRepository
                var mockMapper = new Mock<IMapperAdapter>();

                var mockRepository = new Mock<IUserRepository>();
                // Configurar el comportamiento del repositorio para devolver un usuario nulo al autenticar las credenciales
                mockRepository.Setup(r => r.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(nullUser);

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método AuthenticateUserAsync con la solicitud y obtener el resultado
                var result = service.AuthenticateAsync(userRequest)?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción ArgumentException
                Assert.Equal(errorMessage, result.Message);
            }

            /// <summary>
            /// Prueba para verificar que AuthenticateAsync lance una excepción cuando el mapeador no puede mapear los datos del usuario.
            /// </summary>
            [Theory]
            [InlineData("John", "Doe", "john@example.com", "1234")]
            [InlineData("Jane", "Smith", "jane@example.com", "456")]
            public void AuthenticateAsync_Returns_NegativeResponse_ExceptionThrown(string name, string lastname, string email, string password)
            {
                // ARRANGE
                // Crear una excepción genérica para simular el escenario en el que el mapeador no puede mapear los datos del usuario
                var exception = new Exception();
                var errorMessage = exception.Message;

                // Crear una solicitud de usuario con los datos proporcionados
                var userRequest = new UserRequest
                {
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Password = password
                };

                // Crear un usuario existente con los mismos datos
                var existingUser = new User
                {
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Password = password
                };

                // Configurar el mock del mapeador para que lance una excepción al intentar mapear un usuario a una respuesta de usuario
                var mockMapper = new Mock<IMapperAdapter>();
                mockMapper.Setup(m => m.Map<User, UserResponse>(It.IsAny<User>())).Throws(new Exception());

                // Configurar el mock del repositorio IUserRepository para que devuelva el usuario existente
                var mockRepository = new Mock<IUserRepository>();
                mockRepository.Setup(r => r.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(existingUser);

                // Crear una instancia del servicio de usuario (UserService) con los mocks configurados
                var service = new UserService(mockRepository.Object, mockMapper.Object);

                // ACT
                // Llamar al método AuthenticateUserAsync con la solicitud de usuario y obtener el resultado
                var result = service.AuthenticateAsync(userRequest)?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que Success en la respuesta sea false
                Assert.False(result.Success);
                // Verificar que el mensaje en la respuesta sea igual al mensaje de la excepción
                Assert.Equal(errorMessage, result.Message);
            }
        }
    }
}

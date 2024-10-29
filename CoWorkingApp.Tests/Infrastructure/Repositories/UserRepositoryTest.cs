using CoWorkingApp.API.Infrastructure.Persistence.Repositories;
using CoWorkingApp.API.Infrastructure.UnitOfWorks;
using CoWorkingApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CoWorkingApp.Tests.Infrastructure.Repositories
{
    /// <summary>
    /// Clase de pruebas unitarias para el repositorio UserRepository.
    /// </summary>
    public class UserRepositoryTest
    {
        public class Constructor
        {
            /// <summary>
            /// Verifica que el constructor de la clase UserRepository se pueda crear correctamente
            /// cuando se le pasa un parámetro válido (unitOfWork).
            /// </summary>
            [Fact]
            public void Constructor_ReturnsInstance_When_UnitOfWorkIsNotNull()
            {
                // ARRANGE
                // Se crea un mock de IUnitOfWork
                var unitOfWork = new Mock<IUnitOfWork>();

                // ACT
                // Se intenta crear una instancia de UserRepository con el mock de IUnitOfWork
                var result = () => new UserRepository(unitOfWork.Object);

                // ASSERT
                // Se verifica que la creación de la instancia no lance excepciones
                Assert.NotNull(result);
            }

            /// <summary>
            /// Verifica que el constructor de la clase UserRepository lance una excepción
            /// cuando se le pasa un parámetro nulo (unitOfWork).
            /// </summary>
            [Fact]
            public void Constructor_Throws_ArgumentNullException_When_UnitOfWorkIsNull()
            {
                // ARRANGE
                // Se establece unitOfWork como nulo
                IUnitOfWork? unitOfWork = null;

                // ACT
                // Se intenta crear una instancia de UserRepository con unitOfWork nulo
                var result = () => new UserRepository(unitOfWork);

                // ASSERT
                // Se verifica que la creación de la instancia lance una excepción del tipo ArgumentNullException
                Assert.Throws<ArgumentNullException>(result);
            }
        }

        public class GetAll
        {
            /// <summary>
            /// Verifica que GetAllAsync retorne todos los usuarios existentes en el contexto.
            /// </summary>
            [Fact]
            public void GetAllAsync_Returns_AllUsers_When_ContextContainsUsers()
            {
                // ARRANGE

                // creamos un contexto
                using var context = TestContextFactory.CreateContext();

                // creamos una lista de usuarios de prueba
                var users = new List<User>
                {
                    new User { Id = Guid.NewGuid(), Name = "John", LastName = "Doe", Email = "john@example.com", Password = "123" },
                    new User { Id = Guid.NewGuid(), Name = "Jane", LastName = "Smith", Email = "jane@example.com", Password = "456" }
                };

                // agregamos la lista de usuarios de prueba al conexto
                context.Users.AddRangeAsync(users);
                context.SaveChangesAsync();

                // creamos un mock para simular un UnitOfWork
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                // Pareparamos el comportamiento Context para que retorne el contexto
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // creamos el UserRepository y le inyectamos el mock que simula a IUnitOfWork
                var repositoy = new UserRepository(mockUnitOfWork.Object);

                // ACT

                // invocamos el método GetAllAsync
                var result = repositoy.GetAllAsync()?.Result;

                // ASSERT
                Assert.NotNull(result);
                Assert.Equal(users.Count, result.Count());
                Assert.Equal(users.Select(u => u.Id), result.Select(u => u.Id));
            }

            /// <summary>
            /// Prueba para verificar que GetAllAsync devuelva una colección vacía cuando no hay usuarios en el repositorio.
            /// </summary>
            [Fact]
            public void GetAllAsync_Returns_EmptyCollection_When_NoUsersInRepository()
            {
                // ARRANGE

                // Crear un contexto de prueba utilizando un factory (TestContextFactory)
                using var context = TestContextFactory.CreateContext();

                // Configurar un mock para la unidad de trabajo (IUnitOfWork) y establecer su comportamiento para devolver el contexto de prueba
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Crear un repositorio de usuarios (UserRepository) utilizando el contexto de prueba y la unidad de trabajo simulada
                var repository = new UserRepository(mockUnitOfWork.Object);

                // ACT

                // Llamar al método GetAllAsync del repositorio y obtener el resultado
                var result = repository.GetAllAsync()?.Result;

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que la colección esté vacía
                Assert.Empty(result);
            }
        }

        public class GetById
        {
            /// <summary>
            /// Prueba para verificar que GetByIdAsync devuelva un usuario existente cuando se proporciona un ID de usuario válido.
            /// </summary>
            [Fact]
            public void GetByIdAsync_Returns_ExistingUser_When_ValidUserId()
            {
                // ARRANGE

                // Generar un ID único para el usuario existente
                Guid userId = Guid.NewGuid();

                // Crear una lista de usuarios simulados que incluya al usuario existente
                var users = new List<User>
                {
                    new User { Id = userId, Name = "John", LastName = "Doe", Email = "john@example.com", Password = "123" },
                    new User { Id = Guid.NewGuid(), Name = "Jane", LastName = "Smith", Email = "jane@example.com", Password = "456" }
                };

                // Crear un contexto de prueba utilizando un factory (TestContextFactory)
                using var context = TestContextFactory.CreateContext();

                // Agregar los usuarios simulados al contexto y guardar los cambios
                context.Users.AddRangeAsync(users);
                context.SaveChangesAsync();

                // Configurar un mock para la unidad de trabajo (IUnitOfWork) y establecer su comportamiento para devolver el contexto de prueba
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Crear un repositorio de usuarios (UserRepository) utilizando el contexto de prueba y la unidad de trabajo simulada
                var repository = new UserRepository(mockUnitOfWork.Object);

                // ACT

                // Llamar al método GetByIdAsync del repositorio con el ID del usuario existente y obtener el resultado
                var result = repository.GetByIdAsync(userId)?.Result;

                // ASSERT

                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);

                // Verificar que el ID del usuario en el resultado coincida con el ID del usuario existente
                Assert.Equal(userId, result.Id);
            }

            /// <summary>
            /// Prueba para verificar que GetByIdAsync devuelva null cuando se proporciona un ID de usuario que no existe.
            /// </summary>
            [Fact]
            public void GetByIdAsync_Returns_Null_When_NonExistentUserId()
            {
                // ARRANGE

                // Crear una lista de usuarios simulados que no contienen al usuario con el ID proporcionado
                var users = new List<User>
                {
                    new User { Id = Guid.NewGuid(), Name = "John", LastName = "Doe", Email = "john@example.com", Password = "123" },
                    new User { Id = Guid.NewGuid(), Name = "Jane", LastName = "Smith", Email = "jane@example.com", Password = "456" }
                };

                // Crear un contexto de prueba utilizando un factory (TestContextFactory)
                using var context = TestContextFactory.CreateContext();

                // Agregar los usuarios simulados al contexto y guardar los cambios
                context.Users.AddRangeAsync(users);
                context.SaveChangesAsync();

                // Configurar un mock para la unidad de trabajo (IUnitOfWork) y establecer su comportamiento para devolver el contexto de prueba
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Crear un repositorio de usuarios (UserRepository) utilizando el contexto de prueba y la unidad de trabajo simulada
                var repository = new UserRepository(mockUnitOfWork.Object);

                // ACT

                // Llamar al método GetByIdAsync del repositorio con el ID de un usuario que no existe en el contexto y obtener el resultado
                var result = repository.GetByIdAsync(Guid.NewGuid())?.Result;

                // ASSERT

                // Verificar que el resultado sea nulo, ya que el usuario no existe en el contexto de la base de datos
                Assert.Null(result);
            }
        }

        public class Add
        {
            /// <summary>
            /// Prueba para verificar que AddAsync agrega un nuevo usuario correctamente.
            /// </summary>
            [Fact]
            public void AddAsync_Returns_True_When_NewUser()
            {
                // ARRANGE

                // Crear un nuevo usuario con datos simulados
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = "John",
                    LastName = "Doe",
                    Email = "john@example.com",
                    Password = "123"
                };

                // Crear un contexto de prueba utilizando un factory (TestContextFactory)
                var context = TestContextFactory.CreateContext();

                // Configurar un mock para la unidad de trabajo (IUnitOfWork) y establecer su comportamiento para devolver el contexto de prueba
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Crear un repositorio de usuarios (UserRepository) utilizando el contexto de prueba y la unidad de trabajo simulada
                var repository = new UserRepository(mockUnitOfWork.Object);

                // ACT

                // Llamar al método AddAsync del repositorio con el nuevo usuario y obtener el resultado
                var result = repository.AddAsync(user)?.Result;

                // ASSERT 

                // Verificar que el resultado no sea nulo y que sea verdadero, lo que indica que el usuario fue agregado correctamente
                Assert.NotNull(result);
                Assert.True(result);
            }

            /// <summary>
            /// Prueba para verificar que AddAsync lance una excepción cuando se produce un error al guardar en la base de datos.
            /// </summary>
            [Fact]
            public void AddAsync_Returns_False_When_ThrowException()
            {
                // ARRANGE

                // Crear un contexto de prueba utilizando un factory (TestContextFactory)
                var context = TestContextFactory.CreateContext();

                // Configurar un mock para la unidad de trabajo (IUnitOfWork) y establecer su comportamiento para devolver el contexto de prueba
                // y lanzar una excepción al intentar guardar cambios en la base de datos
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);
                mockUnitOfWork.Setup(u => u.CommitAsync()).ThrowsAsync(new Exception());

                // Crear un repositorio de usuarios (UserRepository) utilizando el contexto de prueba y la unidad de trabajo simulada
                var repository = new UserRepository(mockUnitOfWork.Object);

                // ACT

                // Llamar al método AddAsync del repositorio con cualquier usuario y obtener el resultado
                var result = repository.AddAsync(It.IsAny<User>())?.Result;

                // ASSERT

                // Verificar que el resultado no sea nulo y que sea falso, lo que indica que se lanzó una excepción al guardar en la base de datos
                Assert.NotNull(result);
                Assert.False(result);
            }
        }

        public class Update
        {
            /// <summary>
            /// Prueba para verificar que UpdateAsync actualice un usuario existente correctamente.
            /// </summary>
            [Fact]
            public void UpdateAsync_Returns_True_When_ExistingUser()
            {
                // ARRANGE

                // Crear un usuario existente con datos simulados
                var userExisting = new User
                {
                    Id = Guid.NewGuid(),
                    Name = "John",
                    LastName = "Doe",
                    Email = "john@example.com",
                    Password = "123"
                };

                // Crear un contexto de prueba utilizando un factory (TestContextFactory)
                var context = TestContextFactory.CreateContext();

                // Agregar el usuario existente al contexto y guardar los cambios en la base de datos
                context.Users.AddRangeAsync(userExisting);
                context.SaveChangesAsync();
                context.Entry(userExisting).State = EntityState.Detached;

                // Configurar un mock para la unidad de trabajo (IUnitOfWork) y establecer su comportamiento para devolver el contexto de prueba
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Crear un nuevo objeto de usuario con los datos actualizados
                var userToUpdate = new User
                {
                    Id = userExisting.Id,               // Mantener el mismo ID
                    Name = "Jane",                      // Nuevo nombre
                    LastName = "Smith",                 // Nuevo apellido
                    Email = "jane@example.com",         // Nuevo correo electrónico
                    Password = "456"                    // Nueva contraseña
                };

                // Crear un repositorio de usuarios (UserRepository) utilizando el contexto de prueba y la unidad de trabajo simulada
                var repository = new UserRepository(mockUnitOfWork.Object);

                // ACT

                // Llamar al método UpdateAsync del repositorio con el usuario a actualizar y obtener el resultado
                var result = repository.UpdateAsync(userToUpdate)?.Result;

                // ASSERT

                // Verificar que el resultado no sea nulo y que sea verdadero, lo que indica que el usuario fue actualizado correctamente
                Assert.NotNull(result);
                Assert.True(result);

                // Verificar que el usuario actualizado tenga los mismos valores que el usuario original
                Assert.Equal(userExisting, userToUpdate);
            }

            /// <summary>
            /// Verifica que el método UpdateAsync devuelva false cuando se produce una excepción al intentar guardar cambios en la base de datos.
            /// </summary>
            [Fact]
            public void UpdateAsync_Returns_False_When_ThrowException()
            {
                // ARRANGE

                // Crear un contexto de prueba utilizando un factory (TestContextFactory)
                var context = TestContextFactory.CreateContext();

                // Configurar un mock para la unidad de trabajo (IUnitOfWork) y establecer su comportamiento para devolver el contexto de prueba
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Crear un repositorio de usuarios (UserRepository) utilizando el contexto de prueba y la unidad de trabajo simulada
                var repository = new UserRepository(mockUnitOfWork.Object);

                // ACT

                // Llamar al método UpdateAsync del repositorio con cualquier usuario y obtener el resultado
                var result = repository.UpdateAsync(It.IsAny<User>())?.Result;

                // ASSERT

                // Verificar que el resultado no sea nulo y que sea falso, lo que indica que se produjo una excepción al intentar guardar cambios en la base de datos
                Assert.NotNull(result);
                Assert.False(result);
            }
        }

        public class Remove
        {
            /// <summary>
            /// Verifica que el método RemoveAsync elimine correctamente un usuario existente.
            /// </summary>
            [Fact]
            public void RemoveAsync_Returns_True_When_ExistingUser()
            {
                // ARRANGE

                // Crear un usuario existente para simular la eliminación
                var existingUser = new User
                {
                    Id = Guid.NewGuid(),
                    Name = "John",
                    LastName = "Doe",
                    Email = "john@example.com",
                    Password = "123"
                };

                // Crear un contexto de prueba utilizando un factory (TestContextFactory)
                var context = TestContextFactory.CreateContext();

                // Configurar un mock para la unidad de trabajo (IUnitOfWork) y establecer su comportamiento para devolver el contexto de prueba
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Crear un repositorio de usuarios (UserRepository) utilizando el contexto de prueba y la unidad de trabajo simulada
                var repository = new UserRepository(mockUnitOfWork.Object);

                // ACT

                // Llamar al método RemoveAsync del repositorio con el usuario existente y obtener el resultado
                var result = repository.RemoveAsync(existingUser)?.Result;

                // ASSERT

                // Verificar que el resultado no sea nulo y que sea verdadero, lo que indica que el usuario fue eliminado correctamente
                Assert.NotNull(result);
                Assert.True(result);
            }

            /// <summary>
            /// Verifica que el método RemoveAsync devuelva false cuando se produce una excepción al intentar eliminar un usuario.
            /// </summary>
            [Fact]
            public void RemoveAsync_Returns_False_When_ThrowException()
            {
                // ARRANGE

                // Crear un contexto de prueba utilizando un factory (TestContextFactory)
                var context = TestContextFactory.CreateContext();

                // Configurar un mock para la unidad de trabajo (IUnitOfWork) y establecer su comportamiento para devolver el contexto de prueba
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Crear un repositorio de usuarios (UserRepository) utilizando el contexto de prueba y la unidad de trabajo simulada
                var repository = new UserRepository(mockUnitOfWork.Object);

                // ACT

                // Llamar al método RemoveAsync del repositorio con cualquier usuario (utilizando It.IsAny<User>()) y obtener el resultado
                var result = repository.RemoveAsync(It.IsAny<User>())?.Result;

                // ASSERT

                // Verificar que el resultado no sea nulo y que sea falso, lo que indica que se produjo una excepción al intentar eliminar el usuario
                Assert.NotNull(result);
                Assert.False(result);
            }
        }

        public class Exists
        {
            /// <summary>
            /// Prueba para verificar que ExistsAsync devuelve true cuando existe un usuario.
            /// </summary>
            [Fact]
            public void ExistsAsync_Returns_True_When_ExistingUser()
            {
                // ARRANGE
                // Generar un nuevo ID de usuario
                var userId = Guid.NewGuid();
                // Crear un usuario existente con los datos proporcionados
                var existingUser = new User
                {
                    Id = userId,
                    Name = "John",
                    LastName = "Doe",
                    Email = "john@example.com",
                    Password = "123"
                };

                // Crear el contexto de prueba y agregar el usuario existente
                var context = TestContextFactory.CreateContext();
                context.Users.AddRangeAsync(existingUser);
                context.SaveChangesAsync();

                // Configurar un mock para la unidad de trabajo (IUnitOfWork) y establecer su comportamiento para devolver el contexto de prueba
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Crear un repositorio de usuario (UserRepository) con la unidad de trabajo (IUnitOfWork) mockeada
                var repository = new UserRepository(mockUnitOfWork.Object);

                // ACT
                // Llamar al método ExistsAsync con el ID del usuario existente y obtener el resultado
                var result = repository.ExistsAsync(userId)?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que el resultado sea true (el usuario existe)
                Assert.True(result);
            }

            /// <summary>
            /// Prueba para verificar que ExistsAsync devuelve false cuando no existe un usuario.
            /// </summary>
            [Fact]
            public void ExistsAsync_Returns_False_When_NotExistUser()
            {
                // ARRANGE
                // Crear el contexto de prueba
                var context = TestContextFactory.CreateContext();
                // Configurar un mock para la unidad de trabajo (IUnitOfWork) y establecer su comportamiento para devolver el contexto de prueba
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Crear un repositorio de usuario (UserRepository) con la unidad de trabajo (IUnitOfWork) mockeada
                var repository = new UserRepository(mockUnitOfWork.Object);

                // ACT
                // Llamar al método ExistsAsync con un ID aleatorio y obtener el resultado
                var result = repository.ExistsAsync(Guid.NewGuid())?.Result;

                // ASSERT
                // Verificar que el resultado no sea nulo
                Assert.NotNull(result);
                // Verificar que el resultado sea false (no existe el usuario)
                Assert.False(result);
            }
        }

        public class GetByEmail
        {
            /// <summary>
            /// Verifica que el método GetUserByEmailAsync devuelva un usuario existente cuando se proporciona un correo electrónico válido.
            /// </summary>
            [Fact]
            public void GetByEmailAsync_Returns_ExistingUser_When_ProvidedRegisteredEmail()
            {
                // ARRANGE

                // Crear un usuario existente para simular datos en el contexto de prueba
                var existingUser = new User
                {
                    Id = Guid.NewGuid(),
                    Name = "John",
                    LastName = "Doe",
                    Email = "john@example.com",
                    Password = "123"
                };

                // Crear un contexto de prueba utilizando un factory (TestContextFactory) y agregar el usuario existente al contexto
                var context = TestContextFactory.CreateContext();
                context.Users.AddRangeAsync(existingUser);
                context.SaveChangesAsync();

                // Configurar un mock para la unidad de trabajo (IUnitOfWork) y establecer su comportamiento para devolver el contexto de prueba
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Crear un repositorio de usuarios (UserRepository) utilizando el contexto de prueba y la unidad de trabajo simulada
                var repository = new UserRepository(mockUnitOfWork.Object);

                // ACT

                // Llamar al método GetUserByEmailAsync del repositorio con el correo electrónico del usuario existente y obtener el resultado
                var result = repository.GetByEmailAsync(existingUser.Email)?.Result;

                // ASSERT

                // Verificar que el resultado no sea nulo y que sea igual al usuario existente, lo que indica que se recuperó correctamente el usuario por su correo electrónico
                Assert.NotNull(result);
                Assert.Equal(existingUser, result);
            }

            /// <summary>
            /// Verifica que el método GetUserByEmailAsync devuelva null cuando se proporciona un correo electrónico que no está asociado a ningún usuario existente.
            /// </summary>
            [Fact]
            public void GetByEmailAsync_Returns_Null_When_NonExistentEmail()
            {
                // ARRANGE

                // Crear un usuario no existente para simular un escenario donde el correo electrónico no está asociado a ningún usuario existente
                var nonExistentUser = new User
                {
                    Id = Guid.NewGuid(),
                    Name = "John",
                    LastName = "Doe",
                    Email = "john@example.com",
                    Password = "123"
                };

                // Crear un contexto de prueba utilizando un factory (TestContextFactory)
                var context = TestContextFactory.CreateContext();

                // Configurar un mock para la unidad de trabajo (IUnitOfWork) y establecer su comportamiento para devolver el contexto de prueba
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Crear un repositorio de usuarios (UserRepository) utilizando el contexto de prueba y la unidad de trabajo simulada
                var repository = new UserRepository(mockUnitOfWork.Object);

                // ACT

                // Llamar al método GetUserByEmailAsync del repositorio con el correo electrónico de un usuario no existente y obtener el resultado
                var result = repository.GetByEmailAsync(nonExistentUser.Email)?.Result;

                // ASSERT

                // Verificar que el resultado sea nulo y que no sea igual al usuario no existente, lo que indica que no se encontró ningún usuario con el correo electrónico proporcionado
                Assert.Null(result);
                Assert.NotEqual(nonExistentUser, result);
            }
        }

        public class Authenticate
        {
            /// <summary>
            /// Verifica que el método AuthenticateAsync devuelva el usuario correspondiente cuando se proporcionan credenciales válidas.
            /// </summary>
            [Fact]
            public void AuthenticateAsync_Returns_ExistingUser_When_ValidCredentials()
            {
                // ARRANGE

                // Crear un usuario existente para simular un usuario registrado con credenciales válidas
                var existingUser = new User
                {
                    Id = Guid.NewGuid(),
                    Name = "John",
                    LastName = "Doe",
                    Email = "john@example.com",
                    Password = "123"
                };

                // Crear un contexto de prueba utilizando un factory (TestContextFactory)
                var context = TestContextFactory.CreateContext();

                // Agregar el usuario existente al contexto y guardar los cambios en la base de datos
                context.Users.AddRangeAsync(existingUser);
                context.SaveChangesAsync();

                // Configurar un mock para la unidad de trabajo (IUnitOfWork) y establecer su comportamiento para devolver el contexto de prueba
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Crear un repositorio de usuarios (UserRepository) utilizando el contexto de prueba y la unidad de trabajo simulada
                var repository = new UserRepository(mockUnitOfWork.Object);

                // ACT

                // Llamar al método AuthenticateAsync del repositorio con las credenciales del usuario existente y obtener el resultado
                var result = repository.AuthenticateAsync(existingUser.Email, existingUser.Password)?.Result;

                // ASSERT

                // Verificar que el resultado no sea nulo y sea igual al usuario existente, lo que indica que las credenciales proporcionadas son válidas
                Assert.NotNull(result);
                Assert.Equal(existingUser, result);
            }

            /// <summary>
            /// Verifica que el método AuthenticateAsync devuelva null cuando se proporcionan credenciales de un usuario que no está registrado.
            /// </summary>
            [Fact]
            public void AuthenticateAsync_Returns_Null_When_NonExistentUser()
            {
                // ARRANGE

                // Crear un usuario que no existe en la base de datos para simular credenciales de inicio de sesión incorrectas
                var nonExistentUser = new User
                {
                    Id = Guid.NewGuid(),
                    Name = "John",
                    LastName = "Doe",
                    Email = "john@example.com",
                    Password = "123"
                };

                // Crear un contexto de prueba utilizando un factory (TestContextFactory)
                var context = TestContextFactory.CreateContext();

                // Configurar un mock para la unidad de trabajo (IUnitOfWork) y establecer su comportamiento para devolver el contexto de prueba
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Crear un repositorio de usuarios (UserRepository) utilizando el contexto de prueba y la unidad de trabajo simulada
                var repository = new UserRepository(mockUnitOfWork.Object);

                // ACT

                // Llamar al método AuthenticateAsync del repositorio con las credenciales del usuario no existente y obtener el resultado
                var result = repository.AuthenticateAsync(nonExistentUser.Email, nonExistentUser.Password)?.Result;

                // ASSERT

                // Verificar que el resultado sea nulo, lo que indica que las credenciales proporcionadas no corresponden a ningún usuario registrado
                // También se verifica que el resultado no sea igual al usuario no existente, ya que no se esperaba que se encontrara ninguna coincidencia
                Assert.Null(result);
                Assert.NotEqual(nonExistentUser, result);
            }
        }
    }
}

//using CoWorkingApp.Core.Contracts.UnitOfWork;
//using CoWorkingApp.Core.Entities;
//using CoWorkingApp.Persistence.Context;
//using CoWorkingApp.Persistence.Repositories;
//using CoWorkingApp.Persistence.UnitOfWorks;
//using Microsoft.EntityFrameworkCore;
//using Moq;

//namespace CoWorkingApp.Tests.Persistence.Repositories
//{
//    public class ReservationRepositoryTest
//    {
//        public class Constructor
//        {
//            /// Verifica que el constructor de la clase ReservationRepository se pueda crear correctamente
//            /// cuando se le pasa un parámetro válido (unitOfWork).
//            /// </summary>
//            [Fact]
//            public void Constructor_ReturnsInstance_When_UnitOfWorkIsNotNull()
//            {
//                // ARRANGE
//                // Se crea un mock de IUnitOfWork
//                var unitOfWork = new Mock<IUnitOfWork>();

//                // ACT
//                // Se intenta crear una instancia de ReservationRepository con el mock de IUnitOfWork
//                var result = () => new ReservationRepository(unitOfWork.Object);

//                // ASSERT
//                // Se verifica que la creación de la instancia no lance excepciones
//                Assert.NotNull(result);
//            }

//            [Fact]
//            public void Constructor_ShouldNotThrowException_WhenDbSetIsNotNull()
//            {
//                // Arrange
//                var mockDbSet = new Mock<DbSet<Reservation>>();

//                // Configura el mock para que Set<Reservation>() devuelva un DbSet válido (simula que la base de datos está correctamente configurada)
//                var mockContext = new Mock<CoWorkingContext>(new DbContextOptions<CoWorkingContext>());
//                mockContext.Setup(c => c.Set<Reservation>()).Returns(mockDbSet.Object);

//                // Crear el mock para IUnitOfWork
//                var unitOfWork = new UnitOfWork(mockContext.Object);

//                //Act
//                var repository = new ReservationRepository(unitOfWork);


//                // Assert
//                Assert.NotNull(repository);
//            }

//            [Fact]
//            public void Constructor_ShouldThrowException_WhenDbSetIsNull()
//            {
//                // Arrange
//                // Configura el mock para que Set<Reservation>() devuelva un DbSet válido (simula que la base de datos está correctamente configurada)
//                var mockContext = new Mock<CoWorkingContext>(new DbContextOptions<CoWorkingContext>());

//                // Crear el mock para IUnitOfWork
//                var unitOfWork = new UnitOfWork(mockContext.Object);

//                //Act
//                var result = () => new ReservationRepository(unitOfWork);

//                // Assert
//                var exception = Assert.Throws<InvalidOperationException>(result);
//                Assert.Equal("DbSet could not be initialized.", exception.Message);  // Verifica el mensaje de la excepción
//            }

//            /// <summary>
//            /// Verifica que el constructor de la clase ReservationRepository lance una excepción
//            /// cuando se le pasa un parámetro nulo (unitOfWork).
//            /// </summary>
//            [Fact]
//            public void Constructor_Throws_ArgumentNullException_When_UnitOfWorkIsNull()
//            {
//                // ARRANGE
//                // Se establece unitOfWork como nulo
//                IUnitOfWork? unitOfWork = null;

//                // ACT
//                // Se intenta crear una instancia de ReservationRepository con unitOfWork nulo
//                var result = () => new ReservationRepository(unitOfWork);

//                // ASSERT
//                // Se verifica que la creación de la instancia lance una excepción del tipo ArgumentNullException
//                Assert.Throws<ArgumentNullException>(result);
//            }

//            [Fact]
//            public void Constructor_ShouldThrowInvalidOperationException_WhenDbSetIsNull()
//            {
//                // Arrange
//                var mockContext = new Mock<CoWorkingContext>(new DbContextOptions<CoWorkingContext>());
//                // Configura el mock para lanzar la InvalidOperationException cuando se llame a Set<Reservation>()
//                mockContext.Setup(u => u.Set<Reservation>())
//                              .Throws(new InvalidOperationException("DbSet could not be initialized."));

//                var unitOfWork = new UnitOfWork(mockContext.Object);

//                //Act
//                var result = () => new ReservationRepository(unitOfWork);

//                // Assert
//                var exception = Assert.Throws<InvalidOperationException>(result);
//                Assert.Equal("DbSet could not be initialized.", exception.Message);  // Verifica el mensaje de la excepción
//            }
//        }

//        public class GetAll
//        {
//            /// <summary>
//            /// Verifica si el método GetAllAsync devuelve todas las reservas cuando se llama.
//            /// </summary>
//            [Fact]
//            public async Task GetAllAsync_Returns_AllReservations_When_ContextContainsReservations()
//            {
//                // ARRANGE
//                // Crear un contexto simulado utilizando el factory de contexto de prueba
//                using var context = await TestContextFactory.InitializeDataRerservationsAsync();

//                // Configurar un mock para la unidad de trabajo que devuelva el contexto simulado
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Crear un repositorio de reservas con la unidad de trabajo simulada
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Llamar al método GetAllAsync del repositorio y obtener el resultado
//                var result = await repository.GetAllAsNoTrackingAsync();

//                // ASSERT
//                // Verificar que el resultado no sea nulo, que no esté vacío y que tenga la misma cantidad de elementos que las reservas simuladas
//                Assert.NotNull(result);
//                Assert.NotEmpty(result);
//                Assert.Equal(context.Reservations.Count(), result.Count());
//            }

//            /// <summary>
//            /// Prueba para verificar que GetAllAsync devuelva una colección vacía cuando no hay reservaciones en el repositorio.
//            /// </summary>
//            [Fact]
//            public async Task GetAllAsync_Returns_EmptyCollection_When_NoReservartionsInRepository()
//            {
//                // ARRANGE

//                // Crear un contexto de prueba utilizando un factory (TestContextFactory)
//                using var context = TestContextFactory.CreateContext();

//                // Configurar un mock para la unidad de trabajo (IUnitOfWork) y establecer su comportamiento para devolver el contexto de prueba
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Crear un repositorio de reservaciones (ReservationRepository) utilizando el contexto de prueba y la unidad de trabajo simulada
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT

//                // Llamar al método GetAllAsync del repositorio y obtener el resultado
//                var result = await repository.GetAllAsNoTrackingAsync();

//                // ASSERT

//                // Verificar que el resultado no sea nulo
//                Assert.NotNull(result);

//                // Verificar que la colección esté vacía
//                Assert.Empty(result);
//            }
//        }

//        public class GetById
//        {
//            /// <summary>
//            /// Verifica si el método GetByIdAsync devuelve la reserva existente cuando se proporciona su ID.
//            /// </summary>
//            [Fact]
//            public async Task GetByIdAsync_Returns_ExistingReservation_When_ValidReservationId()
//            {
//                // ARRANGE

//                // Crear un contexto simulado utilizando el factory de contexto
//                using var context = await TestContextFactory.InitializeDataRerservationsAsync();

//                // Crear un ID único para la reserva
//                Guid reservationId = context.Reservations.First().Id;

//                // Configurar un mock para la unidad de trabajo que devuelva el contexto simulado
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Crear un repositorio de reservas con la unidad de trabajo simulada
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Llamar al método GetByIdAsync para obtener la reserva por su ID
//                var result = await repository.GetByIdAsync(reservationId);

//                // ASSERT
//                // Verificar que el resultado no sea nulo y que tenga el ID correcto
//                Assert.NotNull(result);
//                Assert.Equal(reservationId, result.Id);
//            }

//            /// <summary>
//            /// Verifica que el método GetByIdAsync de la clase ReservationRepository devuelva nulo cuando se proporciona el ID de un reservacion que no existe en la base de datos.
//            /// </summary>
//            [Fact]
//            public async Task GetByIdAsync_Returns_Null_When_NonExistentReservationId()
//            {
//                // ARRANGE

//                // Se crea un contexto de prueba utilizando el factory de contexto
//                using var context = await TestContextFactory.InitializeDataRerservationsAsync();

//                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Se crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Se llama al método GetByIdAsync con un ID de reservacion que no existe en la base de datos
//                var result = await repository.GetByIdAsync(Guid.NewGuid());

//                // ASSERT
//                // Se verifica que el resultado sea nulo, lo que indica que no se encontró ningún reservacion con el ID proporcionado
//                Assert.Null(result);
//            }
//        }

//        public class Add
//        {
//            /// <summary>
//            /// Verifica que el método AddAsync de la clase ReservationRepository agregue correctamente un nueva reservacion a la base de datos.
//            /// </summary>
//            [Fact]
//            public async Task AddAsync_Returns_True_When_NewReservation()
//            {
//                // ARRANGE
//                // Se crea un contexto de prueba utilizando el factory de contexto
//                var context = await TestContextFactory.InitializeDataRerservationsAsync();

//                // Se crea un nueva reservacion para agregar a la base de datos
//                var reservation = new Reservation
//                {
//                    Date = DateTime.Now,
//                    UserId = context.Users.First().Id,
//                    SeatId = context.Reservations.First().Id
//                };

//                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Se crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Se llama al método AddAsync para agregar el nuevo reservaciones a la base de datos
//                var result = await repository.Add(reservation);

//                // ASSERT 
//                // Se verifica que el reservaciones se haya agregado correctamente
//                Assert.True(result);
//            }

//            [Fact]
//            public async Task AddAsync_Returns_True_When_UserNotExist()
//            {
//                // ARRANGE
//                // Se crea un contexto de prueba utilizando el factory de contexto
//                var context = await TestContextFactory.InitializeDataRerservationsAsync();

//                // Se crea un nuevo reservaciones para agregar a la base de datos
//                var reservation = new Reservation
//                {
//                    Date = DateTime.Now,
//                    UserId = Guid.NewGuid()
//                };

//                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Se crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Se llama al método AddAsync para agregar el nuevo reservaciones a la base de datos
//                var result = await repository.Add(reservation);

//                // ASSERT 
//                // Se verifica que el reservaciones se haya agregado correctamente
//                Assert.True(result);
//            }

//            [Fact]
//            public async Task AddAsync_Returns_True_When_SeatNotExist()
//            {
//                // ARRANGE
//                // Se crea un contexto de prueba utilizando el factory de contexto
//                var context = await TestContextFactory.InitializeDataRerservationsAsync();

//                // Se crea un nuevo reservaciones para agregar a la base de datos
//                var reservation = new Reservation
//                {
//                    Date = DateTime.Now,
//                    SeatId = Guid.NewGuid()
//                };

//                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Se crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Se llama al método AddAsync para agregar el nuevo reservaciones a la base de datos
//                var result = await repository.Add(reservation);

//                // ASSERT 
//                // Se verifica que el reservaciones se haya agregado correctamente
//                Assert.True(result);
//            }

//            /// <summary>
//            /// Verifica que el método AddAsync de la clase ReservationRepository maneje correctamente una excepción al intentar agregar un nuevo reservaciones a la base de datos.
//            /// </summary>
//            [Fact]
//            public async Task AddAsync_Returns_False_When_ThrowException()
//            {
//                // ARRANGE
//                // Se crea un contexto de prueba utilizando el factory de contexto
//                var context = TestContextFactory.CreateContext();

//                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                // Se configura el mock para lanzar una excepción al intentar confirmar los cambios en la base de datos
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);
//                mockUnitOfWork.Setup(u => u.CommitAsync()).ThrowsAsync(new Exception());

//                // Se crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Se llama al método AddAsync para intentar agregar un nuevo reservaciones a la base de datos, lo que debería provocar una excepción
//                var result = await repository.Add(It.IsAny<Reservation>());

//                // ASSERT
//                // Se verifica que la operación haya fallado (retorna false)
//                Assert.False(result);
//            }
//        }

//        public class Update
//        {
//            /// <summary>
//            /// Verifica que el método UpdateAsync actualice una reserva existente con las claves externas existentes.
//            /// </summary>
//            [Fact]
//            public async Task UpdateAsync_Returns_True_When_ExistitngFKs()
//            {
//                // ARRANGE
//                // Se crea un contexto de prueba utilizando el factory de contexto
//                using var context = await TestContextFactory.InitializeDataRerservationsAsync();

//                // Se crea una reserva existente para simular los datos en la base de datos
//                var reservationExisting = new Reservation
//                {
//                    Date = DateTime.Now,
//                    UserId = context.Users.First().Id,
//                    SeatId = context.Reservations.First().Id,
//                };

//                // Se establece el estado de la reserva existente como "Desvinculado" para simular la carga desde la base de datos
//                context.Entry(reservationExisting).State = EntityState.Detached;

//                // Se establece un mock de IUnitOfWork para simular el trabajo de la unidad de trabajo
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Se crea una nueva fecha
//                var newDate = DateTime.Now.AddDays(1);

//                // Se crea una nueva reserva con los datos actualizados
//                var reservationToUpdate = new Reservation
//                {
//                    Id = reservationExisting.Id,               // Se mantiene el mismo Id de la reserva existente
//                    UserId = reservationExisting.UserId,       // Se mantiene el mismo UserId de la reserva existente
//                    SeatId = reservationExisting.SeatId,       // Se mantiene el mismo SeatId de la reserva existente
//                    Date = newDate,                            // Se actualiza la fecha de la reserva existente
//                };

//                // Se crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Se llama al método UpdateAsync para actualizar la reserva en la base de datos
//                var result = await repository.Update(reservationToUpdate);

//                // ASSERT
//                // Se verifica que la operación haya tenido éxito (retorna true)
//                Assert.True(result);
//                // Se verifica que los datos de la reserva actualizada sean consistentes con los datos esperados
//                Assert.Equal(reservationExisting, reservationToUpdate);
//                Assert.NotEqual(reservationExisting.Date, reservationToUpdate.Date);
//                Assert.Equal(reservationExisting.UserId, reservationToUpdate.UserId);
//                Assert.Equal(reservationExisting.SeatId, reservationToUpdate.SeatId);
//            }

//            /// <summary>
//            /// Verifica que el método UpdateAsync actualice una reserva existente con claves externas inexistentes.
//            /// </summary>
//            [Fact]
//            public async Task UpdateAsync_Returns_True_When_NotExistFKs()
//            {
//                // ARRANGE
//                // Se crea un contexto de prueba utilizando el factory de contexto
//                using var context = await TestContextFactory.InitializeDataRerservationsAsync();

//                // Se crea una reserva existente para simular los datos en la base de datos
//                var reservationExisting = new Reservation
//                {
//                    Date = DateTime.Now,
//                    UserId = context.Users.First().Id,
//                    SeatId = context.Reservations.First().Id,
//                };

//                // Se establece el estado de la reserva existente como "Desvinculado" para simular la carga desde la base de datos
//                context.Entry(reservationExisting).State = EntityState.Detached;

//                // Se establece un mock de IUnitOfWork para simular el trabajo de la unidad de trabajo
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // se crea una nueva fecha
//                var newDate = DateTime.Now.AddDays(1);

//                // Se crea una nueva reserva con los datos actualizados
//                var reservationToUpdate = new Reservation
//                {
//                    Id = reservationExisting.Id,               // Se mantiene el mismo Id de la reserva existente
//                    Date = newDate,                            // Se actualiza la fecha de la reserva existente
//                };

//                // Se crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Se llama al método UpdateAsync para actualizar la reserva en la base de datos
//                var result = await repository.Update(reservationToUpdate);

//                // ASSERT
//                // Se verifica que la operación haya tenido éxito (retorna true)
//                Assert.True(result);
//                // Se verifica que los datos de la reserva actualizada sean consistentes con los datos esperados
//                Assert.Equal(reservationExisting, reservationToUpdate);
//                Assert.NotEqual(reservationExisting.Date, reservationToUpdate.Date);
//                Assert.NotEqual(reservationExisting.UserId, reservationToUpdate.UserId);
//                Assert.NotEqual(reservationExisting.SeatId, reservationToUpdate.SeatId);
//            }

//            /// <summary>
//            /// Verifica que el método UpdateAsync de la clase ReservationRepository maneje correctamente una excepción al intentar actualizar un reservacion en la base de datos.
//            /// </summary>
//            [Fact]
//            public async Task UpdateAsync_Returns_False_When_ThrowException()
//            {
//                // ARRANGE
//                // Se crea un contexto de prueba utilizando el factory de contexto
//                var context = TestContextFactory.CreateContext();

//                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Se crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Se llama al método UpdateAsync con un reservacion genérico para provocar una excepción
//                var result = await repository.Update(It.IsAny<Reservation>());

//                // ASSERT
//                // Se verifica que la operación haya fallado (retorna false)
//                Assert.False(result);
//            }
//        }

//        public class Remove
//        {
//            /// <summary>
//            /// Verifica que el método RemoveAsync de la clase ReservationRepository elimine correctamente un reservacion existente de la base de datos.
//            /// </summary>
//            [Fact]
//            public async Task RemoveAsync_Returns_True_When_ExistingReservation()
//            {
//                // ARRANGE
//                // Se crea un contexto de prueba utilizando el factory de contexto
//                using var context = await TestContextFactory.InitializeDataRerservationsAsync();

//                // Obtiene la primera reserva existente en el contexto de datos
//                var existingReservation = context.Reservations.First();

//                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Se crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Se llama al método RemoveAsync con la reservacion existente para eliminarla de la base de datos
//                var result = await repository.Remove(existingReservation);

//                // ASSERT
//                // Se verifica que la operación haya sido exitosa (retorna true)
//                Assert.True(result);
//            }

//            /// <summary>
//            /// Verifica que el método RemoveAsync de la clase ReservationRepository maneje correctamente una excepción cuando ocurre al intentar eliminar una reservacion de la base de datos.
//            /// </summary>
//            [Fact]
//            public async Task RemoveAsync_Returns_False_When_ThrowException()
//            {
//                // ARRANGE
//                // Se crea un contexto de prueba utilizando el factory de contexto
//                var context = TestContextFactory.CreateContext();

//                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Se crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Se llama al método RemoveAsync con cualquier reservacion para simular una excepción al 0 eliminarlo de la base de datos
//                var result = await repository.Remove(It.IsAny<Reservation>());

//                // ASSERT
//                // Se verifica que la operación haya fallado (retorna false)
//                Assert.False(result);
//            }
//        }

//        public class Exists
//        {
//            /// <summary>
//            /// Verifica que el método ExistsAsync de la clase ReservationRepository devuelva verdadero cuando se busca una reservacion que existe en la base de datos.
//            /// </summary>
//            [Fact]
//            public async Task ExistsAsync_Returns_True_When_ExistingReservation()
//            {
//                // ARRANGE
//                // Inicializa el contexto de prueba con datos de reservaciones
//                var context = await TestContextFactory.InitializeDataRerservationsAsync();

//                // Obtiene la primera reserva existente en el contexto de prueba
//                var existingReservation = context.Reservations.First();

//                // Obtiene el ID de la reserva existente
//                var reservationId = existingReservation.Id;

//                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo y se configura para devolver el contexto de prueba
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Se crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Se llama al método ExistsAsync con el ID de la reserva existente para verificar su existencia en la base de datos
//                var result = await repository.ContainsAsync(reservationId);

//                // ASSERT
//                // Se verifica que sea verdadero, lo que indica que la reserva existe en la base de datos
//                Assert.True(result);
//            }

//            /// <summary>
//            /// Verifica que el método ExistsAsync de la clase ReservationRepository devuelva falso cuando se busca una reservacion que no existe en la base de datos.
//            /// </summary>
//            [Fact]
//            public async Task ExistsAsync_Returns_False_When_NotExistReservation()
//            {
//                // ARRANGE
//                // Se crea un contexto de prueba utilizando el factory de contexto
//                var context = TestContextFactory.CreateContext();

//                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo y se configura para devolver el contexto de prueba
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Se crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Se llama al método ExistsAsync con un ID de reservacion que no existe en la base de datos para verificar su existencia
//                var result = await repository.ContainsAsync(Guid.NewGuid());

//                // ASSERT
//                // Se verifica que sea falso, lo que indica que la reservacion no existe en la base de datos
//                Assert.False(result);
//            }
//        }

//        public class GetByUserId
//        {
//            /// <summary>
//            /// Verifica que el método GetByUserIdAsync de ReservationRepository devuelva reservaciones existentes asociadas a un ID de usuario dado.
//            /// </summary>
//            [Fact]
//            public async Task GetByUserIdAsync_Returns_Reservations_When_SearchingBySpecificUserId()
//            {
//                // ARRANGE
//                // Inicializa el contexto de prueba con datos de reservaciones
//                using var context = await TestContextFactory.InitializeDataRerservationsAsync();

//                // Obtiene el ID de usuario de la primera reserva existente en el contexto de prueba
//                Guid userId = context.Reservations.First().UserId;

//                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo y se configura para devolver el contexto de prueba
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Llama al método GetByUserIdAsync con el ID de usuario para obtener las reservaciones asociadas
//                var result = await repository.GetByUserIdAsNoTrackingAsync(userId);

//                // ASSERT
//                Assert.NotNull(result);
//                Assert.NotEmpty(result);
//                Assert.True(result.All(r => r.UserId == userId));
//            }

//            /// <summary>
//            /// Verifica que el método GetByUserIdAsync de ReservationRepository no devuelva reservaciones cuando no hay ninguna asociada al ID de usuario proporcionado.
//            /// </summary>
//            [Fact]
//            public async Task GetByUserIdAsync_Returns_EmptyCollection_When_SearchingBySpecificUserId()
//            {
//                // ARRANGE
//                // Inicializa el contexto de prueba con datos de reservaciones
//                using var context = await TestContextFactory.InitializeDataRerservationsAsync();

//                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo y se configura para devolver el contexto de prueba
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Se crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Llama al método GetByUserIdAsync con un ID de usuario no existente para buscar reservaciones asociadas
//                var result = await repository.GetByUserIdAsNoTrackingAsync(Guid.NewGuid());

//                // ASSERT
//                Assert.NotNull(result);
//                Assert.Empty(result);
//            }
//        }

//        public class GetBySeatId
//        {
//            /// <summary>
//            /// Verifica que el método GetBySeatIdAsync de ReservationRepository devuelva reservaciones existentes asociadas a un ID de asiento proporcionado.
//            /// </summary>
//            [Fact]
//            public async Task GetBySeatIdAsync_Returns_Reservations_When_SearchingBySpecificSeatId()
//            {
//                // ARRANGE
//                // Inicializa el contexto de prueba con datos de reservaciones
//                using var context = await TestContextFactory.InitializeDataRerservationsAsync();

//                // Obtiene el ID del asiento de la primera reservación existente en el contexto de prueba
//                Guid seatId = context.Reservations.First().SeatId;

//                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo y se configura para devolver el contexto de prueba
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Se crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Llama al método GetBySeatIdAsync con el ID de asiento para obtener las reservaciones asociadas
//                var result = await repository.GetBySeatIdAsNoTrackingAsync(seatId);

//                // ASSERT
//                Assert.NotNull(result);
//                Assert.NotEmpty(result);
//                Assert.True(result.All(r => r.SeatId == seatId));
//            }

//            /// <summary>
//            /// Verifica que el método GetBySeatIdAsync de ReservationRepository maneje correctamente el caso en que no existen reservaciones asociadas a un ID de asiento proporcionado.
//            /// </summary>
//            [Fact]
//            public async Task GetBySeatIdAsync_Returns_EmptyCollection_When_SearchingBySpecificSeatId()
//            {
//                // ARRANGE
//                // Inicializa el contexto de prueba con datos de reservaciones
//                using var context = await TestContextFactory.InitializeDataRerservationsAsync();

//                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo y se configura para devolver el contexto de prueba
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Se crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Llama al método GetBySeatIdAsync con un ID de asiento no existente para obtener las reservaciones asociadas
//                var result = await repository.GetBySeatIdAsNoTrackingAsync(Guid.NewGuid());

//                // ASSERT
//                Assert.NotNull(result);
//                Assert.Empty(result);
//            }
//        }

//        public class GetByDate
//        {
//            /// <summary>
//            /// Verifica que el método GetByDateAsync de ReservationRepository devuelva las reservaciones existentes para una fecha dada.
//            /// </summary>
//            [Fact]
//            public async Task GetByDateAsync_Returns_Reservations_When_SearchingBySpecificDate()
//            {
//                // ARRANGE
//                // Inicializa el contexto de prueba con datos de reservaciones
//                var context = await TestContextFactory.InitializeDataRerservationsAsync();

//                // Obtiene la primera reserva existente en el contexto de prueba
//                var existingReservation = context.Reservations.First();
//                // Obtiene la fecha de la reserva existente
//                var date = existingReservation.Date;

//                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo y se configura para devolver el contexto de prueba
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Se crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Se llama al método GetByDateAsync con la fecha de la reserva existente para obtener las reservaciones para esa fecha
//                var result = await repository.GetByDateAsNoTrackingAsync(date);

//                // ASSERT
//                Assert.NotNull(result);
//                Assert.NotEmpty(result);
//                Assert.True(result.All(r => r.Date == date));
//            }

//            /// <summary>
//            /// Verifica que el método GetByDateAsync de ReservationRepository no devuelva reservaciones cuando no hay ninguna para la fecha dada.
//            /// </summary>
//            [Fact]
//            public async Task GetByDateAsync_Returns_EmptyCollection_When_SearchingBySpecificDate()
//            {
//                // ARRANGE
//                // Inicializa el contexto de prueba con datos de reservaciones
//                var context = TestContextFactory.CreateContext();

//                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo y se configura para devolver el contexto de prueba
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Se crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Se llama al método GetByDateAsync con la fecha actual para verificar si hay reservaciones para esta fecha
//                var result = await repository.GetByDateAsNoTrackingAsync(DateTime.Now);

//                // ASSERT
//                Assert.NotNull(result);
//                Assert.Empty(result);
//            }
//        }

//        public class GetByUserEmail
//        {
//            /// <summary>
//            /// Verifica que el método GetByUserEmailAsync de ReservationRepository devuelva reservaciones existentes asociadas a una dirección de correo electrónico dada.
//            /// </summary>
//            [Fact]
//            public async Task GetByUserEmailAsync_Returns_Reservations_When_SearchingBySpecificEmail()
//            {
//                // ARRANGE
//                // Inicializa el contexto de prueba con datos de reservaciones
//                var context = await TestContextFactory.InitializeDataRerservationsAsync();

//                // Obtiene la primera reserva existente en el contexto de prueba
//                var existingReservation = context.Reservations.First();

//                // Obtiene el usuario asociado a la reservación existente
//                var user = await context.Users.FirstAsync(r => r.Id == existingReservation.UserId);

//                // Obtiene el correo electrónico del usuario
//                var email = user.Email;

//                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo y se configura para devolver el contexto de prueba
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Llama al método GetByUserEmailAsync con el correo electrónico del usuario para obtener las reservaciones asociadas
//                var result = await repository.GetByUserEmailAsNoTrackingAsync(email);

//                // ASSERT
//                Assert.NotNull(result);
//                Assert.NotEmpty(result);
//                Assert.True(result.All(r => r.User != null && r.User.Email == email));
//            }

//            /// <summary>
//            /// Verifica que el método GetByUserEmailAsync de ReservationRepository no devuelva reservaciones cuando no hay ninguna asociada al correo electrónico dado.
//            /// </summary>
//            [Fact]
//            public async Task GetByUserEmailAsync_Returns_EmptyCollection_When_SearchingBySpecificEmail()
//            {
//                // ARRANGE
//                // Inicializa el contexto de prueba
//                var context = TestContextFactory.CreateContext();

//                // Establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo y configurarlo para devolver el contexto de prueba
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Llama al método GetByUserEmailAsync con una dirección de correo electrónico inexistente para verificar si hay reservaciones asociadas a ella
//                var result = await repository.GetByUserEmailAsNoTrackingAsync(It.IsAny<string>());

//                // ASSERT
//                Assert.NotNull(result);
//                Assert.Empty(result);
//            }
//        }

//        public class GetBySeatName
//        {
//            /// <summary>
//            /// Verifica que el método GetBySeatNameAsync de ReservationRepository devuelva reservaciones existentes asociadas a un nombre del asiento dado.
//            /// </summary>
//            [Fact]
//            public async Task GetByUserEmailAsync_Returns_Reservations_When_SearchingBySpecificSeatName()
//            {
//                // ARRANGE
//                // Inicializa el contexto de prueba con datos de reservaciones
//                var context = await TestContextFactory.InitializeDataRerservationsAsync();

//                // Obtiene la primera reserva existente en el contexto de prueba
//                var existingReservation = context.Reservations.First();

//                // Obtiene el asiento asociado a la reservación existente
//                var seat = await context.Seats.FirstAsync(r => r.Id == existingReservation.SeatId);

//                // Obtiene el nombre del asiento
//                var seatName = seat.Name;

//                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo y se configura para devolver el contexto de prueba
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Llama al método GetBySeatNameAsync con el nombre del asiento para obtener las reservaciones asociadas
//                var result = await repository.GetBySeatNameAsNoTrackingAsync(seatName);

//                // ASSERT
//                Assert.NotNull(result);
//                Assert.NotEmpty(result);
//                Assert.True(result.All(r => r.Seat != null && r.Seat.Name == seatName));
//            }

//            /// <summary>
//            /// Verifica que el método GetBySeatNameAsync de ReservationRepository no devuelva reservaciones cuando no hay ninguna asociada al nombre de asiento dado.
//            /// </summary>
//            [Fact]
//            public async Task GetByUserEmailAsync_Returns_EmptyCollection_When_SearchingBySpecificSeatName()
//            {
//                // ARRANGE
//                // Inicializa el contexto de prueba
//                var context = TestContextFactory.CreateContext();

//                // Establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo y configurarlo para devolver el contexto de prueba
//                var mockUnitOfWork = new Mock<IUnitOfWork>();
//                mockUnitOfWork.Setup(u => u.Context).Returns(context);

//                // Crea una instancia de ReservationRepository con el mock de IUnitOfWork
//                var repository = new ReservationRepository(mockUnitOfWork.Object);

//                // ACT
//                // Llama al método GetBySeatNameAsync con un nombre de asiento inexistente para verificar si hay reservaciones asociadas a ella
//                var result = await repository.GetBySeatNameAsNoTrackingAsync(It.IsAny<string>());

//                // ASSERT
//                Assert.NotNull(result);
//                Assert.Empty(result);
//            }
//        }
//    }
//}

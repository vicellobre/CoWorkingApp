using CoWorkingApp.API.Infrastructure.Persistence.Repositories;
using CoWorkingApp.API.Infrastructure.UnitOfWorks;
using CoWorkingApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CoWorkingApp.Tests.Infrastructure.Repositories
{
    public class SeatRepositoryTest
    {
        public class Constructor
        {
            /// <summary>
            /// Verifica que el constructor de SeatRepository se pueda crear correctamente
            /// cuando se le pasa un parámetro válido (unitOfWork).
            /// </summary>
            [Fact]
            public void Constructor_ReturnsInstance_When_UnitOfWorkIsNotNull()
            {
                // ARRANGE
                // Se crea un mock de IUnitOfWork
                var unitOfWork = new Mock<IUnitOfWork>();

                // ACT
                // Se intenta crear una instancia de SeatRepository con el mock de IUnitOfWork
                var result = () => new SeatRepository(unitOfWork.Object);

                // ASSERT
                // Se verifica que la creación de la instancia no lance excepciones
                Assert.NotNull(result);
            }

            /// <summary>
            /// Verifica que el constructor de SeatRepository arroje una excepción ArgumentNullException
            /// cuando se le pasa un parámetro nulo (unitOfWork).
            /// </summary>
            [Fact]
            public void Constructor_Throws_ArgumentNullException_When_UnitOfWorkIsNull()
            {
                // ARRANGE
                // Se inicializa unitOfWork como null
                IUnitOfWork unitOfWork = null;

                // ACT
                // Se intenta crear una instancia de SeatRepository con unitOfWork como null
                var result = () => new SeatRepository(unitOfWork);

                // ASSERT
                // Se verifica que la creación de la instancia arroje una excepción del tipo ArgumentNullException
                Assert.Throws<ArgumentNullException>(result);
            }
        }

        public class GetAll
        {
            /// <summary>
            /// Verifica que el método GetAllAsync de la clase SeatRepository devuelva todas las instancias de asientos correctamente.
            /// </summary>
            [Fact]
            public async void GetAllAsync_Returns_AllSeats_When_ContextContainsSeats()
            {
                // ARRANGE

                // Se crean instancias de asientos de prueba
                var seats = new List<Seat>
                {
                    new Seat { Id = Guid.NewGuid(), Name = "Q-1", IsBlocked = false, Description = "This seat is located near the window." },
                    new Seat { Id = Guid.NewGuid(), Name = "M-2", IsBlocked = true, Description = "This seat is reserved for VIP guests." }
                };

                // Se crea un contexto de prueba utilizando el factory de contexto
                using var context = TestContextFactory.CreateContext();
                // Se agregan los asientos al contexto y se guardan los cambios
                await context.Seats.AddRangeAsync(seats);
                await context.SaveChangesAsync();

                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Se crea una instancia de SeatRepository con el mock de IUnitOfWork
                var repositoy = new SeatRepository(mockUnitOfWork.Object);

                // ACT
                // Se llama al método GetAllAsync para obtener todos los asientos
                var result = await repositoy.GetAllAsync();

                // ASSERT
                // Se verifica que el resultado no sea nulo y contenga la misma cantidad de asientos que los asientos de prueba
                Assert.NotNull(result);
                Assert.Equal(seats.Count, result.Count());
                // Se verifica que los identificadores de los asientos en el resultado coincidan con los de los asientos de prueba
                Assert.Equal(seats.Select(u => u.Id), result.Select(u => u.Id));
            }

            /// <summary>
            /// Verifica que el método GetAllAsync de la clase SeatRepository devuelva una colección vacía cuando no hay asientos en la base de datos.
            /// </summary>
            [Fact]
            public async void GetAllAsync_Returns_EmptyCollection_When_NoSeatsInRepository()
            {
                // ARRANGE
                // Se crea un contexto de prueba utilizando el factory de contexto
                using var context = TestContextFactory.CreateContext();

                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Se crea una instancia de SeatRepository con el mock de IUnitOfWork
                var repository = new SeatRepository(mockUnitOfWork.Object);

                // ACT
                // Se llama al método GetAllAsync para obtener todos los asientos
                var result = await repository.GetAllAsync();

                // ASSERT
                // Se verifica que el resultado no sea nulo y esté vacío
                Assert.NotNull(result);
                Assert.Empty(result);
            }
        }

        public class GetById
        {
            /// <summary>
            /// Verifica que el método GetByIdAsync de la clase SeatRepository devuelva el asiento correspondiente cuando se proporciona un ID de asiento existente.
            /// </summary>
            [Fact]
            public async void GetByIdAsync_Returns_ExistingSeat_When_ValidSeatId()
            {
                // ARRANGE
                // Se genera un nuevo GUID para el ID del asiento
                Guid seatId = Guid.NewGuid();

                // Se crea una lista de asientos simulados
                var seats = new List<Seat>
                {
                    new Seat { Id = seatId, Name = "Q-1", IsBlocked = false, Description = "This seat is located near the window." },
                    new Seat { Id = Guid.NewGuid(), Name = "M-2", IsBlocked = true, Description = "This seat is reserved for VIP guests." }
                };

                // Se crea un contexto de prueba utilizando el factory de contexto y se agregan los asientos simulados
                using var context = TestContextFactory.CreateContext();
                await context.Seats.AddRangeAsync(seats);
                await context.SaveChangesAsync();

                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Se crea una instancia de SeatRepository con el mock de IUnitOfWork
                var repository = new SeatRepository(mockUnitOfWork.Object);

                // ACT
                // Se llama al método GetByIdAsync para obtener el asiento por su ID
                var result = await repository.GetByIdAsync(seatId);

                // ASSERT
                // Se verifica que el resultado no sea nulo y que tenga el ID correcto
                Assert.NotNull(result);
                Assert.Equal(seatId, result.Id);
            }

            /// <summary>
            /// Verifica que el método GetByIdAsync de la clase SeatRepository devuelva nulo cuando se proporciona el ID de un asiento que no existe en la base de datos.
            /// </summary>
            [Fact]
            public async void GetByIdAsync_Returns_Null_When_NonExistentSeatId()
            {
                // ARRANGE
                // Se crea una lista de asientos simulados
                var seats = new List<Seat>
                {
                    new Seat { Id = Guid.NewGuid(), Name = "Q-1", IsBlocked = false, Description = "This seat is located near the window." },
                    new Seat { Id = Guid.NewGuid(), Name = "M-2", IsBlocked = true, Description = "This seat is reserved for VIP guests." }
                };

                // Se crea un contexto de prueba utilizando el factory de contexto y se agregan los asientos simulados
                using var context = TestContextFactory.CreateContext();
                await context.Seats.AddRangeAsync(seats);
                await context.SaveChangesAsync();

                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Se crea una instancia de SeatRepository con el mock de IUnitOfWork
                var repository = new SeatRepository(mockUnitOfWork.Object);

                // ACT
                // Se llama al método GetByIdAsync con un ID de asiento que no existe en la base de datos
                var result = await repository.GetByIdAsync(Guid.NewGuid());

                // ASSERT
                // Se verifica que el resultado sea nulo, lo que indica que no se encontró ningún asiento con el ID proporcionado
                Assert.Null(result);
            }
        }

        public class Add
        {
            /// <summary>
            /// Verifica que el método AddAsync de la clase SeatRepository agregue correctamente un nuevo asiento a la base de datos.
            /// </summary>
            [Fact]
            public async void AddAsync_Returns_True_When_NewSeat()
            {
                // ARRANGE
                // Se crea un nuevo asiento para agregar a la base de datos
                var seat = new Seat
                {
                    Id = Guid.NewGuid(),
                    Name = "Q-1",
                    IsBlocked = false,
                    Description = "This seat is located near the window."
                };

                // Se crea un contexto de prueba utilizando el factory de contexto
                var context = TestContextFactory.CreateContext();

                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Se crea una instancia de SeatRepository con el mock de IUnitOfWork
                var repository = new SeatRepository(mockUnitOfWork.Object);

                // ACT
                // Se llama al método AddAsync para agregar el nuevo asiento a la base de datos
                var result = await repository.AddAsync(seat);

                // ASSERT 
                // Se verifica que el asiento se haya agregado correctamente
                Assert.True(result);
            }

            /// <summary>
            /// Verifica que el método AddAsync de la clase SeatRepository maneje correctamente una excepción al intentar agregar un nuevo asiento a la base de datos.
            /// </summary>
            [Fact]
            public async void AddAsync_Returns_False_When_ThrowException()
            {
                // ARRANGE
                // Se crea un contexto de prueba utilizando el factory de contexto
                var context = TestContextFactory.CreateContext();

                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                // Se configura el mock para lanzar una excepción al intentar confirmar los cambios en la base de datos
                mockUnitOfWork.Setup(u => u.Context).Returns(context);
                mockUnitOfWork.Setup(u => u.Commit()).ThrowsAsync(new Exception());

                // Se crea una instancia de SeatRepository con el mock de IUnitOfWork
                var repository = new SeatRepository(mockUnitOfWork.Object);

                // ACT
                // Se llama al método AddAsync para intentar agregar un nuevo asiento a la base de datos, lo que debería provocar una excepción
                var result = await repository.AddAsync(It.IsAny<Seat>());

                // ASSERT
                // Se verifica que la operación haya fallado (retorna false)
                Assert.False(result);
            }
        }

        public class Update
        {
            /// <summary>
            /// Verifica que el método UpdateAsync de la clase SeatRepository actualice correctamente un asiento existente en la base de datos.
            /// </summary>
            [Fact]
            public async void UpdateAsync_Returns_True_When_ExistingSeat()
            {
                // ARRANGE
                // Se crea un asiento existente para simular los datos en la base de datos
                var seatExisting = new Seat
                {
                    Id = Guid.NewGuid(),
                    Name = "Q-1",
                    IsBlocked = false,
                    Description = "This seat is located near the window."
                };

                // Se crea un contexto de prueba utilizando el factory de contexto
                var context = TestContextFactory.CreateContext();

                // Se agregan los asientos al contexto y se guardan los cambios en la base de datos
                await context.Seats.AddRangeAsync(seatExisting);
                await context.SaveChangesAsync();
                // Se establece el estado del asiento existente como "Desvinculado" para simular la carga desde la base de datos
                context.Entry(seatExisting).State = EntityState.Detached;

                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Se crea un nuevo asiento con los datos actualizados
                var seatToUpdate = new Seat
                {
                    Id = seatExisting.Id,               // Se mantiene el mismo Id del asiento existente
                    Name = "M-2",                       // Se actualiza el nombre del asiento
                    IsBlocked = true,                   // Se actualiza el estado del asiento
                    Description = "This seat is reserved for VIP guests." // Se actualiza la descripción del asiento
                };

                // Se crea una instancia de SeatRepository con el mock de IUnitOfWork
                var repository = new SeatRepository(mockUnitOfWork.Object);

                // ACT
                // Se llama al método UpdateAsync para actualizar el asiento en la base de datos
                var result = await repository.UpdateAsync(seatToUpdate);

                // ASSERT
                // Se verifica que la operación haya tenido éxito (retorna true)
                Assert.True(result);
                // Se verifica que los datos del asiento actualizado sean consistentes con los datos esperados
                Assert.Equal(seatExisting, seatToUpdate);
            }

            /// <summary>
            /// Verifica que el método UpdateAsync de la clase SeatRepository maneje correctamente una excepción al intentar actualizar un asiento en la base de datos.
            /// </summary>
            [Fact]
            public async void UpdateAsync_Returns_False_When_ThrowException()
            {
                // ARRANGE
                // Se crea un contexto de prueba utilizando el factory de contexto
                var context = TestContextFactory.CreateContext();

                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Se crea una instancia de SeatRepository con el mock de IUnitOfWork
                var repository = new SeatRepository(mockUnitOfWork.Object);

                // ACT
                // Se llama al método UpdateAsync con un asiento genérico para provocar una excepción
                var result = await repository.UpdateAsync(It.IsAny<Seat>());

                // ASSERT
                // Se verifica que la operación haya fallado (retorna false)
                Assert.False(result);
            }
        }

        public class Remove
        {
            /// <summary>
            /// Verifica que el método RemoveAsync de la clase SeatRepository elimine correctamente un asiento existente de la base de datos.
            /// </summary>
            [Fact]
            public async void RemoveAsync_Returns_True_When_ExistingSeat()
            {
                // ARRANGE
                // Se crea un asiento existente para realizar la prueba
                var existingSeat = new Seat
                {
                    Id = Guid.NewGuid(),
                    Name = "Q-1",
                    IsBlocked = false,
                    Description = "This seat is located near the window."
                };

                // Se crea un contexto de prueba utilizando el factory de contexto
                var context = TestContextFactory.CreateContext();

                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Se crea una instancia de SeatRepository con el mock de IUnitOfWork
                var repository = new SeatRepository(mockUnitOfWork.Object);

                // ACT
                // Se llama al método RemoveAsync con el asiento existente para eliminarlo de la base de datos
                var result = await repository.RemoveAsync(existingSeat);

                // ASSERT
                // Se verifica que la operación haya sido exitosa (retorna true)
                Assert.True(result);
            }

            /// <summary>
            /// Verifica que el método RemoveAsync de la clase SeatRepository maneje correctamente una excepción cuando ocurre al intentar eliminar un asiento de la base de datos.
            /// </summary>
            [Fact]
            public async void RemoveAsync_Returns_False_When_ThrowException()
            {
                // ARRANGE
                // Se crea un contexto de prueba utilizando el factory de contexto
                var context = TestContextFactory.CreateContext();

                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Se crea una instancia de SeatRepository con el mock de IUnitOfWork
                var repository = new SeatRepository(mockUnitOfWork.Object);

                // ACT
                // Se llama al método RemoveAsync con cualquier asiento para simular una excepción al intentar eliminarlo de la base de datos
                var result = await repository.RemoveAsync(It.IsAny<Seat>());

                // ASSERT
                // Se verifica que la operación haya fallado (retorna false)
                Assert.False(result);
            }
        }

        public class Exists
        {
            /// <summary>
            /// Verifica que el método ExistsAsync de la clase SeatRepository devuelva verdadero cuando se busca un asiento que existe en la base de datos.
            /// </summary>
            [Fact]
            public async void ExistsAsync_Returns_True_When_ExistingSeat()
            {
                // ARRANGE
                // Se genera un ID para el asiento existente
                var seatId = Guid.NewGuid();

                // Se crea un asiento existente con los datos específicos
                var existingSeat = new Seat
                {
                    Id = seatId,
                    Name = "Q-1",
                    IsBlocked = false,
                    Description = "This seat is located near the window."
                };

                // Se crea un contexto de prueba utilizando el factory de contexto y se agrega el asiento existente a la base de datos
                var context = TestContextFactory.CreateContext();
                await context.Seats.AddRangeAsync(existingSeat);
                await context.SaveChangesAsync();

                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo y se configura para devolver el contexto de prueba
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Se crea una instancia de SeatRepository con el mock de IUnitOfWork
                var repository = new SeatRepository(mockUnitOfWork.Object);

                // ACT
                // Se llama al método ExistsAsync con el ID del asiento existente para verificar su existencia en la base de datos
                var result = await repository.ExistsAsync(seatId);

                // ASSERT
                // Se verifica que sea verdadero, lo que indica que el asiento existe en la base de datos
                Assert.True(result);
            }

            /// <summary>
            /// Verifica que el método ExistsAsync de la clase SeatRepository devuelva falso cuando se busca un asiento que no existe en la base de datos.
            /// </summary>
            [Fact]
            public async void ExistsAsync_Returns_False_When_NotExistSeat()
            {
                // ARRANGE
                // Se crea un contexto de prueba utilizando el factory de contexto
                var context = TestContextFactory.CreateContext();

                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo y se configura para devolver el contexto de prueba
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Se crea una instancia de SeatRepository con el mock de IUnitOfWork
                var repository = new SeatRepository(mockUnitOfWork.Object);

                // ACT
                // Se llama al método ExistsAsync con un ID de asiento que no existe en la base de datos para verificar su existencia
                var result = await repository.ExistsAsync(Guid.NewGuid());

                // ASSERT
                // Se verifica que sea falso, lo que indica que el asiento no existe en la base de datos
                Assert.False(result);
            }
        }

        public class GetAvailables
        {
            /// <summary>
            /// Verifica que el método GetAvailablesAsync de la clase SeatRepository devuelva todos los asientos disponibles en la base de datos.
            /// </summary>
            [Fact]
            public async void GetAvailablesAsync_Returns_Seats_When_AvailabilityExists()
            {
                // ARRANGE
                // Se crean varios asientos, algunos bloqueados y otros disponibles
                var seats = new List<Seat>
                {
                    new Seat { Id = Guid.NewGuid(), Name = "Q-1", IsBlocked = false, Description = "This seat is located near the window." },
                    new Seat { Id = Guid.NewGuid(), Name = "M-2", IsBlocked = true, Description = "This seat is reserved for VIP guests." },
                    new Seat { Id = Guid.NewGuid(), Name = "M-1", IsBlocked = false, Description = "This seat is in the back row." },
                    new Seat { Id = Guid.NewGuid(), Name = "M-3", IsBlocked = false, Description = "This seat has extra legroom." }
                };
                // Se cuenta la cantidad de asientos disponibles
                var avaliablesCount = seats.Where(seat => !seat.IsBlocked).Count();

                // Se crea un contexto de prueba utilizando el factory de contexto
                using var context = TestContextFactory.CreateContext();
                // Se agregan los asientos al contexto y se guardan los cambios
                await context.Seats.AddRangeAsync(seats);
                await context.SaveChangesAsync();

                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Se crea una instancia de SeatRepository con el mock de IUnitOfWork
                var repository = new SeatRepository(mockUnitOfWork.Object);

                // ACT
                // Se llama al método GetAvailablesAsync para obtener todos los asientos disponibles
                var result = await repository.GetAvailablesAsync();

                // ASSERT
                // Se verifica que el resultado no sea nulo, no esté vacío, tenga la cantidad correcta de asientos disponibles y que todos los asientos no estén bloqueados
                Assert.NotNull(result);
                Assert.NotEmpty(result);
                Assert.Equal(avaliablesCount, result.Count());
                Assert.True(result.All(seat => !seat.IsBlocked));
            }

            /// <summary>
            /// Verifica que el método GetAvailablesAsync de la clase SeatRepository devuelva una lista vacía cuando no hay asientos disponibles en la base de datos.
            /// </summary>
            [Fact]
            public async void GetAvailablesAsync_Returns_EmptyCollection_When_NoAvailability()
            {
                // ARRANGE
                // Se crean varios asientos, todos bloqueados
                var seats = new List<Seat>
                {
                    new Seat { Id = Guid.NewGuid(), Name = "Q-1", IsBlocked = true, Description = "This seat is located near the window." },
                    new Seat { Id = Guid.NewGuid(), Name = "M-2", IsBlocked = true, Description = "This seat is reserved for VIP guests." },
                    new Seat { Id = Guid.NewGuid(), Name = "M-1", IsBlocked = true, Description = "This seat is in the back row." },
                    new Seat { Id = Guid.NewGuid(), Name = "M-3", IsBlocked = true, Description = "This seat has extra legroom." }
                };

                // Se crea un contexto de prueba utilizando el factory de contexto
                using var context = TestContextFactory.CreateContext();
                // Se agregan los asientos al contexto y se guardan los cambios
                await context.Seats.AddRangeAsync(seats);
                await context.SaveChangesAsync();

                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Se crea una instancia de SeatRepository con el mock de IUnitOfWork
                var repository = new SeatRepository(mockUnitOfWork.Object);

                // ACT
                // Se llama al método GetAvailablesAsync para obtener todos los asientos disponibles
                var result = await repository.GetAvailablesAsync();

                // ASSERT
                // Se verifica que el resultado no sea nulo y esté vacío
                Assert.NotNull(result);
                Assert.Empty(result);
            }
        }

        public class GeatByName
        {
            /// <summary>
            /// Verifica que el método GetByNameAsync de la clase SeatRepository devuelva un asiento existente cuando se proporciona un nombre de asiento existente.
            /// </summary>
            [Theory]
            [InlineData("Q-1", true, "This seat is located near the window.")]
            [InlineData("M-2", false, "This seat is reserved for VIP guests.")]
            public async void GetByNameAsync_Returns_ExistingSeat_When_ProvidedExistingName(string name, bool isBlocked, string description)
            {
                // ARRANGE
                // Se crea un nuevo asiento con los datos proporcionados
                var seat = new Seat
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    IsBlocked = isBlocked,
                    Description = description
                };

                // Se crea un contexto de prueba utilizando el factory de contexto
                using var context = TestContextFactory.CreateContext();
                // Se agrega el asiento al contexto y se guardan los cambios
                await context.Seats.AddRangeAsync(seat);
                await context.SaveChangesAsync();

                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Se crea una instancia de SeatRepository con el mock de IUnitOfWork
                var repository = new SeatRepository(mockUnitOfWork.Object);

                // ACT
                // Se llama al método GetByNameAsync para obtener el asiento por su nombre
                var result = await repository.GetByNameAsync(seat.Name);

                // ASSERT
                // Se verifica que el resultado no sea nulo y que coincida con el asiento creado
                Assert.NotNull(result);
                Assert.Equal(seat.Id, result.Id);
            }

            /// <summary>
            /// Verifica que el método GetByNameAsync de la clase SeatRepository devuelva nulo cuando no se encuentra ningún asiento con el nombre proporcionado.
            /// </summary>
            [Fact]
            public async void GetByNameAsync_Returns_Null_When_NotExistsSeat()
            {
                // ARRANGE
                // Se crea un contexto de prueba utilizando el factory de contexto
                using var context = TestContextFactory.CreateContext();

                // Se establece un mock de IUnitOfWork para simular el trabajo de unidad de trabajo
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(u => u.Context).Returns(context);

                // Se crea una instancia de SeatRepository con el mock de IUnitOfWork
                var repository = new SeatRepository(mockUnitOfWork.Object);

                // ACT
                // Se llama al método GetByNameAsync con un nombre de asiento que no existe en la base de datos
                var result = await repository.GetByNameAsync(It.IsAny<string>());

                // ASSERT
                // Se verifica que el resultado sea nulo, lo que indica que no se encontró ningún asiento con el nombre proporcionado
                Assert.Null(result);
            }
        }
    }
}

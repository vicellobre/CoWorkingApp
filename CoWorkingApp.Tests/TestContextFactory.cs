using CoWorkingApp.Core.Entities;
using CoWorkingApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CoWorkingApp.Tests
{
    /// <summary>
    /// Clase para la creación de instancias de contexto de base de datos utilizadas en las pruebas.
    /// </summary>
    public class TestContextFactory
    {
        /// <summary>
        /// Crea y devuelve una instancia de CoWorkingContext configurada para usar una base de datos en memoria.
        /// </summary>
        /// <returns>Instancia de CoWorkingContext configurada para pruebas.</returns>
        public static CoWorkingContext CreateContext()
        {
            // Configura las opciones del contexto de base de datos en memoria
            var options = new DbContextOptionsBuilder<CoWorkingContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Usar un nombre de base de datos único cada vez
                .Options;

            // Crea una instancia del contexto de base de datos
            var dbContext = new CoWorkingContext(options);

            // Asegura que la base de datos en memoria esté eliminada antes de crearla nuevamente
            dbContext.Database.EnsureDeleted();

            // Crea la base de datos en memoria
            dbContext.Database.EnsureCreated();

            // Devuelve el contexto de base de datos
            return dbContext;
        }

        /// <summary>
        /// Inicializa datos simulados en el contexto de base de datos para reservas.
        /// </summary>
        /// <returns>Instancia del contexto de base de datos con datos simulados de reservas.</returns>
        public static async Task<CoWorkingContext> InitializeDataRerservationsAsync()
        {
            // Crear una lista de usuarios simulados
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Name = "John", LastName = "Doe", Email = "john@example.com", Password = "123" },
                new User { Id = Guid.NewGuid(), Name = "Jane", LastName = "Smith", Email = "jane@example.com", Password = "456" }
            };

            // Crear una lista de asientos simulados
            var seats = new List<Seat>
            {
                new Seat { Id = Guid.NewGuid(), Name = "Q-1", IsBlocked = false, Description = "This seat is located near the window." },
                new Seat { Id = Guid.NewGuid(), Name = "M-2", IsBlocked = true, Description = "This seat is reserved for VIP guests." }
            };

            // Crear una lista de reservas simuladas
            var reservations = new List<Reservation>
            {
                new Reservation { Id = Guid.NewGuid(), Date = DateTime.Now, UserId = users[0].Id, SeatId = seats[0].Id },
                new Reservation { Id = Guid.NewGuid(), Date = DateTime.Now, UserId = users[1].Id, SeatId = seats[1].Id },
            };

            // Crear el contexto de base de datos en memoria
            var context = CreateContext();

            // Agregar las reservas simuladas al contexto
            await context.Reservations.AddRangeAsync(reservations);

            // Agregar los usuarios simulados al contexto
            await context.Users.AddRangeAsync(users);

            // Agregar los asientos simulados al contexto
            await context.Seats.AddRangeAsync(seats);

            // Guardar los cambios en el contexto
            await context.SaveChangesAsync();

            // Devolver el contexto configurado con datos simulados
            return context;
        }
    }
}

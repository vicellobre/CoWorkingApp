using CoWorkingApp.API.Infrastructure.Context;
using CoWorkingApp.API.Infrastructure.Persistence.Repositories;
using CoWorkingApp.API.Infrastructure.UnitOfWorks;
using CoWorkingApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CoWorkingApp.Tests.Infrastructure.UnitOfWorks
{
    /// <summary>
    /// Clase de pruebas para el controlador UnitOfWork.
    /// </summary>
    public class UnitOfWorkTest
    {
        /// <summary>
        /// Verifica que el constructor de UnitOfWork cree una instancia válida cuando el contexto no es nulo.
        /// </summary>
        [Fact]
        public void UnitOfWorkConstructor_Returns_ValidInstance_When_ContextIsNotNull()
        {
            // ARRANGE
            var context = new Mock<CoWorkingContext>(new DbContextOptions<CoWorkingContext>());

            // ACT
            var result = new UnitOfWork(context.Object);

            // ASSERT
            Assert.NotNull(result);
        }

        /// <summary>
        /// Verifica que el constructor de UnitOfWork lance una excepción cuando el contexto es nulo.
        /// </summary>
        [Fact]
        public void UnitOfWorkConstructor_Throws_ArgumentNullException_When_ContextIsNull()
        {
            // ARRANGE
            CoWorkingContext? context = null;

            // ACT
            var result = () => new UnitOfWork(context);

            // ASSERT
            Assert.Throws<ArgumentNullException>(result);
        }

        /// <summary>
        /// Verifica que el método CommitAsync de UnitOfWork llame a SaveChangesAsync exactamente una vez en el contexto del contexto de base de datos.
        /// </summary>
        [Fact]
        public async Task CommitAsync_SavesChangesOnce_When_Called()
        {
            // ARRANGE
            var dbContextMock = new Mock<CoWorkingContext>(new DbContextOptions<CoWorkingContext>());
            var unitOfWork = new UnitOfWork(dbContextMock.Object);

            // ACT
            await unitOfWork.CommitAsync();

            // ASSERT
            dbContextMock.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        /// <summary>
        /// Verifica que el método Dispose de UnitOfWork desecha el contexto de base de datos.
        /// </summary>
        [Fact]
        public void Dispose_DisposesDatabaseContext_When_Called()
        {
            // ARRANGE
            var dbContextMock = new Mock<CoWorkingContext>(new DbContextOptions<CoWorkingContext>());
            var unitOfWork = new UnitOfWork(dbContextMock.Object);

            // ACT
            unitOfWork.Dispose();

            // ASSERT
            dbContextMock.Verify(db => db.Dispose(), Times.Once);
        }

        /// <summary>
        /// Verifica que el método Dispose maneja excepciones lanzadas al desechar el contexto.
        /// </summary>
        [Fact]
        public void Dispose_CatchesException_When_ContextDisposeThrowsException()
        {
            // ARRANGE
            var dbContextMock = new Mock<CoWorkingContext>(new DbContextOptions<CoWorkingContext>());
            dbContextMock.Setup(db => db.Dispose()).Throws(new Exception());

            var unitOfWork = new UnitOfWork(dbContextMock.Object);

            var originalConsoleOut = Console.Out; // Guarda la salida de consola original para restaurarla más adelante
            using (var consoleOutput = new StringWriter())
            {
                Console.SetOut(consoleOutput); // Redirige la salida de consola

                // ACT
                unitOfWork.Dispose();

                // ASSERT
                var output = consoleOutput.ToString();
                Assert.Contains("Error disposing context", output);
            }

            Console.SetOut(originalConsoleOut); // Restaura la salida de consola original
        }
    }
}

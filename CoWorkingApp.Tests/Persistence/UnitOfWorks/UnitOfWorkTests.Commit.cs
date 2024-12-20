//using CoWorkingApp.Persistence.Context;
//using CoWorkingApp.Persistence.UnitOfWorks;
//using Microsoft.EntityFrameworkCore;
//using Moq;

//namespace CoWorkingApp.Tests.Persistence.UnitOfWorks;

//public class UnitOfWorkTests_Commit
//{
//    /// <summary>
//    /// Verifica que el método CommitAsync de UnitOfWork llame a SaveChangesAsync exactamente una vez en el contexto del contexto de base de datos.
//    /// </summary>
//    [Fact]
//    public async Task CommitAsync_SavesChangesOnce_When_Called()
//    {
//        // ARRANGE
//        var dbContextMock = new Mock<CoWorkingContext>(new DbContextOptions<CoWorkingContext>());
//        var unitOfWork = new UnitOfWork(dbContextMock.Object);

//        // ACT
//        await unitOfWork.CommitAsync();

//        // ASSERT
//        dbContextMock.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
//    }
//}

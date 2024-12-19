using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Tests.Core.Shared.Results;

public partial class ResultTests
{
    public class FailureExceptionTests
    {
        [Fact]
        public void Failure_ShouldCreateFailureResultWithException()
        {
            // Arrange
            var exception = new InvalidOperationException("An error occurred");

            // Act
            var result = Result.Failure(exception);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Single(result.Errors);
            Assert.Equal(exception.GetType().Name, result.FirstError.Code);
            Assert.Equal(exception.Message, result.FirstError.Message);
        }
    }
}

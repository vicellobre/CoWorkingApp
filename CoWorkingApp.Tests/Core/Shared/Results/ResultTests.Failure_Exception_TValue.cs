using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Tests.Core.Shared.Results;

public partial class ResultTests
{
    public class FailureExceptionTValueTests
    {
        [Fact]
        public void Failure_ShouldReturnFailureResult_WithSpecifiedException()
        {
            // Arrange
            var exception = new InvalidOperationException("An error occurred");

            // Act
            var result = Result.Failure<string>(exception);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Single(result.Errors);
            Assert.Equal(exception.GetType().Name, result.FirstError.Code);
            Assert.Equal(exception.Message, result.FirstError.Message);
        }
    }
}

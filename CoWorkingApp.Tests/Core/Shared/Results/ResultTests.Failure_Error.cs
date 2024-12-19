using CoWorkingApp.Core.Shared;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Shared.Results;

public partial class ResultTests
{
    public class FailureErrorTests
    {
        [Fact]
        public void Failure_ShouldCreateFailureResultWithError()
        {
            // Arrange
            var error = ERRORS.NullValue;

            // Act
            var result = Result.Failure(error);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Single(result.Errors);
            Assert.Equal(error, result.FirstError);
        }
    }
}

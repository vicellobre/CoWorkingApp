using CoWorkingApp.Core.Shared;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Shared.Results;

public partial class ResultTests
{
    public class FailureErrorsCollectionTests
    {
        [Fact]
        public void Failure_ShouldCreateFailureResultWithErrorsCollection()
        {
            // Arrange
            var errors = new List<Error> { ERRORS.NullValue, ERRORS.Unknown };

            // Act
            var result = Result.Failure(errors);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(errors, result.Errors);
        }
    }
}

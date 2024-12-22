using CoWorkingApp.Core.Shared;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Shared.ResultsT;

public partial class ResultTests
{
    public class FailureErrorsCollectionTValueTests
    {
        [Fact]
        public void Failure_ShouldReturnFailureResult_WithSpecifiedErrorsCollection()
        {
            // Arrange
            var errors = new List<Error> { ERRORS.NullValue, ERRORS.Unknown };

            // Act
            var result = Result.Failure<string>(errors);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(errors, result.Errors);
        }
    }
}

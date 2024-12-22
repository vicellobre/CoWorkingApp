using CoWorkingApp.Core.Shared;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Shared.ResultsT;

public partial class ResultTests
{
    public class FailureErrorTValueTests
    {
        [Fact]
        public void Failure_ShouldReturnFailureResult_WithSpecifiedError()
        {
            // Arrange
            var error = ERRORS.NullValue;

            // Act
            var result = Result<string>.Failure(error);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Single(result.Errors);
            Assert.Equal(error, result.FirstError);
        }
    }
}

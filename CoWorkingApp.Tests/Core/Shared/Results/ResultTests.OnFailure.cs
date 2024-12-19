using CoWorkingApp.Core.Shared;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Shared.Results;

public partial class ResultTests
{
    public class OnFailureTests
    {
        [Fact]
        public void OnFailure_ShouldExecuteAction_WhenResultIsFailure()
        {
            // Arrange
            var result = Result.Failure(ERRORS.NullValue);
            var actionExecuted = false;

            // Act
            result.OnFailure(() => actionExecuted = true);

            // Assert
            Assert.True(actionExecuted);
        }

        [Fact]
        public void OnFailure_ShouldNotExecuteAction_WhenResultIsSuccess()
        {
            // Arrange
            var result = Result.Success();
            var actionExecuted = false;

            // Act
            result.OnFailure(() => actionExecuted = true);

            // Assert
            Assert.False(actionExecuted);
        }
    }
}

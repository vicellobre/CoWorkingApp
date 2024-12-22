using CoWorkingApp.Core.Shared;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Shared.ResultsT;

public partial class ResultTests
{
    public class OnFailureTValueTests
    {
        [Fact]
        public void OnFailure_ShouldExecuteAction_WhenResultIsFailure()
        {
            // Arrange
            var result = Result<string>.Failure(ERRORS.NullValue);
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
            var result = Result<string>.Success("Test Value");
            var actionExecuted = false;

            // Act
            result.OnFailure(() => actionExecuted = true);

            // Assert
            Assert.False(actionExecuted);
        }
    }
}

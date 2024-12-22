using CoWorkingApp.Core.Shared;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Shared.ResultsT;

public partial class ResultTests
{
    public class OnSuccessTValueTests
    {
        [Fact]
        public void OnSuccess_ShouldExecuteAction_WhenResultIsSuccess()
        {
            // Arrange
            var result = Result<string>.Success("Test Value");
            var actionExecuted = false;

            // Act
            result.OnSuccess(() => actionExecuted = true);

            // Assert
            Assert.True(actionExecuted);
        }

        [Fact]
        public void OnSuccess_ShouldNotExecuteAction_WhenResultIsFailure()
        {
            // Arrange
            var result = Result<string>.Failure(ERRORS.NullValue);
            var actionExecuted = false;

            // Act
            result.OnSuccess(() => actionExecuted = true);

            // Assert
            Assert.False(actionExecuted);
        }
    }
}

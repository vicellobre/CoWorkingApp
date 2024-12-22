using CoWorkingApp.Core.Shared;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Shared.Results;

public partial class ResultTests
{
    public class OnSuccessTests
    {
        [Fact]
        public void OnSuccess_ShouldExecuteAction_WhenResultIsSuccess()
        {
            // Arrange
            var result = Result.Success();
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
            var result = Result.Failure(ERRORS.NullValue);
            var actionExecuted = false;

            // Act
            result.OnSuccess(() => actionExecuted = true);

            // Assert
            Assert.False(actionExecuted);
        }
    }
}

using CoWorkingApp.Core.Enumerations;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Tests.Core.Shared.Errors;

public partial class ErrorTests
{
    public class FailureTests
    {
        [Fact]
        public void Failure_ShouldCreateError_WhenParametersAreValid()
        {
            // Arrange
            var code = "ERROR_CODE";
            var message = "Some error message";

            // Act
            var error = Error.Failure(code, message);

            // Assert
            Assert.Equal(code, error.Code);
            Assert.Equal(message, error.Message);
            Assert.Equal(ErrorType.Failure, error.Type);
        }
    }
}

using CoWorkingApp.Core.Enumerations;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Tests.Core.Shared.Errors;

public partial class ErrorTests
{
    public class ValidationTests
    {
        [Fact]
        public void Validation_ShouldCreateError_WhenParametersAreValid()
        {
            // Arrange
            var code = "VALIDATION_ERROR";
            var message = "Validation error occurred";

            // Act
            var error = Error.Validation(code, message);

            // Assert
            Assert.Equal(code, error.Code);
            Assert.Equal(message, error.Message);
            Assert.Equal(ErrorType.Validation, error.Type);
        }
    }
}

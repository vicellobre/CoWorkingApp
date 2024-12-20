using CoWorkingApp.Core.Enumerations;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Tests.Core.Shared.Errors;

public partial class ErrorTests
{
    public class UnexpectedTests
    {
        [Fact]
        public void Unexpected_ShouldCreateError_WhenParametersAreValid()
        {
            // Arrange
            var code = "UNEXPECTED_ERROR";
            var message = "An unexpected error occurred";

            // Act
            var error = Error.Unexpected(code, message);

            // Assert
            Assert.Equal(code, error.Code);
            Assert.Equal(message, error.Message);
            Assert.Equal(ErrorType.Unexpected, error.Type);
        }
    }
}

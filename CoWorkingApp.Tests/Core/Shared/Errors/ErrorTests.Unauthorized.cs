using CoWorkingApp.Core.Enumerations;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Tests.Core.Shared.Errors;

public partial class ErrorTests
{
    public class UnauthorizedTests
    {
        [Fact]
        public void Unauthorized_ShouldCreateError_WhenParametersAreValid()
        {
            // Arrange
            var code = "UNAUTHORIZED_ERROR";
            var message = "Unauthorized error occurred";

            // Act
            var error = Error.Unauthorized(code, message);

            // Assert
            Assert.Equal(code, error.Code);
            Assert.Equal(message, error.Message);
            Assert.Equal(ErrorType.Unauthorized, error.Type);
        }
    }
}

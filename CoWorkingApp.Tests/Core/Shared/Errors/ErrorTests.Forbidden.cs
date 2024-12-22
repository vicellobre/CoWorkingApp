using CoWorkingApp.Core.Enumerations;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Tests.Core.Shared.Errors;

public partial class ErrorTests
{
    public class ForbiddenTests
    {
        [Fact]
        public void Forbidden_ShouldCreateError_WhenParametersAreValid()
        {
            // Arrange
            var code = "FORBIDDEN_ERROR";
            var message = "Forbidden error occurred";

            // Act
            var error = Error.Forbidden(code, message);

            // Assert
            Assert.Equal(code, error.Code);
            Assert.Equal(message, error.Message);
            Assert.Equal(ErrorType.Forbidden, error.Type);
        }
    }
}

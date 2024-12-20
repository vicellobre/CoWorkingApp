using CoWorkingApp.Core.Enumerations;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Tests.Core.Shared.Errors;

public partial class ErrorTests
{
    public class NotFoundTests
    {
        [Fact]
        public void NotFound_ShouldCreateError_WhenParametersAreValid()
        {
            // Arrange
            var code = "NOT_FOUND_ERROR";
            var message = "Not found error occurred";

            // Act
            var error = Error.NotFound(code, message);

            // Assert
            Assert.Equal(code, error.Code);
            Assert.Equal(message, error.Message);
            Assert.Equal(ErrorType.NotFound, error.Type);
        }
    }
}

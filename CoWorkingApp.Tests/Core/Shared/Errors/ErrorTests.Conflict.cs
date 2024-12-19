using CoWorkingApp.Core.Enumerations;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Tests.Core.Shared.Errors;

public partial class ErrorTests
{
    public class ConflictTests
    {
        [Fact]
        public void Conflict_ShouldCreateError_WhenParametersAreValid()
        {
            // Arrange
            var code = "CONFLICT_ERROR";
            var message = "Conflict error occurred";

            // Act
            var error = Error.Conflict(code, message);

            // Assert
            Assert.Equal(code, error.Code);
            Assert.Equal(message, error.Message);
            Assert.Equal(ErrorType.Conflict, error.Type);
        }
    }
}

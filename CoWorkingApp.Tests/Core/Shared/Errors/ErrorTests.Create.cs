using CoWorkingApp.Core.Enumerations;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Tests.Core.Shared.Errors;

public partial class ErrorTests
{
    public class CreateTests
    {
        [Fact]
        public void Create_ShouldThrowArgumentNullException_WhenCodeIsNull()
        {
            // Arrange
            string? code = null;
            var message = "Some error message";
            var type = ErrorType.Failure;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => Error.Create(code, message, type));
            Assert.Equal("Error code cannot be null. (Parameter 'code')", exception.Message);
        }

        [Fact]
        public void Create_ShouldThrowArgumentNullException_WhenMessageIsNull()
        {
            // Arrange
            var code = "ERROR_CODE";
            string? message = null;
            var type = ErrorType.Failure;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => Error.Create(code, message, type));
            Assert.Equal("Error message cannot be null. (Parameter 'message')", exception.Message);
        }

        [Fact]
        public void Create_ShouldReturnError_WhenAllParametersAreValid()
        {
            // Arrange
            var code = "ERROR_CODE";
            var message = "Some error message";
            var type = ErrorType.Failure;

            // Act
            var error = Error.Create(code, message, type);

            // Assert
            Assert.Equal(code, error.Code);
            Assert.Equal(message, error.Message);
            Assert.Equal(type, error.Type);
        }
    }
}

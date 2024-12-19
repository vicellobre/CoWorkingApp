using CoWorkingApp.Core.Enumerations;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Tests.Core.Shared.Errors;

public partial class ErrorTests
{
    public class ExceptionTests
    {
        [Fact]
        public void Exception_ShouldCreateError_WhenParametersAreValid()
        {
            // Arrange
            var code = "EXCEPTION_ERROR";
            var message = "An exception occurred";

            // Act
            var error = Error.Exception(code, message);

            // Assert
            Assert.Equal(code, error.Code);
            Assert.Equal(message, error.Message);
            Assert.Equal(ErrorType.Exception, error.Type);
        }
    }
}

using CoWorkingApp.Core.Enumerations;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Tests.Core.Shared.Errors;

public partial class ErrorTests
{
    public class WithStackTests
    {
        [Fact]
        public void WithStack_ShouldReturnErrorWithStackTrace()
        {
            // Arrange
            var code = "ERROR_CODE";
            var message = "Some error message";
            var type = ErrorType.Failure;
            var originalError = Error.Create(code, message, type);
            var stack = new List<Error>
            {
                Error.Create("STACK_ERROR", "Stack error message", ErrorType.Failure)
            };

            // Act
            var errorWithStack = Error.WithStack(originalError, stack);

            // Assert
            Assert.Equal(code, errorWithStack.Code);
            Assert.Equal(message, errorWithStack.Message);
            Assert.Equal(type, errorWithStack.Type);
            Assert.Equal(stack, errorWithStack.StackTrace);
        }
    }
}

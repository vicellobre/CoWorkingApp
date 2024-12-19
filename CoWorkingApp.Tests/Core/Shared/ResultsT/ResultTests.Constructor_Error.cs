using System.Reflection;
using CoWorkingApp.Core.Shared;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Shared.ResultsT;

public partial class ResultTests
{
    public class Constructor_ErrorTests
    {
        [Fact]
        public void ConstructorWithError_ShouldCreateFailureResult_WithSpecifiedError()
        {
            // Arrange
            var error = ERRORS.NullValue;
            var constructor = typeof(Result<string>).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                [typeof(Error)],
                null);

            // Act
            var result = (Result<string>)constructor?.Invoke([error])!;

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(error, result.FirstError);
        }

        [Fact]
        public void ConstructorWithError_ShouldCreateFailureResult_WithNoneError()
        {
            // Arrange
            var error = ERRORS.None;
            var constructor = typeof(Result<string>).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                [typeof(Error)],
                null);

            // Act
            var result = (Result<string>)constructor?.Invoke([error])!;

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(ERRORS.NullValue, result.FirstError);
        }
    }
}

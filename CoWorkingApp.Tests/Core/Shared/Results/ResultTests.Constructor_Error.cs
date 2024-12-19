using CoWorkingApp.Core.Shared;
using System.Reflection;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Shared.Results;

public partial class ResultTests
{
    public class ConstructorWithErrorTests
    {
        [Fact]
        public void Constructor_ShouldThrowInvalidOperationException_WhenSuccessWithError()
        {
            // Arrange
            var error = ERRORS.NullValue;
            var constructor = typeof(Result).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                [typeof(bool), typeof(Error)],
                null);

            // Act & Assert
            var exception = Assert.Throws<TargetInvocationException>(() =>
                constructor?.Invoke([true, error]));

            // Verify that the InnerException is of type InvalidOperationException
            Assert.IsType<InvalidOperationException>(exception.InnerException);
        }

        [Fact]
        public void Constructor_ShouldThrowInvalidOperationException_WhenFailureWithNoError()
        {
            // Arrange
            var error = ERRORS.None;
            var constructor = typeof(Result).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                [typeof(bool), typeof(Error)],
                null);

            // Act & Assert
            var exception = Assert.Throws<TargetInvocationException>(() =>
                constructor?.Invoke([false, error]));

            // Verify that the InnerException is of type InvalidOperationException
            Assert.IsType<InvalidOperationException>(exception.InnerException);
        }

        [Fact]
        public void Constructor_ShouldNotThrowException_WhenSuccessWithNoError()
        {
            // Arrange
            var error = ERRORS.None;
            var constructor = typeof(Result).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                [typeof(bool), typeof(Error)],
                null);

            // Act
            var result = constructor?.Invoke([true, error]);

            // Assert
            Assert.NotNull(result);
            var typedResult = (Result)result;
            Assert.True(typedResult.IsSuccess);
            Assert.False(typedResult.IsFailure);
        }

        [Fact]
        public void Constructor_ShouldNotThrowException_WhenFailureWithError()
        {
            // Arrange
            var error = ERRORS.NullValue;
            var constructor = typeof(Result).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                [typeof(bool), typeof(Error)],
                null);

            // Act
            var result = constructor?.Invoke([false, error]);

            // Assert
            Assert.NotNull(result);
            var typedResult = (Result)result;
            Assert.False(typedResult.IsSuccess);
            Assert.True(typedResult.IsFailure);
        }
    }
}

using CoWorkingApp.Core.Shared;
using System.Reflection;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Shared.ResultsT;

public partial class ResultTests
{
    public class Constructor_ValueTests
    {
        [Fact]
        public void ConstructorWithValue_ShouldCreateSuccessResult_WhenValueIsNotNull()
        {
            // Arrange
            var value = "Test Value";
            var constructor = typeof(Result<string>).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                [typeof(string)],
                null);

            // Act
            var result = (Result<string>)constructor?.Invoke([value])!;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void ConstructorWithValue_ShouldCreateFailureResult_WhenValueIsNull()
        {
            // Arrange
            string? value = null;
            var constructor = typeof(Result<string>).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                [typeof(string)],
                null);

            // Act
            var result = (Result<string>)constructor?.Invoke([value])!;

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(ERRORS.NullValue, result.FirstError);
        }
    }
}

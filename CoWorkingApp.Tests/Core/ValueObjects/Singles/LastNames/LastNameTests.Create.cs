using CoWorkingApp.Core.Errors;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.LastNames;

public partial class LastNameTests
{
    public class CreateTests
    {
        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsNull()
        {
            // Arrange
            string? value = null;
            var expectedError = ERRORS.LastName.IsNullOrEmpty;

            // Act
            var result = LastName.Create(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedError, result.FirstError);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsEmpty()
        {
            // Arrange
            var value = string.Empty;
            var expectedError = ERRORS.LastName.IsNullOrEmpty;

            // Act
            var result = LastName.Create(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedError, result.FirstError);
            Assert.Contains(expectedError, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsTooShort()
        {
            // Arrange
            var value = "A";
            var expectedError = ERRORS.LastName.TooShort(LastName.MinLength);

            // Act
            var result = LastName.Create(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(expectedError, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsTooLong()
        {
            // Arrange
            var longName = new string('a', 51);
            var expectedError = ERRORS.LastName.TooLong(LastName.MaxLength);

            // Act
            var result = LastName.Create(longName);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(expectedError, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsInvalidFormat()
        {
            // Arrange
            var value = "Inv@lidName";
            var expectedError = ERRORS.LastName.InvalidFormat;

            // Act
            var result = LastName.Create(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(expectedError, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenValueIsValid()
        {
            // Arrange
            var value = "ValidName";

            // Act
            var result = LastName.Create(value);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(value, result.Value.Value);
        }
    }
}
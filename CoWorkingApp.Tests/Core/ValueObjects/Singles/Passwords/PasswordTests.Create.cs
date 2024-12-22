using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.Passwords;

public class PasswordTests
{
    public class CreateTests
    {
        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsNull()
        {
            // Arrange
            string? value = null;
            var expectedError = Errors.Password.IsNullOrEmpty;

            // Act
            var result = Password.Create(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedError, result.FirstError);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsEmpty()
        {
            // Arrange
            var value = string.Empty;
            var expectedError = Errors.Password.IsNullOrEmpty;

            // Act
            var result = Password.Create(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedError, result.FirstError);
            Assert.Contains(expectedError, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsTooShort()
        {
            // Arrange
            var value = "aA1!";
            var expectedError = Errors.Password.TooShort(Password.MinLength);

            // Act
            var result = Password.Create(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(expectedError, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsTooLong()
        {
            // Arrange
            var longPassword = new string('a', 101) + "A1!";
            var expectedError = Errors.Password.TooLong(Password.MaxLength);

            // Act
            var result = Password.Create(longPassword);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(expectedError, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsInvalidFormat()
        {
            // Arrange
            var value = "Password123";
            var expectedError = Errors.Password.InvalidFormat;

            // Act
            var result = Password.Create(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(expectedError, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenValueIsValid()
        {
            // Arrange
            var value = "Valid1@Password";

            // Act
            var result = Password.Create(value);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(value, result.Value.Value);
        }
    }
}

using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.Emails;

public partial class EmailTest
{
    public class CreateTests
    {
        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsNull()
        {
            // Arrange
            string? value = null;
            var expectedError = Errors.Email.IsNullOrEmpty;

            // Act
            var result = Email.Create(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedError, result.FirstError);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsEmpty()
        {
            // Arrange
            var value = string.Empty;
            var expectedError = Errors.Email.IsNullOrEmpty;

            // Act
            var result = Email.Create(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedError, result.FirstError);
            Assert.Contains(expectedError, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsTooShort()
        {
            // Arrange
            var value = "a@b.c";
            var expectedError = Errors.Email.TooShort(Email.MinLength);

            // Act
            var result = Email.Create(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(expectedError, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsTooLong()
        {
            // Arrange
            var longEmail = new string('a', 51) + "@example.com";
            var expectedError = Errors.Email.TooLong(Email.MaxLength);

            // Act
            var result = Email.Create(longEmail);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedError, result.FirstError);
            Assert.Contains(expectedError, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsInvalidFormat()
        {
            // Arrange
            var value = "invalid-email";
            var expectedError = Errors.Email.InvalidFormat;

            // Act
            var result = Email.Create(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(expectedError, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenValueIsValid()
        {
            // Arrange
            var email = "valid.email@example.com";

            // Act
            var result = Email.Create(email);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(email, result.Value.Value);
        }
    }
}

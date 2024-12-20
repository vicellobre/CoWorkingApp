using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.FirstNames;

public partial class FirstNameTest
{
    public class CreateTests
    {
        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsNull()
        {
            // Arrange
            string? value = null;
            var expectedError = Errors.FirstName.IsNullOrEmpty;

            // Act
            var result = FirstName.Create(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedError, result.FirstError);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsEmpty()
        {
            // Arrange
            var value = string.Empty;
            var expectedError = Errors.FirstName.IsNullOrEmpty;

            // Act
            var result = FirstName.Create(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedError, result.FirstError);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsTooShort()
        {
            // Arrange
            var value = "a";
            var expectedError = Errors.FirstName.TooShort(FirstName.MinLength);

            // Act
            var result = FirstName.Create(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(expectedError, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsTooLong()
        {
            // Arrange
            var longFirstName = new string('a', 51);
            var expectedError = Errors.FirstName.TooLong(FirstName.MaxLength);

            // Act
            var result = FirstName.Create(longFirstName);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(expectedError, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsInvalidFormat()
        {
            // Arrange
            var value = "InvalidName123!";
            var expectedError = Errors.FirstName.InvalidFormat;

            // Act
            var result = FirstName.Create(value);

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
            var result = FirstName.Create(value);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(value, result.Value.Value);
        }
    }
}

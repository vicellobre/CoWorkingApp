using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.ValueObjects.Composite;

namespace CoWorkingApp.Tests.Core.ValueObjects.Composite.Credentials;

public partial class CredentialsWithEmailAndPasswordTests
{
    public class CreateTests
    {
        [Fact]
        public void Create_ShouldReturnFailure_WhenEmailIsInvalid()
        {
            // Arrange
            var email = "invalid-email";
            var password = "Valid1@Password";

            // Act
            var result = CredentialsWithEmailAndPassword.Create(email, password);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(Errors.Email.InvalidFormat, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenPasswordIsInvalid()
        {
            // Arrange
            var email = "valid.email@example.com";
            var password = "password";

            // Act
            var result = CredentialsWithEmailAndPassword.Create(email, password);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(Errors.Password.InvalidFormat, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenEmailAndPasswordAreInvalid()
        {
            // Arrange
            var email = "invalid-email";
            var password = "password";

            // Act
            var result = CredentialsWithEmailAndPassword.Create(email, password);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(Errors.Email.InvalidFormat, result.Errors);
            Assert.Contains(Errors.Password.InvalidFormat, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenEmailAndPasswordAreValid()
        {
            // Arrange
            var email = "valid.email@example.com";
            var password = "Valid1@Password";

            // Act
            var result = CredentialsWithEmailAndPassword.Create(email, password);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(email, result.Value.Email.Value);
            Assert.Equal(password, result.Value.Password.Value);
        }
    }
}

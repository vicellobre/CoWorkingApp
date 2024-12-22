using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.Entities.Users;

public partial class UserTests
{
    public class CreateTests
    {
        [Fact]
        public void Create_ShouldReturnFailure_WhenFirstNameIsInvalid()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string firstName = "";
            string lastName = "Doe";
            string email = "valid@example.com";
            string password = "Valid1!";

            // Act
            var result = User.Create(userId, firstName, lastName, email, password);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Contains(Errors.FirstName.IsNullOrEmpty, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenLastNameIsInvalid()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string firstName = "John";
            string lastName = "";
            string email = "valid@example.com";
            string password = "Valid1!";

            // Act
            var result = User.Create(userId, firstName, lastName, email, password);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Contains(Errors.LastName.IsNullOrEmpty, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenEmailIsInvalid()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string firstName = "John";
            string lastName = "Doe";
            string email = "invalid-email";
            string password = "Valid1!";

            // Act
            var result = User.Create(userId, firstName, lastName, email, password);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Contains(Errors.Email.InvalidFormat, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenPasswordIsInvalid()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string firstName = "John";
            string lastName = "Doe";
            string email = "valid@example.com";
            string password = "shor";

            // Act
            var result = User.Create(userId, firstName, lastName, email, password);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Contains(Errors.Password.TooShort(Password.MinLength), result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenAllValuesAreValid()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string firstName = "John";
            string lastName = "Doe";
            string email = "valid@example.com";
            string password = "Valid1!";

            // Act
            var result = User.Create(userId, firstName, lastName, email, password);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(userId, result.Value.Id);
            Assert.Equal("John Doe", result.Value.Name.ToString());
            Assert.Equal(email, result.Value.Credentials.Email.Value);
            Assert.Equal(password, result.Value.Credentials.Password.Value);
        }
    }
}

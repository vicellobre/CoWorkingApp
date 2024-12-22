using CoWorkingApp.Core.DomainErrors;

namespace CoWorkingApp.Tests.Core.Entities.Users;

public partial class UserTests
{
    public class ChangeEmailTests
    {
        [Fact]
        public void ChangeEmail_ShouldReturnFailure_WhenEmailIsInvalid()
        {
            // Arrange
            var user = GetCreateValidUser();

            // Act
            var result = user.ChangeEmail("invalid-email");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(result.Errors, e => e == Errors.Email.InvalidFormat);
        }

        [Fact]
        public void ChangeEmail_ShouldReturnSuccess_WhenEmailIsValid()
        {
            // Arrange
            var user = GetCreateValidUser();

            // Act
            var result = user.ChangeEmail("new@example.com");

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("new@example.com", user.Credentials.Email.Value);
        }

        
    }
}

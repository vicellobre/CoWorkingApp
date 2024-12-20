using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.Entities.Users;

public partial class UserTests
{
    public class ChangePasswordTests
    {
        [Fact]
        public void ChangePassword_ShouldReturnFailure_WhenPasswordIsInvalid()
        {
            // Arrange
            var user = GetCreateValidUser();

            // Act
            var result = user.ChangePassword("shor");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(Errors.Password.TooShort(Password.MinLength), result.Errors);
        }

        [Fact]
        public void ChangePassword_ShouldReturnSuccess_WhenPasswordIsValid()
        {
            // Arrange
            var user = GetCreateValidUser();

            // Act
            var result = user.ChangePassword("Valid2!");

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Valid2!", user.Credentials.Password.Value);
        }
    }
}

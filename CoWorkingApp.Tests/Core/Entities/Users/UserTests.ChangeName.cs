using CoWorkingApp.Core.Errors;

namespace CoWorkingApp.Tests.Core.Entities.Users;

public partial class UserTests
{
    public class ChangeNameTests
    {
        [Fact]
        public void ChangeName_ShouldReturnFailure_WhenFirstNameIsInvalid()
        {
            // Arrange
            var user = GetCreateValidUser();

            // Act
            var result = user.ChangeName("", "Doe");

            // Assert
            Assert.True(result.IsFailure);
            Assert.Contains(result.Errors, e => e == ERRORS.FirstName.IsNullOrEmpty);
        }

        [Fact]
        public void ChangeName_ShouldReturnFailure_WhenLastNameIsInvalid()
        {
            // Arrange
            var user = GetCreateValidUser();

            // Act
            var result = user.ChangeName("John", "");

            // Assert
            Assert.True(result.IsFailure);
            Assert.Contains(result.Errors, e => e == ERRORS.LastName.IsNullOrEmpty);
        }

        [Fact]
        public void ChangeName_ShouldReturnSuccess_WhenFullNameIsValid()
        {
            // Arrange
            var user = GetCreateValidUser();

            // Act
            var result = user.ChangeName("Jane", "Doe");

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Jane Doe", user.Name.ToString());
        }
    }
}

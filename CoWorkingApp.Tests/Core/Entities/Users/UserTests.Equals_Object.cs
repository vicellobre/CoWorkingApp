using CoWorkingApp.Core.Entities;

namespace CoWorkingApp.Tests.Core.Entities.Users;

public partial class UserTests
{
    public class EqualsObjectTests
    {
        [Fact]
        public void Equals_ShouldReturnFalse_WhenObjectIsNull()
        {
            // Arrange
            var user = GetCreateValidUser();

            // Act
            var result = user.Equals((object?)null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenSameReference()
        {
            // Arrange
            var user = GetCreateValidUser();

            // Act
            var result = user.Equals((object)user);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenDifferentType()
        {
            // Arrange
            var user = GetCreateValidUser();
            var other = new AnotherEntityType { Id = user.Id };

            // Act
            var result = user.Equals((object)other);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenSameId()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user1 = User.Create(userId, "John", "Doe", "john.doe@example.com", "Valid1!").Value;
            var user2 = User.Create(userId, "Jane", "Smith", "jane.smith@example.com", "Valid2!").Value;

            // Act
            var result = user1.Equals((object)user2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenDifferentId()
        {
            // Arrange
            var user1 = GetCreateValidUser();
            var user2 = GetCreateValidUser();

            // Act
            var result = user1.Equals((object)user2);

            // Assert
            Assert.False(result);
        }
    }
}
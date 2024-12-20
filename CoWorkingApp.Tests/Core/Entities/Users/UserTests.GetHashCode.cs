using CoWorkingApp.Core.Entities;

namespace CoWorkingApp.Tests.Core.Entities.Users;

public partial class UserTests
{
    public class GetHashCodeTests
    {
        [Fact]
        public void GetHashCode_ShouldReturnSameHashCode_ForSameId()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user1 = User.Create(userId, "John", "Doe", "john.doe@example.com", "Valid1!").Value;
            var user2 = User.Create(userId, "Jane", "Smith", "jane.smith@example.com", "Valid2!").Value;

            // Act
            var hash1 = user1.GetHashCode();
            var hash2 = user2.GetHashCode();

            // Assert
            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void GetHashCode_ShouldReturnDifferentHashCodes_ForDifferentIds()
        {
            // Arrange
            var user1 = GetCreateValidUser();
            var user2 = GetCreateValidUser();

            // Act
            var hash1 = user1.GetHashCode();
            var hash2 = user2.GetHashCode();

            // Assert
            Assert.NotEqual(hash1, hash2);
        }
    }
}
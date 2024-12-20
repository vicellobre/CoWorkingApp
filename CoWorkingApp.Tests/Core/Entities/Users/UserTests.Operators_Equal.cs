using CoWorkingApp.Core.Entities;

namespace CoWorkingApp.Tests.Core.Entities.Users;

public partial class UserTests
{
    public class EqualityOperatorsTests
    {
        [Fact]
        public void EqualityOperator_ShouldReturnTrue_WhenBothAreNull()
        {
            // Arrange
            User? user1 = null;
            User? user2 = null;

            // Act
            var result = user1 == user2;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EqualityOperator_ShouldReturnFalse_WhenFirstIsNull()
        {
            // Arrange
            var user1 = GetCreateValidUser();
            User? user2 = null;

            // Act
            var result = user1 == user2;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualityOperator_ShouldReturnFalse_WhenSecondIsNull()
        {
            // Arrange
            User? user1 = null;
            User? user2 = GetCreateValidUser();

            // Act
            var result = user1 == user2;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualityOperator_ShouldReturnTrue_WhenBothHaveSameId()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user1 = User.Create(userId, "John", "Doe", "john.doe@example.com", "Valid1!").Value;
            var user2 = User.Create(userId, "Jane", "Smith", "jane.smith@example.com", "Valid2!").Value;

            // Act
            var result = user1 == user2;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void InequalityOperator_ShouldReturnFalse_WhenBothAreNull()
        {
            // Arrange
            User? user1 = null;
            User? user2 = null;

            // Act
            var result = user1 != user2;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void InequalityOperator_ShouldReturnTrue_WhenOneIsNull()
        {
            // Arrange
            var user1 = GetCreateValidUser();
            User? user2 = null;

            // Act
            var result = user1 != user2;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void InequalityOperator_ShouldReturnTrue_WhenDifferentIds()
        {
            // Arrange
            var user1 = GetCreateValidUser();
            var user2 = GetCreateValidUser();

            // Act
            var result = user1 != user2;

            // Assert
            Assert.True(result);
        }
    }
}

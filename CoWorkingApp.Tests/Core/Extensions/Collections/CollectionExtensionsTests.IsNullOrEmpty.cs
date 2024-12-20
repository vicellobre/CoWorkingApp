using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Tests.Core.Extensions.Collections;

public partial class CollectionExtensionsTests
{
    public class IsNullOrEmptyTests
    {
        [Fact]
        public void IsNullOrEmpty_ShouldReturnTrue_WhenCollectionIsNull()
        {
            // Arrange
            List<int>? collection = null;

            // Act
            var result = collection!.IsNullOrEmpty();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrEmpty_ShouldReturnTrue_WhenCollectionIsEmpty()
        {
            // Arrange
            var collection = new List<int>();

            // Act
            var result = collection.IsNullOrEmpty();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrEmpty_ShouldReturnFalse_WhenCollectionIsNotEmpty()
        {
            // Arrange
            var collection = new List<int> { 1, 2, 3 };

            // Act
            var result = collection.IsNullOrEmpty();

            // Assert
            Assert.False(result);
        }
    }
}

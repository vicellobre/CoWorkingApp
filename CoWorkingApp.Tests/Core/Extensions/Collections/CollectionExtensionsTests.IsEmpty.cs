using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Tests.Core.Extensions.Collections;

public partial class CollectionExtensionsTests
{
    public class IsEmptyTests
    {
        [Fact]
        public void IsEmpty_ShouldReturnTrue_WhenCollectionIsEmpty()
        {
            // Arrange
            var collection = new List<int>();

            // Act
            var result = collection.IsEmpty();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsEmpty_ShouldReturnFalse_WhenCollectionIsNotEmpty()
        {
            // Arrange
            var collection = new List<int> { 1, 2, 3 };

            // Act
            var result = collection.IsEmpty();

            // Assert
            Assert.False(result);
        }
    }
}

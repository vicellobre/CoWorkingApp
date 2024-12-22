using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Tests.Core.Extensions.Collections;

public partial class CollectionExtensionsTests
{
    public class IsNullTests
    {
        [Fact]
        public void IsNull_ShouldReturnTrue_WhenCollectionIsNull()
        {
            // Arrange
            List<int>? collection = null;

            // Act
            var result = collection!.IsNull();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNull_ShouldReturnFalse_WhenCollectionIsNotNull()
        {
            // Arrange
            var collection = new List<int>();

            // Act
            var result = collection.IsNull();

            // Assert
            Assert.False(result);
        }
    }
}

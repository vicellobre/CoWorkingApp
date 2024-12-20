using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Tests.Core.Extensions.Strings;

public partial class StringExtensionsTests
{
    public class SplitByWhitespaceTests
    {
        [Fact]
        public void SplitByWhitespace_ShouldReturnEmptyArray_WhenInputIsEmpty()
        {
            // Arrange
            string input = "";

            // Act
            var result = input.SplitByWhitespace();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void SplitByWhitespace_ShouldSplitStringByWhitespace()
        {
            // Arrange
            string input = "Hello    World\tHow\nAre You";

            // Act
            var result = input.SplitByWhitespace();

            // Assert
            Assert.Equal(new[] { "Hello", "World", "How", "Are", "You" }, result);
        }
    }
}

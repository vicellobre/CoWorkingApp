using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Tests.Core.Extensions.Strings;

public partial class StringExtensionsTests
{
    public class ReplaceWhitespaceTests
    {
        [Fact]
        public void ReplaceWhitespace_ShouldReturnSameString_WhenInputIsEmpty()
        {
            // Arrange
            string input = "";
            string replacement = "-";

            // Act
            var result = input.ReplaceWhitespace(replacement);

            // Assert
            Assert.Equal(input, result);
        }

        [Fact]
        public void ReplaceWhitespace_ShouldReplaceWhitespaceWithSpecifiedValue()
        {
            // Arrange
            string input = "Hello    World\tHow\nAre You";
            string replacement = "-";

            // Act
            var result = input.ReplaceWhitespace(replacement);

            // Assert
            Assert.Equal("Hello-World-How-Are-You", result);
        }
    }
}

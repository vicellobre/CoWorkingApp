using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Tests.Core.Extensions.Strings;

public partial class StringExtensionsTests
{
    public class CapitalizeTests
    {
        [Fact]
        public void Capitalize_ShouldReturnSameString_WhenInputIsEmpty()
        {
            // Arrange
            string input = "";

            // Act
            var result = input.Capitalize();

            // Assert
            Assert.Equal(input, result);
        }

        [Fact]
        public void Capitalize_ShouldCapitalizeFirstLetter()
        {
            // Arrange
            string input = "hello";

            // Act
            var result = input.Capitalize();

            // Assert
            Assert.Equal("Hello", result);
        }

        [Fact]
        public void Capitalize_ShouldHandleSingleCharacter()
        {
            // Arrange
            string input = "a";

            // Act
            var result = input.Capitalize();

            // Assert
            Assert.Equal("A", result);
        }
    }
}

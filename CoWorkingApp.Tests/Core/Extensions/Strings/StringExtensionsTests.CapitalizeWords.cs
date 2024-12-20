using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Tests.Core.Extensions.Strings;

public partial class StringExtensionsTests
{
    public class CapitalizeWordsTests
    {
        [Fact]
        public void CapitalizeWords_ShouldReturnSameString_WhenInputIsEmpty()
        {
            // Arrange
            string input = "";

            // Act
            var result = input.CapitalizeWords();

            // Assert
            Assert.Equal(input, result);
        }

        [Fact]
        public void CapitalizeWords_ShouldCapitalizeEachWord()
        {
            // Arrange
            string input = "hello world how are you";

            // Act
            var result = input.CapitalizeWords();

            // Assert
            Assert.Equal("Hello World How Are You", result);
        }
    }
}

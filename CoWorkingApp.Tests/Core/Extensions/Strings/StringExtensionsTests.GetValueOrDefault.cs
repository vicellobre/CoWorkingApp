using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Tests.Core.Extensions.Strings;

public partial class StringExtensionsTests
{
    public class GetValueOrDefaultTests
    {
        [Fact]
        public void GetValueOrDefault_ShouldReturnDefaultValue_WhenInputIsEmpty()
        {
            // Arrange
            string input = "";
            string defaultValue = "Default";

            // Act
            var result = input.GetValueOrDefault(defaultValue);

            // Assert
            Assert.Equal(defaultValue, result);
        }

        [Fact]
        public void GetValueOrDefault_ShouldReturnInputValue_WhenInputIsNotEmpty()
        {
            // Arrange
            string input = "Hello";
            string defaultValue = "Default";

            // Act
            var result = input.GetValueOrDefault(defaultValue);

            // Assert
            Assert.Equal(input, result);
        }
    }
}

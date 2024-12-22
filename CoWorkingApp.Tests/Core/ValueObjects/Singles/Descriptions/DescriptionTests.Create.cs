using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.Descriptions;

public partial class DescriptionTests
{
    public class CreateTests
    {
        [Fact]
        public void Create_ShouldReturnSuccess_WhenValueIsNull()
        {
            // Arrange
            string? value = null;

            // Act
            var result = Description.Create(value);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(string.Empty, result.Value.Value);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenValueIsEmpty()
        {
            // Arrange
            var value = string.Empty;

            // Act
            var result = Description.Create(value);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(value, result.Value.Value);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenValueIsValid()
        {
            // Arrange
            var value = "Valid description";

            // Act
            var result = Description.Create(value);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(value, result.Value.Value);
        }
    }
}

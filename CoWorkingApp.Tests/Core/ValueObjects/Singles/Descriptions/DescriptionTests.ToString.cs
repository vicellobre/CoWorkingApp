using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.Descriptions;

public partial class DescriptionTests
{
    public class ToStringTests
    {
        [Fact]
        public void ToString_ShouldReturnDescriptionValue()
        {
            // Arrange
            var value = "Valid description";
            var description = Description.Create(value).Value;

            // Act
            var result = description.ToString();

            // Assert
            Assert.Equal(value, result);
        }

        [Fact]
        public void ToString_ShouldReturnEmptyString_WhenDescriptionIsEmpty()
        {
            // Arrange
            var description = Description.Create(string.Empty).Value;

            // Act
            var result = description.ToString();

            // Assert
            Assert.Equal(string.Empty, result);
        }
    }
}

using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.Descriptions;

public partial class DescriptionTests
{
    public class ImplicitConversionTests
    {
        [Fact]
        public void ImplicitConversion_ShouldReturnDescriptionValue()
        {
            // Arrange
            var value = "Valid description";
            var description = Description.Create(value).Value;

            // Act
            string result = description;

            // Assert
            Assert.Equal(value, result);
        }

        [Fact]
        public void ImplicitConversion_ShouldReturnEmptyString_WhenDescriptionIsEmpty()
        {
            // Arrange
            var description = Description.Create(string.Empty).Value;

            // Act
            string result = description;

            // Assert
            Assert.Equal(string.Empty, result);
        }
    }
}

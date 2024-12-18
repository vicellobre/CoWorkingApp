using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.Dates;

public partial class DateTests
{
    public class ToStringTests
    {
        [Fact]
        public void ToString_ShouldReturnDateValueAsString()
        {
            // Arrange
            var value = new DateTime(2024, 12, 18);
            var date = Date.Create(value).Value;

            // Act
            var result = date.ToString();

            // Assert
            Assert.Equal(value.ToString(), result);
        }
    }
}

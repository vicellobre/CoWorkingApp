using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.Dates;

public partial class DateTests
{
    public class ImplicitConversionTests
    {
        [Fact]
        public void ImplicitConversion_ShouldReturnDateTimeValue()
        {
            // Arrange
            var value = new DateTime(2024, 12, 18);
            var date = Date.Create(value).Value;

            // Act
            DateTime result = date;

            // Assert
            Assert.Equal(value, result);
        }
    }
}

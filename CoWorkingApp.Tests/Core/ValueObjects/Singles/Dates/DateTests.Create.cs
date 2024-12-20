using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.Dates;

public partial class DateTests
{
    public class CreateTests
    {
        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsDefault()
        {
            // Arrange
            DateTime value = default;
            var expectedError = Errors.Date.Invalid;

            // Act
            var result = Date.Create(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedError, result.FirstError);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenValueIsValid()
        {
            // Arrange
            var value = new DateTime(2024, 12, 18);

            // Act
            var result = Date.Create(value);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(value, result.Value.Value);
        }
    }
}

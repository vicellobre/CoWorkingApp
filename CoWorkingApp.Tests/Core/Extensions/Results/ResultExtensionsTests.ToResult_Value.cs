using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Tests.Core.Extensions.Results;

public partial class ResultExtensionsTests
{
    public class ToResultValueTests
    {
        [Fact]
        public void ToResult_ShouldCreateSuccessResult_FromValue()
        {
            // Arrange
            var value = "Test Value";

            // Act
            var result = value.ToResult();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(value, result.Value);
        }
    }
}

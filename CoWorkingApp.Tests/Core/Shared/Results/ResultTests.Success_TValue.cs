using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Tests.Core.Shared.Results;

public partial class ResultTests
{
    public class SuccessTValueTests
    {
        [Fact]
        public void Success_ShouldReturnSuccessResult_WithSpecifiedValue()
        {
            // Arrange
            var value = "Test Value";

            // Act
            var result = Result.Success(value);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(value, result.Value);
        }
    }
}

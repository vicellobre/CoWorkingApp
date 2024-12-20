using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Tests.Core.Shared.ResultsT;

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
            var result = Result<string>.Success(value);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(value, result.Value);
        }
    }
}

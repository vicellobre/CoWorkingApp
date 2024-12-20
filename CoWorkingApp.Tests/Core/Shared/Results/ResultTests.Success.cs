using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Tests.Core.Shared.Results;

public partial class ResultTests
{
    public class SuccessTests
    {
        [Fact]
        public void Success_ShouldCreateSuccessfulResult()
        {
            // Act
            var result = Result.Success();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Empty(result.Errors);
        }
    }
}

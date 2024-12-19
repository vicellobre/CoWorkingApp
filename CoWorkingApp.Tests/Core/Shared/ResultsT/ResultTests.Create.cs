using CoWorkingApp.Core.Shared;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Shared.ResultsT;

public partial class ResultTests
{
    public class CreateTValueTests
    {
        [Fact]
        public void Create_ShouldReturnSuccessResult_WhenValueIsNotNull()
        {
            // Arrange
            var value = "Test Value";

            // Act
            var result = Result<string>.Create(value);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void Create_ShouldReturnFailureResult_WhenValueIsNull()
        {
            // Arrange
            string? value = null;

            // Act
            var result = Result<string>.Create(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(ERRORS.NullValue, result.FirstError);
        }
    }
}

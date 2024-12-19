using CoWorkingApp.Core.Shared;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Shared.ResultsT;

public partial class ResultTests
{
    public class GetValueOrDefaultTValueTests
    {
        [Fact]
        public void GetValueOrDefault_ShouldReturnValue_WhenResultIsSuccess()
        {
            // Arrange
            var value = "Test Value";
            var result = Result<string>.Success(value);

            // Act
            var defaultValue = "Default Value";
            var returnValue = result.GetValueOrDefault(defaultValue);

            // Assert
            Assert.Equal(value, returnValue);
        }

        [Fact]
        public void GetValueOrDefault_ShouldReturnDefaultValue_WhenResultIsFailure()
        {
            // Arrange
            var result = Result<string>.Failure(ERRORS.NullValue);

            // Act
            var defaultValue = "Default Value";
            var returnValue = result.GetValueOrDefault(defaultValue);

            // Assert
            Assert.Equal(defaultValue, returnValue);
        }
    }
}

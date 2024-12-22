using CoWorkingApp.Core.Shared;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Shared.ResultsT;

public partial class ResultTests
{
    public class FirstErrorTValueTests
    {
        [Fact]
        public void FirstError_ShouldReturnNone_WhenResultIsSuccess()
        {
            // Arrange
            var result = Result<string>.Success("Test Value");

            // Act
            var firstError = result.FirstError;

            // Assert
            Assert.Equal(ERRORS.None, firstError);
        }

        [Fact]
        public void FirstError_ShouldReturnFirstError_WhenResultIsFailure()
        {
            // Arrange
            var errors = new List<Error> { ERRORS.NullValue, ERRORS.Unknown };
            var result = Result<string>.Failure(errors);

            // Act
            var firstError = result.FirstError;

            // Assert
            Assert.Equal(errors.First(), firstError);
        }
    }
}

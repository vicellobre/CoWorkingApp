using CoWorkingApp.Core.Shared;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Shared.Results;

public partial class ResultTests
{
    public class FirstErrorTests
    {
        [Fact]
        public void FirstError_ShouldReturnNone_WhenErrorsCollectionIsEmpty()
        {
            // Arrange
            var result = Result.Success();

            // Act
            var firstError = result.FirstError;

            // Assert
            Assert.Equal(ERRORS.None, firstError);
        }

        [Fact]
        public void FirstError_ShouldReturnFirstError_WhenErrorsCollectionIsNotEmpty()
        {
            // Arrange
            var errors = new List<Error> { ERRORS.NullValue, ERRORS.Unknown };
            var result = Result.Failure(errors);

            // Act
            var firstError = result.FirstError;

            // Assert
            Assert.Equal(ERRORS.NullValue, firstError);
        }
    }
}

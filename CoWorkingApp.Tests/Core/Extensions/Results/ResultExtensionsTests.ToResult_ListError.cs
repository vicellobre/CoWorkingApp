using CoWorkingApp.Core.Extensions;
using CoWorkingApp.Core.Shared;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Extensions.Results;

public partial class ResultExtensionsTests
{
    public class ToResultListErrorTests
    {
        [Fact]
        public void ToResult_ShouldCreateFailureResult_FromListOfErrors()
        {
            // Arrange
            List<Error> errors = [ERRORS.NullValue, ERRORS.Unknown];

            // Act
            var result = errors.ToResult<string>();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(errors, result.Errors);
        }
    }
}

using CoWorkingApp.Core.Extensions;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Extensions.Results;

public partial class ResultExtensionsTests
{
    public class ToResultErrorTests
    {
        [Fact]
        public void ToResult_ShouldCreateFailureResult_FromError()
        {
            // Arrange
            var error = ERRORS.NullValue;

            // Act
            var result = error.ToResult<string>();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(error, result.FirstError);
        }
    }
}

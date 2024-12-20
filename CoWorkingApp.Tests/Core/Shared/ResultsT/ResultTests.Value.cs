using CoWorkingApp.Core.Shared;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Shared.ResultsT;

public partial class ResultTests
{
    public class ValueTValueTests
    {
        [Fact]
        public void Value_ShouldReturnSpecifiedValue_WhenResultIsSuccess()
        {
            // Arrange
            var value = "Test Value";
            var result = Result<string>.Success(value);

            // Act
            var returnValue = result.Value;

            // Assert
            Assert.Equal(value, returnValue);
        }

        [Fact]
        public void Value_ShouldThrowInvalidOperationException_WhenResultIsFailure()
        {
            // Arrange
            var result = Result<string>.Failure(ERRORS.NullValue);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => result.Value);
        }
    }
}

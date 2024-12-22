using CoWorkingApp.Core.Extensions;
using CoWorkingApp.Core.Shared;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;
using Xunit;

namespace CoWorkingApp.Tests.Core.Extensions.Results;

public partial class ResultExtensionsTests
{
    public class MatchResultTValueTResultTests
    {
        [Fact]
        public void Match_ShouldExecuteOnSuccess_WhenResultIsSuccess()
        {
            // Arrange
            var result = Result.Success("Test Value");
            static int onSuccess(string value) => value.Length;
            static int onFailure(Error error) => 0;

            // Act
            var matchResult = result.Match(onSuccess, onFailure);

            // Assert
            Assert.Equal(10, matchResult);
        }

        [Fact]
        public void Match_ShouldExecuteOnFailure_WhenResultIsFailure()
        {
            // Arrange
            var result = Result.Failure<string>(ERRORS.NullValue);
            static int onSuccess(string value) => value.Length;
            static int onFailure(Error error) => 0;

            // Act
            var matchResult = result.Match(onSuccess, onFailure);

            // Assert
            Assert.Equal(0, matchResult);
        }
    }
}

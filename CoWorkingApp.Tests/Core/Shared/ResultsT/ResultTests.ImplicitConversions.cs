using CoWorkingApp.Core.Shared;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Shared.ResultsT;

public partial class ResultTests
{
    public class ImplicitConversionsTValueTests
    {
        [Fact]
        public void ImplicitConversion_ShouldCreateSuccessResult_FromValue()
        {
            // Arrange
            var value = "Test Value";

            // Act
            Result<string> result = value;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void ImplicitConversion_ShouldCreateFailureResult_FromError()
        {
            // Arrange
            var error = ERRORS.NullValue;

            // Act
            Result<string> result = error;

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(error, result.FirstError);
        }

        [Fact]
        public void ImplicitConversion_ShouldCreateFailureResult_FromErrorsCollection()
        {
            // Arrange
            var errors = new List<Error> { ERRORS.NullValue, ERRORS.Unknown };

            // Act
            Result<string> result = errors;

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(errors, result.Errors);
        }

        [Fact]
        public void ImplicitConversion_ShouldCreateFailureResult_FromErrorArray()
        {
            // Arrange
            var errors = new Error[] { ERRORS.NullValue, ERRORS.Unknown };

            // Act
            Result<string> result = errors;

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(errors, result.Errors);
        }

        [Fact]
        public void ImplicitConversion_ShouldCreateFailureResult_FromHashSetOfErrors()
        {
            // Arrange
            var errors = new HashSet<Error> { ERRORS.NullValue, ERRORS.Unknown };

            // Act
            Result<string> result = errors;

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.True(errors.SetEquals(result.Errors));
        }

        [Fact]
        public void ImplicitConversion_ShouldCreateSuccessResult_FromResultTValueSuccess()
        {
            // Arrange
            var resultTValue = Result<string>.Success("Test Value");

            // Act
            Result result = resultTValue;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Empty(resultTValue.Errors);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void ImplicitConversion_ShouldCreateFailureResult_FromResultTValueFailure()
        {
            // Arrange
            var error = ERRORS.NullValue;
            var resultTValue = Result<string>.Failure(error);

            // Act
            Result result = resultTValue;

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Single(result.Errors);
            Assert.Equal(resultTValue.FirstError, result.FirstError);
        }
    }
}

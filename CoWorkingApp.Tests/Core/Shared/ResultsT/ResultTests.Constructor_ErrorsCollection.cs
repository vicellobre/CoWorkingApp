using System.Reflection;
using CoWorkingApp.Core.Shared;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Shared.ResultsT;

public partial class ResultTests
{
    public class ConstructorWithErrorsCollectionTValueTests
    {
        [Fact]
        public void ConstructorWithErrorsCollection_ShouldCreateFailureResult_WithSpecifiedErrors()
        {
            // Arrange
            var errors = new List<Error> { ERRORS.NullValue, ERRORS.Unknown };
            var constructor = typeof(Result<string>).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                [typeof(ICollection<Error>)],
                null);

            // Act
            var result = (Result<string>)constructor?.Invoke([errors])!;

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(errors, result.Errors);
        }

        [Fact]
        public void ConstructorWithErrorsCollection_ShouldCreateFailureResult_WithEmptyErrors()
        {
            // Arrange
            var errors = ERRORS.EmptyErrors;
            var constructor = typeof(Result<string>).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                [typeof(ICollection<Error>)],
                null);

            // Act
            var result = (Result<string>)constructor?.Invoke([errors])!;

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Equal(ERRORS.NullValue, result.FirstError);
        }
    }
}

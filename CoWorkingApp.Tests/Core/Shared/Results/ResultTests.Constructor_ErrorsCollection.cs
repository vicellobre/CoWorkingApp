using CoWorkingApp.Core.Shared;
using System.Reflection;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Tests.Core.Shared.Results;

public partial class ResultTests
{
    public class ConstructorWithErrorsCollectionTests
    {
        [Fact]
        public void Constructor_ShouldThrowInvalidOperationException_WhenSuccessWithErrors()
        {
            // Arrange
            var errors = new List<Error> { ERRORS.NullValue };
            var constructor = typeof(Result).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                [typeof(bool), typeof(ICollection<Error>)],
                null);

            // Act & Assert
            var exception = Assert.Throws<TargetInvocationException>(() =>
                constructor?.Invoke([true, errors]));

            Assert.IsType<InvalidOperationException>(exception.InnerException);
        }

        [Fact]
        public void Constructor_ShouldThrowInvalidOperationException_WhenFailureWithNoErrors()
        {
            // Arrange
            var errors = new List<Error>();
            var constructor = typeof(Result).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                [typeof(bool), typeof(ICollection<Error>)],
                null);

            // Act & Assert
            var exception = Assert.Throws<TargetInvocationException>(() =>
                constructor?.Invoke([false, errors]));

            Assert.IsType<InvalidOperationException>(exception.InnerException);
        }
    }
}

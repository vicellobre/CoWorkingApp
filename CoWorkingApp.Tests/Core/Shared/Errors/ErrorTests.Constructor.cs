using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Tests.Core.Shared.Errors;

public partial class ErrorTests
{
    public class ConstructorTests
    {
        [Fact]
        public void DefaultConstructor_ShouldThrowInvalidOperationException()
        {
            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => new Error());
            Assert.Equal("Use the static Create method to instantiate Error", exception.Message);
        }
    }
}

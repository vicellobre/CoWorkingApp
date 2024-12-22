using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Tests.Core.Shared.ResultsT;

public partial class ResultTests
{
    public class ConstructorDefaultTValueTests
    {
        [Fact]
        public void DefaultConstructor_ShouldThrowInvalidOperationException()
        {
            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => new Result<string>());
            Assert.Equal("Use the static methods Success, Failure or Create to instantiate Result.", exception.Message);
        }
    }
}

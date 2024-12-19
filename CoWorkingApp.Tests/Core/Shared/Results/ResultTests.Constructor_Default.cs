using CoWorkingApp.Core.Shared;


namespace CoWorkingApp.Tests.Core.Shared.Results;

public partial class ResultTests
{
    public class Constructor_DefaultTests
    {
        [Fact]
        public void DefaultConstructor_ShouldThrowInvalidOperationException()
        {
            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => new Result());
            Assert.Equal("Use the static methods Success or Failure to instantiate Result.", exception.Message);
        }
    }
}

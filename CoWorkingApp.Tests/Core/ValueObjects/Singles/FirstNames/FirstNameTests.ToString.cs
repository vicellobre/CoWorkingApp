using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.FirstNames;

public partial class FirstNameTest
{
    public class ToStringTests
    {
        [Fact]
        public void ToString_ShouldReturnFirstnameValue()
        {
            // Arrange
            var value = "example name";
            var firstName = FirstName.Create(value).Value;

            // Act
            var result = firstName.ToString();

            // Assert
            Assert.Equal(value, result);
        }

        [Fact]
        public void ToString_ShouldThrowInvalidOperationException_WhenFirstNameIsEmpty()
        {
            // Arrange
            var result = FirstName.Create(string.Empty);
            var expectedMessage = "The value of a failure result cannot be accessed.";

            // Act & Assert
            Assert.False(result.IsSuccess);
            var exception = Assert.Throws<InvalidOperationException>(() => result.Value.ToString()!);
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}

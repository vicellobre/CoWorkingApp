using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.LastNames;

public partial class LastNameTests
{
    public partial class LastNameTest
    {
        public class ToStringTests
        {
            [Fact]
            public void ToString_ShouldReturnLastNameValue()
            {
                // Arrange
                var value = "example name";
                var lastName = LastName.Create(value).Value;

                // Act
                var result = lastName.ToString();

                // Assert
                Assert.Equal(value, result);
            }

            [Fact]
            public void ToString_ShouldThrowInvalidOperationException_WhenLastNameIsEmpty()
            {
                // Arrange
                var result = LastName.Create(string.Empty);
                var expectedMessage = "The value of a failure result cannot be accessed.";

                // Act & Assert
                Assert.False(result.IsSuccess);
                var exception = Assert.Throws<InvalidOperationException>(() => result.Value.ToString());
                Assert.Equal(expectedMessage, exception.Message);
            }
        }
    }
}
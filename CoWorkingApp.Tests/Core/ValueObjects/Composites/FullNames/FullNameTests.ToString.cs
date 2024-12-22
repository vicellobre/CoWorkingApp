using CoWorkingApp.Core.ValueObjects.Composite;

namespace CoWorkingApp.Tests.Core.ValueObjects.Composites.FullNames;

public partial class FullNameTests
{
    public class ToStringTests
    {
        [Fact]
        public void ToString_ShouldReturnFullNameValue()
        {
            // Arrange
            var firstName = "ValidFirstName";
            var lastName = "ValidLastName";
            var fullName = FullName.Create(firstName, lastName).Value;

            // Act
            var result = fullName.ToString();

            // Assert
            var expectedValue = $"{firstName} {lastName}";
            Assert.Equal(expectedValue, result);
        }
    }
}

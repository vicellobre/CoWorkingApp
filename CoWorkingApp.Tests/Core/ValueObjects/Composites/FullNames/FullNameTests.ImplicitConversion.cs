using CoWorkingApp.Core.ValueObjects.Composite;

namespace CoWorkingApp.Tests.Core.ValueObjects.Composites.FullNames;

public partial class FullNameTests
{
    public class ImplicitConversionTests
    {
        [Fact]
        public void ImplicitConversion_ShouldReturnFullNameValue()
        {
            // Arrange
            var firstName = "ValidFirstName";
            var lastName = "ValidLastName";
            var fullName = FullName.Create(firstName, lastName).Value;

            // Act
            string result = fullName;

            // Assert
            var expectedValue = $"{firstName} {lastName}";
            Assert.Equal(expectedValue, result);
        }
    }
}

using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.ValueObjects.Composite;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Composites.FullNames;

public partial class FullNameTests
{
    public class CreateTests
    {
        [Fact]
        public void Create_ShouldReturnFailure_WhenFirstNameIsInvalid()
        {
            // Arrange
            var firstName = "A";
            var lastName = "ValidLastName";

            // Act
            var result = FullName.Create(firstName, lastName);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(Errors.FirstName.TooShort(FirstName.MinLength), result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenLastNameIsInvalid()
        {
            // Arrange
            var firstName = "ValidFirstName";
            var lastName = "";

            // Act
            var result = FullName.Create(firstName, lastName);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(Errors.LastName.IsNullOrEmpty, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenFirstNameAndLastNameAreInvalid()
        {
            // Arrange
            var firstName = "A";
            var lastName = "";

            // Act
            var result = FullName.Create(firstName, lastName);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(Errors.FirstName.TooShort(FirstName.MinLength), result.Errors);
            Assert.Contains(Errors.LastName.IsNullOrEmpty, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenFirstNameAndLastNameAreValid()
        {
            // Arrange
            var firstName = "ValidFirstName";
            var lastName = "ValidLastName";

            // Act
            var result = FullName.Create(firstName, lastName);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(firstName, result.Value.FirstName.Value);
            Assert.Equal(lastName, result.Value.LastName.Value);
        }
    }
}

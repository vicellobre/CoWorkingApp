using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Tests.Core.Extensions.Enums;

public class EnumExtensionsTests
{
    private enum TestEnum
    {
        ValueOne = 1,
        ValueTwo = 2,
        ValueThree = 3
    }

    public class ToIntTests
    {
        [Fact]
        public void ToInt_ShouldReturnIntegerValueOfEnum()
        {
            // Arrange
            TestEnum enumValue = TestEnum.ValueTwo;

            // Act
            int intValue = enumValue.ToInt();

            // Assert
            Assert.Equal(2, intValue);
        }

        [Fact]
        public void ToInt_ShouldReturnIntegerValueOfEnum_WhenValueIsThree()
        {
            // Arrange
            TestEnum enumValue = TestEnum.ValueThree;

            // Act
            int intValue = enumValue.ToInt();

            // Assert
            Assert.Equal(3, intValue);
        }
    }
}

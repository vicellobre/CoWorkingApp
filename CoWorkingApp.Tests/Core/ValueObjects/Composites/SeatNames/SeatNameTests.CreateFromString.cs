﻿using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.ValueObjects.Composite;

namespace CoWorkingApp.Tests.Core.ValueObjects.Composites.SeatNames;

public partial class SeatNameTests
{
    public class CreateFromStringTests
    {
        [Fact]
        public void CreateFromString_ShouldReturnFailure_WhenValueIsNullOrEmpty()
        {
            // Arrange
            string? value = null;

            // Act
            var result = SeatName.CreateFromString(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(Errors.SeatName.IsNullOrEmpty, result.FirstError);
        }

        [Fact]
        public void CreateFromString_ShouldReturnFailure_WhenValueIsInvalidFormat()
        {
            // Arrange
            var value = "InvalidFormat";

            // Act
            var result = SeatName.CreateFromString(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(Errors.SeatName.InvalidFormat, result.FirstError);
        }

        [Fact]
        public void CreateFromString_ShouldReturnFailure_WhenSeatRowIsInvalid()
        {
            // Arrange
            var value = "-A1";

            // Act
            var result = SeatName.CreateFromString(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(Errors.SeatName.InvalidFormat, result.Errors);
        }

        [Fact]
        public void CreateFromString_ShouldReturnFailure_WhenSeatNumberIsInvalid()
        {
            // Arrange
            var value = "1-";

            // Act
            var result = SeatName.CreateFromString(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(Errors.SeatName.InvalidFormat, result.Errors);
        }

        [Fact]
        public void CreateFromString_ShouldReturnSuccess_WhenValueIsValid()
        {
            // Arrange
            var value = "1-A1";

            // Act
            var result = SeatName.CreateFromString(value);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("1", result.Value.Row.Value);
            Assert.Equal("A1", result.Value.Number.Value);
        }
    }
}

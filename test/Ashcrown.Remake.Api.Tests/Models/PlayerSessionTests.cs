using Ashcrown.Remake.Api.Models;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Api.Tests.Models;

public class PlayerSessionTests
{
    [Fact]
    public void Secret_ShouldBeReturnedOnlyOnce()
    {
        // Arrange
        var session = new PlayerSession();

        // Act
        var firstSecret = session.Secret;
        var secondSecret = session.Secret;

        // Assert
        firstSecret.Should().NotBeEmpty();
        secondSecret.Should().BeEmpty();
    }

    [Fact]
    public void Secret_ShouldBeValid_WhenCorrectlyProvided()
    {
        // Arrange
        var session = new PlayerSession();
        var secret = session.Secret;

        // Act
        var isValid = session.ValidateSecret(secret);

        // Assert
        isValid.Should().BeTrue();
    }

    [Fact]
    public void Secret_ShouldBeInvalid_AfterItHasBeenReturned()
    {
        // Arrange
        var session = new PlayerSession();

        // Act
        var firstValidation = session.ValidateSecret(session.Secret);
        var secondValidation = session.ValidateSecret(session.Secret);

        // Assert
        firstValidation.Should().BeTrue();
        secondValidation.Should().BeFalse();
    }

    [Fact]
    public void Secret_ShouldBeInvalid_WhenIncorrectSecretProvided()
    {
        // Arrange
        var session = new PlayerSession();

        // Act
        var isValid = session.ValidateSecret("incorrect_secret");

        // Assert
        isValid.Should().BeFalse();
    }

    [Fact]
    public void ValidateSecret_ShouldBeCaseSensitive()
    {
        // Arrange
        var session = new PlayerSession();
        var secret = session.Secret.ToUpper();

        // Act
        var isValid = session.ValidateSecret(secret);

        // Assert
        isValid.Should().BeFalse();
    }
}
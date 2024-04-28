using Ashcrown.Remake.Api.Services;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Api.Tests.Services;

public class PlayerSessionServiceTests
{
    [Fact]
    public async Task CreateSession_ShouldAddNewSession_WhenPlayerNameIsNew()
    {
        // Arrange
        var service = new PlayerSessionService();
        
        // Act
        var session = await service.CreateSession("player1");
        
        // Assert
        session.Should().NotBeNull();
        session!.IconName.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task CreateSession_ShouldReturnExistingSession_WhenSessionAlreadyExists()
    {
        // Arrange
        var service = new PlayerSessionService();
        await service.CreateSession("player1");
        
        // Act
        var session = await service.CreateSession("player1");
    
        // Assert
        session.Should().NotBeNull();
    }

    [Fact]
    public async Task GetSession_ShouldRetrieveCorrectSession_WhenSessionExists()
    {
        // Arrange
        var service = new PlayerSessionService();
        await service.CreateSession("player1");
        
        // Act
        var session = await service.GetSession("player1");
        
        // Assert
        session.Should().NotBeNull();
    }

    [Fact]
    public async Task GetSession_ShouldReturnNull_WhenSessionDoesNotExist()
    {
        // Arrange
        var service = new PlayerSessionService();
        
        // Act
        var session = await service.GetSession("unknown_player");
        
        // Assert
        session.Should().BeNull();
    }

    [Fact]
    public async Task RemoveSession_ShouldRemoveSession_WhenCorrectSecretIsProvided()
    {
        // Arrange
        var service = new PlayerSessionService();
        await service.CreateSession("player1");
        var session = await service.GetSession("player1");
        var secret = session!.Secret;
        
        // Act
        var removed = await service.RemoveSession("player1", secret);
        
        //Assert
        removed.Should().BeTrue();
    }

    [Fact]
    public async Task RemoveSession_ShouldFail_WhenIncorrectSecretIsProvided()
    {
        // Arrange
        var service = new PlayerSessionService();
        await service.CreateSession("player1");
        
        // Act
        var act = async () => await service.RemoveSession("player1", "wrong_secret");

        // Assert;
        await act.Should().ThrowAsync<Exception>().WithMessage("Invalid secret provided!");
    }

    [Fact]
    public async Task UpdateSession_ShouldModifySession_WhenCorrectSecretIsProvided()
    {
        // Arrange
        var service = new PlayerSessionService();
        await service.CreateSession("player1");
        var session = await service.GetSession("player1");
        var secret = session!.Secret;
        
        // Act
        await service.UpdateSession("player1", secret, s => s.CrownName = "UpdatedCrownName");
        var updatedSession = await service.GetSession("player1");
        
        // Assert
        updatedSession!.CrownName.Should().Be("UpdatedCrownName");
    }

    [Fact]
    public async Task UpdateSession_ShouldThrowException_WhenIncorrectSecretIsProvided()
    {
        // Arrange
        var service = new PlayerSessionService();
        await service.CreateSession("player1");
        
        // Act
        var action = async () =>
            await service.UpdateSession("player1", "wrong_secret", s => s.CrownName = "UpdatedCrownName");
        
        // Assert
        await action.Should().ThrowAsync<Exception>().WithMessage("Invalid secret provided!");
    }

    [Fact]
    public async Task GetCurrentInUsePlayerNames_ShouldReturnAllPlayerNames()
    {
        // Arrange
        var service = new PlayerSessionService();
        await service.CreateSession("player1");
        await service.CreateSession("player2");
        
        // Act
        var playerNames = await service.GetCurrentInUsePlayerNames();
        
        // Assert
        playerNames.Should().Contain(new List<string> {"player1", "player2"});
        playerNames.Should().HaveCount(2);
    }

    [Fact]
    public async Task RemoveStaleSessions_ShouldRemoveStaleSessions()
    {
        // Arrange
        var service = new PlayerSessionService();
        await service.CreateSession("player1");
        
        // Act
        var removedCount = await service.RemoveStaleSessions(0); 
        
        // Assert
        removedCount.Should().Be(1);
        var remainingSessions = await service.GetCurrentInUsePlayerNames();
        remainingSessions.Should().BeEmpty();
    }
}
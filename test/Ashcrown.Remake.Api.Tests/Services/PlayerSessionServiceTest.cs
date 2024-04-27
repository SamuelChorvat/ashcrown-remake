using Ashcrown.Remake.Api.Services;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Api.Tests.Services;

public class PlayerSessionServiceTests
{
    [Fact]
    public async Task CreateSession_ShouldAddSession_WhenNameIsAvailable()
    {
        // Arrange
        var service = new PlayerSessionService();
        const string playerName = "NewPlayer";

        // Act
        var result = await service.CreateSession(playerName);

        // Assert
        result.Should().BeTrue();
        (await service.GetSession(playerName)).Should().NotBeNull();
    }

    [Fact]
    public async Task CreateSession_ShouldFail_WhenNameIsNotAvailable()
    {
        // Arrange
        var service = new PlayerSessionService();
        const string playerName = "ExistingPlayer";
        await service.CreateSession(playerName);

        // Act
        var result = await service.CreateSession(playerName);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task RemoveSession_ShouldSuccessfullyRemoveSession()
    {
        // Arrange
        var service = new PlayerSessionService();
        const string playerName = "ExistingPlayer";
        await service.CreateSession(playerName);

        // Act
        var removed = await service.RemoveSession(playerName);
        var session = await service.GetSession(playerName);

        // Assert
        removed.Should().BeTrue();
        session.Should().BeNull();
    }

    [Fact]
    public async Task UpdateSession_ShouldModifySession()
    {
        // Arrange
        var service = new PlayerSessionService();
        const string playerName = "ExistingPlayer";
        await service.CreateSession(playerName);
        var newTime = DateTime.UtcNow.AddHours(1);

        // Act
        await service.UpdateSession(playerName, session => session.LastRequestDateTime = newTime);

        // Assert
        var session = await service.GetSession(playerName);
        session.Should().NotBeNull();
        session?.LastRequestDateTime.Should().BeCloseTo(newTime, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public async Task RemoveStaleSessions_ShouldRemoveOnlySessionsOlderThanSpecifiedMinutes()
    {
        // Arrange
        var service = new PlayerSessionService();
        const string activePlayer = "ActivePlayer";
        const string stalePlayer = "StalePlayer";
        await service.CreateSession(activePlayer);
        await service.CreateSession(stalePlayer);

        // Adjust the LastRequestDateTime to simulate staleness
        service.UpdateSession(stalePlayer, session => session.LastRequestDateTime = DateTime.UtcNow.AddMinutes(-61)).Wait();

        // Act
        var sessionsRemoved = await service.RemoveStaleSessions(60);

        // Assert
        var sessions = await service.GetCurrentInUsePlayerNames();
        sessions.Should().Contain(activePlayer);
        sessions.Should().NotContain(stalePlayer);
        sessionsRemoved.Should().Be(1);
    }

    [Fact]
    public async Task GetCurrentInUsePlayerNames_ShouldReturnAllActivePlayerNames()
    {
        // Arrange
        var service = new PlayerSessionService();
        const string playerOne = "PlayerOne";
        const string playerTwo = "PlayerTwo";
        await service.CreateSession(playerOne);
        await service.CreateSession(playerTwo);

        // Act
        var players = await service.GetCurrentInUsePlayerNames();

        // Assert
        players.Should().Contain(new[] { playerOne, playerTwo });
    }
}
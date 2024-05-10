using Ashcrown.Remake.Api.Models;
using Ashcrown.Remake.Api.Models.Enums;
using Ashcrown.Remake.Api.Services;
using Ashcrown.Remake.Api.Services.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Ashcrown.Remake.Api.Tests.Services;

public class MatchmakerServiceTests
{
    private readonly MatchmakerService _matchmakerService;
    private readonly Mock<IPlayerSessionService> _mockPlayerSessionService = new();
    private readonly Mock<IBattleService> _mockBattleService = new();
    private readonly Mock<IDraftService> _mockDraftService = new();

    public MatchmakerServiceTests()
    {
        _mockBattleService.Setup(x => x.AddAcceptedMatch(It.IsAny<Guid>(), It.IsAny<FoundMatch>(), It.IsAny<DraftMatch?>())).Returns(true);
        _matchmakerService = new MatchmakerService(_mockPlayerSessionService.Object, _mockBattleService.Object, _mockDraftService.Object);
    }
    
    [Theory]
    [InlineData(FindMatchType.BlindPrivate, "")]
    [InlineData(FindMatchType.DraftPrivate, null)]
    public async Task AddToMatchmaking_ShouldReturnFalse_WhenPrivateMatchTypeWithoutOpponentName(FindMatchType matchType, string opponentName)
    {
        // Act
        var result = await _matchmakerService.AddToMatchmaking("player1", matchType, opponentName);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(FindMatchType.BlindPublic, null)]
    [InlineData(FindMatchType.DraftPublic, null)]
    [InlineData(FindMatchType.BlindPrivate, "opponent1")]
    [InlineData(FindMatchType.DraftPrivate, "opponent2")]
    public async Task AddToMatchmaking_ShouldReturnTrue_WhenValidInputsProvided(FindMatchType matchType, string opponentName)
    {
        // Act
        var result = await _matchmakerService.AddToMatchmaking("player1", matchType, opponentName);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task AddToMatchmaking_ShouldReplaceExistingEntry_WhenPlayerAlreadyInMatchmaking()
    {
        // Arrange
        await _matchmakerService.AddToMatchmaking("player1", FindMatchType.BlindPublic, null);

        // Act
        var result = await _matchmakerService.AddToMatchmaking("player1", FindMatchType.BlindPrivate, "opponent1");

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public async Task RemoveFromMatchMaking_ShouldReturnTrue_WhenPlayerExists()
    {
        // Arrange
        await _matchmakerService.AddToMatchmaking("player1", FindMatchType.BlindPublic, null);

        // Act
        var result = await _matchmakerService.RemoveFromMatchMaking("player1");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task RemoveFromMatchMaking_ShouldReturnFalse_WhenPlayerDoesNotExist()
    {
        // Act
        var result = await _matchmakerService.RemoveFromMatchMaking("player2");

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task TryToMatchPlayer_ShouldReturnExistingMatchIfFound()
    {
        // Arrange
        var foundMatch = new FoundMatch
        {
            FindMatchType = FindMatchType.BlindAi,
            PlayerNames = ["player1","player2"]
        };
        _matchmakerService.AddFoundMatch(foundMatch.MatchId, foundMatch); // Assuming we can add a mock setup here

        // Act
        var result = await _matchmakerService.TryToMatchPlayer("player1");

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task TryToMatchPlayer_ShouldThrowArgumentNullException_IfNoMatchSettingFound()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _matchmakerService.TryToMatchPlayer("player2"));
    }

    [Fact]
    public async Task TryToMatchPlayer_ShouldThrowArgumentNullException_IfNoSessionFound()
    {
        // Arrange
        await _matchmakerService.AddToMatchmaking("player3", FindMatchType.BlindAi, null);
        _mockPlayerSessionService.Setup(s => s.GetSession("player3")).Returns((PlayerSession)null!);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _matchmakerService.TryToMatchPlayer("player3"));
    }

    [Fact]
    public async Task TryToMatchPlayer_ShouldCreateAndReturnAIMatch()
    {
        // Arrange
        var playerSession = new PlayerSession { PlayerName = "player4" };
        await _matchmakerService.AddToMatchmaking("player4", FindMatchType.BlindAi, null);
        _mockPlayerSessionService.Setup(s => s.GetSession("player4")).Returns(playerSession);

        // Act
        var result = await _matchmakerService.TryToMatchPlayer("player4");

        // Assert
        result.Should().NotBeNull();
        result?.MatchType.Should().Be(FindMatchType.BlindAi);
    }
    
    [Fact]
    public async Task GetFoundMatchStatus_ShouldReturnConfirmed_WhenAcceptedByBattleService()
    {
        // Arrange
        var matchId = Guid.NewGuid();
        _mockBattleService.Setup(x => x.IsAcceptedMatch(matchId)).Returns(true);

        // Act
        var status = await _matchmakerService.GetFoundMatchStatus(matchId);

        // Assert
        status.Should().Be(FoundMatchStatus.Confirmed);
    }

    [Fact]
    public async Task GetFoundMatchStatus_ShouldReturnCancelled_WhenMatchNotFoundOrCancelled()
    {
        // Arrange
        var matchId = Guid.NewGuid();

        // Act
        var status = await _matchmakerService.GetFoundMatchStatus(matchId);

        // Assert
        status.Should().Be(FoundMatchStatus.Cancelled);
    }

    [Fact]
    public async Task GetFoundMatchStatus_ShouldReturnConfirmed_WhenBothPlayersAccepted()
    {
        // Arrange
        var matchId = Guid.NewGuid();
        _mockBattleService.Setup(x => x.AddAcceptedMatch(matchId, It.IsAny<FoundMatch>(), It.IsAny<DraftMatch?>())).Returns(true);
        var foundMatch = new FoundMatch
        {
            MatchFoundTime = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(1)),
            PlayerAccepted = [true, true],
            FindMatchType = FindMatchType.BlindAi
        };
        _matchmakerService.AddFoundMatch(matchId, foundMatch);

        // Act
        var status = await _matchmakerService.GetFoundMatchStatus(matchId);

        // Assert
        status.Should().Be(FoundMatchStatus.Confirmed);
    }

    [Fact]
    public async Task GetFoundMatchStatus_ShouldReturnCancelled_WhenMatchExpired()
    {
        // Arrange
        var matchId = Guid.NewGuid();
        var foundMatch = new FoundMatch
        {
            MatchFoundTime = DateTime.UtcNow.Subtract(TimeSpan.FromHours(1)),
            PlayerAccepted = [false, false],
            FindMatchType = FindMatchType.BlindAi
        };
        _matchmakerService.AddFoundMatch(matchId, foundMatch);

        // Act
        var status = await _matchmakerService.GetFoundMatchStatus(matchId);

        // Assert
        status.Should().Be(FoundMatchStatus.Cancelled);
    }

    [Fact]
    public async Task GetFoundMatchStatus_ShouldReturnPending_WhenNotAllConditionsMet()
    {
        // Arrange
        var matchId = Guid.NewGuid();
        var foundMatch = new FoundMatch
        {
            MatchId = matchId,
            MatchFoundTime = DateTime.UtcNow,
            PlayerAccepted = [true, false],
            FindMatchType = FindMatchType.BlindAi
        };
        _matchmakerService.AddFoundMatch(matchId, foundMatch);
        

        // Act
        var status = await _matchmakerService.GetFoundMatchStatus(matchId);

        // Assert
        status.Should().Be(FoundMatchStatus.Pending);
    }
    
    [Fact]
    public async Task RemoveStaleFindMatches_ShouldRemoveStaleEntries()
    {
        // Arrange
        await _matchmakerService.AddToMatchmaking("activePlayer", FindMatchType.BlindPublic, null);
        await _matchmakerService.AddToMatchmaking("stalePlayer1", FindMatchType.BlindPublic, null);
        await _matchmakerService.AddToMatchmaking("stalePlayer2", FindMatchType.BlindPublic, null);
        
        _mockPlayerSessionService.Setup(s => s.GetSession("activePlayer")).Returns(new PlayerSession
        {
            PlayerName = "activePlayer"
        });
        _mockPlayerSessionService.Setup(s => s.GetSession("stalePlayer1")).Returns((PlayerSession)null!);
        _mockPlayerSessionService.Setup(s => s.GetSession("stalePlayer2")).Returns((PlayerSession)null!);
        
        // Act
        var removedCount = await _matchmakerService.RemoveStaleFindMatches();

        // Assert
        removedCount.Should().Be(2);
    }
    
    [Fact]
    public async Task RemoveStaleFoundMatches_ShouldRemoveExpiredMatches()
    {
        // Arrange
        var validMatch = new FoundMatch
        {
            MatchFoundTime =
                DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(AshcrownApiConstants.TimeToAcceptMatchFoundSeconds - 10)),
            FindMatchType = FindMatchType.DraftPrivate
        };
        var staleMatch = new FoundMatch
        {
            MatchFoundTime =
                DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(AshcrownApiConstants.TimeToAcceptMatchFoundSeconds + 10)),
            FindMatchType = FindMatchType.BlindPublic
        };

        _matchmakerService.AddFoundMatch(Guid.NewGuid(), validMatch);
        _matchmakerService.AddFoundMatch(Guid.NewGuid(), staleMatch);
        
        // Act
        var removedCount = await _matchmakerService.RemoveStaleFoundMatches();

        // Assert
        removedCount.Should().Be(1);
    }
}
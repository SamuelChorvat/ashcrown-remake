using Ashcrown.Remake.Api.BackgroundServices;
using Ashcrown.Remake.Api.Services.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ashcrown.Remake.Api.Tests.BackgroundServices;

public class PlayerSessionCleanupServiceTests
{
    private class TestablePlayerSessionCleanupService(
        IPlayerSessionService playerSessionService,
        ILogger<PlayerSessionCleanupService> logger)
        : PlayerSessionCleanupService(playerSessionService, logger)
    {
        public async Task TestExecuteAsync(CancellationToken stoppingToken)
        {
            await ExecuteAsync(stoppingToken);
        }
    }
    
    [Fact]
    public async Task ExecuteAsync_ContinuesUntilCancellationRequested()
    {
        // Arrange
        const int sessionsRemoved = 5;
        var mockPlayerSessionService = new Mock<IPlayerSessionService>();
        var mockLogger = new Mock<ILogger<PlayerSessionCleanupService>>();
        var cancellationTokenSource = new CancellationTokenSource();
        var service = new TestablePlayerSessionCleanupService(mockPlayerSessionService.Object, mockLogger.Object);
        mockPlayerSessionService.Setup(s => s.RemoveStaleSessions(It.IsAny<int>()))
            .ReturnsAsync(sessionsRemoved);

        // Act
        var task = Task.Run(() => service.TestExecuteAsync(cancellationTokenSource.Token), cancellationTokenSource.Token);
        var act = async () => await task;
        await Task.Delay(100, cancellationTokenSource.Token);
        await cancellationTokenSource.CancelAsync();

        // Assert
        await act.Should().ThrowAsync<TaskCanceledException>();
        mockPlayerSessionService.Verify(s => s.RemoveStaleSessions(It.IsAny<int>()), Times.AtLeastOnce);

    }

}
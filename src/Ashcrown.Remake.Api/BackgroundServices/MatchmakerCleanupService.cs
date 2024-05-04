using Ashcrown.Remake.Api.Services.Interfaces;

namespace Ashcrown.Remake.Api.BackgroundServices;

public class MatchmakerCleanupService(IMatchmakerService matchmakerService, ILogger<MatchmakerCleanupService> logger) 
    : BackgroundService
{
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(AshcrownApiConstants.TimeToAcceptMatchFoundSeconds);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Matchmaker cleanup started!");
            var foundMatches = await matchmakerService.RemoveStaleFoundMatches();
            logger.LogInformation("Matchmaker cleanup finished! Removed {FoundMatched} found match(es)", foundMatches);
            await Task.Delay(_interval, stoppingToken);
        }
    }
}
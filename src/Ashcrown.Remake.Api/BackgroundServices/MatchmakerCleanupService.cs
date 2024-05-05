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
            var findMatches = await matchmakerService.RemoveStaleFindMatches();
            logger.LogInformation("Matchmaker cleanup finished! " +
                                  "Removed {FindMatches} find match(es) and {FoundMatches} found match(es)", 
                findMatches, foundMatches);
            await Task.Delay(_interval, stoppingToken);
        }
    }
}
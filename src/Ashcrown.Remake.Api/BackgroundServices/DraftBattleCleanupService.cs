using Ashcrown.Remake.Api.Services.Interfaces;

namespace Ashcrown.Remake.Api.BackgroundServices;

public class DraftBattleCleanupService(IDraftService draftService, 
    IBattleService battleService, ILogger<DraftBattleCleanupService> logger) : BackgroundService
{
    private const int StaleMatchLimitInMinutes = 120;
    
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(StaleMatchLimitInMinutes);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Draft cleanup started!");
            var draftsRemoved = await draftService.ClearStaleMatches(StaleMatchLimitInMinutes);
            logger.LogInformation("Draft cleanup finished! Removed {DraftsRemoved} draft(s)", draftsRemoved);
            
            logger.LogInformation("Battle cleanup started!");
            var battlesRemoved = await battleService.ClearStaleMatches(StaleMatchLimitInMinutes);
            logger.LogInformation("Battle cleanup finished! Removed {BattlesRemoved} battle(s)", battlesRemoved);
            
            await Task.Delay(_interval, stoppingToken);
        }
    }
}
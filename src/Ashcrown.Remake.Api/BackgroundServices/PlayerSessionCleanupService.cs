using Ashcrown.Remake.Api.Services.Interfaces;

namespace Ashcrown.Remake.Api.BackgroundServices;

public class PlayerSessionCleanupService(IPlayerSessionService playerSessionService, 
    ILogger<PlayerSessionCleanupService> logger) : BackgroundService
{
    private const int StaleSessionLimitInMinutes = 3;
    
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(StaleSessionLimitInMinutes);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Session cleanup started!");
            var sessionsRemoved = await playerSessionService.RemoveStaleSessions(StaleSessionLimitInMinutes);
            logger.LogInformation("Session cleanup finished! Removed {SessionsRemoved} session(s)", sessionsRemoved);
            await Task.Delay(_interval, stoppingToken);
        }
    }
}
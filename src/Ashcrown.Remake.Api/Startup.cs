using Ashcrown.Remake.Api.BackgroundServices;
using Ashcrown.Remake.Api.Services;
using Ashcrown.Remake.Api.Services.Interfaces;
using Ashcrown.Remake.Core.Champion;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Api;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddControllers();
        services.AddSwaggerGen();
        
        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddHostedService<PlayerSessionCleanupService>();

        services.AddSingleton<IChampionFactory, ChampionFactory>();
        services.AddSingleton<IChampionDataService, ChampionDataService>();
        services.AddSingleton<IPlayerSessionService, PlayerSessionService>();
        services.AddSingleton<IMatchmakerService, MatchmakerService>();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();
    }
}
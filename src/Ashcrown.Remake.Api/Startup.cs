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
        
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAshcrownOrigins",
                builder => builder.SetIsOriginAllowedToAllowWildcardSubdomains()
                    .WithOrigins("https://ashcrown.com", "https://*.ashcrown.com")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });
        
        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddHostedService<PlayerSessionCleanupService>();
        services.AddHostedService<MatchmakerCleanupService>();
        services.AddHostedService<DraftBattleCleanupService>();

        services.AddSingleton<IChampionFactory, ChampionFactory>();
        services.AddSingleton<IChampionDataService, ChampionDataService>();
        services.AddSingleton<IPlayerSessionService, PlayerSessionService>();
        services.AddSingleton<IMatchmakerService, MatchmakerService>();
        services.AddSingleton<IBattleService, BattleService>();
        services.AddSingleton<IDraftService, DraftService>();
        services.AddSingleton<IMatchHistoryService, MatchHistoryService>();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseCors("AllowAshcrownOrigins");
        }

        app.UseHttpsRedirection();
        app.MapControllers();
    }
}
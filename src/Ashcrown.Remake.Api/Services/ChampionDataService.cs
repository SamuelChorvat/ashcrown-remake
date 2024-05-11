using Ashcrown.Remake.Api.Dtos.Outbound;
using Ashcrown.Remake.Api.Services.Interfaces;
using Ashcrown.Remake.Core.Battle;
using Ashcrown.Remake.Core.Champion;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Champions.Eluard.Champion;
using Ashcrown.Remake.Core.Champions.Sarfu.Champion;
using Microsoft.Extensions.Logging.Abstractions;

namespace Ashcrown.Remake.Api.Services;

public class ChampionDataService(IChampionFactory championFactory) : IChampionDataService
{
    private List<ChampionData>? _cachedChampionsData;

    public Task<List<ChampionData>> GetChampionsData()
    {
        if (_cachedChampionsData is not null) return Task.FromResult(_cachedChampionsData);
        
        var championsData = new List<ChampionData>();
        foreach (var championsName in ChampionConstants.AllChampionsNames)
        {
            var battleLogic = new BattleLogic(false, new NullLoggerFactory());
            var battlePlayer = new BattlePlayer(1, "Name", false,
                [AlthalosConstants.Name, EluardConstants.Name, SarfuConstants.Name], 
                battleLogic, new TeamFactory(), new NullLoggerFactory());
            var champion = championFactory.CreateChampion(battleLogic, battlePlayer, 
                championsName, 1, new NullLoggerFactory());
            var championData = new ChampionData
            {
                Name = champion.Name,
                Title = champion.Title,
                Bio = champion.Bio,
                Attributes = champion.Attributes,
                Artist = champion.Artist
            };

            for (var i = 0; i < 4; i++)
            {
                foreach (var ability in champion.Abilities[i])
                {
                    championData.Abilities.Add(new AbilityData
                    {
                        Name = ability.Name,
                        Description = ability.Description,
                        Cooldown = ability.OriginalCooldown,
                        Classes = ability.AbilityClasses.Select(x => x.ToString()).ToArray(),
                        Cost = ability.OriginalCost,
                        Slot = i + 1
                    });
                }
            }
            
            championsData.Add(championData);
        }

        _cachedChampionsData = championsData;
        return Task.FromResult(_cachedChampionsData);
    }
}
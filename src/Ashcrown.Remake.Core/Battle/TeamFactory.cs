using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Battle;

public class TeamFactory : ITeamFactory
{
    private readonly ChampionFactory _championFactory = new();

    public IChampion[] CreateTeam(IBattleLogic battleLogic, string[] championNames, IBattlePlayer battlePlayer, ILoggerFactory loggerFactory)
    {
        var champions = new IChampion[championNames.Length];
        for (var i = 0; i < championNames.Length; i++)
        {
            champions[i] = _championFactory.CreateChampion(battleLogic, battlePlayer, championNames[i], i + 1, loggerFactory);
        }

        return champions;
    }
}
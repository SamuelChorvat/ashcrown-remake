using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Battle;

public class TeamFactory : ITeamFactory
{
    public IChampion[] CreateTeam(IBattleLogic battleLogic, string[] championNames, IBattlePlayer battlePlayer, ILoggerFactory loggerFactory)
    {
        var champions = new IChampion[championNames.Length];
        for (var i = 0; i < championNames.Length; i++)
        {
            champions[i] = CreateChampion(battleLogic, battlePlayer, championNames[i], i + 1, loggerFactory);
        }

        return champions;
    }

    private static IChampion CreateChampion(IBattleLogic battleLogic, IBattlePlayer battlePlayer, string championName, int championNo, ILoggerFactory loggerFactory)
    {
        var formattedChampionName = championName.Replace("'","");
        var className = $"Ashcrown.Remake.Core.Champions.{formattedChampionName}.Champion.{formattedChampionName}";
        var type = Type.GetType(className);

        if (type == null) throw new Exception("Champion class not found");
        
        object[] constructorArgs = [battleLogic, battlePlayer, championNo, loggerFactory];
        var instance = Activator.CreateInstance(type, constructorArgs);

        if (instance == null) throw new Exception("Champion instance creation failed");
        return (IChampion) instance;
    }
}
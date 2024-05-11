using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champion;

public class ChampionFactory : IChampionFactory
{
    public IChampion CreateChampion(IBattleLogic battleLogic, IBattlePlayer battlePlayer, string championName, int championNo,
        ILoggerFactory loggerFactory)
    {
        var formattedChampionName = championName.Replace("'","");
        var className = $"Ashcrown.Remake.Core.Champions.{formattedChampionName}.Champion.{formattedChampionName}";
        var type = Type.GetType(className);

        if (type == null) throw new Exception($"Champion class not found {championName}, {className}");
        
        object[] constructorArgs = [battleLogic, battlePlayer, championNo, loggerFactory];
        var instance = Activator.CreateInstance(type, constructorArgs);

        if (instance == null) throw new Exception($"Champion instance creation failed {championName}, {className}");
        return (IChampion) instance;
    }
}
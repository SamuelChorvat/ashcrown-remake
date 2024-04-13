using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Battle;

public class TeamFactory : ITeamFactory
{
    public IChampion[] CreateTeam(IBattleLogic battleLogic, string[] championNames, IBattlePlayer battlePlayer)
    {
        var champions = new IChampion[championNames.Length];
        for (var i = 0; i < championNames.Length; i++)
        {
            champions[i] = CreateChampion(battleLogic, battlePlayer, championNames[i], i + 1);
        }

        return champions;
    }

    private static IChampion CreateChampion(IBattleLogic battleLogic, IBattlePlayer battlePlayer, string championName,  int championNo)
    {
        var formattedChampionName = championName.Replace("'","");
        var className = $"Ashcrown.Remake.Core.Champions.{formattedChampionName}.Champion.{formattedChampionName}";
        var type = Type.GetType(className);

        if (type == null) throw new Exception("Champion class not found");
        
        object[] constructorArgs = [battleLogic, battlePlayer, championNo];
        var instance = Activator.CreateInstance(type, constructorArgs);

        if (instance == null) throw new Exception("Champion instance creation failed");
        return (IChampion) instance;
    }
}
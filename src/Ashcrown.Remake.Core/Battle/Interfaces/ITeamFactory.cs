using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Battle.Interfaces;

public interface ITeamFactory
{
    IChampion[] CreateTeam(string[] championNames, IBattlePlayer battlePlayer);
    string GetAbilityHistoryString();
}
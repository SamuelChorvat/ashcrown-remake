using Ashcrown.Remake.Core.V1.Champion.Interfaces;

namespace Ashcrown.Remake.Core.V1.Battle.Interfaces;

public interface ITeamFactory
{
    IChampion[] CreateTeam(string[] championNames, IBattlePlayer battlePlayer);
    string GetAbilityHistoryString();
}
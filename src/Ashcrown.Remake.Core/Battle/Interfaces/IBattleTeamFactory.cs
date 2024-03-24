using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Battle.Interfaces;

public interface IBattleTeamFactory
{
    IEnumerable<IChampion> CreateBattleTeam(IEnumerable<string> championNames);
}
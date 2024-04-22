using Ashcrown.Remake.Core.Champion.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Battle.Interfaces;

public interface ITeamFactory
{
    IChampion[] CreateTeam(IBattleLogic battleLogic, string[] championNames, IBattlePlayer battlePlayer, ILoggerFactory loggerFactory);
}
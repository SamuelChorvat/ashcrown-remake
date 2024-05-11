using Ashcrown.Remake.Core.Battle.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champion.Interfaces;

public interface IChampionFactory
{
    IChampion CreateChampion(IBattleLogic battleLogic, IBattlePlayer battlePlayer, string championName, int championNo,
        ILoggerFactory loggerFactory);
}
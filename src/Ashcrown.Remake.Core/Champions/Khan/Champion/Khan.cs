using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Khan.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Khan.Champion;

public class Khan : ChampionBase<KhanConstants>
{
    public Khan(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new MortalStrike(this),
            new Bladestorm(this),
            new HandOfTheProtector(this),
            new Perception(this));
    }
}
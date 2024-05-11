using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Branley.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Branley.Champion;

public class Branley : ChampionBase<BranleyConstants>
{
    public Branley(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new Plunder(this),
            new FireTheCannons(this),
            new RaiseTheFlag(this),
            new DefensiveManeuver(this));
    }
}
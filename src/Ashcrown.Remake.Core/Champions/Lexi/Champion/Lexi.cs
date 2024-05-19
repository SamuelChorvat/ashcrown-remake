using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Lexi.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Lexi.Champion;

public class Lexi : ChampionBase<LexiConstants>
{
    public Lexi(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new ThornWhip(this),
            new Nourish(this),
            new Tranquility(this),
            new Shadowmeld(this));
    }
}
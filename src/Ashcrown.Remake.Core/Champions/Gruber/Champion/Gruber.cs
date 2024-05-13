using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Gruber.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Gruber.Champion;

public class Gruber : ChampionBase<GruberConstants>
{
    public Gruber(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new PoisonInjection(this),
            new AdaptiveVirus(this),
            new ExplosiveLeech(this),
            new DNAEnhancement(this));
    }
}
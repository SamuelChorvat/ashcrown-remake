using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Eluard.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Eluard.Champion;

public class Eluard : ChampionBase<EluardConstants>
{
    public Eluard(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new SwordStrike(this),
            new Devastate(this), 
            new UnyieldingWill(this), 
            new Evade(this));
    }
}
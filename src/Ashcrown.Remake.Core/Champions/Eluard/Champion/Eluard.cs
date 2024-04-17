using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champions.Eluard.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Eluard.Champion;

public class Eluard : Core.Champion.Abstract.Champion
{
    public Eluard(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, EluardConstants.Eluard, loggerFactory)
    {
        SetStartAbilities(
            new SwordStrike(this),
            new Devastate(this), 
            new UnyieldingWill(this), 
            new Evade(this));
    }
}
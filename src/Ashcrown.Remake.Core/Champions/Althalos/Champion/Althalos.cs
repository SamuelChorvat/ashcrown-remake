using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champions.Althalos.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Althalos.Champion;

public class Althalos : Core.Champion.Abstract.Champion<AlthalosConstants>
{
    public Althalos(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new HammerOfJustice(this),
            new HolyLight(this), 
            new CrusaderOfLight(this), 
            new DivineShield(this));
    }
}
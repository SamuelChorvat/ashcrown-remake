using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champions.Althalos.Abilities;

namespace Ashcrown.Remake.Core.Champions.Althalos.Champion;

public class Althalos : Core.Champion.Abstract.Champion
{
    public Althalos(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo) 
        : base(battleLogic, battlePlayer, championNo, AlthalosConstants.Althalos)
    {
        SetStartAbilities(
            new HammerOfJustice(this),
            new HolyLight(this), 
            new CrusaderOfLight(this), 
            new DivineShield(this));
    }
}
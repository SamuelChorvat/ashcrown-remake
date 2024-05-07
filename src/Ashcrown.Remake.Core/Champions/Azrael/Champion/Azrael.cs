using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Azrael.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Azrael.Champion;

public class Azrael : ChampionBase<AzraelConstants>
{
    public Azrael(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new SoulRealm(this),
            new Reap(this),
            new CursedMark(this),
            new Disappear(this));
    }

    public override int AiApplyChampionSpecificPenalty(int totalPoints, IAbility ability)
    {
        if (ability.Owner.BattlePlayer.PlayerNo == BattlePlayer.PlayerNo) {
            return totalPoints;
        }

        if (ActiveEffectController.ActiveEffectPresentByActiveEffectName(AzraelConstants.CursedMarkMeActiveEffect)) {
            return totalPoints - 50;
        }

        return totalPoints;
    }
}
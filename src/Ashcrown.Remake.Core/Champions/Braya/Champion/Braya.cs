using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Braya.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Braya.Champion;

public class Braya : ChampionBase<BrayaConstants>
{
    public Braya(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new HuntersFocus(this),
            new QuickShot(this),
            new KillShot(this),
            new Disengage(this));
    }

    public override int AiApplyChampionSpecificPenalty(int totalPoints, IAbility ability)
    {
        if (!ActiveEffectController.ActiveEffectPresentByActiveEffectName(BrayaConstants.HuntersFocusIgnoreActiveEffect)
            || ability.Owner.BattlePlayer.PlayerNo == BattlePlayer.PlayerNo
            || ability is { Harmful: false, CostIncrease: false }) {
            return totalPoints;
        }

        return 0;
    }
}
using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Aniel.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Aniel.Champion;

namespace Ashcrown.Remake.Core.Champions.Aniel.Abilities;

public class BladeOfGluttony : AbilityBase
{
        public BladeOfGluttony(IChampion owner)
        : base(owner,
               AnielConstants.BladeOfGluttony,
               2,
               [0,0,0,1,0],
               [AbilityClass.Strategic, AbilityClass.Instant, AbilityClass.Melee],
               AbilityTarget.Enemy,
               AbilityType.EnemyEnergySteal,
               3)
    {
        Duration1 = 1;
        ReceiveDamageIncreasePoint1 = 15;
        Harmful = true;
        Debuff = true;
        EnergyRemove = true;
        EnergyAmount = 1;

        Description = $"{AnielConstants.Name} removes <sprite=4> from one enemy. " +
                      $"If used on the same enemy one turn after {AnielConstants.EnchantedGarlicBomb.HighlightInGold()} that enemy will also take " +
                      $"{ReceiveDamageIncreasePoint1} more damage from {"physical damage".HighlightInOrange()} for {Duration1} turn. " +
                      $"If used on the same enemy one turn after {AnielConstants.Condemn.HighlightInGold()} that enemy will also take " +
                      $"{ReceiveDamageIncreasePoint1} more damage from {"magic damage".HighlightInBlue()} for {Duration1} turn.";
    }

    public override void AdditionalReceiveAbilityEnergyStealLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AnielConstants.EnchantedGarlicBombUsedTargetCantActiveEffect))
        {
            Owner.ChampionController.DealActiveEffect(target,
                this,
                new BladeOfGluttonyUsedPhysicalActiveEffect(this, target),
                true, appliedAdditionalLogic);

        }
        else if (target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AnielConstants.CondemnUsedActiveEffect))
        {
            Owner.ChampionController.DealActiveEffect(target,
                this,
                new BladeOfGluttonyUsedMagicActiveEffect(this, target),
                true, appliedAdditionalLogic);
        }
        else
        {
            Owner.ChampionController.DealActiveEffect(target,
                this,
                new BladeOfGluttonyUsedActiveEffect(this, target),
                true, appliedAdditionalLogic);
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetRemoveEnergyPoints(EnergyAmount, 1, this, target);
        if (target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AnielConstants.EnchantedGarlicBombUsedTargetCantActiveEffect)
            || target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AnielConstants.CondemnUsedActiveEffect))
        {
            totalPoints += 30;
        }

        return totalPoints;
    }
}
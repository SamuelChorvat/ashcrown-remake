using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Khan.Champion;

namespace Ashcrown.Remake.Core.Champions.Khan.Abilities;

public class MortalStrike : AbilityBase
{
    public MortalStrike(IChampion champion) 
        : base(champion, 
            KhanConstants.MortalStrike, 
            1,
            [0,1,0,0,1], 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamageAndDebuff, 
            1)
    {
        Damage1 = 35;
        Duration1 = 2;
        DealHealReductionPercent1 = 25;
        ReceiveHealReductionPercent1 = 50;
        
        Description = $"{KhanConstants.Name} deals {$"{Damage1} physical damage".HighlightInOrange()} to one enemy. " +
                      $"For {Duration1} turns that enemy wll receive {ReceiveHealReductionPercent1}% less healing and healing they deal will be reduced by {DealHealReductionPercent1}%.";
        Harmful = true;
        Debuff = true;
        PhysicalDamage = true;
        Damaging = true;

        ActiveEffectOwner = KhanConstants.Name;
        ActiveEffectName = KhanConstants.MortalStrikeActiveEffect;
    }

    public override int DealAbilityDamageModifier(IChampion target, int amount, bool secondary)
    {
        var newAmount = amount;

        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(KhanConstants.BladestormMeActiveEffect)) {
            newAmount += Owner.ActiveEffectController.GetActiveEffectByName(KhanConstants.BladestormMeActiveEffect)!.OriginAbility.BonusDamage1 
                         * Owner.ActiveEffectController.GetActiveEffectCountByName(KhanConstants.BladestormMeActiveEffect);
        }

        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(KhanConstants.HandOfTheProtectorActiveEffect)) {
            newAmount += Owner.ActiveEffectController.GetActiveEffectByName(KhanConstants.HandOfTheProtectorActiveEffect)!.OriginAbility.BonusDamage1 
                         * Owner.ActiveEffectController.GetActiveEffectCountByName(KhanConstants.HandOfTheProtectorActiveEffect);
            newAmount = Math.Max(newAmount, 0);
        }

        return newAmount;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(DealAbilityDamageModifier(target, Damage1, false), 
            1, this, target);
        totalPoints += T.GetHealingReductionPoints(Duration1, this, target);
        return totalPoints;
    }
}
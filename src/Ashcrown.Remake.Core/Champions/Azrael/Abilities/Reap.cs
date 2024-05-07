using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Azrael.Champion;

namespace Ashcrown.Remake.Core.Champions.Azrael.Abilities;

public class Reap : AbilityBase
{
    public Reap(IChampion champion) 
        : base(champion, 
            AzraelConstants.Reap, 
            0,
            [0,0,0,1,0],
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamageAndDebuff, 
            2)
    {
        Damage1 = 20;
        BonusDamage1 = 10;
        BonusDamage2 = 20;
        Duration1 = 1;
        
        Description = $"{AzraelConstants.Name} deals {$"{Damage1} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()} to one enemy. " +
                      $"If that enemy uses a new ability the following turn {AzraelConstants.Reap.HighlightInGold()} will deal an additional {$"{BonusDamage1} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()} to that enemy for {Duration1} turn. " +
                      $"This ability {"ignores invulnerability".HighlightInPurple()}.";
        Harmful = true;
        PhysicalDamage = true;
        Damaging = true;
        PiercingDamage = true;
        IgnoreInvulnerability = true;
        Debuff = true;
        ActiveEffectOwner = AzraelConstants.Name;
        ActiveEffectName = AzraelConstants.ReapTargetActiveEffect;
    }

    public override int ReceiveAbilityDamageModifier(IChampion target, int amount)
    {
        var newAmount = amount;

        if (target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AzraelConstants.ReapTriggeredTargetActiveEffect)) {
            newAmount += BonusDamage1 * target.ActiveEffectController.GetActiveEffectCountByName(AzraelConstants.ReapTriggeredTargetActiveEffect);
        }

        return newAmount;
    }

    public override int DealAbilityDamageModifier(IChampion target, int amount, bool secondary)
    {
        var newAmount = amount;

        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AzraelConstants.ReapMeActiveEffect)) {
            newAmount += BonusDamage2 * Owner.ActiveEffectController.GetActiveEffectCountByName(AzraelConstants.ReapMeActiveEffect);
        }

        return newAmount;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(
            ReceiveAbilityDamageModifier(target, DealAbilityDamageModifier(target, Damage1, false)), 
            1, this, target);
        totalPoints += 10;
        return totalPoints;
    }
}
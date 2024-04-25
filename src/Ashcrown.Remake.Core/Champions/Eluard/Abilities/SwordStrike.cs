using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Eluard.Champion;

namespace Ashcrown.Remake.Core.Champions.Eluard.Abilities;

public class SwordStrike : Ability.Abstract.Ability
{
    public SwordStrike(IChampion champion) 
        : base(champion, 
            EluardConstants.SwordStrike, 
            0,
            [0,0,1,0,0],
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage)
    {
        Damage1 = 20;
        BonusDamage1 = 10;
        Description =
            $"{EluardConstants.Name} strikes one enemy, dealing {$"{Damage1} physical damage".HighlightInOrange()} to them. " +
            $"While {EluardConstants.UnyieldingWill.HighlightInGold()} is active this ability " +
            $"will deal an additional {$"{BonusDamage1} physical damage".HighlightInOrange()}.";
        Harmful = true;
        PhysicalDamage = true;
        Damaging = true;
    }

    public override int DealAbilityDamageModifier(IChampion target, int amount, bool secondary)
    {
        var newAmount = amount;

        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(EluardConstants.UnyieldingWillActiveEffect)) {
            newAmount += BonusDamage1 * 
                         Owner.ActiveEffectController.GetActiveEffectCountByName(EluardConstants.UnyieldingWillActiveEffect);
        }

        return newAmount;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(
            DealAbilityDamageModifier(target, Damage1, false), 1,this, target);
        return totalPoints;
    }
}
using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Branley.Champion;

namespace Ashcrown.Remake.Core.Champions.Branley.Abilities;

public class Plunder : AbilityBase
{
    public Plunder(IChampion champion) 
        : base(champion, 
            BranleyConstants.Plunder, 
            0,
            [0,1,0,0,1], 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee],
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            1)
    {
        Damage1 = 30;
        Damage2 = 45;
        
        Description = $"{BranleyConstants.Name} deals {$"{Damage1} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()} to one enemy. " +
                      $"During {BranleyConstants.RaiseTheFlag.HighlightInGold()}, this ability will deal {$"{Damage2} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()} to one enemy and it will {"ignore invulnerability".HighlightInPurple()}.";
        Harmful = true;
        Damaging = true;
        PhysicalDamage = true;
        PiercingDamage = true;
    }

    public override int DealAbilityDamageModifier(IChampion target, int amount, bool secondary)
    {
        var newAmount = amount;

        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(BranleyConstants.RaiseTheFlagMeActiveEffect)) {
            newAmount = Damage2;
        }

        return newAmount;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(
            DealAbilityDamageModifier(target, Damage1, false), 1, this, target);
        return totalPoints;
    }
}
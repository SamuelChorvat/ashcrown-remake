using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Sarfu.Champion;

namespace Ashcrown.Remake.Core.Champions.Sarfu.Abilities;

public class Charge : Ability.Abstract.Ability
{
    public Charge(IChampion champion) 
        : base(champion, 
            SarfuConstants.Charge, 
            1,
            [0,1,0,0,1],
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage)
    {
        Damage1 = 35;
        BonusDamage1 = 25;
        Description = $"{SarfuConstants.Name} stabs one enemy with his sword, dealing {$"{Damage1} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()}. " +
                      $"This ability will deal an additional {$"{BonusDamage1} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()} to an enemy affected by {SarfuConstants.Duel.HighlightInGold()}.";
        Harmful = true;
        Damaging = true;
        PhysicalDamage = true;
        PiercingDamage = true;
    }

    public override int ReceiveAbilityDamageModifier(IChampion target, int amount)
    {
        var newAmount = amount;
        newAmount += BonusDamage1 
                     * target.ActiveEffectController.GetActiveEffectCountByName(SarfuConstants.DuelTargetActiveEffect);
        return newAmount;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(
            ReceiveAbilityDamageModifier(target, Damage1), 1,this, target);
        return totalPoints;
    }
}
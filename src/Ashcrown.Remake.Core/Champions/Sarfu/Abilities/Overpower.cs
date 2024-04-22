using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Sarfu.Champion;

namespace Ashcrown.Remake.Core.Champions.Sarfu.Abilities;

public class Overpower : Ability.Abstract.Ability
{
    public Overpower(IChampion champion) 
        : base(champion, 
            SarfuConstants.Overpower, 
            0,
            [0,0,1,0,1],
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage)
    {
        Damage1 = 30;
        BonusDamage1 = 15;
        Description = $"{SarfuConstants.Sarfu} slashes one enemy with his axe, dealing {$"{Damage1} physical damage".HighlightInOrange()}. " +
                      $"This ability will deal an additional {$"{BonusDamage1} physical damage".HighlightInOrange()} to an enemy affected by {SarfuConstants.Duel.HighlightInGold()}.";
        PhysicalDamage = true;
        Harmful = true;
        Damaging = true;
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
        var totalPoints = T.GetDamagePoints(ReceiveAbilityDamageModifier(target, Damage1), 1,this, target);
        return totalPoints;
    }
}
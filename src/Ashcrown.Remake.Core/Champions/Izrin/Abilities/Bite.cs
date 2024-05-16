using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Izrin.Champion;

namespace Ashcrown.Remake.Core.Champions.Izrin.Abilities;

public class Bite : AbilityBase
{
    public Bite(IChampion champion) 
        : base(champion, 
            IzrinConstants.Bite, 
            0,
            [0,0,0,0,2], 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee],
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            3)
    {
        Damage1 = 20;
        BonusDamage1 = 20;
        
        Description = $"{IzrinConstants.Name} bites one enemy dealing {$"{Damage1} physical damage".HighlightInOrange()} to them. " +
                      $"{IzrinConstants.Bite.HighlightInGold()} will deal and additional {$"{BonusDamage1} physical damage".HighlightInOrange()} for each ally that is dead " +
                      $"and an additional {$"{BonusDamage1} physical damage".HighlightInOrange()} for every {"30 health".HighlightInGreen()} that {IzrinConstants.Name} is missing.";
        Harmful = true;
        PhysicalDamage = true;
        Damaging = true;
    }

    public override int DealAbilityDamageModifier(IChampion target, int amount, bool secondary)
    {
        var newAmount = amount;

        newAmount += BonusDamage1 * Owner.BattlePlayer.NoOfDead();
        newAmount += BonusDamage1 * ((int) ((double)100 - Owner.Health)/30);

        return newAmount;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(DealAbilityDamageModifier(target,Damage1, false), 
            1, this, target);
        return totalPoints;
    }
}
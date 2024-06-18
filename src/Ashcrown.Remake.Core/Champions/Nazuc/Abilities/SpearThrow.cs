using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Nazuc.Champion;

namespace Ashcrown.Remake.Core.Champions.Nazuc.Abilities;

public class SpearThrow : AbilityBase
{
    public SpearThrow(IChampion champion) 
        : base(champion, 
            NazucConstants.SpearThrow, 
            0,
            [0,0,1,0,1], 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            1)
    {
        Damage1 = 30;
        BonusDamage1 = 5;
        
        Description = $"This ability does {$"{Damage1} physical damage".HighlightInOrange()} to one enemy. During {NazucConstants.SpearBarrage.HighlightInOrange()} this ability's cost is reduced by <sprite=4>.";
        Harmful = true;
        Damaging = true;
        PhysicalDamage = true;
    }

    public override int ReceiveAbilityDamageModifier(IChampion target, int amount)
    {
        var newAmount = amount;
        newAmount += BonusDamage1 *  target.ActiveEffectController.GetActiveEffectCountByName(NazucConstants.HuntersMarkActiveEffect);
        return newAmount;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(ReceiveAbilityDamageModifier(target, Damage1), 1, this, target);
        return totalPoints;
    }
}
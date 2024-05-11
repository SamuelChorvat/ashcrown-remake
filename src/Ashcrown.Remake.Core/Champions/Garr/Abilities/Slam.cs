using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Garr.Champion;

namespace Ashcrown.Remake.Core.Champions.Garr.Abilities;

public class Slam : AbilityBase
{
    public Slam(IChampion champion) 
        : base(champion, 
            GarrConstants.Slam, 
            0,
            [0,0,1,0,0], 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            1)
    {
        Damage1 = 20;
        BonusDamage1 = 20;
        
        Description = $"{GarrConstants.Name} slams one enemy with his axe dealing {$"{Damage1} physical damage".HighlightInOrange()} to them.";
        Harmful = true;
        PhysicalDamage = true;
        Damaging = true;
    }

    public override int DealAbilityDamageModifier(IChampion target, int amount, bool secondary)
    {
        var newAmount = amount;

        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GarrConstants.RecklessnessActiveEffect)) {
            newAmount += BonusDamage1 * Owner.ActiveEffectController
                .GetActiveEffectByName(GarrConstants.RecklessnessActiveEffect)!.Stacks;
        }

        return newAmount;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(DealAbilityDamageModifier(target, Damage1, false), 
            1, this, target);
        return totalPoints;
    }
}
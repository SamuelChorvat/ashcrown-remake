using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Nerissa.Champion;

namespace Ashcrown.Remake.Core.Champions.Nerissa.Abilities;

public class Overflow : AbilityBase
{
    public Overflow(IChampion champion) 
        : base(champion, 
            NerissaConstants.Overflow, 
            0,
            [1,0,0,0,0], 
            [AbilityClass.Magic, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            1)
    {
        Damage1 = 15;
        BonusDamage1 = 5;
        
        Description = $"{NerissaConstants.Name} targets one enemy. They will lose all {"destructible defense".HighlightInYellow()} " +
                      $"and receive {$"{Damage1} {"piercing".HighlightInBold()} magic damage".HighlightInBlue()}.";
        Harmful = true;
        Damaging = true;
        MagicDamage = true;
        PiercingDamage = true;
    }

    public override int ReceiveAbilityDamageModifier(IChampion target, int amount)
    {
        var newAmount = amount;
        newAmount += BonusDamage1 *  target.ActiveEffectController
            .GetActiveEffectCountByName(NerissaConstants.DrownActiveEffect);
        return newAmount;
    }

    public override void AdditionalDealAbilityDamageLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        var i = 0;
        while (i < target.ActiveEffects.Count) {
            if (target.ActiveEffects[i].DestructibleDefense > 0) {
                target.ActiveEffects[i].DestructibleDefense = 0;
                target.ActiveEffects[i].RemoveDestructibleDefense(this, null);
                i = 0;
            } else {
                i += 1;
            }
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(ReceiveAbilityDamageModifier(target, Damage1)
                                            + T.GetTotalDestructible(target), 1, this, target);
        return totalPoints;
    }
}
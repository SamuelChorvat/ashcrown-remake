using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Lexi.Champion;

namespace Ashcrown.Remake.Core.Champions.Lexi.Abilities;

public class Nourish : AbilityBase
{
    public Nourish(IChampion champion) 
        : base(champion, 
            LexiConstants.Nourish, 
            1,
            [1,0,0,0,0], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Ally, 
            AbilityType.AllyHeal, 
            2)
    {
        Heal1 = 0;
        
        Description = $"{LexiConstants.Name} heals half of hers or one ally's missing health (rounded down) and {"removes".HighlightInPurple()} all enemy affliction effects from them. " +
                      $"While {LexiConstants.Tranquility.HighlightInGold()} is active, this ability will cost <sprite=4> and have no cooldown.";
        Helpful = true;
        SelfCast = true;
        Healing = true;
    }

    public override void AdditionalReceiveAbilityHealingLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        target.ActiveEffectController.RemoveEnemyAfflictions();
    }

    public override int DealAbilityHealingModifier(IChampion target, int amount)
    {
        return (int) ((double)100 - target.Health)/2;
    }

    public override bool UseChecks()
    {
        var aeRef = Owner.ActiveEffectController.GetActiveEffectByName(LexiConstants.TranquilityActiveEffect);
        if (aeRef is {Stacks: > 0}) {
            aeRef.Stacks -= 1;
        }

        return true;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetHealingPoints(DealAbilityHealingModifier(target, Heal1), 
            1, this, target);
        totalPoints += T.GetRemoveAfflictionsPoints(this, target);
        return totalPoints;
    }
}
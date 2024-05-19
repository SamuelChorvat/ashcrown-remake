using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Lexi.Champion;

namespace Ashcrown.Remake.Core.Champions.Lexi.Abilities;

public class Tranquility : AbilityBase
{
    public Tranquility(IChampion champion) 
        : base(champion, 
            LexiConstants.Tranquility, 
            0,
            [0,0,0,0,1], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Self, 
            AbilityType.AllyBuff, 
            3)
    {
        Heal1 = 25;
        
        Description = $"{LexiConstants.Name} gains 3 {LexiConstants.Tranquility.HighlightInGold()} stacks. During this time, " +
                      $"each time Lexi uses an ability other than {LexiConstants.Shadowmeld.HighlightInGold()}, she will use up 1 {LexiConstants.Tranquility.HighlightInGold()} stack. " +
                      $"While {LexiConstants.Tranquility.HighlightInGold()} is active, {LexiConstants.Name} can use this ability again for no cost to restore {$"{Heal1} health".HighlightInGreen()} " +
                      $"and {"remove".HighlightInPurple()} all enemy harmful effects on her.";
        SelfCast = true;
        Helpful = true;
        Buff = true;

        ActiveEffectOwner = LexiConstants.Name;
        ActiveEffectName = LexiConstants.TranquilityActiveEffect;
    }

    public override void AdditionalReceiveAbilityHealingLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        target.ActiveEffectController.RemoveEnemyHarmfulEffects();
    }

    public override bool UseChecks()
    {
        var aeRef = Owner.ActiveEffectController.GetActiveEffectByName(LexiConstants.TranquilityActiveEffect);
        if (aeRef != null && aeRef.Stacks > 0) {
            aeRef.Stacks -= 1;
        }

        return true;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        int totalPoints;
        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(LexiConstants.TranquilityActiveEffect)) {
            totalPoints = T.GetHealingPoints(Heal1, 1, this, target);
            totalPoints += T.GetRemoveHarmfulPoints(this, target);
        } else {
            totalPoints = T.GetSpecialConditionPoints(AiCalculatorConstants.InfiniteNumberOfTurns);
        }
        return totalPoints;
    }
}
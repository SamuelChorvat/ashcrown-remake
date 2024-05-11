using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Arabela.Champion;

namespace Ashcrown.Remake.Core.Champions.Arabela.Abilities;

public class FlashHeal : AbilityBase
{
    public FlashHeal(IChampion champion) 
        : base(champion, 
            ArabelaConstants.FlashHeal, 
            0,
            [1,0,0,0,0],
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Ally, 
            AbilityType.AllyHeal,
            2)
    {
        Heal1 = 25;
        Description = $"{ArabelaConstants.Name} heals herself or one ally for {$"{Heal1} health".HighlightInGreen()} and {"removes".HighlightInPurple()} all enemy {"afflictions".HighlightInRed()} from them.";
        Helpful = true;
        SelfCast = true;
        Healing = true;
    }

    public override void AdditionalReceiveAbilityHealingLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        target.ActiveEffectController.RemoveEnemyAfflictions();
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetHealingPoints(Heal1, 1, this, target);
        totalPoints += T.GetRemoveAfflictionsPoints(this, target);
        return totalPoints;
    }
}
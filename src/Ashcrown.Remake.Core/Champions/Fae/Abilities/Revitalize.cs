using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Fae.Champion;

namespace Ashcrown.Remake.Core.Champions.Fae.Abilities;

public class Revitalize : AbilityBase
{
    public Revitalize(IChampion champion) 
        : base(champion, 
            FaeConstants.Revitalize, 
            1, 
            new int[] {0,0,0,0,2}, 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Ally, 
            AbilityType.AllyHeal, 
            3)
    {
        Heal1 = 35;
        
        Description = $"{FaeConstants.Name} heals herself or one ally for {$"{Heal1} health".HighlightInGreen()} and " +
                      $"{"removes".HighlightInPurple()} all enemy afflictions from them.";
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
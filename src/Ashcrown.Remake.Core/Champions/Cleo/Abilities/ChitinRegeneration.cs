using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cleo.Champion;

namespace Ashcrown.Remake.Core.Champions.Cleo.Abilities;

public class ChitinRegeneration : AbilityBase
{
    public ChitinRegeneration(IChampion champion) 
        : base(champion, 
            CleoConstants.ChitinRegeneration, 
            3,
            [2,0,0,0,0], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Self, 
            AbilityType.AllyHeal, 
            2)
    {
        Description = $"{CleoConstants.Name} heals herself back to {"full health".HighlightInGreen()} " +
                      $"and {"removes".HighlightInPurple()} all harmful effects on her.";
        SelfCast = true;
        Helpful = true;
        Healing = true; ;
    }

    public override void AdditionalReceiveAbilityHealingLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        target.Health = 100;
        target.ActiveEffectController.RemoveEnemyHarmfulEffects();
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetHealingPoints(100, 1, this, target);
        totalPoints += T.GetRemoveHarmfulPoints(this, target);
        return totalPoints;
    }
}
using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cronos.Champion;

namespace Ashcrown.Remake.Core.Champions.Cronos.Abilities;

public class GravityWell : AbilityBase
{
    public GravityWell(IChampion champion) 
        : base(champion, 
            CronosConstants.GravityWell, 
            0,
            [1,0,0,0,0], 
            [AbilityClass.Affliction, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            1)
    {
        Damage1 = 20;
        Damage2 = 10;
        
        Description = $"{CronosConstants.Name} creates a powerful gravitational vortex at the target's location, " +
                      $"dealing {$"{Damage1} affliction damage".HighlightInRed()} to them and {$"{Damage2} affliction damage".HighlightInRed()} to all other enemies. " +
                      $"Only the primary target can {"counter".HighlightInPurple()} or {"reflect".HighlightInPurple()} this ability.";
        Harmful = true;
        Damaging = true;
        AfflictionDamage = true;
    }

    public override void AdditionalDealAbilityDamageLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        var secondaryTargets = target.BattlePlayer.GetOtherChampions(target);
        if (secondaryTargets == null) return;
        foreach (var secondaryTarget in secondaryTargets) {
            Owner.ChampionController.DealAbilityDamage(Damage2, secondaryTarget, 
                this, true, appliedAdditionalLogic);
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this , target);
        totalPoints += Damage2 * 2;
        return totalPoints;
    }
}
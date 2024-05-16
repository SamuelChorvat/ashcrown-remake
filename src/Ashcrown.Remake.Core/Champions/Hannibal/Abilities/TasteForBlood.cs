using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Hannibal.Champion;

namespace Ashcrown.Remake.Core.Champions.Hannibal.Abilities;

public class TasteForBlood : AbilityBase
{
    public TasteForBlood(IChampion champion) 
        : base(champion, 
            HannibalConstants.TasteForBlood, 
            0,
            [0,1,1,0,0], 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            1)
    {
        Damage1 = 35;
        
        Description = $"{HannibalConstants.Name} bites an enemy, dealing {$"{Damage1} physical damage".HighlightInOrange()} " +
                      $"and healing himself for the amount of health lost by the target.";
        Harmful = true;
        PhysicalDamage = true;
        Damaging = true;
    }

    public override void AdditionalSubtractHealthLogic(int toSubtract, IChampion victim, 
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        Owner.ChampionController.DealAbilityHealing(Math.Min(victim.Health, toSubtract), 
            Owner, this, appliedAdditionalLogic);
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        totalPoints += T.GetHealingPoints(Math.Min(Damage1, target.Health), 1, this, Owner);
        return totalPoints;
    }
}
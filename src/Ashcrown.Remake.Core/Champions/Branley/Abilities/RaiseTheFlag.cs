using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Branley.Champion;

namespace Ashcrown.Remake.Core.Champions.Branley.Abilities;

public class RaiseTheFlag : AbilityBase
{
    public RaiseTheFlag(IChampion champion) 
        : base(champion, 
            BranleyConstants.RaiseTheFlag, 
            3,
            [0,0,0,0,1], 
            [AbilityClass.Strategic,AbilityClass.Instant], 
            AbilityTarget.Enemies, 
            AbilityType.EnemiesDebuff, 
            3)
    {
        Duration1 = 2;
        ReceiveDamageReductionPoint1 = 5;
        
        Description = $"For {Duration1} turns, all enemies will have their Physical and Strategic abilities cost an additional <sprite=4>. " +
                      $"During this time Branley will gain {$"{ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()} and {BranleyConstants.Plunder.HighlightInGold()} will " +
                      $"deal {$"45 {"piercing".HighlightInBold()} physical damage".HighlightInOrange()} that {"ignores invulnerability".HighlightInPurple()}. This ability {"ignores invulnerability".HighlightInPurple()}.";
        SelfDisplay = true;
        Harmful = true;
        Debuff = true;
        Buff = true;
        Helpful = true;
        IgnoreInvulnerability = true;
        CostIncrease = true;
        CostIncreaseClasses = [AbilityClass.Physical, AbilityClass.Strategic];

        ActiveEffectOwner = BranleyConstants.Name;
        ActiveEffectName = BranleyConstants.RaiseTheFlagTargetActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetCostIncreasePoints(Duration1, this, target);
        return totalPoints;
    }

    public override int CalculateSingletonSelfEffectTotalPoints<T>()
    {
        var totalPoints = T.GetPointsReductionPoints(ReceiveDamageReductionPoint1, Duration1, Owner);
        totalPoints += T.GetSpecialConditionPoints(Duration1);
        return totalPoints;
    }
}
using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Luther.Champion;

namespace Ashcrown.Remake.Core.Champions.Luther.Abilities;

public class LivingForge : AbilityBase
{
    public LivingForge(IChampion champion) 
        : base(champion, 
            LutherConstants.LivingForge, 
            4,
            [0,0,0,0,1], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Enemies, 
            AbilityType.EnemiesDebuff, 
            3)
    {
        Duration1 = 4;
        ReceiveDamageReductionPoint1 = 20;
        DealDamageIncreasePoint1 = 5;
        
        Description = $"{LutherConstants.Name} targets all enemies and gains {$"{ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()} for {Duration1} turns. " +
                      $"The following {Duration1} turns, this ability will be replaced by {LutherConstants.ForgeSpirit.HighlightInGold()} and any time the enemy team uses a new Strategic ability, " +
                      $"{LutherConstants.Name}'s damage will be increased by {DealDamageIncreasePoint1} for 1 turn.";
        SelfDisplay = true;
        Debuff = true;
        Harmful = true;
        Buff = true;
        Helpful = true;

        ActiveEffectOwner = LutherConstants.Name;
        ActiveEffectName = LutherConstants.LivingForgeTargetActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        return 0;
    }

    public override int CalculateSingletonSelfEffectTotalPoints<T>()
    {
        var totalPoints = T.GetPointsReductionPoints(ReceiveDamageReductionPoint1, Duration1, Owner);
        totalPoints += T.GetSpecialConditionPoints(Duration1);
        return totalPoints;
    }
}
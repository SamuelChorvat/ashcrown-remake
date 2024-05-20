using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Luther.Champion;

namespace Ashcrown.Remake.Core.Champions.Luther.Abilities;

public class FieryBrand : AbilityBase
{
    public FieryBrand(IChampion champion) 
        : base(champion, 
            LutherConstants.FieryBrand, 
            0,
            [0,0,0,0,1], 
            [AbilityClass.Strategic, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDebuff, 
            2)
    {
        Duration1 = 1;
        
        Description = $"{LutherConstants.Name} brands one enemy. The following turn, the first new Strategic ability that the enemy uses " +
                      $"will be {"countered".HighlightInPurple()} and if {LutherConstants.Flamestrike.HighlightInGold()} is used on that enemy, it will last 2 additional turns. " +
                      $"This ability is {"invisible".HighlightInPurple()}.";
        Harmful = true;
        Debuff = true;
        Invisible = true;

        CounterClasses = [AbilityClass.Strategic];

        ActiveEffectOwner = LutherConstants.Name;
        ActiveEffectName = LutherConstants.FieryBrandTargetActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetCounterPointsTargetEnemy(Duration1, this, target);
        return totalPoints;
    }
}
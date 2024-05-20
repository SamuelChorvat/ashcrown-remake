using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Luther.Champion;

namespace Ashcrown.Remake.Core.Champions.Luther.Abilities;

public class Flamestrike : AbilityBase
{
    public Flamestrike(IChampion champion) 
        : base(champion, 
            LutherConstants.Flamestrike, 
            1,
            [0,1,0,0,1], 
            [AbilityClass.Physical, AbilityClass.Action, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyActionControl, 
            1)
    {
        Damage1 = 20;
        Duration1 = 2;
        
        Description = $"{LutherConstants.Name} strikes one enemy for {Duration1} turns, " +
                      $"dealing {$"{Damage1} physical damage".HighlightInOrange()} to them and increasing the cost of all their Strategic abilities by <sprite=4> during this time.";
        Harmful = true;
        Debuff = true;
        PhysicalDamage = true;
        Damaging = true;
        SelfDisplay = true;

        CostIncrease = true;
        CostIncreaseClasses = [AbilityClass.Strategic];

        ActiveEffectOwner = LutherConstants.Name;
        ActiveEffectSourceName = LutherConstants.FlamestrikeMeActiveEffect;
        ActiveEffectTargetName = LutherConstants.FlamestrikeTargetActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var duration = Duration1;
        if (target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(LutherConstants.FieryBrandCounterActiveEffect)) {
            duration += 2;
        }

        var totalPoints = T.GetDamagePoints(Damage1, duration, this, target);
        totalPoints += T.GetCostIncreasePoints(duration, this, target);
        return totalPoints;
    }
}
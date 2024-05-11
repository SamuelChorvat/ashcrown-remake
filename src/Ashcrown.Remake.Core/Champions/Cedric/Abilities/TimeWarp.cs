using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cedric.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Cedric.Champion;

namespace Ashcrown.Remake.Core.Champions.Cedric.Abilities;

public class TimeWarp : AbilityBase
{
    public TimeWarp(IChampion champion) 
        : base(champion, 
            CedricConstants.TimeWarp, 
            2,
            [0,0,0,0,2], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Allies, 
            AbilityType.AlliesBuff, 
            3)
    {
        Duration1 = 2;
        
        Description = $"For {Duration1} turns, {CedricConstants.Name} and his allies will have the cost of their abilities " +
                      $"reduced by <sprite=4> and the cooldown of their abilities reduced by 1 turn.";
        Helpful = true;
        Buff = true;
        SelfCast = true;

        CostDecreaseClasses = [AbilityClass.All];
        CooldownDecreaseClasses = [AbilityClass.All];
        ActiveEffectOwner = CedricConstants.Name;
        ActiveEffectName = CedricConstants.TimeWarpActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        int totalPoints;
        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(CedricConstants.MirrorImageActiveEffect)) {
            var mirrorImageStacks = Owner.ActiveEffectController
                .GetActiveEffectByName(CedricConstants.MirrorImageActiveEffect)!.Stacks;
            totalPoints = T.GetCostDecreasePoints(
                Duration1 + mirrorImageStacks, this, target);
            totalPoints += T.GetCooldownDecreasePoints(
                Duration1 + mirrorImageStacks, this, target);
        } else {
            totalPoints = T.GetCostDecreasePoints(Duration1, this, target);
            totalPoints += T.GetCooldownDecreasePoints(Duration1, this, target);
        }
        return totalPoints;
    }
}
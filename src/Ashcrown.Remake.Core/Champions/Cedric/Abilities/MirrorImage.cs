using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cedric.Champion;

namespace Ashcrown.Remake.Core.Champions.Cedric.Abilities;

public class MirrorImage : AbilityBase
{
    public MirrorImage(IChampion champion) 
        : base(champion, 
            CedricConstants.MirrorImage, 
            1, 
            new int[] {0,0,0,0,1}, 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Self, 
            AbilityType.AllyBuff, 
            2)
    {
        DestructibleDefense1 = 25;
        
        Description = $"{CedricConstants.Name} creates a clone of himself. " +
                      $"Each time this ability is used, he will gain {$"{DestructibleDefense1} destructible defense".HighlightInYellow()} " +
                      $"and all his abilities will last an additional turn. This effect stacks.";
        Helpful = true;
        Buff = true;
        SelfCast = true;
        ActiveEffectOwner = CedricConstants.Name;
        ActiveEffectName = CedricConstants.MirrorImageActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDestructibleDefensePoints(DestructibleDefense1, target);
        totalPoints += T.GetSpecialConditionPoints(AiCalculatorConstants.InfiniteNumberOfTurns);
        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(CedricConstants.MirrorImageActiveEffect)) {
            totalPoints -= Owner.ActiveEffectController.GetActiveEffectByName(CedricConstants.MirrorImageActiveEffect)!.Stacks * 40;
        }
        return totalPoints;
    }
}
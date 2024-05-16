using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Hrom.Champion;

namespace Ashcrown.Remake.Core.Champions.Hrom.Abilities;

public class LightningBarrier : AbilityBase
{
    public LightningBarrier(IChampion champion) 
        : base(champion, 
            HromConstants.LightningBarrier, 
            2,
            [0,0,0,0,1], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Ally, 
            AbilityType.AllyBuff, 
            2)
    {
        Duration1 = 1;
        
        Description = $"{HromConstants.Name} creates a barrier to protect himself or one ally for {Duration1} turn. " +
                      $"The first enemy harmful non-Strategic ability used on this ally will be {"countered".HighlightInPurple()}. " +
                      $"This ability is {"invisible".HighlightInPurple()}.";
        SelfCast = true;
        Helpful = true;
        Buff = true;
        Invisible = true;

        ActiveEffectOwner = HromConstants.Name;
        ActiveEffectName = HromConstants.LightningBarrierActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetCounterHarmfulPointsTargetAlly(Duration1, this, target);
        return totalPoints;
    }
}
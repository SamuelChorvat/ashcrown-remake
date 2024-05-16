using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Hrom.Champion;

namespace Ashcrown.Remake.Core.Champions.Hrom.ActiveEffects;

public class LightningBarrierEndActiveEffect : ActiveEffectBase
{
    public LightningBarrierEndActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(HromConstants.LightningBarrierEndActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Description = $"- {HromConstants.LightningBarrier.HighlightInGold()} has ended\n";
        Duration = Duration1;
        TimeLeft = Duration1;
        NoTick = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return Description + $"{"ENDS THIS TURN".HighlightInGold()}";
    }

    public override void EndTurnChecks()
    {
        Target.ActiveEffectController.RemoveActiveEffect(this);
    }
}
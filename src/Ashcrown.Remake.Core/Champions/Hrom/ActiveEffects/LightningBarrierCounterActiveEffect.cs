using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Hrom.Champion;

namespace Ashcrown.Remake.Core.Champions.Hrom.ActiveEffects;

public class LightningBarrierCounterActiveEffect : ActiveEffectBase
{
    public LightningBarrierCounterActiveEffect(IAbility originAbility, IChampion championTarget, string counterName) 
        : base(HromConstants.LightningBarrierCounterActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Description = $"- {counterName.HighlightInGold()} has been {"countered".HighlightInPurple()}\n";
        Duration = Duration1;
        TimeLeft = Duration1;
        NoTick = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return Description + $"{"ENDS THIS TURN".HighlightInGold()}";
    }

    public override void StartTurnChecks()
    {
        Target.ActiveEffectController.RemoveActiveEffect(this);
    }
}
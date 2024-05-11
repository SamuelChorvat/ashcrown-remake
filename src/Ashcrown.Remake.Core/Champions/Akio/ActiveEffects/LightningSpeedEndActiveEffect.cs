using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Akio.Champion;

namespace Ashcrown.Remake.Core.Champions.Akio.ActiveEffects;

public class LightningSpeedEndActiveEffect : ActiveEffectBase
{
    public LightningSpeedEndActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AkioConstants.LightningSpeedEndActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Description = $"- {AkioConstants.LightningSpeed.HighlightInGold()} has ended\n";
        Duration = Duration1;
        TimeLeft = Duration1;
        NoTick = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return $"{Description} {"ENDS THIS TURN".HighlightInGold()}";
    }

    public override void EndTurnChecks()
    {
        Target.ActiveEffectController.RemoveActiveEffect(this);
    }
}
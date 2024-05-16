using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Hrom.Champion;

namespace Ashcrown.Remake.Core.Champions.Hrom.ActiveEffects;

public class LightningStrikeHelperActiveEffect : ActiveEffectBase
{
    public LightningStrikeHelperActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(HromConstants.LightningStrikeHelperActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Description = $"- {HromConstants.LightningStorm.HighlightInGold()} may be used this turn\n";
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
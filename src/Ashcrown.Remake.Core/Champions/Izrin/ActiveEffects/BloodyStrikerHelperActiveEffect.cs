using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Izrin.Champion;

namespace Ashcrown.Remake.Core.Champions.Izrin.ActiveEffects;

public class BloodyStrikerHelperActiveEffect : ActiveEffectBase
{
    public BloodyStrikerHelperActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(IzrinConstants.BloodyStrikeHelperActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        BonusDamage1 = originAbility.BonusDamage1;
        
        Description = $"- {IzrinConstants.BloodyStrike.HighlightInGold()} deals an additional {$"{BonusDamage1} physical damage".HighlightInOrange()} this turn\n";
        Duration = Duration1;
        TimeLeft = Duration1;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
        NoTick = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return Description + "ENDS THIS TURN".HighlightInGold();
    }

    public override void EndTurnChecks()
    {
        Target.ActiveEffectController.RemoveActiveEffect(this);
    }
}
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Luther.Champion;

namespace Ashcrown.Remake.Core.Champions.Luther.ActiveEffects;

public class FieryBrandEndActiveEffect : ActiveEffectBase
{
    public FieryBrandEndActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(LutherConstants.FieryBrandEndActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Description = $"- {LutherConstants.FieryBrand.HighlightInGold()} has ended\n";
        Duration = Duration1;
        TimeLeft = Duration1;
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
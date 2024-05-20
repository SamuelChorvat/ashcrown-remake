using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Luther.Champion;

namespace Ashcrown.Remake.Core.Champions.Luther.ActiveEffects;

public class FieryBrandTargetActiveEffect : ActiveEffectBase
{
    public FieryBrandTargetActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(LutherConstants.FieryBrandTargetActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1 + 1;
        
        Description = $"- First new Strategic ability used by this champion will be {"countered".HighlightInPurple()}\n"
                           + $"- This ability is {"invisible".HighlightInPurple()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        Hidden = true;
    }

    public override void OnRemove()
    {
        OriginAbility.Owner.ChampionController.DealActiveEffect(Target, OriginAbility, 
            new FieryBrandEndActiveEffect(OriginAbility, Target), true, new AppliedAdditionalLogic());
    }

    public override bool CounterOnMe(IAbility ability)
    {
        if (!ability.AbilityClassesContains(AbilityClass.Strategic)) return false;
        if (OriginAbility.Owner.AbilityController.GetMyAbilityByName(LutherConstants.FieryBrand) != null) {
            OriginAbility.Owner.ChampionController.DealActiveEffect(Target, OriginAbility,
                new FieryBrandCounterActiveEffect(OriginAbility, Target, ability.Name), true, new AppliedAdditionalLogic());
        }

        Target.ActiveEffectController.RemoveActiveEffect(this);

        return true;
    }
}
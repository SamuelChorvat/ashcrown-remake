using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Gruber.Champion;

namespace Ashcrown.Remake.Core.Champions.Gruber.ActiveEffects;

public class PoisonInjectionPartTwoActiveEffect : ActiveEffectBase
{
    public PoisonInjectionPartTwoActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(GruberConstants.PoisonInjectionPartTwoActiveEffect, originAbility, championTarget)
    {
        if(Target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GruberConstants.PoisonInjectionStacksActiveEffect)) {
            Duration1 = originAbility.Duration1 + Target.ActiveEffectController
                .GetActiveEffectByName(GruberConstants.PoisonInjectionStacksActiveEffect)!.Stacks;
        } else {
            Duration1 = originAbility.Duration1;
        }
        
        Description = $"- This champion is {"stunned".HighlightInPurple()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Stun = originAbility.Stun;
        StunType = originAbility.StunType;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
    }

    public override void OnRemove()
    {
        OriginAbility.Owner.ChampionController.DealActiveEffect(Target, OriginAbility, 
            new PoisonInjectionStacksActiveEffect(OriginAbility, Target), true, new AppliedAdditionalLogic());
    }
}
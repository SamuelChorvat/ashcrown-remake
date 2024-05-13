using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Gruber.Champion;

namespace Ashcrown.Remake.Core.Champions.Gruber.ActiveEffects;

public class PoisonInjectionPartOneActiveEffect : ActiveEffectBase
{
    public PoisonInjectionPartOneActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(GruberConstants.PoisonInjectionPartOneActiveEffect, originAbility, championTarget)
    {
        Duration1 = 2;
        Damage1 = originAbility.Damage2;
        
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        AfflictionDamage = true;
        Unique = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        var stacks = 0;
        if (Target.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(GruberConstants.PoisonInjectionStacksActiveEffect)) {
            stacks = Target.ActiveEffectController
                .GetActiveEffectByName(GruberConstants.PoisonInjectionStacksActiveEffect)!.Stacks;
        }
		var turns = stacks > 0 ? "turns" : "turn";
        return $"- This champion will be {"stunned".HighlightInPurple()} for {1 + stacks} {turns} and will take {$"{Damage1} affliction damage".HighlightInRed()} when this ends"
               + GetTimeLeftAffix();
    }

    public override void OnRemove()
    {
        if (!RemoveIt) return;
        Target.ChampionController.ReceiveActiveEffectDamage(Damage1, this, new AppliedAdditionalLogic());
        OriginAbility.Owner.ChampionController.DealActiveEffect(Target, OriginAbility, 
            new PoisonInjectionPartTwoActiveEffect(OriginAbility, Target), true, new AppliedAdditionalLogic());
    }

    public override bool ActiveEffectChecks()
    {
        return !Target.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(GruberConstants.PoisonInjectionPartTwoActiveEffect);
    }
}
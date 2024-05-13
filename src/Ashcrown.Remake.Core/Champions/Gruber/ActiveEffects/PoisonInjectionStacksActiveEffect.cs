using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Gruber.Champion;

namespace Ashcrown.Remake.Core.Champions.Gruber.ActiveEffects;

public class PoisonInjectionStacksActiveEffect : ActiveEffectBase
{
    public PoisonInjectionStacksActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(GruberConstants.PoisonInjectionStacksActiveEffect, originAbility, championTarget)
    {
        Description = "override";
        Duration = Duration1;
        TimeLeft = Duration1;
        Infinite = true;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        Stackable = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        var turn = Stacks > 1 ? "turns" : "turn";
        return $"- {GruberConstants.PoisonInjection.HighlightInGold()} will {"stun".HighlightInPurple()} this champion " +
               $"for an additional {Stacks} {turn}"
               + GetTimeLeftAffix();
    }
}
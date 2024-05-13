using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Gruber.Champion;

namespace Ashcrown.Remake.Core.Champions.Gruber.ActiveEffects;

public class AdaptiveVirusAllyActiveEffect : ActiveEffectBase
{
    public AdaptiveVirusAllyActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(GruberConstants.AdaptiveVirusAllyActiveEffect, originAbility, championTarget)
    {
        DestructibleDefense1 = originAbility.DestructibleDefense1;
        Duration1 = originAbility.Duration1;
        
        DestructibleDefense = originAbility.DestructibleDefense1;
        Description = "override";
        Duration = Duration1;
        TimeLeft = Duration1;
        Infinite = true;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
        Stackable = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return $"- This champion has {$"{DestructibleDefense} points of destructible defense".HighlightInYellow()}" 
               + GetTimeLeftAffix();
    }

    public override void AddStack(IActiveEffect activeEffect)
    {
        DestructibleDefenseStack(activeEffect);
    }

    public override void RemoveDestructibleDefense(IAbility? ability = null, IActiveEffect? activeEffect = null)
    {
        Target.ActiveEffectController.RemoveActiveEffect(this);
    }
}
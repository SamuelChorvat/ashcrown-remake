using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cedric.Champion;

namespace Ashcrown.Remake.Core.Champions.Cedric.ActiveEffects;

public class MirrorImageActiveEffect : ActiveEffectBase
{
    public MirrorImageActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(CedricConstants.MirrorImageActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        DestructibleDefense1 = originAbility.DestructibleDefense1;
        
        Duration = Duration1;
        TimeLeft = Duration1;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
        Infinite = true;
        DestructibleDefense = DestructibleDefense1;
        Stackable = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        var desc = "";
		
        desc = Stacks > 1 ? $"- This champion's abilities will last an additional {1*Stacks} turns" 
            : "- This champion's abilities will last an additional turn";
		
        if (DestructibleDefense > 0 ) {
            desc += $"\n- This champion has {$"{DestructibleDefense} points of destructible defense".HighlightInYellow()}";
        } 
		
        return desc + GetTimeLeftAffix();
    }

    public override void AddStack(IActiveEffect activeEffect)
    {
        StandardStack(activeEffect);
        DestructibleDefenseStack(activeEffect);
    }
}
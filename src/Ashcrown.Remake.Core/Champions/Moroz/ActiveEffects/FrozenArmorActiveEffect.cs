using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Moroz.Champion;

namespace Ashcrown.Remake.Core.Champions.Moroz.ActiveEffects;

public class FrozenArmorActiveEffect : ActiveEffectBase
{
    public FrozenArmorActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(MorozConstants.FrozenArmorActiveEffect, originAbility, championTarget)
    {
		
        DestructibleDefense1 = originAbility.DestructibleDefense1;
        Duration1 = originAbility.Duration1;
        
        DestructibleDefense = originAbility.DestructibleDefense1;
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

    public override void DestructibleDefenseStack(IActiveEffect activeEffect)
    {
        DestructibleDefense = activeEffect.DestructibleDefense1;
    }
}
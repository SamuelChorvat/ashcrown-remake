using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cleo.Champion;

namespace Ashcrown.Remake.Core.Champions.Cleo.ActiveEffects;

public class HealingSalveActiveEffect : ActiveEffectBase
{
    public HealingSalveActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(CleoConstants.HealingSalveActiveEffect, originAbility, championTarget)
    {
        DestructibleDefense1 = originAbility.DestructibleDefense1;
        
        DestructibleDefense = DestructibleDefense1;
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
        DestructibleDefenseStack(activeEffect);;
    }

    public override void RemoveDestructibleDefense(IAbility? ability = null, IActiveEffect? activeEffect = null)
    {
        Target.ActiveEffectController.RemoveActiveEffect(this);
    }
}
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Jafali.Champion;

namespace Ashcrown.Remake.Core.Champions.Jafali.ActiveEffects;

public class AngerActiveEffect : ActiveEffectBase
{
    public AngerActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(JafaliConstants.AngerActiveEffect, originAbility, championTarget)
    {
        DestructibleDefense1 = originAbility.DestructibleDefense1;
        Duration1 = originAbility.Duration1;
        
        DestructibleDefense = originAbility.DestructibleDefense1;
        Duration = Duration1;
        TimeLeft = Duration1;
        Infinite = true;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return $"- This champion has {$"{DestructibleDefense} points of destructible defense".HighlightInYellow()}\n"
               + $"- Enemy that destroys the destructible defense will have {JafaliConstants.DecayingSoul.HighlightInGold()} applied to them"
               + GetTimeLeftAffix();
    }

    public override void RemoveDestructibleDefense(IAbility? ability = null, IActiveEffect? activeEffect = null)
    {
        if (ability != null) {
            OriginAbility.Owner.ChampionController.DealActiveEffect(ability.Owner, OriginAbility,
                new DecayingSoulActiveEffect(OriginAbility, ability.Owner), true, new AppliedAdditionalLogic());
        } else if (activeEffect != null) {
            OriginAbility.Owner.ChampionController.DealActiveEffect(activeEffect.OriginAbility.Owner, OriginAbility,
                new DecayingSoulActiveEffect(OriginAbility, activeEffect.OriginAbility.Owner), 
                true, new AppliedAdditionalLogic());
        }
        Target.ActiveEffectController.RemoveActiveEffect(this);
    }

    public override void OnAdd()
    {
        if (Target.AbilityController.GetMyAbilityByName(JafaliConstants.DecayingSoul) != null) {
            Target.CurrentAbilities[0] = Target.AbilityController.GetMyAbilityByName(JafaliConstants.DecayingSoul)!;
        }
    }

    public override void OnRemove()
    {
        if (Target.AbilityController.GetMyAbilityByName(JafaliConstants.Anger) != null) {
            Target.CurrentAbilities[0] = Target.AbilityController.GetMyAbilityByName(JafaliConstants.Anger)!;
        }
    }
}
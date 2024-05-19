using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Lexi.Champion;

namespace Ashcrown.Remake.Core.Champions.Lexi.ActiveEffects;

public class TranquilityActiveEffect : ActiveEffectBase
{
    public TranquilityActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(LexiConstants.TranquilityActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Duration = originAbility.Duration1;
        TimeLeft = originAbility.Duration1;
        Infinite = true;
        Stacks = 3;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
        CannotBeRemoved = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return $"- {Target.Name} has {Stacks} {LexiConstants.Tranquility.HighlightInGold()} stacks{GetTimeLeftAffix()}";
    }

    public override void OnApply()
    {
        if (Stacks <= 0) {
            RemoveIt = true;
        }
    }

    public override void OnAdd()
    {
        if (Target.AbilityController.GetMyAbilityByName(LexiConstants.Nourish) != null) {
            Target.AbilityController.GetMyAbilityByName(LexiConstants.Nourish)!.OriginalCost = [0,0,0,0,1];
            Target.AbilityController.GetMyAbilityByName(LexiConstants.Nourish)!.OriginalCooldown = 0;
        }

        if (Target.AbilityController.GetMyAbilityByName(LexiConstants.Tranquility) != null) {
            Target.AbilityController.GetMyAbilityByName(LexiConstants.Tranquility)!.OriginalCost = [0,0,0,0,0];
        }
    }

    public override void OnRemove()
    {
        if (Target.AbilityController.GetMyAbilityByName(LexiConstants.Nourish) != null 
            && Target.ActiveEffectController.GetActiveEffectCountByName(LexiConstants.TranquilityActiveEffect) == 1) {
            Target.AbilityController.GetMyAbilityByName(LexiConstants.Nourish)!.OriginalCost = [1,0,0,0,0];
            Target.AbilityController.GetMyAbilityByName(LexiConstants.Nourish)!.OriginalCooldown = 1;
        }

        if (Target.AbilityController.GetMyAbilityByName(LexiConstants.Tranquility) != null 
            && Target.ActiveEffectController.GetActiveEffectCountByName(LexiConstants.TranquilityActiveEffect) == 1) {
            Target.AbilityController.GetMyAbilityByName(LexiConstants.Tranquility)!.OriginalCost = [0,0,0,0,1];
        }
    }

    public override bool CustomDealActiveEffectLogic(IChampion dealer, IChampion target, IAbility ability,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (!target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(LexiConstants.TranquilityActiveEffect))
            return false;
        dealer.ChampionController.DealAbilityHealing(ability.Heal1, target, ability, appliedAdditionalLogic);
        return true;
    }

    public override void EndTurnChecks()
    {
        if (Stacks <= 0) {
            Target.ActiveEffectController.RemoveActiveEffect(this);
        }
    }
}
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Lucifer.Champion;

namespace Ashcrown.Remake.Core.Champions.Lucifer.ActiveEffects;

public class DarkChaliceActiveEffect : ActiveEffectBase
{
    public DarkChaliceActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(LuciferConstants.DarkChaliceActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Damage1 = originAbility.Damage1;
        
        Description = $"- {LuciferConstants.CursedCrow.HighlightInGold()} can be used\n"
                           + $"- This ability is {"invisible".HighlightInPurple()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Buff = originAbility.Debuff;
        Helpful = originAbility.Helpful;
        Damaging = originAbility.Damaging;
        MagicDamage = originAbility.MagicDamage;
        Hidden = true;
        Infinite = true;
    }

    public override void OnAdd()
    {
        if (Target.AbilityController.GetMyAbilityByName(LuciferConstants.CursedCrow) != null) {
            Target.CurrentAbilities[2] = Target.AbilityController.GetMyAbilityByName(LuciferConstants.CursedCrow)!;
        }
    }

    public override void OnRemove()
    {
        if (Target.ActiveEffectController.GetActiveEffectCountByName(LuciferConstants.DarkChaliceActiveEffect) !=
            1) return;
        if (Target.AbilityController.GetMyAbilityByName(LuciferConstants.DarkChalice) != null) {
            Target.CurrentAbilities[2] = Target.AbilityController.GetMyAbilityByName(LuciferConstants.DarkChalice)!;
        }
    }

    public override int ReceiveActiveEffectDamageModifier(IChampion target, int amount)
    {
        var newAmount = amount;
        if (target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(LuciferConstants.CursedCrowActiveEffect)) {
            newAmount += target.ActiveEffectController.GetActiveEffectByName(LuciferConstants.CursedCrowActiveEffect)!.Damage1 
                         * target.ActiveEffectController.GetActiveEffectByName(LuciferConstants.CursedCrowActiveEffect)!.Stacks;
        }
        return newAmount;
    }

    //TODO Refactor this
    public static void Trigger(IChampion victim)
    {
        if (!victim.ActiveEffectController.ActiveEffectPresentByActiveEffectName(LuciferConstants
                .DarkChaliceActiveEffect)) return;
        var chaliceAe = victim.ActiveEffectController.GetActiveEffectByName(LuciferConstants.DarkChaliceActiveEffect)!;
        var toDeal = chaliceAe.Damage1;
        for (var i = 0; i < 3; i++) {
            victim.BattlePlayer.GetEnemyPlayer().Champions[i].ChampionController.ReceiveActiveEffectDamage(toDeal, 
                chaliceAe, new AppliedAdditionalLogic());
        }
    }
}
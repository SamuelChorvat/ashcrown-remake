using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Branley.Champion;

namespace Ashcrown.Remake.Core.Champions.Branley.ActiveEffects;

public class RaiseTheFlagTargetActiveEffect : ActiveEffectBase
{
    public RaiseTheFlagTargetActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(BranleyConstants.RaiseTheFlagTargetActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1 + 1;
        Description = "- This champion's Physical and Strategic abilities cost an additional <sprite=4>";
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
    }

    public override void OnAdd()
    {
        for (var i = 0; i < 4; i++) {
            for (var j = 0; j < Target.Abilities[i].Count; j++) {
                if (Target.Abilities[i][j].AbilityClassesContains(AbilityClass.Physical) 
                    || Target.Abilities[i][j].AbilityClassesContains(AbilityClass.Strategic)) {
                    Target.Abilities[i][j].RandomCostModifier = 1;
                }
            }
        }
    }

    public override void OnRemove()
    {
        for (var i = 0; i < 4; i++) {
            for (var j = 0; j < Target.Abilities[i].Count; j++) {
                if (Target.Abilities[i][j].AbilityClassesContains(AbilityClass.Physical) 
                    || Target.Abilities[i][j].AbilityClassesContains(AbilityClass.Strategic)) {
                    Target.Abilities[i][j].RemoveCostModifier(-1);
                }
            }
        }
    }

    public override void AdditionalDealActiveEffectLogic(IChampion dealer, IChampion target, IAbility ability, bool secondary,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        dealer.ChampionController.EnemyDebuffMyBuff(this, ability, BranleyConstants.Name,
            BranleyConstants.RaiseTheFlagTargetActiveEffect, 
            BranleyConstants.RaiseTheFlagMeActiveEffect, appliedAdditionalLogic);
    }
}
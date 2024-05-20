using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Luther.Champion;

namespace Ashcrown.Remake.Core.Champions.Luther.ActiveEffects;

public class FlamestrikeTargetActiveEffect : ActiveEffectBase
{
    public FlamestrikeTargetActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(LutherConstants.FlamestrikeTargetActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Damage1 = originAbility.Damage1;
        
        Description = $"- This champion will receive {$"{Damage1} physical damage".HighlightInOrange()}\n"
                           + "- This champion's Strategic ability costs are increased by <sprite=4>";
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        Damaging = originAbility.Damaging;
        PhysicalDamage = originAbility.PhysicalDamage;
		
        EndsOnTargetDeath = true;
        EndsOnCasterDeath = true;
        PauseOnCasterStun = true;
        PauseOnTargetInvulnerability = true;
    }

    public override void OnApply()
    {
        OnApplyActionControlTargetDamage();
    }

    public override void OnAdd()
    {
        for (var i = 0; i < 4; i++) {
            for (var j = 0; j < Target.Abilities[i].Count; j++) {
                if (Target.Abilities[i][j].AbilityClassesContains(AbilityClass.Strategic)) {
                    Target.Abilities[i][j].RandomCostModifier = 1;
                }
            }
        }
    }

    public override void OnRemove()
    {
        for (var i = 0; i < 4; i++) {
            for (var j = 0; j < Target.Abilities[i].Count; j++) {
                if (Target.Abilities[i][j].AbilityClassesContains(AbilityClass.Strategic)) {
                    Target.Abilities[i][j].RemoveCostModifier(-1);
                }
            }
        }
    }

    public override void AdditionalDealActiveEffectLogic(IChampion dealer, IChampion target, IAbility ability, bool secondary,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(LutherConstants.FieryBrandCounterActiveEffect)) {
            TimeLeft += target.ActiveEffectController
                .GetActiveEffectCountByName(LutherConstants.FieryBrandCounterActiveEffect) * 2;
        }
    }
}
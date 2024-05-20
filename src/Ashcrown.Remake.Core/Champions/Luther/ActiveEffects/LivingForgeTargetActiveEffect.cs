using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Luther.Champion;

namespace Ashcrown.Remake.Core.Champions.Luther.ActiveEffects;

public class LivingForgeTargetActiveEffect : ActiveEffectBase
{
    public LivingForgeTargetActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(LutherConstants.LivingForgeTargetActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1 + 1;
        
        Description = $"- Any time this champion uses a new Strategic ability, {originAbility.Owner.Name}'s damage will be increased by {originAbility.DealDamageIncreasePoint1} for 1 turn";
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
    }

    public override void AdditionalDealActiveEffectLogic(IChampion dealer, IChampion target, IAbility ability, bool secondary,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        dealer.ChampionController.EnemyDebuffMyBuff(this, ability, LutherConstants.Name,
            LutherConstants.LivingForgeTargetActiveEffect, LutherConstants.LivingForgeMeActiveEffect, appliedAdditionalLogic);
    }

    public override void EndTurnChecks()
    {
        if (!Target.AbilityController.UsedNewAbility) return;
        if (Target.AbilityController.LastUsedAbility!.AbilityClassesContains(AbilityClass.Strategic)) {
            OriginAbility.Owner.ChampionController.DealActiveEffect(OriginAbility.Owner, OriginAbility, new LivingForgeBuffActiveEffect(OriginAbility, OriginAbility.Owner), 
                true, new AppliedAdditionalLogic());
        }
    }
}
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Aniel.Champion;

namespace Ashcrown.Remake.Core.Champions.Aniel.ActiveEffects;

public class EnchantedGarlicBombUsedTargetCantActiveEffect : ActiveEffectBase
{
    public EnchantedGarlicBombUsedTargetCantActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AnielConstants.EnchantedGarlicBombUsedTargetCantActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1 + 1;
        Duration = Duration1;
        TimeLeft = Duration1;
        Description = $"- This champion cannot {"reduce damage".HighlightInPurple()} or become {"invulnerable".HighlightInPurple()}";
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        DisableDamageReceiveReduction = originAbility.DisableDamageReceiveReduction;
        DisableInvulnerability = originAbility.DisableInvulnerability;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return Duration - TimeLeft <= 1 
            ? $"- {AnielConstants.BladeOfGluttony.HighlightInGold()} will make this champion take 15 more damage from {"physical damage".HighlightInOrange()}{GetTimeLeftAffix(1)}\n{Description}{GetTimeLeftAffix()}" 
            : $"{Description}{GetTimeLeftAffix()}";
    }

    public override void AdditionalDealActiveEffectLogic(IChampion dealer, IChampion target, IAbility ability, bool secondary, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        dealer.ChampionController.DealActiveEffect(dealer,
            ability, new EnchantedGarlicBombUsedMeActiveEffect(ability, dealer), true, appliedAdditionalLogic);
    }

    public override void AdditionalReceiveActiveEffectLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AnielConstants.CondemnUsedActiveEffect))
        {
            OriginAbility.Owner.ChampionController.DealAbilityDamage(
                OriginAbility.Damage1 * target.ActiveEffectController.GetActiveEffectCountByName(AnielConstants.CondemnUsedActiveEffect),
                target, OriginAbility, true, appliedAdditionalLogic);
        }

        if (target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AnielConstants.BladeOfGluttonyUsedActiveEffect) ||
            target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AnielConstants.BladeOfGluttonyUsedMagicActiveEffect) ||
            target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AnielConstants.BladeOfGluttonyUsedPhysicalActiveEffect))
        {
            OriginAbility.Owner.ChampionController.DealActiveEffect(target,
                OriginAbility, new EnchantedGarlicBombUsedTargetStunActiveEffect(OriginAbility, target), true, appliedAdditionalLogic);
        }
    }
}
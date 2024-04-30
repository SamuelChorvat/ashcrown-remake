using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Sarfu.Champion;

namespace Ashcrown.Remake.Core.Champions.Sarfu.ActiveEffects;

public class DuelTargetActiveEffect : ActiveEffectBase
{
    public DuelTargetActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(SarfuConstants.DuelTargetActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Description =
            $"- This champion cannot {"reduce damage".HighlightInPurple()} or become {"invulnerable".HighlightInPurple()}"
            + $"\n- {SarfuConstants.Overpower.HighlightInGold()} and {SarfuConstants.Charge.HighlightInGold()} will deal bonus damage to this champion"
            + $"\n- If {originAbility.Owner.Name} dies this ability ends";
        Duration = Duration1;
        TimeLeft = Duration1;
        DisableDamageReceiveReduction = originAbility.DisableDamageReceiveReduction;
        DisableInvulnerability = originAbility.DisableInvulnerability;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        EndsOnCasterDeath = true;
    }

    public override void AdditionalDealActiveEffectLogic(IChampion dealer, IChampion target, IAbility ability, bool secondary,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        dealer.ChampionController.DealActiveEffect(dealer, ability, 
            new DuelMeActiveEffect(ability, dealer), true, appliedAdditionalLogic);
    }
}
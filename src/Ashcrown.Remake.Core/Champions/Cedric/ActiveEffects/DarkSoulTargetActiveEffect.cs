using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cedric.Champion;

namespace Ashcrown.Remake.Core.Champions.Cedric.ActiveEffects;

public class DarkSoulTargetActiveEffect : ActiveEffectBase
{
    public DarkSoulTargetActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(CedricConstants.DarkSoulTargetActiveEffect, originAbility, championTarget)
    {
       
		
        Duration1 = originAbility.Duration1;
        Damage1 = originAbility.Damage1;
        
        Description = $"- This champion will receive {$"{Damage1} magic damage".HighlightInBlue()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        Damaging = originAbility.Damaging;
        MagicDamage = originAbility.MagicDamage;
		
        EndsOnTargetDeath = true;//TODO should be here or only in Me part
        EndsOnCasterDeath = true;
        PauseOnCasterStun = true;
        PauseOnTargetInvulnerability = true;
    }

    public override void OnApply()
    {
        OnApplyActionControlTargetDamage();
    }

    public override void AdditionalDealActiveEffectLogic(IChampion dealer, IChampion target, IAbility ability, bool secondary,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (dealer.ActiveEffectController.ActiveEffectPresentByActiveEffectName(CedricConstants.MirrorImageActiveEffect)) {
            TimeLeft += dealer.ActiveEffectController.GetActiveEffectByName(CedricConstants.MirrorImageActiveEffect)!.Stacks;
        }
    }

    public override void AdditionalProcessDeathLogic()
    {
        if (!OriginAbility.Owner.Alive) return;
        OriginAbility.Owner.ChampionController.ReceiveAbilityHealing(OriginAbility.Heal1, 
            OriginAbility, new AppliedAdditionalLogic());

        var soulKillAe = new DarkSoulKillActiveEffect(OriginAbility, OriginAbility.Owner);
        soulKillAe.OriginAbility.Owner.ChampionController.DealActiveEffect(soulKillAe.OriginAbility.Owner,
            soulKillAe.OriginAbility, soulKillAe, true, new AppliedAdditionalLogic());
    }
}
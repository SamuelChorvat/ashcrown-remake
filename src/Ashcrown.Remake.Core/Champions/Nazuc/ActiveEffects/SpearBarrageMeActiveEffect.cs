using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Nazuc.Champion;

namespace Ashcrown.Remake.Core.Champions.Nazuc.ActiveEffects;

public class SpearBarrageMeActiveEffect : ActiveEffectBase
{
    public SpearBarrageMeActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(NazucConstants.SpearBarrageMeActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Duration = Duration1;
        TimeLeft = Duration1;
        Source = true;
		
        EndsOnTargetDeath = true;
        EndsOnCasterDeath = true;
        PauseOnCasterStun = true;
        PauseOnTargetInvulnerability = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return GetActionControlDescription();
    }

    public override void OnApply()
    {
        OnApplyActionControlMe();
    }

    public override void AdditionalDealActiveEffectLogic(IChampion dealer, IChampion target, IAbility ability, bool secondary,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        dealer.ChampionController.DealActiveEffect(dealer, OriginAbility,
            new SpearBarrageBuffActiveEffect(OriginAbility, dealer), true, appliedAdditionalLogic);
    }
}
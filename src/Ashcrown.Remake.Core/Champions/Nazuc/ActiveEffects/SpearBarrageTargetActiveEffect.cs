using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Nazuc.Champion;

namespace Ashcrown.Remake.Core.Champions.Nazuc.ActiveEffects;

public class SpearBarrageTargetActiveEffect : ActiveEffectBase
{
    public SpearBarrageTargetActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(NazucConstants.SpearBarrageTargetActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Damage1 = originAbility.Damage1;
        BonusDamage1 = originAbility.BonusDamage1;
        
        Description = $"- This champion will receive {$"{Damage1} physical damage".HighlightInOrange()}";
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
        if (!Paused) {
            if (Target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(NazucConstants.HuntersMarkActiveEffect)) {
                OriginAbility.Owner.ChampionController.DealActiveEffectDamage(Damage1 + BonusDamage1, Target, this, new AppliedAdditionalLogic());
            } else {
                OriginAbility.Owner.ChampionController.DealActiveEffectDamage(Damage1, Target, this, new AppliedAdditionalLogic());
            }	
        }
		
        TickDown();
		
        if (CasterLink!.TimeLeft != TimeLeft) {
            CasterLink.TimeLeft = TimeLeft;
        }
    }
}
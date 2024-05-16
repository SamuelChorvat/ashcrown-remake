using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Garr.Champion;

namespace Ashcrown.Remake.Core.Champions.Garr.ActiveEffects;

public class BarbedChainTargetActiveEffect : ActiveEffectBase
{
    public BarbedChainTargetActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(GarrConstants.BarbedChainTargetActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Damage1 = originAbility.Damage1;
        
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
            var toDeal = Damage1;
			
            if (OriginAbility.Owner.ActiveEffectController
                .ActiveEffectPresentByActiveEffectName(GarrConstants.RecklessnessActiveEffect)) {
                toDeal += OriginAbility.Owner.ActiveEffectController
                    .GetActiveEffectByName(GarrConstants.RecklessnessActiveEffect)!.Stacks * 10;
            }
			
            OriginAbility.Owner.ChampionController.DealActiveEffectDamage(toDeal, Target, this, 
                new AppliedAdditionalLogic());
        }
		
        TickDown();
				
        if (CasterLink!.TimeLeft != TimeLeft) {
            CasterLink.TimeLeft = TimeLeft;
        }
    }
}
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Dura.Champion;

namespace Ashcrown.Remake.Core.Champions.Dura.ActiveEffects;

public class SonicWavesTargetActiveEffect : ActiveEffectBase
{
    public SonicWavesTargetActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(DuraConstants.SonicWavesTargetActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Damage1 = originAbility.Damage1;
        DealDamageReductionPoint1 = originAbility.DealDamageReductionPoint1;
        
        Description = $"- This champion will receive {$"{Damage1} physical damage".HighlightInOrange()}\n"
                           + $"- This champion's non-Affliction abilities will deal {DealDamageReductionPoint1} less damage";
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        Damaging = originAbility.Damaging;
        PhysicalDamage = originAbility.MagicDamage;
        AllDamageDealModifier = new PointsPercentageModifier(-DealDamageReductionPoint1);
		
        EndsOnTargetDeath = true;
        PauseOnCasterStun = true;
        EndsOnCasterDeath = true;
        PauseOnTargetInvulnerability = true;
    }

    public override void OnApply()
    {
        OnApplyActionControlTargetDamage();
    }
}
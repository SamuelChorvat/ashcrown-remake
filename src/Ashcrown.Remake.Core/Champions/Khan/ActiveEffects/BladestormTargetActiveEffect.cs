using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Khan.Champion;

namespace Ashcrown.Remake.Core.Champions.Khan.ActiveEffects;

public class BladestormTargetActiveEffect : ActiveEffectBase
{
    public BladestormTargetActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(KhanConstants.BladestormTargetActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Damage1 = originAbility.Damage1;
        
        Description = $"- This champion will receive {$"{Damage1} physical damage".HighlightInOrange()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Debuff = originAbility.Debuff;
        Harmful = originAbility.Harmful;
        Damaging = originAbility.Damaging;
        PhysicalDamage = originAbility.PhysicalDamage;
		
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
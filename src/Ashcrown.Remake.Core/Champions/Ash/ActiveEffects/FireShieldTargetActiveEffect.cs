using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Ash.Champion;

namespace Ashcrown.Remake.Core.Champions.Ash.ActiveEffects;

public class FireShieldTargetActiveEffect : ActiveEffectBase
{
    public FireShieldTargetActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AshConstants.FireShieldTargetActiveEffect, originAbility, championTarget)
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
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Gwen.Champion;

namespace Ashcrown.Remake.Core.Champions.Gwen.ActiveEffects;

public class CharmTargetActiveEffect : ActiveEffectBase
{
    public CharmTargetActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(GwenConstants.CharmTargetActiveEffect, originAbility, championTarget)
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
        EndsOnCasterDeath = true;
        PauseOnCasterStun = true;
        PauseOnTargetInvulnerability = true;
    }

    public override void OnApply()
    {
        OnApplyActionControlTargetDamage();
    }
}
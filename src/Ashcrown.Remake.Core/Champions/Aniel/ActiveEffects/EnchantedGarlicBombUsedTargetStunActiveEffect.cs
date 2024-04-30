using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Aniel.Champion;

namespace Ashcrown.Remake.Core.Champions.Aniel.ActiveEffects;

public class EnchantedGarlicBombUsedTargetStunActiveEffect : ActiveEffectBase
{
    public EnchantedGarlicBombUsedTargetStunActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AnielConstants.EnchantedGarlicBombUsedTargetStunActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration2 + 1;
        Duration = Duration1;
        TimeLeft = Duration1;
        Description = $"- This champion is {"stunned".HighlightInPurple()}";
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        Stun = originAbility.Stun;
        StunType = originAbility.StunType;
    }
}
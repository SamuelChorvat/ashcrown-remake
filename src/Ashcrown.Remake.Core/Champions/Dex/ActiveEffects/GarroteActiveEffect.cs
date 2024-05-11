using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Dex.Champion;

namespace Ashcrown.Remake.Core.Champions.Dex.ActiveEffects;

public class GarroteActiveEffect : ActiveEffectBase
{
    public GarroteActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(DexConstants.GarroteActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Description = $"- This champion's Physical and Strategic abilities are {"stunned".HighlightInPurple()}";
        Duration = Duration1 + 1;
        TimeLeft = Duration1 + 1;
        Stun = originAbility.Stun;
        StunType = originAbility.StunType;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
    }
}
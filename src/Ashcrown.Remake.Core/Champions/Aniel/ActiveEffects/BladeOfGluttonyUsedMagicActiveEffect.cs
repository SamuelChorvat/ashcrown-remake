using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Aniel.Champion;

namespace Ashcrown.Remake.Core.Champions.Aniel.ActiveEffects;

public class BladeOfGluttonyUsedMagicActiveEffect : ActiveEffectBase
{
    public BladeOfGluttonyUsedMagicActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AnielConstants.BladeOfGluttonyUsedMagicActiveEffect, originAbility, championTarget)
    {
        ReceiveDamageIncreasePoint1 = originAbility.ReceiveDamageIncreasePoint1;
        Duration1 = originAbility.Duration1 + 1;
        
        Description = 
            $"- {AnielConstants.Condemn.HighlightInGold()} will deal an additional {"25 physical damage".HighlightInOrange()} to this champion\n"
            + $"- {AnielConstants.EnchantedGarlicBomb.HighlightInGold()} will also {"stun".HighlightInPurple()} this champion for 1 turn\n"
            + $"- This champion will take {ReceiveDamageIncreasePoint1} more damage from {"magic damage".HighlightInBlue()}";
        
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = true;
        Debuff = true;
        MagicDamageReceiveModifier = new PointsPercentageModifier(ReceiveDamageIncreasePoint1);
    }
}
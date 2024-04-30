using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Aniel.Champion;

namespace Ashcrown.Remake.Core.Champions.Aniel.ActiveEffects;

public class BladeOfGluttonyUsedActiveEffect : ActiveEffectBase
{
    public BladeOfGluttonyUsedActiveEffect(IAbility originAbility, IChampion championTarget)
        : base(AnielConstants.BladeOfGluttonyUsedActiveEffect, originAbility, championTarget)
    {
        Description = $"- {AnielConstants.Condemn.HighlightInGold()} will deal an additional " +
                      $"{"25 physical damage".HighlightInOrange()} to this champion\n" +
                      $"- {AnielConstants.EnchantedGarlicBomb.HighlightInGold()} will also " +
                      $"{"stun".HighlightInPurple()} this champion for 1 turn";
        Duration = 2;
        TimeLeft = 2;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
    }
}
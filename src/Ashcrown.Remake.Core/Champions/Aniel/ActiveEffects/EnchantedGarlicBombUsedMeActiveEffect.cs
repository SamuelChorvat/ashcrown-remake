using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Aniel.Champion;

namespace Ashcrown.Remake.Core.Champions.Aniel.ActiveEffects;

public class EnchantedGarlicBombUsedMeActiveEffect : ActiveEffectBase
{
    public EnchantedGarlicBombUsedMeActiveEffect(IAbility originAbility, IChampion championTarget)
        : base(AnielConstants.EnchantedGarlicBombUsedMeActiveEffect, originAbility, championTarget)
    {
        Duration = 2;
        TimeLeft = 2;
        Description =
            $"- {AnielConstants.Condemn.HighlightInGold()} will make this champion {"invulnerable".HighlightInPurple()} for 1 turn";
        Buff = true;
        Helpful = true;
    }
}
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Aniel.Champion;

namespace Ashcrown.Remake.Core.Champions.Aniel.ActiveEffects;

public class CondemnUsedActiveEffect : ActiveEffectBase
{
    public CondemnUsedActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AnielConstants.CondemnUsedActiveEffect,
            originAbility,
            championTarget)
    {
        Description = $"- {AnielConstants.EnchantedGarlicBomb.HighlightInGold()} will deal {"10 magic damage".HighlightInBlue()} to this champion\n" +
                      $"- {AnielConstants.BladeOfGluttony.HighlightInGold()} will make this champion take 15 more damage from {"magic damage".HighlightInBlue()}";
        Duration = 2;
        TimeLeft = 2;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
    }
}
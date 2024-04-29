using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Abstract;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Akio.Champion;

namespace Ashcrown.Remake.Core.Champions.Akio.ActiveEffects;

public class DragonRageActiveEffect : ActiveEffectBase
{
    public DragonRageActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AkioConstants.DragonRageActiveEffect, originAbility, championTarget)
    {
        BonusDamage1 = originAbility.BonusDamage1;
        Duration1 = originAbility.Duration1;
        Duration = Duration1;
        TimeLeft = Duration1;
        Infinite = true;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
        Stackable = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return $"- {AkioConstants.DragonRage.HighlightInGold()} will deal an additional " +
               $"{$"{Stacks*BonusDamage1} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()}"
               + GetTimeLeftAffix();
    }
}
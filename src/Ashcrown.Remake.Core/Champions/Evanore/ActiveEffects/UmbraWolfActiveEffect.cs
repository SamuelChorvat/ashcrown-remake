using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Evanore.Champion;

namespace Ashcrown.Remake.Core.Champions.Evanore.ActiveEffects;

public class UmbraWolfActiveEffect : ActiveEffectBase
{
    public UmbraWolfActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(EvanoreConstants.UmbraWolfActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        ReceiveDamageReductionPercent1 = originAbility.ReceiveDamageReductionPercent1;
        
        Description = $"- This champion has {$"{ReceiveDamageReductionPercent1}% damage reduction".HighlightInYellow()}\n"
                           + $"- This champion {"ignore stun".HighlightInPurple()} effects";
        Duration = Duration1;
        TimeLeft = Duration1;
		
        IgnoreStuns = originAbility.IgnoreStuns;
        AllDamageReceiveModifier = new PointsPercentageModifier(0, -ReceiveDamageReductionPercent1);
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }
}
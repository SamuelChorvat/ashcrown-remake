using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Arabela.Champion;

namespace Ashcrown.Remake.Core.Champions.Arabela.ActiveEffects;

public class DivineTrapMeActiveEffect : ActiveEffectBase
{
    public DivineTrapMeActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(ArabelaConstants.DivineTrapMeActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        ReceiveDamageReductionPoint1 = originAbility.ReceiveDamageReductionPoint1;
        Description = $"- This champion has {$"{ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        AllDamageReceiveModifier = new PointsPercentageModifier(-ReceiveDamageReductionPoint1);
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }
}
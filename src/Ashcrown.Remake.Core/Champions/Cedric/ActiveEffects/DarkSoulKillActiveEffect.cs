using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cedric.Champion;

namespace Ashcrown.Remake.Core.Champions.Cedric.ActiveEffects;

public class DarkSoulKillActiveEffect : ActiveEffectBase
{
    public DarkSoulKillActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(CedricConstants.DarkSoulKillActiveEffect, originAbility, championTarget)
    {
        DealDamageIncreasePoint1 = 5;
        ReceiveDamageReductionPoint1 = 5;
        
        Duration = Duration1;
        TimeLeft = Duration1;
        Buff = true;
        Helpful = true;
        Infinite = true;
        AllDamageDealModifier = new PointsPercentageModifier(DealDamageIncreasePoint1);
        AllDamageReceiveModifier = new PointsPercentageModifier(-ReceiveDamageReductionPoint1);
        Stackable = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return $"- This champion will deal an additional {$"{DealDamageIncreasePoint1*Stacks} magic damage".HighlightInBlue()}\n"
               + $"- This champion has {$"{ReceiveDamageReductionPoint1*Stacks} points of damage reduction".HighlightInYellow()}"
               + GetTimeLeftAffix();
    }

    public override void AddStack(IActiveEffect activeEffect)
    {
        Stacks += 1;
        AllDamageDealModifier = new PointsPercentageModifier(DealDamageIncreasePoint1 * Stacks);
        AllDamageReceiveModifier = new PointsPercentageModifier(-(ReceiveDamageReductionPoint1 * Stacks));
		
        //TODO note, dont forget to add point to totals if it happens after they are counted, for all other instances like this
        OriginAbility.Owner.ChampionController.TotalAllDamageDealIncrease.Points += DealDamageIncreasePoint1;
        OriginAbility.Owner.ChampionController.TotalAllDamageReceiveReduce.Points += ReceiveDamageReductionPoint1;
    }
}
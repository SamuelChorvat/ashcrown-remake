using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Azrael.Champion;

namespace Ashcrown.Remake.Core.Champions.Azrael.ActiveEffects;

public class SoulRealmActiveEffect : ActiveEffectBase
{
    public SoulRealmActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AzraelConstants.SoulRealmActiveEffect, originAbility, championTarget)
    {
        ReceiveDamageReductionPercent1 = originAbility.ReceiveDamageReductionPercent1;
        Duration1 = originAbility.Duration1;
        
        Duration = Duration1;
        TimeLeft = Duration1;
        AllDamageReceiveModifier = new PointsPercentageModifier(0, -ReceiveDamageReductionPercent1);
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
        Infinite = true;
        Stackable = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return $"- This champion has {$"{ReceiveDamageReductionPercent1*Stacks}% damage reduction".HighlightInYellow()}" + GetTimeLeftAffix();
    }

    public override void AddStack(IActiveEffect activeEffect)
    {
        Stacks += 1;
        AllDamageReceiveModifier = new PointsPercentageModifier(0, -(ReceiveDamageReductionPercent1 * Stacks));
    }

    public override void AdditionalDealActiveEffectLogic(IChampion dealer, IChampion target, IAbility ability, bool secondary,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (dealer.AbilityController.GetMyAbilityByName(AzraelConstants.Reap) != null) {
            dealer.ChampionController.DealActiveEffect(dealer, dealer.AbilityController.GetMyAbilityByName(AzraelConstants.Reap)!,
                new ReapMeActiveEffect(dealer.AbilityController.GetMyAbilityByName(AzraelConstants.Reap)!,dealer), true, appliedAdditionalLogic);
        }
    }
}
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Ash.Champion;

namespace Ashcrown.Remake.Core.Champions.Ash.ActiveEffects;

public class PhoenixFlamesActiveEffect : ActiveEffectBase
{
    public PhoenixFlamesActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AshConstants.PhoenixFlamesActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Damage1 = originAbility.Damage1;
        ReceiveDamageReductionPoint1 = originAbility.ReceiveDamageReductionPoint1;
        DealDamageIncreasePoint1 = originAbility.DealDamageIncreasePoint1;
        
        Duration = Duration1;
        TimeLeft = Duration1;
        Infinite = true;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
        Damaging = originAbility.Damaging;
        AfflictionDamage = originAbility.AfflictionDamage;
        CannotBeRemoved = true;
        Stackable = true;
		
        AllDamageReceiveModifier = new PointsPercentageModifier(-ReceiveDamageReductionPoint1);
        AllDamageDealModifier = new PointsPercentageModifier(DealDamageIncreasePoint1);
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return $"- {Target.Name} has {$"{Stacks*ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()}\n"
               + $"- {Target.Name} will deal an additional {$"{Stacks*DealDamageIncreasePoint1} magic damage".HighlightInBlue()}"
               + GetTimeLeftAffix();
    }

    public override void AddStack(IActiveEffect activeEffect)
    {
        if (Stacks >= 10) return;
        Stacks += 1;
        AllDamageReceiveModifier = new PointsPercentageModifier(-(Stacks * ReceiveDamageReductionPoint1));
        AllDamageDealModifier = new PointsPercentageModifier(Stacks * DealDamageIncreasePoint1);
			
        Target.ChampionController.ReceiveAbilityDamage(OriginAbility.Damage1, OriginAbility, 
            true, new AppliedAdditionalLogic());
    }

    public override void OnAdd()
    {
        Target.ChampionController.ReceiveAbilityDamage(OriginAbility.Damage1, OriginAbility, 
            true, new AppliedAdditionalLogic());
    }

    public override void EndTurnChecks()
    {
        switch (Stacks)
        {
            case < 5:
            {
                if (Target.AbilityController.GetMyAbilityByName(AshConstants.Fireball) != null) {
                    Target.CurrentAbilities[1] = Target.AbilityController.GetMyAbilityByName(AshConstants.Fireball)!;
                }

                break;
            }
            case >= 5 and < 8:
            {
                if (Target.AbilityController.GetMyAbilityByName(AshConstants.FireWhirl) != null) {
                    Target.CurrentAbilities[1] = Target.AbilityController.GetMyAbilityByName(AshConstants.FireWhirl)!;
                }

                break;
            }
            case >= 8:
            {
                if (Target.AbilityController.GetMyAbilityByName(AshConstants.Inferno) != null) {
                    Target.CurrentAbilities[1] = Target.AbilityController.GetMyAbilityByName(AshConstants.Inferno)!;
                }

                break;
            }
        }
    }
}
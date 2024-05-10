using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Garr.Champion;

namespace Ashcrown.Remake.Core.Champions.Garr.ActiveEffects;

public class RecklessnessActiveEffect : ActiveEffectBase
{
    public RecklessnessActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(GarrConstants.RecklessnessActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Duration = Duration1;
        TimeLeft = Duration1;
        Infinite = true;
        Helpful = originAbility.Helpful;
        Buff =  originAbility.Buff;
        CannotBeRemoved = true;
        Stackable = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return $"- {GarrConstants.Slam.HighlightInGold()} will deal an additional {$"{Stacks*20} physical damage".HighlightInOrange()}\n"
               + $"- {GarrConstants.BarbedChain.HighlightInOrange()} will deal an additional {$"{Stacks*10} physical damage".HighlightInOrange()}"
               + GetTimeLeftAffix();
    }

    public override void AddStack(IActiveEffect activeEffect)
    {
        Target.ChampionController.ReceiveAbilityDamage(activeEffect.OriginAbility.Damage1, 
            activeEffect.OriginAbility, true, new AppliedAdditionalLogic());
        if (Stacks < 3) {
            Stacks += 1;
        }
    }

    public override void OnAdd()
    {
        Target.ChampionController.ReceiveAbilityDamage(OriginAbility.Damage1, OriginAbility, true, 
            new AppliedAdditionalLogic());
    }
}
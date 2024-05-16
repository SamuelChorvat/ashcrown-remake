using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Izrin.Champion;

namespace Ashcrown.Remake.Core.Champions.Izrin.ActiveEffects;

public class BloodyStrikerActiveEffect : ActiveEffectBase
{
    public BloodyStrikerActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(IzrinConstants.BloodyStrikeActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        ReceiveDamageReductionPoint1 = 10;
        BonusDamage1 = originAbility.BonusDamage1;
        
        Description = $"- This champion has {$"{ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()}\n"
                           + $"- {IzrinConstants.BloodyStrike.HighlightInGold()} will deal an additional {$"{BonusDamage1} physical damage".HighlightInOrange()} next turn";
        Duration = Duration1;
        TimeLeft = Duration1;
        AllDamageReceiveModifier = new PointsPercentageModifier(-ReceiveDamageReductionPoint1);
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }

    public override void OnRemove()
    {
        OriginAbility.Owner.ChampionController.DealActiveEffect(Target, OriginAbility, 
            new BloodyStrikerHelperActiveEffect(OriginAbility, Target), true, new AppliedAdditionalLogic());
    }
}
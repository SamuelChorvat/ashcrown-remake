using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Izrin.Champion;

namespace Ashcrown.Remake.Core.Champions.Izrin.ActiveEffects;

public class RakeActiveEffect : ActiveEffectBase
{
    public RakeActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(IzrinConstants.RakeActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        DealDamageReductionPoint1 = 15;
        
        Description = $"- This champion's non-Affliction abilities will deal {DealDamageReductionPoint1} less damage";
        Duration = Duration1 + 1;
        TimeLeft = Duration1 + 1;
        AllDamageDealModifier = new PointsPercentageModifier(-DealDamageReductionPoint1);
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
    }

    public override void AdditionalDealActiveEffectLogic(IChampion dealer, IChampion target, IAbility ability, bool secondary,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        TimeLeft += dealer.BattlePlayer.NoOfDead();
    }
}
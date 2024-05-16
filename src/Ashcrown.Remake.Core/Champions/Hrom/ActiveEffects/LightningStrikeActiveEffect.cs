using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Hrom.Champion;

namespace Ashcrown.Remake.Core.Champions.Hrom.ActiveEffects;

public class LightningStrikeActiveEffect : ActiveEffectBase
{
    public LightningStrikeActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(HromConstants.LightningStrikeActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Description = $"- {HromConstants.LightningStorm.HighlightInGold()} may be used next turn\n";
        Duration = Duration1;
        TimeLeft = Duration1;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return Description + $"{"1 TURN LEFT".HighlightInGold()}";
    }

    public override void OnApply()
    {
        OriginAbility.Owner.ChampionController.DealActiveEffect(Target, OriginAbility, 
            new LightningStrikeHelperActiveEffect(OriginAbility, Target), true, new AppliedAdditionalLogic());
        RemoveIt = true;
    }
}
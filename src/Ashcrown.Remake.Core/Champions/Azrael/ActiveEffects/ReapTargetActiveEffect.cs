using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Azrael.Champion;

namespace Ashcrown.Remake.Core.Champions.Azrael.ActiveEffects;

public class ReapTargetActiveEffect : ActiveEffectBase
{
    public ReapTargetActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AzraelConstants.ReapTargetActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1 + 1;
        BonusDamage1 = originAbility.BonusDamage1;
        
        Description = $"- If this champion uses a new ability, {AzraelConstants.Reap.HighlightInGold()} will deal an additional {$"{BonusDamage1} {"piercing"} physical damage".HighlightInOrange()} to this champion the following turn";
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
    }

    public override void EndTurnChecks()
    {
        if(Target.AbilityController.LastUsedAbility != null) {
            OriginAbility.Owner.ChampionController.DealActiveEffect(Target, OriginAbility, 
                new ReapTriggeredTargetActiveEffect(OriginAbility, Target), true, new AppliedAdditionalLogic());
        }

        Target.ActiveEffectController.RemoveActiveEffect(this);
    }
}
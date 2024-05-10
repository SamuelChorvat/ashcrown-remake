using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Evanore.Champion;

namespace Ashcrown.Remake.Core.Champions.Evanore.ActiveEffects;

public class MonstrousBearActiveEffect : ActiveEffectBase
{
    public MonstrousBearActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(EvanoreConstants.MonstrousBearActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1 + 1;
        Damage1 = originAbility.Damage2;
        
        Description = $"- This champion will receive {$"{Damage1} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()} if they use a new ability";
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        Damaging = originAbility.Damaging;
        PhysicalDamage = originAbility.PhysicalDamage;
        PiercingDamage = true;
    }

    public override void EndTurnChecks()
    {
        if (Target.AbilityController.UsedNewAbility) {
            Target.ChampionController.ReceiveActiveEffectDamage(Damage1, this, new AppliedAdditionalLogic());
        }
    }
}
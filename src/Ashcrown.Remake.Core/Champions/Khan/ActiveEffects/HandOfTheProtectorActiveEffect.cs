using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Khan.Champion;

namespace Ashcrown.Remake.Core.Champions.Khan.ActiveEffects;

public class HandOfTheProtectorActiveEffect : ActiveEffectBase
{
    public HandOfTheProtectorActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(KhanConstants.HandOfTheProtectorActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Heal1 = originAbility.Heal1;
        
        Description = $"- This champion will be healed by {$"{Heal1} health points".HighlightInGreen()} at the end of their turn\n" +
                           $"- {KhanConstants.MortalStrike.HighlightInGold()} will deal {$"{-originAbility.BonusDamage1} LESS physical damage".HighlightInOrange()}\n" +
                           $"- This champion is {"invulnerable".HighlightInPurple()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
        Healing = originAbility.Healing;
        Invulnerability = originAbility.Invulnerability;
        TypeOfInvulnerability = originAbility.TypeOfInvulnerability;
    }

    public override void EndTurnChecks()
    {
        OriginAbility.Owner.ChampionController.DealActiveEffectHealing(Heal1, Target, 
            this, new AppliedAdditionalLogic());
    }
}
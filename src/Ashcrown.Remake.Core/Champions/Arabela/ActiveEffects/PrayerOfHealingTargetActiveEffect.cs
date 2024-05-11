using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Arabela.Champion;

namespace Ashcrown.Remake.Core.Champions.Arabela.ActiveEffects;

public class PrayerOfHealingTargetActiveEffect : ActiveEffectBase
{
    public PrayerOfHealingTargetActiveEffect(IAbility originAbility, IChampion championTarget)
        : base(ArabelaConstants.PrayerOfHealingTargetActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Heal1 = originAbility.Heal1;
        Description = $"- This champion will be healed by {$"{Heal1} health points".HighlightInGreen()} at the end of their turn";
        Duration = Duration1;
        TimeLeft = Duration1;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
        Healing = originAbility.Healing;
    }

    public override void AdditionalDealActiveEffectLogic(IChampion dealer, IChampion target, 
        IAbility ability, bool secondary, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        dealer.ChampionController.DealActiveEffect(dealer, ability, 
            new PrayerOfHealingMeActiveEffect(ability, dealer), true, appliedAdditionalLogic);
    }

    public override void EndTurnChecks()
    {
        OriginAbility.Owner.ChampionController.DealActiveEffectHealing(Heal1, Target, this, new AppliedAdditionalLogic());
    }
}
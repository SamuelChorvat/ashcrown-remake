using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Azrael.Champion;

namespace Ashcrown.Remake.Core.Champions.Azrael.ActiveEffects;

public class CursedMarkMeActiveEffect : ActiveEffectBase
{
    public CursedMarkMeActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AzraelConstants.CursedMarkMeActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Damage1 = originAbility.Damage1;
        
        Description = $"- This champion is {"invulnerable".HighlightInPurple()} to Strategic abilities\n"
                           + $"- Any enemy that uses a new harmful ability on this champion will be dealt {$"{Damage1} {"piercing".HighlightInBold()} magic damage".HighlightInBlue()} permanently";
        Duration = Duration1;
        TimeLeft = Duration1;
        Invulnerability = originAbility.Invulnerability;
        TypeOfInvulnerability = originAbility.TypeOfInvulnerability;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }

    public override void AdditionalDealActiveEffectLogic(IChampion dealer, IChampion target, IAbility ability, bool secondary,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (dealer.AbilityController.GetMyAbilityByName(AzraelConstants.Reap) != null) {
            dealer.ChampionController.DealActiveEffect(dealer, dealer.AbilityController.GetMyAbilityByName(AzraelConstants.Reap)!,
                new ReapMeActiveEffect(dealer.AbilityController.GetMyAbilityByName(AzraelConstants.Reap)!,dealer), true, appliedAdditionalLogic);
        }
    }

    public override void ReceiveReaction(IAbility ability)
    {
        if (!ability.Harmful || ability.Owner.BattlePlayer.PlayerNo == Target.BattlePlayer.PlayerNo) return;
        var curse = new CursedMarkTargetActiveEffect(OriginAbility, ability.Owner);
        curse.OriginAbility.Owner.ChampionController.DealActiveEffect(ability.Owner,
            OriginAbility, curse, true, new AppliedAdditionalLogic());
    }
}
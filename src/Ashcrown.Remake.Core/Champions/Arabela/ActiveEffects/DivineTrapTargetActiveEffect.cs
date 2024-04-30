using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Arabela.Champion;

namespace Ashcrown.Remake.Core.Champions.Arabela.ActiveEffects;

public class DivineTrapTargetActiveEffect : ActiveEffectBase
{
    public DivineTrapTargetActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(ArabelaConstants.DivineTrapTargetActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Damage1 = originAbility.Damage1;
        BonusDamage1 = originAbility.BonusDamage1;
        Description = 
            $"- This champion will receive {$"{Damage1} {"piercing".HighlightInBold()} magic damage".HighlightInBlue()} at the end of this turn\n" +
            $"- This champion will receive another {$"{BonusDamage1} {"piercing".HighlightInBold()} magic damage".HighlightInBlue()} if they use a new ability\n" +
            $"- This damage {"ignore invulnerability".HighlightInPurple()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Hidden = true;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        Damaging = originAbility.Damaging;
        MagicDamage = originAbility.MagicDamage;
        PiercingDamage = originAbility.PiercingDamage;
        IgnoreInvulnerability = originAbility.IgnoreInvulnerability;
        NoTick = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return $"{Description}{"ENDS THIS TURN".HighlightInGold()}";
    }

    public override void AdditionalDealActiveEffectLogic(IChampion dealer, IChampion target, IAbility ability, bool secondary, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        dealer.ChampionController.DealActiveEffect(dealer, ability, new DivineTrapMeActiveEffect(ability, dealer), true, appliedAdditionalLogic);
    }

    public override void EndTurnChecks()
    {
        if (Target.AbilityController.UsedNewAbility)
        {
            Target.ChampionController.ReceiveActiveEffectDamage(Damage1 + BonusDamage1, this, new AppliedAdditionalLogic());
        }
        else
        {
            Target.ChampionController.ReceiveActiveEffectDamage(Damage1, this, new AppliedAdditionalLogic());
        }

        OriginAbility.Owner.ChampionController.DealActiveEffect(Target, OriginAbility, new DivineTrapEndActiveEffect(OriginAbility, Target), true, new AppliedAdditionalLogic());
        Target.ActiveEffectController.RemoveActiveEffect(this);
    }
}
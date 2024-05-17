using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Jafali.Champion;

namespace Ashcrown.Remake.Core.Champions.Jafali.ActiveEffects;

public class DecayingSoulActiveEffect : ActiveEffectBase
{
    public DecayingSoulActiveEffect(IAbility originAbility, IChampion championTarget) : 
        base(JafaliConstants.DecayingSoulActiveEffect, originAbility, championTarget)
    {
        Duration1 = 5;
        Damage1 = 5;

        Description = $"- This champion will receive {$"{Damage1} affliction damage".HighlightInRed()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = true;
        Damaging = true;
        AfflictionDamage = true;
        Debuff = true;
    }

    public override void OnApply()
    {
        OnApplyDot();
    }

    public override string GetAbilityName()
    {
        return JafaliConstants.DecayingSoul;
    }

    public override void AdditionalDealActiveEffectLogic(IChampion dealer, IChampion target, IAbility ability, bool secondary,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (secondary) {
            return;
        }

        switch (ability.Name)
        {
            case JafaliConstants.Anger when dealer.AbilityController.GetMyAbilityByName(JafaliConstants.Anger) != null:
            {
                if (!dealer.ActiveEffectController.ActiveEffectPresentByActiveEffectName(JafaliConstants.AngerActiveEffect)) {
                    OriginAbility.Owner.ChampionController.DealActiveEffect(dealer, OriginAbility,
                        new AngerActiveEffect(dealer.AbilityController.GetMyAbilityByName(JafaliConstants.Anger)!, dealer), 
                        true, appliedAdditionalLogic);
                }

                break;
            }
            case JafaliConstants.Envy when dealer.AbilityController.GetMyAbilityByName(JafaliConstants.Envy) != null:
            {
                if (!dealer.ActiveEffectController.ActiveEffectPresentByActiveEffectName(JafaliConstants.EnvyActiveEffect)) {
                    OriginAbility.Owner.ChampionController.DealActiveEffect(dealer, OriginAbility,
                        new EnvyActiveEffect(dealer.AbilityController.GetMyAbilityByName(JafaliConstants.Envy)!, dealer), 
                        true, appliedAdditionalLogic);
                }

                break;
            }
        }
    }
}
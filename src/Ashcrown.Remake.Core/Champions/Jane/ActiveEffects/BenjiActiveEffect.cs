using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Jane.Champion;

namespace Ashcrown.Remake.Core.Champions.Jane.ActiveEffects;

public class BenjiActiveEffect : ActiveEffectBase
{
    public BenjiActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(JaneConstants.BenjiActiveEffect, originAbility, championTarget)
    {
        Damage1 = originAbility.Damage1;
		
        ReceiveDamageReductionPoint1 = originAbility.ReceiveDamageReductionPoint1;
        Duration1 = originAbility.Duration1;
        BonusDamage1 = originAbility.BonusDamage1;
        
        Description = $"- This champion has {$"{ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()}\n"
                           + $"- If an enemy uses a new non-Affliction ability on this champion, they will receive {$"{Damage1} physical damage".HighlightInOrange()}\n"
                           + $"- {JaneConstants.GoForTheThroat.HighlightInGold()} can be used";
        Duration = Duration1;
        TimeLeft = Duration1;
        AllDamageReceiveModifier = new PointsPercentageModifier(-ReceiveDamageReductionPoint1);
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
        Damaging = originAbility.Damaging;
        PhysicalDamage = originAbility.PhysicalDamage;
    }

    public override void OnAdd()
    {
        if (Target.AbilityController.GetMyAbilityByName(JaneConstants.GoForTheThroat) != null) {
            Target.CurrentAbilities[0] = Target.AbilityController.GetMyAbilityByName(JaneConstants.GoForTheThroat)!;
        }
    }

    public override void OnRemove()
    {
        if (Target.ActiveEffectController.GetActiveEffectCountByName(JaneConstants.BenjiActiveEffect) != 1) return;
        if (Target.AbilityController.GetMyAbilityByName(JaneConstants.Benji) != null) {
            Target.CurrentAbilities[0] = Target.AbilityController.GetMyAbilityByName(JaneConstants.Benji)!;
        }
    }

    public override void ReceiveReaction(IAbility ability)
    {
        if (!ability.AbilityClassesContains(AbilityClass.Affliction) && ability.Harmful
                                                            && ability.Owner.BattlePlayer.PlayerNo != Target.BattlePlayer.PlayerNo) {
            ability.Owner.ChampionController.ReceiveActiveEffectDamage(Damage1, this, new AppliedAdditionalLogic());
        }
    }
}
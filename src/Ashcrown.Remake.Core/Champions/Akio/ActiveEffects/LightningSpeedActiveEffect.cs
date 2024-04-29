using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Abstract;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Akio.Champion;

namespace Ashcrown.Remake.Core.Champions.Akio.ActiveEffects;

public class LightningSpeedActiveEffect : ActiveEffectBase
{
    public LightningSpeedActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AkioConstants.LightningSpeedActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        BonusDamage1 = originAbility.BonusDamage1;
        Description = $"- This champion will {"ignore".HighlightInPurple()} all harmful effects except energy cost changed\n"
            + $"- If a new enemy harmful ability is used on this champion, {AkioConstants.DragonRage.HighlightInGold()} will permanently deal an additional {$"{BonusDamage1} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()}\n"
            + $"- This ability is {"invisible".HighlightInPurple()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
        IgnoreHarmful = originAbility.IgnoreHarmful;
        CannotBeRemoved = true;
        Hidden = true;
    }

    public override void OnRemove()
    {
        if(!Target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AkioConstants.LightningSpeedEndActiveEffect)) {
            OriginAbility.Owner.ChampionController.DealActiveEffect(Target, OriginAbility, 
                new LightningSpeedEndActiveEffect(OriginAbility, Target), true, new AppliedAdditionalLogic());
        }
    }

    public override void ReceiveReaction(IAbility ability)
    {
        if (!ability.Harmful || ability.Owner.BattlePlayer.PlayerNo == Target.BattlePlayer.PlayerNo) return;
        if (OriginAbility.Owner.AbilityController.GetMyAbilityByName(AkioConstants.DragonRage) != null)
        {
            OriginAbility.Owner.ChampionController.DealActiveEffect(OriginAbility.Owner,
                OriginAbility.Owner.AbilityController.GetMyAbilityByName(AkioConstants.DragonRage)!,
                new DragonRageActiveEffect(OriginAbility.Owner.AbilityController.GetMyAbilityByName(AkioConstants.DragonRage)!,
                    OriginAbility.Owner), true, new AppliedAdditionalLogic());
        }
    }
}
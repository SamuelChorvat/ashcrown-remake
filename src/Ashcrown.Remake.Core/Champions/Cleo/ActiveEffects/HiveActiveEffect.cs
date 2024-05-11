using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cleo.Champion;

namespace Ashcrown.Remake.Core.Champions.Cleo.ActiveEffects;

public class HiveActiveEffect : ActiveEffectBase
{
    public HiveActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(CleoConstants.HiveActiveEffect, originAbility, championTarget)
    {
        Heal1 = originAbility.BonusHeal1;
        
        Description = $"- This champion will be healed by {$"{Heal1} health points".HighlightInGreen()} at the start of their turn";
        Duration = Duration1;
        TimeLeft = Duration1;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
        Healing = originAbility.Healing;
        Infinite = true;
        EndsOnCasterDeath = true;
    }

    public override void OnApply()
    {
        Target.ChampionController.ReceiveActiveEffectHealing(Heal1, this, 
            new AppliedAdditionalLogic());
        base.OnApply();
    }
}
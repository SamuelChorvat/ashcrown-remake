using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Gruber.Champion;

namespace Ashcrown.Remake.Core.Champions.Gruber.ActiveEffects;

public class ExplosiveLeechActiveEffect : ActiveEffectBase
{
    public ExplosiveLeechActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(GruberConstants.ExplosiveLeechActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Damage1 = originAbility.Damage1;
        Damage2 = originAbility.Damage2;
        
        Duration = Duration1;
        TimeLeft = Duration1;
        Debuff = true;
        Harmful = true;
        Damaging = true;
        AfflictionDamage = true;
        Infinite = true;
        Hidden = true;
        Stackable = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return $"- The first enemy champion that uses a new harmful ability on this ally will take {$"{Damage1*Stacks} affliction damage".HighlightInRed()}\n"
               + $"- This champion will take {$"{Damage2*Stacks} affliction damage".HighlightInRed()} when this is triggered\n"
               + $"- This ability is {"invisible".HighlightInPurple()} and ends when triggered"
               + GetTimeLeftAffix();
    }

    public override void OnRemove()
    {
        OriginAbility.Owner.ChampionController.DealActiveEffect(Target, OriginAbility, 
            new ExplosiveLeechEndActiveEffect(OriginAbility, Target), true, new AppliedAdditionalLogic());
    }

    public override void ReceiveReaction(IAbility ability)
    {
        if (!ability.Harmful || ability.Owner.BattlePlayer.PlayerNo == Target.BattlePlayer.PlayerNo) return;
        Target.ChampionController.ReceiveActiveEffectDamage(Damage2 * Stacks, 
            this, new AppliedAdditionalLogic());
        ability.Owner.ChampionController.ReceiveActiveEffectDamage(Damage1 * Stacks, 
            this, new AppliedAdditionalLogic());
        Target.ActiveEffectController.RemoveActiveEffect(this);
    }
}
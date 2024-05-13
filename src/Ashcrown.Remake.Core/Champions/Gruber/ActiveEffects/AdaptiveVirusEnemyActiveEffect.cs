using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Gruber.Champion;

namespace Ashcrown.Remake.Core.Champions.Gruber.ActiveEffects;

public class AdaptiveVirusEnemyActiveEffect : ActiveEffectBase
{
    public AdaptiveVirusEnemyActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(GruberConstants.AdaptiveVirusEnemyActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Damage1 = originAbility.Damage1;
        
        Description = "override";
        Duration = Duration1;
        TimeLeft = Duration1;
        Debuff = true;
        Harmful = true;
        Damaging = true;
        AfflictionDamage = true;
        Infinite = true;
        Stackable = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return $"- This champion will receive {$"{Damage1*Stacks} affliction damage".HighlightInRed()}" + GetTimeLeftAffix();
    }

    public override void OnApply()
    {
        Target.ChampionController.ReceiveActiveEffectDamage(Damage1 * Stacks, 
            this, new AppliedAdditionalLogic());
        TickDown();
    }
}
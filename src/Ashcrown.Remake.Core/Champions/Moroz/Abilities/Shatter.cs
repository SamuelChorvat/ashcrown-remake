using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Moroz.Champion;

namespace Ashcrown.Remake.Core.Champions.Moroz.Abilities;

public class Shatter : AbilityBase
{
    public Shatter(IChampion champion) 
        : base(champion, 
            MorozConstants.Shatter, 
            0,
            [2,0,0,0,0], 
            [AbilityClass.Magic, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            1)
    {
        Damage1 = 0;
        
        Description = $"{MorozConstants.Name} shatters a frozen enemy. " +
                      $"The enemy is {"instantly killed".HighlightInPurple()}. " +
                      $"This ability requires {MorozConstants.Freeze.HighlightInGold()} to be active on the enemy. " +
                      $"This ability cannot be {"countered".HighlightInPurple()} or {"reflected".HighlightInPurple()}.";
        Harmful = true;
        Damaging = true;
        MagicDamage = true;
        Counterable = false;
        Reflectable = false;
    }

    public override bool CustomReceiveAbilityDamageLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(MorozConstants.FreezeTargetActiveEffect)) {
            target.ChampionController.SubtractHealth(0, appliedAdditionalLogic,this);
        }
        return true;
    }

    public override int SubtractHealthModifier(int toSubtract, IChampion victim)
    {
        return victim.Health;
    }

    public override int[] TargetsModifier(int[] targets)
    {
        for (var j = 0; j < 3; j++) {
            if (!Owner.BattlePlayer.GetEnemyPlayer().Champions[j].ActiveEffectController
                    .ActiveEffectPresentByActiveEffectName(MorozConstants.FreezeTargetActiveEffect)) {
                targets[j + 3] = 0;
            }
        }
        return targets;
    }

    public override void StartTurnChecks()
    {
        if(Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(MorozConstants.FreezeMeActiveEffect) 
           && !Copied) {
            Owner.CurrentAbilities[0] = this;
        }
    }

    public override void EndTurnChecks()
    {
        if(Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(MorozConstants.FreezeMeActiveEffect) 
           && !Copied) {
            Owner.CurrentAbilities[0] = this;
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        return AiCalculatorConstants.HighestPriority;
    }
}
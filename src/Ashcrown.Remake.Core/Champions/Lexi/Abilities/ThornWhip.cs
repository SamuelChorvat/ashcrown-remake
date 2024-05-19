using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Lexi.Champion;

namespace Ashcrown.Remake.Core.Champions.Lexi.Abilities;

public class ThornWhip : AbilityBase
{
    public ThornWhip(IChampion champion) 
        : base(champion, 
            LexiConstants.ThornWhip, 
            0,
            [0,0,0,1,0], 
            [AbilityClass.Magic, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            1)
    {
        Damage1 = 25;
        Damage2 = 35;
        Damage3 = 10;
        
        Description = $"{LexiConstants.Name} brings a vine to life that deals {$"{Damage1} magic damage".HighlightInBlue()} to one enemy. " +
                      $"While {LexiConstants.Tranquility.HighlightInGold()} is active, this ability will deal {$"{Damage2} magic damage".HighlightInBlue()} to one enemy and {$"{Damage3} magic damage".HighlightInBlue()} to all others. " +
                      $"Only the primary target can {"counter".HighlightInPurple()} or {"reflect".HighlightInPurple()} this ability.";
        Harmful = true;
        MagicDamage = true;
        Damaging = true;
    }

    public override int DealAbilityDamageModifier(IChampion target, int amount, bool secondary)
    {
        var newAmount = amount;

        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(LexiConstants.TranquilityActiveEffect) 
            && !secondary) {
            newAmount = Damage2;
        }

        return newAmount;
    }

    public override void AdditionalDealAbilityDamageLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (!Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(LexiConstants.TranquilityActiveEffect))
            return;
        var secondaryTargets = target.BattlePlayer.GetOtherChampions(target);
        if (secondaryTargets == null) return;
        foreach (var secondaryTarget in secondaryTargets) {
            Owner.ChampionController.DealAbilityDamage(Damage3, secondaryTarget, this, 
                true, appliedAdditionalLogic);
        }
    }

    public override bool UseChecks()
    {
        var aeRef = Owner.ActiveEffectController.GetActiveEffectByName(LexiConstants.TranquilityActiveEffect);
        if (aeRef is {Stacks: > 0}) {
            aeRef.Stacks -= 1;
        }

        return true;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(DealAbilityDamageModifier(target, Damage1, false), 
            1, this, target);
        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(LexiConstants.TranquilityActiveEffect)) {
            totalPoints += Damage3 * 2;
        }
        return totalPoints;
    }
}
using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Braya.Champion;

namespace Ashcrown.Remake.Core.Champions.Braya.Abilities;

public class KillShot : AbilityBase
{
    public KillShot(IChampion champion) 
        : base(champion, 
            BrayaConstants.KillShot, 
            0,
            [0,2,0,0,0], 
            [AbilityClass.Physical,AbilityClass.Instant,AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            3)
    {
        Damage1 = 40;
        BonusDamage1 = 5;
        
        Description = $"{BrayaConstants.Name} gathers up her remaining {BrayaConstants.HuntersFocus.HighlightInGold()} stacks, " +
                      $"shooting off a powerful arrow that deals {$"{Damage1} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()} to one enemy. " +
                      $"This ability {"ignores invulnerability".HighlightInPurple()} and it will deal and additional {$"{BonusDamage1} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()} for each {BrayaConstants.HuntersFocus.HighlightInGold()} stack remaining. " +
                      $"This ability uses up all {BrayaConstants.HuntersFocus.HighlightInGold()} stacks.";
        Harmful = true;
        Damaging = true;
        PhysicalDamage = true;
        PiercingDamage = true;
        IgnoreInvulnerability = true;
        Active = false;
    }

    public override int DealAbilityDamageModifier(IChampion target, int amount, bool secondary)
    {
        var newAmount = amount;

        var aeRef = Owner.ActiveEffectController.GetActiveEffectByName(BrayaConstants.HuntersFocusStacksActiveEffect);
        if (aeRef == null) return newAmount;
        newAmount += BonusDamage1 * aeRef.Stacks;
        aeRef.Stacks = 0;

        return newAmount;
    }

    public override bool UseChecks()
    {
        var aeRef = Owner.ActiveEffectController.GetActiveEffectByName(BrayaConstants.HuntersFocusStacksActiveEffect);

        if (aeRef is { Stacks: > 0 }) {
            aeRef.RemoveIt = true;
        } else {
            return false;
        }

        return true;
    }

    public override void StartTurnChecks()
    {
        var focus = Owner.ActiveEffectController.GetActiveEffectByName(BrayaConstants.HuntersFocusStacksActiveEffect);
        if (focus is { Stacks: > 0 } && IsReady()) {
            Active = true;
        } else {
            Active = false;
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var newAmount = Damage1;

        var aeRef = Owner.ActiveEffectController.GetActiveEffectByName(BrayaConstants.HuntersFocusStacksActiveEffect);
        if (aeRef != null) {
            newAmount += BonusDamage1 * aeRef.Stacks;
        }

        var totalPoints = T.GetDamagePoints(newAmount, 1, this, target);
        return totalPoints;
    }
}
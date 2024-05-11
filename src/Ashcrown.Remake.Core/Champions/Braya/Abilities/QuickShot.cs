using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Braya.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Braya.Champion;

namespace Ashcrown.Remake.Core.Champions.Braya.Abilities;

public class QuickShot : AbilityBase
{
    public QuickShot(IChampion champion) 
        : base(champion, 
            BrayaConstants.QuickShot, 
            0,
            [0,1,0,0,0], 
            [AbilityClass.Physical,AbilityClass.Instant,AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            2)
    {
        Damage1 = 20;
        Duration1 = 1;
        
        Description = $"Shooting off a lightning fast arrow, {BrayaConstants.Name} deals {$"{Damage1} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()} to one enemy " +
                      $"that {"ignores invulnerability".HighlightInPurple()}. " +
                      $"For 1 turn, {BrayaConstants.Name} will also become {"invulnerable".HighlightInPurple()} to all non-Strategic abilities. " +
                      $"This ability uses up 1 {BrayaConstants.HuntersFocus.HighlightInGold()} stack.";
        Harmful = true;
        Helpful = true;
        Buff = true;
        Damaging = true;
        PhysicalDamage = true;
        PiercingDamage = true;
        Invulnerability = true;
        TypeOfInvulnerability = [AbilityClass.Physical,AbilityClass.Magic,AbilityClass.Affliction];
        IgnoreInvulnerability = true;
        Active = false;
    }

    public override void AdditionalDealAbilityDamageLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        Owner.ChampionController.DealActiveEffect(Owner,
            this, new QuickShotActiveEffect(this, Owner), true, appliedAdditionalLogic);
    }

    public override bool UseChecks()
    {
        var aeRef = Owner.ActiveEffectController.GetActiveEffectByName(BrayaConstants.HuntersFocusStacksActiveEffect);

        if (aeRef is { Stacks: > 0 }) {
            aeRef.Stacks -= 1;
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
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        totalPoints += T.GetInvulnerabilityPoints(Duration1, this, Owner);
        return totalPoints;
    }
}
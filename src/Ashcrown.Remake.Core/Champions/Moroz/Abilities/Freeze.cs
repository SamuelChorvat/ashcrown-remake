using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Moroz.Champion;

namespace Ashcrown.Remake.Core.Champions.Moroz.Abilities;

public class Freeze : AbilityBase
{
    public Freeze(IChampion champion) 
        : base(champion, 
            MorozConstants.Freeze, 
            2,
            [1,0,0,0,1], 
            [AbilityClass.Strategic, AbilityClass.Ranged, AbilityClass.Control], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyActionControl, 
            1)
    {
        Duration1 = 2;
        
        Description = $"{MorozConstants.Name} freezes one enemy, {"stunning".HighlightInPurple()} their non-Strategic abilities for {Duration1} turns. " +
                      $"For {Duration1} turns, that enemy cannot {"reduce damage".HighlightInPurple()} or become {"invulnerable".HighlightInPurple()}. " +
                      $"During this time, this ability will be replaced by {MorozConstants.Shatter.HighlightInGold()}.";
        SelfDisplay = true;
        Harmful = true;
        Debuff = true;
        Stun = true;
        StunType = [AbilityClass.Magic, AbilityClass.Physical, AbilityClass.Affliction];
        DisableDamageReceiveReduction = true;
        DisableInvulnerability = true;
        
        ActiveEffectOwner = MorozConstants.Name;
        ActiveEffectSourceName = MorozConstants.FreezeMeActiveEffect;
        ActiveEffectTargetName = MorozConstants.FreezeTargetActiveEffect;
    }

    public override void StartTurnChecks()
    {
        if(!Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(MorozConstants.FreezeMeActiveEffect) 
           && !Copied) {
            Owner.CurrentAbilities[0] = this;
        }
    }

    public override void EndTurnChecks()
    {
        if(!Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(MorozConstants.FreezeMeActiveEffect) 
           && !Copied) {
            Owner.CurrentAbilities[0] = this;
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetStunPoints(this, Duration1, target);
        totalPoints += T.GetDisableInvulnerabilityAndDamageReductionPoints(Duration1, target);
        totalPoints += T.GetSpecialConditionPoints(Duration1);
        return totalPoints;
    }
}
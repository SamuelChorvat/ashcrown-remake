using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Sarfu.Champion;

namespace Ashcrown.Remake.Core.Champions.Sarfu.Abilities;

public class Duel : AbilityBase
{
    public Duel(IChampion champion) 
        : base(champion, 
            SarfuConstants.Duel, 
            4,
            [0,0,0,0,1],
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Enemy, 
            AbilityType.EnemiesDebuff,
            3)
    {
        Duration1 = 4;
        ReceiveDamageReductionPoint1 = 10;
        Description = $"{SarfuConstants.Name} challenges one enemy to duel. " +
                      $"For {Duration1} tuns, {SarfuConstants.Name} will gain {$"{ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()}. " +
                      $"During this time that enemy will be unable to {"reduce damage".HighlightInPurple()} or become {"invulnerable".HighlightInPurple()}. " +
                      $"{SarfuConstants.Duel.HighlightInGold()} ends if {SarfuConstants.Name} dies.";
        SelfDisplay = true;
        DisableDamageReceiveReduction = true;
        DisableInvulnerability = true;
        Harmful = true;
        Helpful = true;
        Debuff = true;
        Buff = true;
        ActiveEffectOwner = SarfuConstants.Name;
        ActiveEffectName = SarfuConstants.DuelTargetActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetPointsReductionPoints(ReceiveDamageReductionPoint1, Duration1, Owner);
        totalPoints += T.GetSpecialConditionPoints(Duration1);
        totalPoints += T.GetDisableInvulnerabilityAndDamageReductionPoints(Duration1, target);
        return totalPoints;
    }
}
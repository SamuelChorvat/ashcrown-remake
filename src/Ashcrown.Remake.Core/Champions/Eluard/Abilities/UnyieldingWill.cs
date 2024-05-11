using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Eluard.Champion;

namespace Ashcrown.Remake.Core.Champions.Eluard.Abilities;

public class UnyieldingWill : AbilityBase
{
    public UnyieldingWill(IChampion champion) 
        : base(champion, 
            EluardConstants.UnyieldingWill, 
            3,
            [0,0,0,0,1],
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Self, 
            AbilityType.AllyBuff,
            3)
    {
        ReceiveDamageReductionPoint1 = 15;
        Duration1 = 4;
        BonusDamage1 = 10;
        Description = $"{EluardConstants.Name} gains {$"{ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()} for {Duration1} turns. " +
                      $"During this time {EluardConstants.SwordStrike.HighlightInGold()} will deal an additional {$"{BonusDamage1} physical damage".HighlightInOrange()} " +
                      $"and {EluardConstants.Devastate.HighlightInGold()} can be used.";
        Helpful = true;
        Buff = true;
        SelfCast = true;
        ActiveEffectOwner = EluardConstants.Name;
        ActiveEffectName = EluardConstants.UnyieldingWillActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        int totalPoints = T.GetPointsReductionPoints(ReceiveDamageReductionPoint1, Duration1, Owner);
        totalPoints += T.GetSpecialConditionPoints(Duration1);
        return totalPoints;
    }
}
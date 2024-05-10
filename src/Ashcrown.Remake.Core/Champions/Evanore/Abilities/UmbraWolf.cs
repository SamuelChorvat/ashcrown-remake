using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Evanore.Champion;

namespace Ashcrown.Remake.Core.Champions.Evanore.Abilities;

public class UmbraWolf : AbilityBase
{
    public UmbraWolf(IChampion champion) 
        : base(champion, 
            EvanoreConstants.UmbraWolf, 
            3,
            [0,0,0,0,1], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Self, 
            AbilityType.AllyBuff, 
            3)
    {
        Duration1 = 4;
        ReceiveDamageReductionPercent1 = 50;
        
        Description = $"{EvanoreConstants.Name} summons an umbra wolf to aid her in battle. " +
                      $"For {Duration1} turns, {EvanoreConstants.Name} will {"ignore stun".HighlightInPurple()} effects " +
                      $"and gain {$"{ReceiveDamageReductionPercent1}% damage reduction".HighlightInYellow()}.";
        SelfCast = true;
        IgnoreStuns = true;
        Helpful = true;
        Buff = true;

        ActiveEffectOwner = EvanoreConstants.Name;
        ActiveEffectName = EvanoreConstants.UmbraWolfActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetIgnoreStunEffectsPoints(Duration1, this, target);
        totalPoints += T.GetPercentageReductionPoints(ReceiveDamageReductionPercent1, Duration1, target);
        return totalPoints;
    }
}
using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Jane.Champion;

namespace Ashcrown.Remake.Core.Champions.Jane.Abilities;

public class Benji : AbilityBase
{
    public Benji(IChampion champion) 
        : base(champion, 
            JaneConstants.Benji, 
            4,
            [0,0,0,0,1], 
            [AbilityClass.Physical, AbilityClass.Instant ], 
            AbilityTarget.Self, 
            AbilityType.AllyBuff, 
            1)
    {
        ReceiveDamageReductionPoint1 = 10;
        Duration1 = 4;
        Damage1 = 10;
        
        Description = $"{JaneConstants.Name} calls Benji to aid her. The following {Duration1} turns, she will gain {$"{ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()}. " +
                      $"During this time, this ability will be replaced by {JaneConstants.GoForTheThroat.HighlightInOrange()} and any enemy that uses a new harmful non-Affliction ability on {JaneConstants.Name} " +
                      $"will receive {$"{Damage1} physical damage".HighlightInOrange()}.";
        Helpful = true;
        Buff = true;
        SelfCast = true;
        Damaging = true;
        PhysicalDamage = true;

        ActiveEffectOwner = JaneConstants.Name;
        ActiveEffectName = JaneConstants.BenjiActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetPointsReductionPoints(ReceiveDamageReductionPoint1, Duration1, target);
        totalPoints += T.GetSpecialConditionPoints(Duration1);
        return totalPoints;
    }
}
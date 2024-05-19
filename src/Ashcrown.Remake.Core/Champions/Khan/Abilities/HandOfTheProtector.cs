using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Khan.Champion;

namespace Ashcrown.Remake.Core.Champions.Khan.Abilities;

public class HandOfTheProtector : AbilityBase
{
    public HandOfTheProtector(IChampion champion) 
        : base(champion, 
            KhanConstants.HandOfTheProtector, 
            4,
            [0,0,1,0,0], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Self, 
            AbilityType.AllyBuff, 
            3)
    {
        Duration1 = 2;
        Heal1 = 15;
        BonusDamage1 = -10;
        
        Description = $"This ability makes {KhanConstants.Name} {"invulnerable".HighlightInPurple()} for {Duration1} turns and will heal him {$"{Heal1} health".HighlightInGreen()} each turn. " +
                      $"{KhanConstants.MortalStrike.HighlightInGold()} will deal {$"{BonusDamage1} LESS physical damage".HighlightInOrange()} during this time.";
        SelfCast = true;
        Invulnerability = true;
        TypeOfInvulnerability = [AbilityClass.All];
        Helpful = true;
        Buff = true;
        Healing = true;

        ActiveEffectOwner = KhanConstants.Name;
        ActiveEffectName = KhanConstants.HandOfTheProtectorActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetInvulnerabilityPoints(Duration1, this, target);
        totalPoints += T.GetHealingPoints(Heal1, Duration1, this, target);
        return totalPoints;
    }
}
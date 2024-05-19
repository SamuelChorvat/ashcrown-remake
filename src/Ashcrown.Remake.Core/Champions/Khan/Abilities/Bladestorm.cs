using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Khan.Champion;

namespace Ashcrown.Remake.Core.Champions.Khan.Abilities;

public class Bladestorm : AbilityBase
{
    public Bladestorm(IChampion champion) 
        : base(champion, 
            KhanConstants.Bladestorm, 
            0,
            [0,0,1,0,0], 
            [AbilityClass.Physical,AbilityClass.Melee,AbilityClass.Action], 
            AbilityTarget.Enemies, 
            AbilityType.EnemiesActionControl, 
            2)
    {
        Duration1 = 3;
        Damage1 = 5;
        BonusDamage1 = 10;
        
        Description = $"{KhanConstants.Name} will deal {$"{Damage1} physical damage".HighlightInOrange()} to all enemies for {Duration1} turns. During this time, " +
                      $"{KhanConstants.MortalStrike.HighlightInGold()} will deal an additional {$"{BonusDamage1} physical damage".HighlightInOrange()}.";
        SelfDisplay = true;
        Debuff = true;
        Harmful = true;
        Damaging = true;
        PhysicalDamage = true;

        ActiveEffectOwner = KhanConstants.Name;
        ActiveEffectSourceName = KhanConstants.BladestormMeActiveEffect;
        ActiveEffectTargetName = KhanConstants.BladestormTargetActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, Duration1, this, target);
        return totalPoints;
    }

    public override int CalculateSingletonSelfEffectTotalPoints<T>()
    {
        var totalPoints = BonusDamage1;
        return totalPoints;
    }
}
using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Fae.Champion;

namespace Ashcrown.Remake.Core.Champions.Fae.Abilities;

public class Corruption : AbilityBase
{
    public Corruption(IChampion champion) 
        : base(champion, 
            FaeConstants.Corruption, 
            1,
            [0,0,0,0,1], 
            [AbilityClass.Affliction, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDebuff, 
            2)
    {
        Damage1 = 10;
        Duration1 = 3;
        
        Description = $"{FaeConstants.Name} corrupts one enemy who takes {$"{Damage1} affliction damage".HighlightInRed()} for {Duration1} turns. " +
                      $"This ability may not be used on en enemy already affected by it.";
        Harmful = true;
        Damaging = true;
        AfflictionDamage = true;
        Debuff = true;
        UniqueActiveEffect = true;
        ActiveEffectOwner = FaeConstants.Name;
        ActiveEffectName = FaeConstants.CorruptionActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, Duration1, this, target);
        return totalPoints;
    }
}
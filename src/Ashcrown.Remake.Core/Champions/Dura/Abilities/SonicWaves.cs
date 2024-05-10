using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Dura.Champion;

namespace Ashcrown.Remake.Core.Champions.Dura.Abilities;

public class SonicWaves : AbilityBase
{
    public SonicWaves(IChampion champion) 
        : base(champion, 
            DuraConstants.SonicWaves, 
            1,
            [1,0,0,0,1], 
            [AbilityClass.Physical, AbilityClass.Action, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyActionControl, 
            1)
    {
        Damage1 = 25;
        DealDamageReductionPoint1 = 5;
        Duration1 = 2;
        
        Description = $"{DuraConstants.Name} deals {$"{Damage1} physical damage".HighlightInOrange()} to one enemy for {Duration1} turns. " +
                      $"During this time, that enemy will deal 5 less damage with any non-Affliction ability.";
        Harmful = true;
        Debuff = true;
        PhysicalDamage = true;
        Damaging = true;
        ActiveEffectOwner = DuraConstants.Name;
        ActiveEffectSourceName = DuraConstants.SonicWavesMeActiveEffect;
        ActiveEffectTargetName = DuraConstants.SonicWavesTargetActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, Duration1, this, target);
        totalPoints += 10;
        return totalPoints;
    }
}
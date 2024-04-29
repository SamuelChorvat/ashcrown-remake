using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Akio.Champion;

namespace Ashcrown.Remake.Core.Champions.Akio.Abilities;

public class MasterfulSlice : AbilityBase
{
    public MasterfulSlice(IChampion champion) : 
        base(champion, 
            AkioConstants.MasterfulSlice, 
            1,
            [0,0,0,0,1], 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDebuff, 
            1)
    {
        Duration1 = 3;
        Damage1 = 10;
        Damage2 = 5;
        Description = $"{AkioConstants.Name} cuts one enemy. " +
                      $"For {Duration1} turns, that enemy will receive {$"{Damage1} physical damage".HighlightInOrange()} " +
                      $"and if they use new non-Strategic ability during this time, they will receive {$"{Damage2} physical damage".HighlightInOrange()}";
        Harmful = true;
        Debuff = true;
        Damaging = true;
        PhysicalDamage = true;
        ActiveEffectOwner = AkioConstants.Name;
        ActiveEffectName = AkioConstants.MasterfulSliceActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, Duration1, this, target);
        totalPoints += 20;
        return totalPoints;
    }
}
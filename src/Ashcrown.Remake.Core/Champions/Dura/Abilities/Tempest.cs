using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Dura.Champion;

namespace Ashcrown.Remake.Core.Champions.Dura.Abilities;

public class Tempest : AbilityBase
{
    public Tempest(IChampion champion) 
        : base(champion, 
            DuraConstants.Tempest, 
            1,
            [1,0,1,0,0], 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            3)
    {
        Damage1 = 40;
        
        Description = $"{DuraConstants.Name} deals {$"{Damage1} physical damage".HighlightInOrange()} to one enemy " +
                      $"and {"removes".HighlightInPurple()} <sprite=4> from them.";
        Harmful = true;
        PhysicalDamage = true;
        Damaging = true;
        EnergyRemove = true;
        EnergyAmount = 1;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        totalPoints += T.GetRemoveEnergyPoints(EnergyAmount, 1, this, target);
        return totalPoints;
    }
}
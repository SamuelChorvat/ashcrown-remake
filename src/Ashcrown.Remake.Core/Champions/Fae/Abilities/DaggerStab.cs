using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Fae.Champion;

namespace Ashcrown.Remake.Core.Champions.Fae.Abilities;

public class DaggerStab : AbilityBase
{
    public DaggerStab(IChampion champion) 
        : base(champion, 
            FaeConstants.DaggerStab, 
            0,
            [0,0,0,0,1], 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            1)
    {
        Damage1 = 15;
        
        Description = $"{FaeConstants.Name} stabs one enemy, dealing {$"{Damage1} physical damage".HighlightInOrange()} to them.";
        Harmful = true;
        PhysicalDamage = true;
        Damaging = true;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        return totalPoints;
    }
}
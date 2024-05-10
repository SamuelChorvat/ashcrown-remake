using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Dex.Champion;

namespace Ashcrown.Remake.Core.Champions.Dex.Abilities;

public class ShurikenThrow : AbilityBase
{
    public ShurikenThrow(IChampion champion) 
        : base(champion, 
            DexConstants.ShurikenThrow, 
            0,
            [0,0,0,0,2], 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemies, 
            Ability.Enums.AbilityType.EnemiesDamage, 
            1)
    {
        Damage1 = 15;
        
        Description = $"{DexConstants.Name} throws shurikens, dealing {$"{Damage1} physical damage".HighlightInOrange()} to all enemies. " +
                      $"While {DexConstants.Nightblade.HighlightInOrange()} is active this ability's cost is reduced by <sprite=4>.";
        Harmful = true;
        Damaging = true;
        PhysicalDamage = true;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        return totalPoints;
    }
}
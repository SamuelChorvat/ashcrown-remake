using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Branley.Champion;

namespace Ashcrown.Remake.Core.Champions.Branley.Abilities;

public class FireTheCannons : AbilityBase
{
    public FireTheCannons(IChampion champion) 
        : base(champion, 
            BranleyConstants.FireTheCannons, 
            3,
            [0,0,1,0,0], 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemies, 
            AbilityType.EnemiesDamageAndDebuff, 
            2)
    {
        Damage1 = 10;
        Duration1 = 1;
        
        Description = $"{BranleyConstants.Name} fires at all enemies, dealing {$"{Damage1} physical damage".HighlightInOrange()} " +
                      $"to them and {"stunning".HighlightInPurple()} their Physical and Affliction abilities for {Duration1} turn.";
        Stun = true;
        StunType = [AbilityClass.Physical, AbilityClass.Affliction];
        Harmful = true;
        Debuff = true;
        PhysicalDamage = true;
        Damaging = true;
        ActiveEffectOwner = BranleyConstants.Name;
        ActiveEffectName = BranleyConstants.FireTheCannonsActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        totalPoints += T.GetStunPoints(this, Duration1, target);
        return totalPoints;
    }
}
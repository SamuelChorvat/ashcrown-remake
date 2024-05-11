using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cleo.Champion;

namespace Ashcrown.Remake.Core.Champions.Cleo.Abilities;

public class VenomousSting : AbilityBase
{
    public VenomousSting(IChampion champion) 
        : base(champion, 
            CleoConstants.VenomousSting, 
            0,
            [0,0,1,0,1], 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee],
            AbilityTarget.Enemy, 
            AbilityType.EnemiesDamageAndDebuff, 
            1)
    {
        Damage1 = 30;
        Duration1 = 1;
        
        Description = $"{CleoConstants.Name} stings one enemy, dealing {$"{Damage1} physical damage".HighlightInOrange()} to them " +
                      $"and {"stunning".HighlightInPurple()} their Physical and Strategic abilities for {Duration1} turn.";
        Stun = true;
        StunType = [AbilityClass.Physical, AbilityClass.Strategic];
        Harmful = true;
        Debuff = true;
        PhysicalDamage = true;
        Damaging = true;
        ActiveEffectOwner = CleoConstants.Name;
        ActiveEffectName = CleoConstants.VenomousStingActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        totalPoints += T.GetStunPoints(this, Duration1, target);
        return totalPoints;
    }
}
using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Jane.Champion;

namespace Ashcrown.Remake.Core.Champions.Jane.Abilities;

public class CounterShot : AbilityBase
{
    public CounterShot(IChampion champion) 
        : base(champion, 
            JaneConstants.CounterShot, 
            0,
            [0,0,1,0,1], 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamageAndDebuff, 
            2)
    {
        Damage1 = 30;
        Duration1 = 2;
        
        Description = $"{JaneConstants.Name} shoots one enemy, dealing {$"{Damage1} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()} to them. " +
                      $"For {Duration2} turns all new {"stun".HighlightInPurple()} abilities that the enemy uses will be decreased by 2 turns.";
        Harmful = true;
        Debuff = true;
        PhysicalDamage = true;
        PiercingDamage = true;
        Damaging = true;

        ActiveEffectOwner = JaneConstants.Name;
        ActiveEffectName = JaneConstants.CounterShotActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        totalPoints += T.GetReduceStunDurationPoints(Duration1, this, target);
        return totalPoints;
    }
}
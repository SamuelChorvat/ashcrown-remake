using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Jane.Champion;

namespace Ashcrown.Remake.Core.Champions.Jane.Abilities;

public class GoForTheThroat : AbilityBase
{
    public GoForTheThroat(IChampion champion) 
        : base(champion, 
            JaneConstants.GoForTheThroat, 
            0,
            [0,0,1,0,0], 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            1)
    {
        Damage1 = 25;
        
        Description = $"{JaneConstants.Name} instructs Benji to attack an enemy, dealing {$"{Damage1} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()} to them.";
        Harmful = true;
        PhysicalDamage = true;
        Damaging = true;
        PiercingDamage = true;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        return totalPoints;
    }
}
using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Jafali.Champion;

namespace Ashcrown.Remake.Core.Champions.Jafali.Abilities;

public class Pride : AbilityBase
{
    public Pride(IChampion champion) 
        : base(champion, 
            JafaliConstants.Pride, 
            2,
            [0,2,0,0,1], 
            [AbilityClass.Affliction, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            3)
    {
        Damage1 = 20;
        
        Description = $"{JafaliConstants.Name} deals {$"{Damage1} affliction damage".HighlightInRed()} to one enemy " +
                      $"and {"removes".HighlightInPurple()} <sprite=4> for each {JafaliConstants.DecayingSoul.HighlightInOrange()} on them.";
        Harmful = true;
        AfflictionDamage = true;
        Damaging = true;
        EnergyRemove = true;
        EnergyAmount = 0;
    }

    public override void StartTurnChecks()
    {
        EnergyAmount = 0;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        totalPoints += T.GetRemoveEnergyPoints(target.ActiveEffectController
            .GetActiveEffectCountByName(JafaliConstants.DecayingSoul), 1, this, target);
        return totalPoints;
    }

    //TODO Refactor this?
    public static int GetEnergyAmount(IChampion target)
    {
        return target.ActiveEffectController.GetActiveEffectCountByName(JafaliConstants.DecayingSoulActiveEffect);
    }
}
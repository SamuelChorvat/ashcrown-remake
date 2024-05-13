using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Gruber.Champion;

namespace Ashcrown.Remake.Core.Champions.Gruber.Abilities;

public class ExplosiveLeech : AbilityBase
{
    public ExplosiveLeech(IChampion champion) 
        : base(champion, 
            GruberConstants.ExplosiveLeech, 
            0, 
            new int[] {0,0,0,0,0}, 
            [AbilityClass.Affliction, AbilityClass.Instant], 
            AbilityTarget.Ally, 
            AbilityType.AllyBuff, 
            3)
    {
        Damage1 = 20;
        Damage2 = 10;
        
        Description = $"{GruberConstants.Name} hides an explosive leech on an ally. " +
                      $"The first enemy champion to use a new harmful ability on that ally will take {$"{Damage1} affliction damage".HighlightInRed()}; " +
                      $"that ally will also take {$"{Damage2} affliction damage".HighlightInRed()}. This damage {"ignores invulnerability".HighlightInPurple()}. " +
                      $"This ability is {"invisible".HighlightInPurple()} and can stack.";
        Harmful = true;
        Debuff = true;
        Damaging = true;
        AfflictionDamage = true;
        Invisible = true;
        ActiveEffectOwner = GruberConstants.Name;
        ActiveEffectName = GruberConstants.ExplosiveLeechActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = Damage1 * 2 + AiCalculatorConstants.BaseDamagePoints;
        totalPoints += 100 - target.Health;
        return totalPoints;
    }
}
using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Lucifer.Champion;

namespace Ashcrown.Remake.Core.Champions.Lucifer.Abilities;

public class CursedCrow : AbilityBase
{
    public CursedCrow(IChampion champion) 
        : base(champion, 
            LuciferConstants.CursedCrow, 
            0,
            [0,0,0,0,0], 
            [AbilityClass.Strategic, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDebuff, 
            3)
    {
        Damage1 = 10;
        
        Description = $"{LuciferConstants.Name} targets one enemy. {LuciferConstants.DarkChalice.HighlightInOrange()} will deal an additional {$"{Damage1} magic damage".HighlightInBlue()} to that enemy permanently. " +
                      $"This effect stacks. This ability is {"invisible".HighlightInPurple()}.";
        Harmful = true;
        Debuff = true;
        Invisible = true;

        ActiveEffectOwner = LuciferConstants.Name;
        ActiveEffectName = LuciferConstants.CursedCrowActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = (int)(Damage1 + AiCalculatorConstants.BaseDamagePoints 
                                        + Math.Round((double)(100 - target.Health)/2));
        return totalPoints;
    }
}
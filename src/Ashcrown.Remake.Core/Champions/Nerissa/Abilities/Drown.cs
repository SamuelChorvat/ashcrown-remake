using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Nerissa.Champion;

namespace Ashcrown.Remake.Core.Champions.Nerissa.Abilities;

public class Drown : AbilityBase
{
    public Drown(IChampion champion) 
        : base(champion, 
            NerissaConstants.Drown, 
            0,
            [1,0,0,0,0], 
            [AbilityClass.Affliction, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDebuff, 
            2)
    {
        Damage1 = 10;
        
        Description = $"{NerissaConstants.Name} drowns one enemy. " +
                      $"They will receive {$"{Damage1} affliction damage".HighlightInRed()} for the rest of the game and {NerissaConstants.Overflow.HighlightInGold()} " +
                      $"will deal an additional {$"5 {"piercing".HighlightInBold()} magic damage".HighlightInBlue()} to them. " +
                      $"This ability cannot target enemy already affected by it and will end if Nerissa dies.";
        Harmful = true;
        Damaging = true;
        AfflictionDamage = true;
        Debuff = true;
        UniqueActiveEffect = true;

        ActiveEffectOwner = NerissaConstants.Name;
        ActiveEffectName = NerissaConstants.DrownActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, AiCalculatorConstants.InfiniteNumberOfTurns, this, target);
        return totalPoints;
    }
}
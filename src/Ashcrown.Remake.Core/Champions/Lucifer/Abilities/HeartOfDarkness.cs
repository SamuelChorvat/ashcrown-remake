using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Lucifer.Champion;

namespace Ashcrown.Remake.Core.Champions.Lucifer.Abilities;

public class HeartOfDarkness : AbilityBase
{
    public HeartOfDarkness(IChampion champion) 
        : base(champion, 
            LuciferConstants.HeartOfDarkness, 
            0,
            [0,0,0,0,0], 
            [AbilityClass.Strategic, AbilityClass.Instant ], 
            AbilityTarget.Self, 
            AbilityType.AllyBuff, 
            1)
    {
        Description = $"This ability affects {LuciferConstants.ShadowBolts.HighlightInGold()}. " +
                      $"First use changes Rank 1 to Rank 2. Second use changes Rank 2 to Rank 3. Third use changes Rank 3 back to Rank 1. " +
                      $"This ability is {"invisible".HighlightInPurple()}.";
        Helpful = true;
        Buff = true;
        SelfCast = true;
        Invisible = true;

        ActiveEffectOwner = LuciferConstants.Name;
        ActiveEffectName = LuciferConstants.HeartOfDarknessActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var heartOfDarknessAe = Owner.ActiveEffectController.GetActiveEffectByName(LuciferConstants.HeartOfDarknessActiveEffect);

        int totalPoints;
        if (heartOfDarknessAe == null || heartOfDarknessAe.Description.Contains("Rank 1") || heartOfDarknessAe.Description.Contains("Rank 2")) {
            totalPoints = T.GetSpecialConditionPoints(AiCalculatorConstants.InfiniteNumberOfTurns);
        } else {
            totalPoints = 0;
        }

        return totalPoints;
    }
}
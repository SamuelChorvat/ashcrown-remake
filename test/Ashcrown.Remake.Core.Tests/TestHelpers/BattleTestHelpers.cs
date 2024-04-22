using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound;

namespace Ashcrown.Remake.Core.Tests.TestHelpers;

public static class BattleTestHelpers
{
    public static EndTurn CreateEndTurnWithOneAbilityUsed(int casterNo, int abilityNo, int[] targets, int[] spentEnergy)
    {
        var endTurn = new EndTurn
        {
            EndTurnAbilities = new List<EndTurnAbility>(),
            SpentEnergy = spentEnergy
        };
        endTurn.EndTurnAbilities.Add(new EndTurnAbility()
        {
            Order = 1,
            CasterNo = casterNo,
            AbilityNo = abilityNo,
            Targets = targets
        });
        return endTurn;
    }

    public static void PassNumberOfTurns(int currentTurnPlayerNo, IBattleLogic battleLogic, int numberOfTurnsToPass)
    {
        while (numberOfTurnsToPass > 0) {
            if (currentTurnPlayerNo == 1) {
                battleLogic.EndTurnProcesses(1);
                currentTurnPlayerNo = 2;
            } else {
                battleLogic.EndTurnProcesses(2);
                currentTurnPlayerNo = 1;
            }
            numberOfTurnsToPass -= 1;
        }
    }
}
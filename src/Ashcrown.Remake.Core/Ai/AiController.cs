using Ashcrown.Remake.Core.Ai.Interfaces;
using Ashcrown.Remake.Core.Ai.Models;
using Ashcrown.Remake.Core.Battle.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Ai;

public class AiController(
    IBattleLogic battleLogic,
    IAiAbilitySelector aiAbilitySelector,
    IAiEnergySelector aiEnergySelector,
    ILogger<AiController> logger) : IAiController
{
    public void EndBattleTurn()
    {
        IList<AiMaximizedAbility> selectedAbilities = new List<AiMaximizedAbility>();
        try {
            selectedAbilities = aiAbilitySelector.SelectAbilities<AiUtils,AiPointsCalculator>();
        } catch (Exception e) {
            battleLogic.EndBattleOnAiError("AI exception selecting abilities -> " + e);
            logger.LogError(e, "Exception message -> {Message}", e.Message);
        }

        if (selectedAbilities.Count > 0) {
            var selectedEnergyToSpend = aiEnergySelector.SelectEnergyToSpend(selectedAbilities);
            if (!battleLogic.GetAiOpponentBattlePlayer().SpendEnergy(selectedEnergyToSpend)) {
                battleLogic.EndBattleOnAiError("Error spending AI selected energy");
            }

            foreach (var selectedAbility in selectedAbilities) {
                logger.LogDebug("AI selected ability -> {AbilityName}", selectedAbility.Ability?.Name);
            }

            var packedAbilities = AiUtils.PackSelectedAbilities(selectedAbilities, selectedEnergyToSpend);
            try {
                if(!battleLogic.AbilitiesUsed(battleLogic.GetAiOpponentPlayerNo(), packedAbilities, 
                       selectedEnergyToSpend)) {
                    battleLogic.EndBattleOnAiError("Error using AI selected abilities!");
                }
            } catch (Exception e) {
                battleLogic.EndBattleOnAiError("Ai exception using abilities -> " + e);
                logger.LogError(e,"Exception message -> {Message}", e.Message);
                logger.LogError("Ability history string -> {AbilityHistoryString}",
                    battleLogic.BattleHistoryRecorder.GetAbilityHistoryString());
            }
        }
        
        try {
            battleLogic.EndTurnProcesses(battleLogic.GetAiOpponentPlayerNo());
        } catch (Exception e) {
            battleLogic.EndBattleOnAiError("AI exception during end turn processes! -> " + e);
            logger.LogError(e, "Exception message -> {Message}", e.Message);
        }
    }
}
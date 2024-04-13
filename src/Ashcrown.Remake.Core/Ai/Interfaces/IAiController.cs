using Ashcrown.Remake.Core.Battle.Interfaces;

namespace Ashcrown.Remake.Core.Ai.Interfaces;

public interface IAiController
{
    IBattleLogic BattleLogic { get; init; }
    void EndBattleTurn();
    void EndMatchOnAiError(string errorMessage);
}
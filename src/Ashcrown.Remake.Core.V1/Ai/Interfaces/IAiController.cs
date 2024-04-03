using Ashcrown.Remake.Core.V1.Battle.Interfaces;

namespace Ashcrown.Remake.Core.V1.Ai.Interfaces;

public interface IAiController
{
    IBattleLogic BattleLogic { get; init; }
    void EndBattleTurn();
    void EndMatchOnAiError();
}
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Battle.Interfaces;

public interface IBattleEngine
{
    IEnumerable<IChampion> ChampionDeaths { get; init; }
    IEnumerable<IBattleParticipant> BattleParticipants { get; init; }
    int TurnCount { get; init; }
    DateTime StartTime { get; init; }
    DateTime EndTime { get; }
    bool IsAiBattle { get; init; }

    IBattleParticipant GetOpponentOf(IBattleParticipant battleParticipant);
    IAiBattleParticipant GetAiParticipant();
    void ProcessDeaths();
    void InitializeParticipants();
    void EndTurnProcesses();
}
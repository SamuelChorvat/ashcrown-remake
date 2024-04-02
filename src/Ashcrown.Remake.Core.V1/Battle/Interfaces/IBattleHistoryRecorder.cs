using Ashcrown.Remake.Core.V1.Ability.Models;

namespace Ashcrown.Remake.Core.V1.Battle.Interfaces;

public interface IBattleHistoryRecorder
{
    IList<AbilityHistoryRecord> AbilityHistoryRecords { get; init; }
    void RecordInAbilityHistory(UsedAbility[] used);
}
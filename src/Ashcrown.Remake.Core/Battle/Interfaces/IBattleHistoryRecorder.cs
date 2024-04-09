using Ashcrown.Remake.Core.Ability.Models;

namespace Ashcrown.Remake.Core.Battle.Interfaces;

public interface IBattleHistoryRecorder
{
    IList<AbilityHistoryRecord> AbilityHistoryRecords { get; init; }
    void RecordInAbilityHistory(UsedAbility?[] used);
    string GetAbilityHistoryString();
}
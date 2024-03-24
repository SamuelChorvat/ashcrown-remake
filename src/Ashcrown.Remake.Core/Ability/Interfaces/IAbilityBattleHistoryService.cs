using Ashcrown.Remake.Core.Ability.Models;

namespace Ashcrown.Remake.Core.Ability.Interfaces;

public interface IAbilityBattleHistoryService
{
    IEnumerable<AbilityHistoryRecord> AbilityHistoryRecords { get; init;}
    
    void RecordInAbilityHistory(IEnumerable<UsedAbility> usedAbilities);

    string GetAbilityHistoryString();
}
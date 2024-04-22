using Ashcrown.Remake.Core.Ai.Interfaces;
using Ashcrown.Remake.Core.Ai.Models;

namespace Ashcrown.Remake.Core.Ability.Interfaces;

public interface IAbilityController
{
    IAbility? LastUsedAbility { get; }
    bool UsedNewAbility { get; }
    bool AiAbilitySelected { set; get; }
    bool UseAbility(IAbility ability, int[] targets);
    bool ClientCanUseAbilityChecks(IAbility ability);
    bool IsStunnedToUseAbility(IAbility ability);
    IAbility GetMyAbilityByName(string abilityName);
    IAbility GetCurrentAbility(int abilityNo);
    void StartTurnFieldsReset();
    void TickDownAbilitiesCooldowns();
    int GetNumberOfTargets(IEnumerable<int> targets);
    int[] GetPossibleTargetsForAbility(int abilityNo);
    int[] GetUsableAbilities(int[] currentEnergy, int toSubtract);
    void SetAiActiveAbilities(int[] currentResources, int toSubtract); //TODO Move to AI?
    AiMaximizedAbility? GetBestMaximizedAbility<TAiUtils,TAiPointsCalculator>() where TAiUtils : IAiUtils 
        where TAiPointsCalculator : IAiPointsCalculator; //TODO Move to AI?
}
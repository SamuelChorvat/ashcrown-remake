using Ashcrown.Remake.Core.V1.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.V1.Ai.Models;
using Ashcrown.Remake.Core.V1.Champion.Interfaces;

namespace Ashcrown.Remake.Core.V1.Ability.Interfaces;

public interface IAbilityController
{
    IChampion Owner { get; init; }
    IAbility? LastUsedAbility { get; }
    bool UsedNewAbility { get; }
    bool AiAbilitySelected { set; }
    bool UseAbility(IAbility ability, int[] targets);
    bool ClientCanUseAbilityChecks(IAbility ability);
    bool IsStunnedToUseAbility(IAbility ability);
    IAbility GetMyAbilityByName(string abilityName);
    IAbility GetCurrentAbility(int abilityNo);
    void StartTurnFieldsReset();
    void TickDownAbilitiesCooldowns();
    int GetNumberOfTargets(int[] targets);
    int[] GetPossibleTargetsForAbility(int abilityNo);
    int[] GetUsableAbilities(int[] currentEnergy, int toSubtract);
    void SetAiActiveAbilities(int[] currentResources, int toSubtract); //TODO Move to AI?
    AiMaximizedAbility GetBestMaximizedAbility(); //TODO Move to AI?
    bool IsAiAbilitySelected(); //TODO Move to AI?
}
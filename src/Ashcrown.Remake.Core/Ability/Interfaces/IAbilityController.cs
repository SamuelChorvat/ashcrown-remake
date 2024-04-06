﻿using Ashcrown.Remake.Core.Ai.Interfaces;
using Ashcrown.Remake.Core.Ai.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;

namespace Ashcrown.Remake.Core.Ability.Interfaces;

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
    int GetNumberOfTargets(IEnumerable<int> targets);
    int[] GetPossibleTargetsForAbility(int abilityNo);
    int[] GetUsableAbilities(int[] currentEnergy, int toSubtract);
    void SetAiActiveAbilities(int[] currentResources, int toSubtract); //TODO Move to AI?
    AiMaximizedAbility? GetBestMaximizedAbility<T>() where T : IAiUtils; //TODO Move to AI?
}
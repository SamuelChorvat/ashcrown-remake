using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.ActiveEffect.Interfaces;

public interface IActiveEffectController
{
    IChampion Owner { get; init; }
    void ApplyActiveEffects();
    void AddActiveEffect(IActiveEffect activeEffect);
    void RemoveActiveEffect(IActiveEffect activeEffect);
    void PauseActiveEffect(IActiveEffect activeEffect); //On this champion
    bool ActiveEffectPresentByOriginAbilityName(string abilityName);
    bool ActiveEffectPresentByActiveEffectName(string abilityName);
    void RemoveEnemyAfflictions();
    void RemoveEnemyHarmfulEffects();
    void RemoveMyActiveEffectsOnStunFromAll();
    void RemoveMyActiveEffectsOnDeathFromAll();
    void PauseMyActiveEffectsOnStunFromAll();
    void RemoveChampionsActiveEffectsOnStun(IChampion fromChampion);
    void RemoveChampionsActiveEffectsOnDeath(IChampion fromChampion);
    void RemoveActiveEffectsOnInvulnerability();
    void PauseActiveEffectsOnInvulnerability();
    void PauseChampionsActiveEffectsOnStun(IChampion fromChampion);
    void ClearActiveEffects();
    void ClearMarkedActiveEffects();
    void AddActiveEffectModifiersOnAddNew(IActiveEffect activeEffect);
    void AddActiveEffectModifiersOnResume(IActiveEffect activeEffect);
    void RemoveActiveEffectModifiersOnRemovePresent(IActiveEffect activeEffect);
    void RemoveActiveEffectModifiersOnPausePresent(IActiveEffect activeEffect);
    IActiveEffect? GetActiveEffectByName(string activeEffectName, int championNo, int playerNo);
    IActiveEffect? GetActiveEffectByName(string activeEffectName);
    IList<IActiveEffect> GetActiveEffectsByName(string activeEffectName);
    IActiveEffect? GetLastActiveEffectByName(string activeEffectName);
    int GetActiveEffectCountByName(string activeEffectName);
    IList<IActiveEffect> GetCurrentActiveEffectsSeparately();
    void CheckResume();
}
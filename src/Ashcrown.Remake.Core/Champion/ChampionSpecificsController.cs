using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champion;

public class ChampionSpecificsController(IChampion owner) : IChampionSpecificsController
{
    public void StartTurnChampionSpecificsChecks()
    {
        owner.ActiveEffectController.CheckResume();
        StartTurnAbilityChecks();
        StartTurnActiveEffectChecks();
    }

    public void EndTurnChampionSpecificsChecks()
    {
        owner.ActiveEffectController.CheckResume();
        EndTurnAbilityChecks();
        EndTurnActiveEffectChecks();
    }

    private void StartTurnAbilityChecks()
    {
        for (var i = 0; i < ChampionConstants.MaxNumberOfCurrentAbilities; i++) {
            foreach(var ability in owner.Abilities[i]) {
                ability.StartTurnChecks();
            }
        }
    }
    
    private void StartTurnActiveEffectChecks()
    {
        var currentActiveEffects = owner.ActiveEffectController.GetCurrentActiveEffectsSeparately();
        foreach (var activeEffect in currentActiveEffects) {
            activeEffect.StartTurnChecks();
        }
    }
    
    private void EndTurnAbilityChecks()
    {
        for (var i = 0; i < ChampionConstants.MaxNumberOfCurrentAbilities; i++) {
            foreach(var ability in owner.Abilities[i]) {
                ability.EndTurnChecks();
            }
        }
    }

    private void EndTurnActiveEffectChecks()
    {
        var currentActiveEffects = owner.ActiveEffectController.GetCurrentActiveEffectsSeparately();
        foreach (var activeEffect in currentActiveEffects) {
            activeEffect.EndTurnChecks();
            if (activeEffect is {Stun: true, Reflected: true}) {
                owner.ChampionController.OnStun(activeEffect.OriginAbility);
            }
            if (activeEffect is {Source: true, ChildrenLinks.Count: 0, Reflected: true}) {
                owner.ActiveEffectController.RemoveActiveEffect(activeEffect);
            }
        }
    }
}
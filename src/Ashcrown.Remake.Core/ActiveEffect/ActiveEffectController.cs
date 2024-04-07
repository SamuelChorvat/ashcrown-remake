using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Microsoft.Extensions.Logging;
using IActiveEffect = Ashcrown.Remake.Core.ActiveEffect.Interfaces.IActiveEffect;

namespace Ashcrown.Remake.Core.ActiveEffect;

using IActiveEffect = Interfaces.IActiveEffect;

public class ActiveEffectController(
    IChampion owner,
    ILogger<ActiveEffectController> logger) : IActiveEffectController
{
    public required IChampion Owner { get; init; } = owner;
    public void ApplyActiveEffects()
    {
        if (!Owner.Alive) {
            return;
        }

        var currentActiveEffects = GetCurrentActiveEffectsSeparately();
        foreach (var activeEffect in currentActiveEffects) {
            logger.LogDebug( "Apply: {ActiveEffectName}", activeEffect.Name);
            if (activeEffect is { Stun: true, Paused: false }) {
                Owner.ChampionController.OnStun(activeEffect.OriginAbility);
            }
            activeEffect.OnApply();
        }
    }

    public void AddActiveEffect(IActiveEffect activeEffect)
    {
        if (!AddActiveEffectChecks(activeEffect)) {
            if (activeEffect.CasterLink == null) return;
            activeEffect.CasterLink.ChildrenLinks.Remove(activeEffect);
            if (activeEffect.CasterLink.ChildrenLinks.Count == 0 && activeEffect.EndsOnTargetDeath) {
                activeEffect.CasterLink.OriginAbility.Owner.ActiveEffectController.RemoveActiveEffect(activeEffect.CasterLink);
            }

            return;
        }

        logger.LogDebug("{OwnerName}: {ActiveEffectName} added", Owner.Name, activeEffect.Name);

        activeEffect.Target = Owner;
        Owner.ActiveEffects.Add(activeEffect);
		
        AddActiveEffectModifiersOnAddNew(activeEffect);
		
        activeEffect.OnAdd();
		
        if (activeEffect is { Stun: true, Reflected: false }) {
            Owner.ChampionController.OnStun(activeEffect.OriginAbility);
        }
		
        if (activeEffect.Invulnerability) {
            Owner.ChampionController.OnInvulnerability(activeEffect.OriginAbility);
        }	
    }

    public void RemoveActiveEffect(IActiveEffect activeEffect)
    {
        if (!RemoveActiveEffectChecks(activeEffect)) {
            logger.LogDebug("Remove ActiveEffect check triggered!");
            return;
        }

        logger.LogDebug("Removed: {ActiveEffectName}", activeEffect.Name);
		
        activeEffect.OnRemove();
		
        RemoveActiveEffectModifiersOnRemovePresent(activeEffect);
		
        if (activeEffect.CasterLink != null) {
            activeEffect.CasterLink.ChildrenLinks.Remove(activeEffect);
            if (activeEffect.CasterLink.ChildrenLinks.Count == 0 && activeEffect.EndsOnTargetDeath) {
                activeEffect.CasterLink.OriginAbility.Owner.ActiveEffectController.RemoveActiveEffect(activeEffect.CasterLink);
            }
        }
		
        Owner.ActiveEffects.Remove(activeEffect);
    }

    public void PauseActiveEffect(IActiveEffect activeEffect)
    {
        if (!PauseActiveEffectChecks(activeEffect)) {
            return;
        }
		
        RemoveActiveEffectModifiersOnPausePresent(activeEffect);
		
        activeEffect.Paused = true;
    }

    public bool ActiveEffectPresentByOriginAbilityName(string abilityName)
    {
        return Owner.ActiveEffects.Any(activeEffect => activeEffect.OriginAbility.Name.Equals(abilityName));
    }

    public bool ActiveEffectPresentByActiveEffectName(string abilityName)
    {
        return Owner.ActiveEffects.Any(activeEffect => activeEffect.Name.Equals(abilityName));
    }

    public void RemoveEnemyAfflictions()
    {
        var currentActiveEffects = GetCurrentActiveEffectsSeparately();
        foreach (var activeEffect in currentActiveEffects) {
            if (activeEffect.OriginAbility.Owner.BattlePlayer.PlayerNo != Owner.BattlePlayer.PlayerNo &&
                activeEffect.OriginAbility.AbilityClassesContains(AbilityClass.Affliction) &&
                !activeEffect.CannotBeRemoved ) {

                RemoveActiveEffect(activeEffect);
            }
        }
    }

    public void RemoveEnemyHarmfulEffects()
    {
        var currentActiveEffects = GetCurrentActiveEffectsSeparately();
        foreach (var activeEffect in currentActiveEffects) {
            if (activeEffect.OriginAbility.Owner.BattlePlayer.PlayerNo != Owner.BattlePlayer.PlayerNo &&
                activeEffect is { Harmful: true, CannotBeRemoved: false } ) {

                RemoveActiveEffect(activeEffect);
            }
        }
    }

    public void RemoveMyActiveEffectsOnStunFromAll()
    {
        for (var i = 1; i <= 2; i++) {
            for (var j = 0; j < 3; j++) {
                if (Owner.BattleLogic.GetBattlePlayer(i).Champions[j].Alive 
                    && Owner != Owner.BattleLogic.GetBattlePlayer(i).Champions[j]) {
                    Owner.BattleLogic.GetBattlePlayer(i).Champions[j]
                        .ActiveEffectController.RemoveChampionsActiveEffectsOnStun(Owner);
                }
            }
        }
        if (Owner.Alive) {
            Owner.ActiveEffectController.RemoveChampionsActiveEffectsOnStun(Owner);
        }
    }

    public void RemoveMyActiveEffectsOnDeathFromAll()
    {
        for (var i = 1; i <= 2; i++) {
            for (var j = 0; j < 3; j++) {
                if (Owner.BattleLogic.GetBattlePlayer(i).Champions[j].Alive) {
                    Owner.BattleLogic.GetBattlePlayer(i).Champions[j]
                        .ActiveEffectController.RemoveChampionsActiveEffectsOnDeath(Owner);
                }
            }
        }
    }

    public void PauseMyActiveEffectsOnStunFromAll()
    {
        for (var i = 1; i <= 2; i++) {
            for (var j = 0; j < 3; j++) {
                if (Owner.BattleLogic.GetBattlePlayer(i).Champions[j].Alive 
                    && Owner != Owner.BattleLogic.GetBattlePlayer(i).Champions[j]) {
                    Owner.BattleLogic.GetBattlePlayer(i).Champions[j]
                        .ActiveEffectController.PauseChampionsActiveEffectsOnStun(Owner);
                }
            }
        }
        if (Owner.Alive) {
            Owner.ActiveEffectController.PauseChampionsActiveEffectsOnStun(Owner);
        }
    }

    public void RemoveChampionsActiveEffectsOnStun(IChampion fromChampion)
    {
        var currentActiveEffects = GetCurrentActiveEffectsSeparately();
        foreach (var activeEffect in currentActiveEffects) {
            if (activeEffect.OriginAbility.Owner.BattlePlayer.PlayerNo == fromChampion.BattlePlayer.PlayerNo &&
                activeEffect.OriginAbility.Owner.ChampionNo == fromChampion.ChampionNo &&
                activeEffect.OriginAbility.Owner.Name.Equals(fromChampion.Name) &&
                fromChampion.AbilityController.IsStunnedToUseAbility(activeEffect.OriginAbility) &&
                activeEffect.EndsOnCasterStun) {

                RemoveActiveEffect(activeEffect);
            }
        }
    }

    public void RemoveChampionsActiveEffectsOnDeath(IChampion fromChampion)
    {
        var currentActiveEffects = GetCurrentActiveEffectsSeparately();
        foreach (var activeEffect in currentActiveEffects) {
            if (activeEffect.OriginAbility.Owner.BattlePlayer.PlayerNo == fromChampion.BattlePlayer.PlayerNo &&
                activeEffect.OriginAbility.Owner.ChampionNo == fromChampion.ChampionNo &&
                activeEffect.OriginAbility.Owner.Name.Equals(fromChampion.Name) &&
                fromChampion.AbilityController.IsStunnedToUseAbility(activeEffect.OriginAbility) &&
                activeEffect.EndsOnCasterDeath) {

                RemoveActiveEffect(activeEffect);
            }
        }
    }

    public void RemoveActiveEffectsOnInvulnerability()
    {
        var currentActiveEffects = GetCurrentActiveEffectsSeparately();
        foreach (var activeEffect in currentActiveEffects) {
            if (activeEffect is { EndsOnTargetInvulnerability: true, ChildrenLinks.Count: 0 } &&
                Owner.ChampionController.IsInvulnerableTo(activeEffect.OriginAbility) &&
                !activeEffect.Source) {
                RemoveActiveEffect(activeEffect);
            }
        }
    }

    public void PauseActiveEffectsOnInvulnerability()
    {
        foreach (var activeEffect in Owner.ActiveEffects)
        {
            if (activeEffect is { PauseOnTargetInvulnerability: true, ChildrenLinks.Count: 0 } &&
                Owner.ChampionController.IsInvulnerableTo(activeEffect.OriginAbility) &&
                !activeEffect.Source) {
				
                PauseActiveEffect(activeEffect);
            }
        }
    }

    public void PauseChampionsActiveEffectsOnStun(IChampion fromChampion)
    {
        foreach (var activeEffect in Owner.ActiveEffects)
        {
            if (activeEffect.OriginAbility.Owner.BattlePlayer.PlayerNo == fromChampion.BattlePlayer.PlayerNo && 
                activeEffect.OriginAbility.Owner.ChampionNo == fromChampion.ChampionNo &&
                activeEffect.OriginAbility.Owner.Name.Equals(fromChampion.Name) && 
                fromChampion.AbilityController.IsStunnedToUseAbility(activeEffect.OriginAbility) &&
                activeEffect.PauseOnCasterStun) {
				
                PauseActiveEffect(activeEffect);
            }
        }
    }

    public void ClearActiveEffects()
    {
        var currentActiveEffects = GetCurrentActiveEffectsSeparately();
        foreach (var activeEffect in currentActiveEffects)
        {
            if (!activeEffect.EndsOnTargetDeath ||
                activeEffect.CasterLink == null) continue;
            activeEffect.CasterLink.ChildrenLinks.Remove(activeEffect);
            if (activeEffect.CasterLink.ChildrenLinks.Count == 0) {
                activeEffect.CasterLink.OriginAbility.Owner.ActiveEffectController.RemoveActiveEffect(activeEffect.CasterLink);
            }
        }
		
        while (Owner.ActiveEffects.Count > 0) {
            RemoveActiveEffect(Owner.ActiveEffects[0]);
        }
    }

    public void ClearMarkedActiveEffects()
    {
        var currentActiveEffects = GetCurrentActiveEffectsSeparately();
        foreach (var activeEffect in currentActiveEffects) {
            if (activeEffect.RemoveIt) {
                RemoveActiveEffect(activeEffect);
            }
        }
    }

    public void AddActiveEffectModifiersOnAddNew(IActiveEffect activeEffect)
    {
        Owner.ChampionController.AddActiveEffectModifiers(activeEffect);
    }

    public void AddActiveEffectModifiersOnResume(IActiveEffect activeEffect)
    {
        Owner.ChampionController.AddActiveEffectModifiers(activeEffect);
    }

    public void RemoveActiveEffectModifiersOnRemovePresent(IActiveEffect activeEffect)
    {
        Owner.ChampionController.RemoveActiveEffectModifiers(activeEffect);
    }

    public void RemoveActiveEffectModifiersOnPausePresent(IActiveEffect activeEffect)
    {
        Owner.ChampionController.RemoveActiveEffectModifiers(activeEffect);
    }

    public IActiveEffect? GetActiveEffectByName(string activeEffectName, int championNo, int playerNo)
    {
        return Owner.ActiveEffects.FirstOrDefault(
            activeEffect => activeEffect.Name.Equals(activeEffectName) 
                            && activeEffect.OriginAbility.Owner.ChampionNo == championNo 
                            && activeEffect.OriginAbility.Owner.BattlePlayer.PlayerNo == playerNo);
    }

    public IActiveEffect? GetActiveEffectByName(string activeEffectName)
    {
        return Owner.ActiveEffects.FirstOrDefault(activeEffect => activeEffect.Name.Equals(activeEffectName));
    }

    public IList<IActiveEffect> GetActiveEffectsByName(string activeEffectName)
    {
        return Owner.ActiveEffects.Where(activeEffect => activeEffect.Name.Equals(activeEffectName)).ToList();
    }

    public IActiveEffect? GetLastActiveEffectByName(string activeEffectName)
    {
        IActiveEffect? toReturn = null;

        foreach (var activeEffect in Owner.ActiveEffects) {
            if (activeEffect.Name.Equals(activeEffectName)) {
                toReturn = activeEffect;
            }
        }
		
        return toReturn;
    }

    public int GetActiveEffectCountByName(string activeEffectName)
    {
        return Owner.ActiveEffects.Count(activeEffect => activeEffect.Name.Equals(activeEffectName));
    }

    public IList<IActiveEffect> GetCurrentActiveEffectsSeparately()
    {
        return Owner.ActiveEffects.ToList();
    }

    public void CheckResume()
    {
        foreach (var activeEffect in Owner.ActiveEffects)
        {
            if (!activeEffect.Paused) continue;
            if (activeEffect.CasterLink != null)
            {
                switch (activeEffect.PauseOnCasterStun)
                {
                    case true when
                        activeEffect.PauseOnTargetInvulnerability:
                    {
                        if (!activeEffect.CasterLink.OriginAbility.Owner
                                .AbilityController.IsStunnedToUseAbility(activeEffect.OriginAbility) &&
                            !Owner.ChampionController.IsInvulnerableTo(activeEffect.OriginAbility)) {

                            logger.LogDebug("Resumed 1");
                            activeEffect.Paused = false;
                            AddActiveEffectModifiersOnResume(activeEffect);
                        }

                        break;
                    }
                    case true:
                    {
                        if (!activeEffect.CasterLink.OriginAbility.Owner.
                                AbilityController.IsStunnedToUseAbility(activeEffect.OriginAbility)) {

                            logger.LogDebug("Resumed 2");
                            activeEffect.Paused = false;
                            AddActiveEffectModifiersOnResume(activeEffect);
                        }

                        break;
                    }
                    default:
                    {
                        if (activeEffect.PauseOnTargetInvulnerability) {
                            if (!Owner.ChampionController.IsInvulnerableTo(activeEffect.OriginAbility)) {

                                logger.LogDebug("Resumed 3");
                                activeEffect.Paused = false;
                                AddActiveEffectModifiersOnResume(activeEffect);
                            }
                        }

                        break;
                    }
                }
            } else
            {
                if (Owner.AbilityController.IsStunnedToUseAbility(activeEffect.OriginAbility)) continue;
                activeEffect.Paused = false;
                AddActiveEffectModifiersOnResume(activeEffect);
            }
        }
    }

    private bool AddActiveEffectChecks(IActiveEffect? activeEffect)
    {
        if (activeEffect == null) {
            return false;
        }
		
        if(!Owner.Alive) {
            return false;
        }
		
        if(activeEffect is { TimeLeft: < 1, Infinite: false }) {
            return false;
        }
		
        if(activeEffect.OriginAbility.Owner.BattlePlayer.PlayerNo != Owner.BattlePlayer.PlayerNo) {
            if(Owner.ChampionController.IsInvulnerableTo(activeEffect.OriginAbility)) {
                return false;
            }
        }
		
        if (activeEffect.Unique && ActiveEffectPresentByActiveEffectName(activeEffect.Name)) {
            return false;
        }

        if (!activeEffect.Stackable) return activeEffect.ActiveEffectChecks();
        if (!ActiveEffectPresentByActiveEffectName(activeEffect.Name)) return activeEffect.ActiveEffectChecks();
        GetActiveEffectByName(activeEffect.Name)!.AddStack(activeEffect);
        return false;
    }

    private bool RemoveActiveEffectChecks(IActiveEffect? activeEffect)
    {
        return activeEffect != null && Owner.ActiveEffects.Contains(activeEffect);
    }
    
    private bool PauseActiveEffectChecks(IActiveEffect? activeEffect)
    {
        if (activeEffect == null) {
            return false;
        }
		
        return Owner.Alive && Owner.ActiveEffects.Contains(activeEffect);
    }
}
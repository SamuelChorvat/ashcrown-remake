using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.ActiveEffect;

public class ActiveEffectController(
    IChampion owner,
    ILogger<ActiveEffectController> logger) : IActiveEffectController
{
    public void ApplyActiveEffects()
    {
        if (!owner.Alive) {
            return;
        }

        var currentActiveEffects = GetCurrentActiveEffectsSeparately();
        foreach (var activeEffect in currentActiveEffects) {
            logger.LogDebug( "Apply: {ActiveEffectName}", activeEffect.Name);
            if (activeEffect is { Stun: true, Paused: false }) {
                owner.ChampionController.OnStun(activeEffect.OriginAbility);
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

        logger.LogDebug("{OwnerName}: {ActiveEffectName} added", owner.Name, activeEffect.Name);

        activeEffect.Target = owner;
        owner.ActiveEffects.Add(activeEffect);
		
        AddActiveEffectModifiersOnAddNew(activeEffect);
		
        activeEffect.OnAdd();
		
        if (activeEffect is { Stun: true, Reflected: false }) {
            owner.ChampionController.OnStun(activeEffect.OriginAbility);
        }
		
        if (activeEffect.Invulnerability) {
            owner.ChampionController.OnInvulnerability(activeEffect.OriginAbility);
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
		
        owner.ActiveEffects.Remove(activeEffect);
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
        return owner.ActiveEffects.Any(activeEffect => activeEffect.OriginAbility.Name.Equals(abilityName));
    }

    public bool ActiveEffectPresentByActiveEffectName(string abilityName)
    {
        return owner.ActiveEffects.Any(activeEffect => activeEffect.Name.Equals(abilityName));
    }

    public void RemoveEnemyAfflictions()
    {
        var currentActiveEffects = GetCurrentActiveEffectsSeparately();
        foreach (var activeEffect in currentActiveEffects) {
            if (activeEffect.OriginAbility.Owner.BattlePlayer.PlayerNo != owner.BattlePlayer.PlayerNo &&
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
            if (activeEffect.OriginAbility.Owner.BattlePlayer.PlayerNo != owner.BattlePlayer.PlayerNo &&
                activeEffect is { Harmful: true, CannotBeRemoved: false } ) {

                RemoveActiveEffect(activeEffect);
            }
        }
    }

    public void RemoveMyActiveEffectsOnStunFromAll()
    {
        for (var i = 1; i <= 2; i++) {
            for (var j = 0; j < 3; j++) {
                if (owner.BattleLogic.GetBattlePlayer(i).Champions[j].Alive 
                    && owner != owner.BattleLogic.GetBattlePlayer(i).Champions[j]) {
                    owner.BattleLogic.GetBattlePlayer(i).Champions[j]
                        .ActiveEffectController.RemoveChampionsActiveEffectsOnStun(owner);
                }
            }
        }
        if (owner.Alive) {
            owner.ActiveEffectController.RemoveChampionsActiveEffectsOnStun(owner);
        }
    }

    public void RemoveMyActiveEffectsOnDeathFromAll()
    {
        for (var i = 1; i <= 2; i++) {
            for (var j = 0; j < 3; j++) {
                if (owner.BattleLogic.GetBattlePlayer(i).Champions[j].Alive) {
                    owner.BattleLogic.GetBattlePlayer(i).Champions[j]
                        .ActiveEffectController.RemoveChampionsActiveEffectsOnDeath(owner);
                }
            }
        }
    }

    public void PauseMyActiveEffectsOnStunFromAll()
    {
        for (var i = 1; i <= 2; i++) {
            for (var j = 0; j < 3; j++) {
                if (owner.BattleLogic.GetBattlePlayer(i).Champions[j].Alive 
                    && owner != owner.BattleLogic.GetBattlePlayer(i).Champions[j]) {
                    owner.BattleLogic.GetBattlePlayer(i).Champions[j]
                        .ActiveEffectController.PauseChampionsActiveEffectsOnStun(owner);
                }
            }
        }
        if (owner.Alive) {
            owner.ActiveEffectController.PauseChampionsActiveEffectsOnStun(owner);
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
                owner.ChampionController.IsInvulnerableTo(activeEffect.OriginAbility) &&
                !activeEffect.Source) {
                RemoveActiveEffect(activeEffect);
            }
        }
    }

    public void PauseActiveEffectsOnInvulnerability()
    {
        foreach (var activeEffect in owner.ActiveEffects)
        {
            if (activeEffect is { PauseOnTargetInvulnerability: true, ChildrenLinks.Count: 0 } &&
                owner.ChampionController.IsInvulnerableTo(activeEffect.OriginAbility) &&
                !activeEffect.Source) {
				
                PauseActiveEffect(activeEffect);
            }
        }
    }

    public void PauseChampionsActiveEffectsOnStun(IChampion fromChampion)
    {
        foreach (var activeEffect in owner.ActiveEffects)
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
		
        while (owner.ActiveEffects.Count > 0) {
            RemoveActiveEffect(owner.ActiveEffects[0]);
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
        owner.ChampionController.AddActiveEffectModifiers(activeEffect);
    }

    public void AddActiveEffectModifiersOnResume(IActiveEffect activeEffect)
    {
        owner.ChampionController.AddActiveEffectModifiers(activeEffect);
    }

    public void RemoveActiveEffectModifiersOnRemovePresent(IActiveEffect activeEffect)
    {
        owner.ChampionController.RemoveActiveEffectModifiers(activeEffect);
    }

    public void RemoveActiveEffectModifiersOnPausePresent(IActiveEffect activeEffect)
    {
        owner.ChampionController.RemoveActiveEffectModifiers(activeEffect);
    }

    public IActiveEffect? GetActiveEffectByName(string activeEffectName, int championNo, int playerNo)
    {
        return owner.ActiveEffects.FirstOrDefault(
            activeEffect => activeEffect.Name.Equals(activeEffectName) 
                            && activeEffect.OriginAbility.Owner.ChampionNo == championNo 
                            && activeEffect.OriginAbility.Owner.BattlePlayer.PlayerNo == playerNo);
    }

    public IActiveEffect? GetActiveEffectByName(string activeEffectName)
    {
        return owner.ActiveEffects.FirstOrDefault(activeEffect => activeEffect.Name.Equals(activeEffectName));
    }

    public IList<IActiveEffect> GetActiveEffectsByName(string activeEffectName)
    {
        return owner.ActiveEffects.Where(activeEffect => activeEffect.Name.Equals(activeEffectName)).ToList();
    }

    public IActiveEffect? GetLastActiveEffectByName(string activeEffectName)
    {
        IActiveEffect? toReturn = null;

        foreach (var activeEffect in owner.ActiveEffects) {
            if (activeEffect.Name.Equals(activeEffectName)) {
                toReturn = activeEffect;
            }
        }
		
        return toReturn;
    }

    public int GetActiveEffectCountByName(string activeEffectName)
    {
        return owner.ActiveEffects.Count(activeEffect => activeEffect.Name.Equals(activeEffectName));
    }

    public IList<IActiveEffect> GetCurrentActiveEffectsSeparately()
    {
        return owner.ActiveEffects.ToList();
    }

    public void CheckResume()
    {
        foreach (var activeEffect in owner.ActiveEffects)
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
                            !owner.ChampionController.IsInvulnerableTo(activeEffect.OriginAbility)) {

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
                            if (!owner.ChampionController.IsInvulnerableTo(activeEffect.OriginAbility)) {

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
                if (owner.AbilityController.IsStunnedToUseAbility(activeEffect.OriginAbility)) continue;
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
		
        if(!owner.Alive) {
            return false;
        }
		
        if(activeEffect is { TimeLeft: < 1, Infinite: false }) {
            return false;
        }
		
        if(activeEffect.OriginAbility.Owner.BattlePlayer.PlayerNo != owner.BattlePlayer.PlayerNo) {
            if(owner.ChampionController.IsInvulnerableTo(activeEffect.OriginAbility)) {
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
        return activeEffect != null && owner.ActiveEffects.Contains(activeEffect);
    }
    
    private bool PauseActiveEffectChecks(IActiveEffect? activeEffect)
    {
        if (activeEffect == null) {
            return false;
        }
		
        return owner.Alive && owner.ActiveEffects.Contains(activeEffect);
    }
}
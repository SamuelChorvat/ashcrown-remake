using Ashcrown.Remake.Core.V1.Ability.Enums;
using Ashcrown.Remake.Core.V1.Ability.Interfaces;
using Ashcrown.Remake.Core.V1.Ability.Models;
using Ashcrown.Remake.Core.V1.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.V1.Ai.Models;
using Ashcrown.Remake.Core.V1.Battle.Models;
using Ashcrown.Remake.Core.V1.Champion.Interfaces;

namespace Ashcrown.Remake.Core.V1.Ability;

public class AbilityController(
    IChampion owner,
    IAbilityFactory abilityFactory,
    IActiveEffectFactory activeEffectFactory) : IAbilityController
{
    private readonly IAbilityFactory _abilityFactory = abilityFactory;

    public required IChampion Owner { get; init; } = owner;

    public IAbility? LastUsed { get; private set; }

    public bool UsedNewAbility { get; private set; }

    public bool AiAbilitySelected { get; set; }
    
    public bool UseAbility(IAbility ability, int[] targets)
    {
        return ability.AbilityType switch
        {
            AbilityType.EnemyDebuff => UseEnemyDebuff(targets, ability.Name, ability.ActiveEffectOwner,
                ability.ActiveEffectName),
            AbilityType.AllyHeal => UseAllyHeal(targets, ability.Name),
            AbilityType.AlliesHeal => UseAlliesHeal(targets, ability.Name),
            AbilityType.EnemiesDebuff => UseEnemiesDebuff(targets, ability.Name, ability.ActiveEffectOwner,
                ability.ActiveEffectName),
            AbilityType.AllyBuff => UseAllyBuff(targets, ability.Name, ability.ActiveEffectOwner,
                ability.ActiveEffectName),
            AbilityType.AlliesBuff => UseAlliesBuff(targets, ability.Name, ability.ActiveEffectOwner,
                ability.ActiveEffectName),
            AbilityType.AlliesBuffEnemiesDebuff => UseAlliesBuffEnemiesDebuff(targets, ability.Name,
                ability.ActiveEffectOwner, ability.ActiveEffectAlliesName, ability.ActiveEffectEnemiesName),
            AbilityType.EnemiesDamageAndDebuff => UseEnemiesDamageAndDebuff(targets, ability.Name,
                ability.ActiveEffectOwner, ability.ActiveEffectName),
            AbilityType.EnemyDamageAndDebuff => UseEnemyDamageAndDebuff(targets, ability.Name,
                ability.ActiveEffectOwner, ability.ActiveEffectName),
            AbilityType.EnemyDamage => UseEnemyDamage(targets, ability.Name),
            AbilityType.EnemiesDamage => UseEnemiesDamage(targets, ability.Name),
            AbilityType.EnemyEnergySteal => UseEnemyEnergySteal(targets, ability.Name),
            AbilityType.EnemyActionControl => UseEnemyActionControl(targets, ability.Name, ability.ActiveEffectOwner,
                ability.ActiveEffectSourceName, ability.ActiveEffectTargetName),
            AbilityType.EnemiesActionControl => UseEnemiesActionControl(targets, ability.Name,
                ability.ActiveEffectOwner, ability.ActiveEffectSourceName, ability.ActiveEffectTargetName),
            AbilityType.AllyOrEnemyActiveEffect => UseAllyOrEnemyActiveEffect(targets, ability.Name,
                ability.ActiveEffectOwner, ability.ActiveEffectAllyName, ability.ActiveEffectEnemyName),
            AbilityType.AllyActiveEffectOrEnemyDamage => UseAllyActiveEffectOrEnemyDamage(targets, ability.Name,
                ability.ActiveEffectOwner, ability.ActiveEffectName),
            AbilityType.AllyOrEnemyDamage => UseAllyOrEnemyDamage(targets, ability.Name),
            _ => throw new ArgumentOutOfRangeException(nameof(ability), "Ability type out of range")
        };
    }

    private bool UseAllyOrEnemyDamage(int[] targets, string abilityName)
    {
        throw new NotImplementedException();
    }

    private bool UseAllyActiveEffectOrEnemyDamage(int[] targets, string abilityName, string? abilityActiveEffectOwner, string? abilityActiveEffectName)
    {
        throw new NotImplementedException();
    }

    private bool UseAllyOrEnemyActiveEffect(int[] targets, string abilityName, string? abilityActiveEffectOwner, string? abilityActiveEffectAllyName, string? abilityActiveEffectEnemyName)
    {
        throw new NotImplementedException();
    }

    private bool UseEnemiesActionControl(int[] targets, string abilityName, string? abilityActiveEffectOwner, string? abilityActiveEffectSourceName, string? abilityActiveEffectTargetName)
    {
        throw new NotImplementedException();
    }

    private bool UseEnemyActionControl(int[] targets, string abilityName, string? abilityActiveEffectOwner, string? abilityActiveEffectSourceName, string? abilityActiveEffectTargetName)
    {
        throw new NotImplementedException();
    }

    private bool UseEnemyEnergySteal(int[] targets, string abilityName)
    {
        throw new NotImplementedException();
    }

    private bool UseEnemiesDamage(int[] targets, string abilityName)
    {
        throw new NotImplementedException();
    }

    private bool UseEnemyDamage(int[] targets, string abilityName)
    {
        throw new NotImplementedException();
    }

    private bool UseEnemyDamageAndDebuff(int[] targets, string abilityName, string? abilityActiveEffectOwner, string? abilityActiveEffectName)
    {
        throw new NotImplementedException();
    }

    private bool UseEnemiesDamageAndDebuff(int[] targets, string abilityName, string? abilityActiveEffectOwner, string? abilityActiveEffectName)
    {
        throw new NotImplementedException();
    }

    private bool UseAlliesBuffEnemiesDebuff(int[] targets, string abilityName, string? abilityActiveEffectOwner, string? abilityActiveEffectAlliesName, string? abilityActiveEffectEnemiesName)
    {
        throw new NotImplementedException();
    }

    private bool UseAlliesBuff(int[] targets, string abilityName, string? abilityActiveEffectOwner, string? abilityActiveEffectName)
    {
        throw new NotImplementedException();
    }

    private bool UseAllyBuff(int[] targets, string abilityName, string? abilityActiveEffectOwner, string? abilityActiveEffectName)
    {
        throw new NotImplementedException();
    }

    private bool UseEnemiesDebuff(int[] targets, string abilityName, string? abilityActiveEffectOwner, string? abilityActiveEffectName)
    {
        throw new NotImplementedException();
    }

    private bool UseAlliesHeal(int[] targets, string abilityName)
    {
        throw new NotImplementedException();
    }

    private bool UseAllyHeal(int[] targets, string abilityName)
    {
        throw new NotImplementedException();
    }

    private bool UseEnemyDebuff(int[] targets, string abilityName, string? abilityActiveEffectOwner, string? abilityActiveEffectName)
    {
        if (!UseAbilityChecks(GetMyAbilityByName(abilityName), targets)) {
            return false;
        }
		
        var usedAbility = GetMyAbilityByName(abilityName);
		
        if (CounterOnMe(usedAbility, targets)) {
            return true;
        }

        var abilityReflected = ReflectOnEnemy(usedAbility, targets);
        if (!abilityReflected.Reflected && !abilityReflected.IsSelfReflected() && CounterOnEnemy(usedAbility, targets)) {
            return true;
        }

        for (var i = abilityReflected.Reflected ? 0 : 3; 
             i < (abilityReflected.Reflected ? targets.Length - 3 : targets.Length); i++)
        {
            if (targets[i] != 1) continue;
            var target = abilityReflected.Reflected ? Owner.BattlePlayer.Champions[i]
                : Owner.BattlePlayer.GetEnemyPlayer().Champions[i - 3];
            if (abilityReflected.Reflected) {
                target.ReceivedReflectedAbilities.Add(usedAbility);
            }

            var activeEffect = activeEffectFactory.CreateActiveEffect(abilityActiveEffectOwner!, 
                abilityActiveEffectName!, usedAbility, target);
            activeEffect.Reflected = abilityReflected.Reflected;

            target.ChampionController.TargetedByAbility(usedAbility);
            Owner.ChampionController.DealActiveEffect(target,
                usedAbility,
                activeEffect,
                false, new AppliedAdditionalLogic());

            usedAbility.OnUse();

            return true;
        }
		
        return false;
    }

    public bool ClientCanUseAbilityChecks(IAbility ability)
    {
        throw new NotImplementedException();
    }

    public bool IsStunnedToUseAbility(IAbility ability)
    {
        throw new NotImplementedException();
    }

    public IAbility GetMyAbilityByName(string abilityName)
    {
        throw new NotImplementedException();
    }

    public IAbility GetCurrentAbility(int abilityNo)
    {
        throw new NotImplementedException();
    }

    public void StartTurnFieldsReset()
    {
        throw new NotImplementedException();
    }

    public void TickDownAbilitiesCooldowns()
    {
        throw new NotImplementedException();
    }

    public int GetNumberOfTargets(int[] targets)
    {
        throw new NotImplementedException();
    }

    public int[] GetPossibleTargetsForAbility(int abilityNo)
    {
        throw new NotImplementedException();
    }

    public int[] GetUsableAbilities(int[] currentEnergy, int toSubtract)
    {
        throw new NotImplementedException();
    }

    public void SetAiActiveAbilities(int[] currentResources, int toSubtract)
    {
        throw new NotImplementedException();
    }

    public AiMaximizedAbility GetBestMaximizedAbility()
    {
        throw new NotImplementedException();
    }

    public bool IsAiAbilitySelected()
    {
        throw new NotImplementedException();
    }
    
    private bool CounterOnEnemy(IAbility usedAbility, int[] targets)
    {
        throw new NotImplementedException();
    }

    private AbilityReflected ReflectOnEnemy(IAbility usedAbility, int[] targets)
    {
        throw new NotImplementedException();
    }

    private bool CounterOnMe(IAbility usedAbility, int[] targets)
    {
        throw new NotImplementedException();
    }

    private bool UseAbilityChecks(IAbility ability, int[] targets)
    {
        throw new NotImplementedException();
    }
}
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Battle.Models;

namespace Ashcrown.Remake.Core.Champion.Interfaces;

//TODO Refactor this? Move some to IChampion and maybe create IModifierController instead?
public interface IChampionController
{
    PointsPercentageModifier TotalAllDamageDealReduce { get; set; }
    PointsPercentageModifier TotalAllDamageDealIncrease { get; set; }
    PointsPercentageModifier TotalAllDamageReceiveReduce { get; set; }
    PointsPercentageModifier TotalAllDamageReceiveIncrease { get; set; }
    
    PointsPercentageModifier TotalPhysicalDamageDealReduce { get; set; }
    PointsPercentageModifier TotalPhysicalDamageDealIncrease { get; set; }
    PointsPercentageModifier TotalPhysicalDamageReceiveReduce { get; set; }
    PointsPercentageModifier TotalPhysicalDamageReceiveIncrease { get; set; }
    
    PointsPercentageModifier TotalMagicDamageDealReduce { get; set; }
    PointsPercentageModifier TotalMagicDamageDealIncrease { get; set; }
    PointsPercentageModifier TotalMagicDamageReceiveReduce { get; set; }
    PointsPercentageModifier TotalMagicDamageReceiveIncrease { get; set; }
    
    PointsPercentageModifier TotalHealingDealReduce { get; set; }
    PointsPercentageModifier TotalHealingDealIncrease { get; set; }
    PointsPercentageModifier TotalHealingReceiveReduce { get; set; }
    PointsPercentageModifier TotalHealingReceiveIncrease { get; set; }
    
    void TargetedByAbility(IAbility ability);
    void ReceiveAbilityDamage(int amount, IAbility ability, bool secondary, AppliedAdditionalLogic appliedAdditionalLogic);
    void ReceiveActiveEffectDamage(int amount, IActiveEffect activeEffect, AppliedAdditionalLogic appliedAdditionalLogic);
    void ReceiveAbilityHealing(int amount, IAbility ability, AppliedAdditionalLogic appliedAdditionalLogic);
    void ReceiveActiveEffectHealing(int amount, IActiveEffect activeEffect, AppliedAdditionalLogic appliedAdditionalLogic);
    void DealAbilityDamage(int amount, IChampion target, IAbility ability, bool secondary, AppliedAdditionalLogic appliedAdditionalLogic);
    void DealActiveEffectDamage(int amount, IChampion target, IActiveEffect activeEffect, AppliedAdditionalLogic appliedAdditionalLogic);
    void DealDamageAndAddActiveEffect(int amount, IChampion target, IAbility ability, IActiveEffect activeEffect);
    void DealActiveEffect(IChampion target, IAbility ability, IActiveEffect activeEffect, bool secondary, AppliedAdditionalLogic appliedAdditionalLogic);
    void ReceiveActiveEffect(IActiveEffect activeEffect, bool secondary, AppliedAdditionalLogic appliedAdditionalLogic);
    void DealAbilityHealing(int amount, IChampion target, IAbility ability, AppliedAdditionalLogic appliedAdditionalLogic);
    void DealActiveEffectHealing(int amount, IChampion target, IActiveEffect activeEffect, AppliedAdditionalLogic appliedAdditionalLogic);
    void DealAbilityEnergySteal(IChampion target, IAbility ability, bool secondary, AppliedAdditionalLogic appliedAdditionalLogic);
    void DealActiveEffectEnergySteal(IChampion target, IActiveEffect activeEffect, AppliedAdditionalLogic appliedAdditionalLogic);
    void ReceiveAbilityEnergySteal(IAbility ability, int amount, bool secondary, AppliedAdditionalLogic appliedAdditionalLogic);
    void ReceiveActiveEffectEnergySteal(IActiveEffect activeEffect, int amount, AppliedAdditionalLogic appliedAdditionalLogic);
    int ApplyToDealDamageModifiers(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null);
    int ApplyToReceiveDamageModifiers(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null);
    int ApplyToDealHealingModifiers(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null);
    int ApplyToReceiveHealingModifiers(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null);
    void SetModifiers();
    int DealDamageModifiersNoDisables(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null);
    int DealDamageModifiersIgnoreDamageReduction(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null);
    int ReceiveDamageModifiersCaseNoDisables(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null);
    int ReceiveDamageModifiersCaseCannotReduceDamage(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null);
    int DealHealingModifiersCaseNoDisables(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null);
    int ReceiveHealingModifiersCaseNoDisables(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null);
    int ReceiveHealingModifiersCaseIgnoreHealingReduction(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null);
    int ApplyDestructibleDefense(int amount, IAbility? ability = null, IActiveEffect? activeEffect = null);
    void SubtractHealth(int toSubtract, AppliedAdditionalLogic appliedAdditionalLogic, 
        IAbility? ability = null, IActiveEffect? activeEffect = null);
    void AddHealth(int toAdd);
    void OnDeath();
    void ProcessDeath();
    void OnStun(IAbility ability);
    void OnInvulnerability(IAbility ability);
    bool IsIgnoringHealing();
    bool IsIgnoringStuns();
    bool IsIgnoringDamage();
    bool IsIgnoringHarmful();
    bool IsIgnoringReceivedDamageReduction();
    bool IsInvulnerabilityDisabled();
    bool IsInvulnerableToFriendlyAbility(IAbility? ability);
    bool IsInvulnerableTo(IAbility? ability = null, IActiveEffect? activeEffect = null, AbilityClass[]? abilityClasses = null);
    bool IsClientChampionInvulnerableTo(IAbility? ability);
    void EnemyDebuffMyBuff(IActiveEffect activeEffect, IAbility ability, string activeEffectOwnerName, string debuffName, string buffName, AppliedAdditionalLogic appliedAdditionalLogic);
    void EnemyDamageMyBuff(IAbility ability, string abilityName, string activeEffectOwnerName, string buffName, AppliedAdditionalLogic appliedAdditionalLogic);
    void DealReactionsCheck(IAbility ability, bool secondary);
    void ReceiveReactionsCheck(IAbility ability, bool secondary);
    void AddActiveEffectModifiers(IActiveEffect activeEffect);
    void RemoveActiveEffectModifiers(IActiveEffect activeEffect);
    void AddModifierOperation(PointsPercentageModifier modifierIncrease, PointsPercentageModifier modifierReduce, PointsPercentageModifier modifier);
    void RemoveModifierOperation(PointsPercentageModifier modifierIncrease, PointsPercentageModifier modifierReduce, PointsPercentageModifier modifier);
    void InitializeModifiers();
}
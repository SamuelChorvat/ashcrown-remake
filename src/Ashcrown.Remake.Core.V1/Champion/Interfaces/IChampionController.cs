using Ashcrown.Remake.Core.V1.Ability.Interfaces;
using Ashcrown.Remake.Core.V1.Ability.Models;
using Ashcrown.Remake.Core.V1.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.V1.Battle.Models;

namespace Ashcrown.Remake.Core.V1.Champion.Interfaces;

public interface IChampionController
{
    IChampion Owner { get; init; }
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
    int ApplyToDealDamageModifiers(int amount, IAbility ability, IActiveEffect activeEffect);
    int ApplyToReceiveDamageModifiers(int amount, IAbility ability, IActiveEffect activeEffect);
    int ApplyToDealHealingModifiers(int amount, IAbility ability, IActiveEffect activeEffect);
    int ApplyToReceiveHealingModifiers(int amount, IAbility ability, IActiveEffect activeEffect);
    void SetModifiers();
    int DealDamageModifiersNoDisables(int amount, IAbility ability, IActiveEffect activeEffect);
    int DealDamageModifiersIgnoreDamageReduction(int amount, IAbility ability, IActiveEffect activeEffect);
    int ReceiveDamageModifiersCaseNoDisables(int amount, IAbility ability, IActiveEffect activeEffect);
    int ReceiveDamageModifiersCaseCannotReduceDamage(int amount, IAbility ability, IActiveEffect activeEffect);
    int DealHealingModifiersCaseNoDisables(int amount, IAbility ability, IActiveEffect activeEffect);
    int ReceiveHealingModifiersCaseNoDisables(int amount, IAbility ability, IActiveEffect activeEffect);
    int ReceiveHealingModifiersCaseIgnoreHealingReduction(int amount, IAbility ability, IActiveEffect activeEffect);
    int ApplyDestructibleDefense(int amount, IAbility ability, IActiveEffect activeEffect);
    void SubtractHealth(int toSubtract, IAbility ability, IActiveEffect activeEffect, AppliedAdditionalLogic appliedAdditionalLogic);
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
    bool IsInvulnerableToFriendlyAbility(IAbility ability);
    bool IsInvulnerableTo(IAbility ability, IActiveEffect activeEffect);
    bool IsClientChampionInvulnerableTo(IAbility ability);
    void EnemyDebuffMyBuff(IActiveEffect activeEffect, IAbility ability, string activeEffectOwnerName, string debuffName, string buffName, AppliedAdditionalLogic appliedAdditionalLogic);
    void EnemyDamageMyBuff(IAbility ability, string abName, string activeEffectOwnerName, string buffName, AppliedAdditionalLogic appliedAdditionalLogic);
    void DealReactionsCheck(IAbility ability, bool secondary);
    void ReceiveReactionsCheck(IAbility ability, bool secondary);
    void AddActiveEffectModifiers(IActiveEffect activeEffect);
    void RemoveActiveEffectModifiers(IActiveEffect activeEffect);
    void AddModifierOperation(PointsPercentageModifier modifierIncreaseRef, PointsPercentageModifier modifierReduceRef, PointsPercentageModifier abRefModifier);
    void RemoveModifierOperation(PointsPercentageModifier modifierIncreaseRef, PointsPercentageModifier modifierReduceRef, PointsPercentageModifier abRefModifier);
    void InitializeModifiers();
}
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.ActiveEffect.Interfaces;

public interface IActiveEffect
{
    IAbility OriginAbility { get; set; }
    IChampion Target { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    int Duration { get; set; }
    int TimeLeft { get; set; }
    AbilityClass[]? ActiveEffectClasses { get; set; }
    int DestructibleDefense { get; set; }
    bool Hidden { get; set; }
    int Stacks { get; set; }
    bool Infinite { get; set; }
    IActiveEffect? CasterLink { get; set; }
    IList<IActiveEffect> ChildrenLinks { get; set; }
    bool Fresh { get; set; }
    bool Paused { get; set; }
    bool Source { get; set; }
    bool Unique { get; set; }
    bool Stackable { get; set; }
    bool NoTick { get; set; }
    bool Reflected { get; set; }
    bool IgnoreInvulnerability { get; set; }
    bool Harmful { get; set; }
    bool Helpful { get; set; }
    bool Debuff { get; set; }
    bool Buff { get; set; }
    bool CannotBeRemoved { get; set; }
    bool Healing { get; set; }
    bool Damaging { get; set; }
    bool Invulnerability { get; set; }
    AbilityClass[]? TypeOfInvulnerability { get; set; }
    bool Stun { get; set; }
    AbilityClass[]? StunType { get; set; }
    bool IgnoreHealing { get; set; }
    bool IgnoreDamage { get; set; }
    bool DisableInvulnerability { get; set; }
    bool DisableDamageReceiveReduction { get; set; }
    bool IgnoreStuns { get; set; }
    bool IgnoreHarmful { get; set; }
    bool PiercingDamage { get; set; }
    bool AfflictionDamage { get; set; }
    bool MagicDamage { get; set; }
    bool PhysicalDamage { get; set; }
    bool InvulnerableToFriendlyAbilities { get; set; }
    bool EndsOnCasterStun { get; set; }
    bool PauseOnCasterStun { get; set; }
    bool PauseOnTargetInvulnerability { get; set; }
    bool EndsOnCasterDeath { get; set; }
    bool EndsOnTargetInvulnerability { get; set; }
    bool EndsOnTargetDeath { get; set; }
    int EnergyAmount { get; set; }
    bool EnergySteal { get; set; }
    bool EnergyRemove { get; set; }
    int Damage1 { get; set; }
    int Damage2 { get; set; }
    int BonusDamage1 { get; set; }
    int Heal1 { get; set; }
    int ReceiveDamageReductionPoint1 { get; set; }
    int ReceiveDmgIncreasePoint1 { get; set; }
    int ReceiveDamageReductionPercent1 { get; set; }
    int DestructibleDefense1 { get; set; }
    int DealDamageReductionPoint1 { get; set; }
    int DealDmgIncreasePoint1 { get; set; }
    int DealHealIncreasePoint1 { get; set; }
    int DealHealReductionPercent1 { get; set; }
    int ReceiveHealReductionPercent1 { get; set; }
    int Duration1 { get; set; }
    int Duration2 { get; set; }
    PointsPercentageModifier? AllDamageDealModifier { get; set; }
    PointsPercentageModifier? PhysicalDamageDealModifier { get; set; }
    PointsPercentageModifier? MagicDamageDealModifier { get; set; }
    PointsPercentageModifier? AllDamageReceiveModifier { get; set; }
    PointsPercentageModifier? PhysicalDamageReceiveModifier { get; set; }
    PointsPercentageModifier? MagicDamageReceiveModifier { get; set; }
    PointsPercentageModifier? HealingDealModifier { get; set; }
    PointsPercentageModifier? HealingReceiveModifier { get; set; }
    bool RemoveIt { get; set; }

    string GetDescriptionWithTimeLeftAffix(int playerNo);
    string GetTimeLeftAffix(int? customTurnsLeft);
    string GetTimeLeftAffixActionControl();
    string GetActionControlDescription();
    void TickDown();
    bool WasCastedByMe(int playerNo); // TODO Swap all playerNo stuff with ref to Player instead?
    string GetAbilityName();
    bool IsHidden(int playerNo);
    bool InvulnerabilityContains(AbilityClass abilityClass);
    bool StunnedContains(AbilityClass abilityClass);
    void AddStack(IActiveEffect activeEffect);
    void StandardStack(IActiveEffect activeEffect);
    void DestructibleDefenseStack(IActiveEffect activeEffect);
    void OnAdd();
    void OnRemove();
    void OnApply();
    void OnApplyDot();
    void OnApplyActionControlMe();
    void OnApplyActionControlTargetDamage();
    
    int ReceiveActiveEffectDamageModifier(IChampion target, int amount);

    bool CustomDealActiveEffectLogic(IChampion dealer, IChampion target, IAbility ability, AppliedAdditionalLogic appliedAdditionalLogic);

    void AdditionalDealActiveEffectLogic(IChampion dealer, IChampion target, IAbility ability, bool secondary, AppliedAdditionalLogic appliedAdditionalLogic);

    void AdditionalReceiveActiveEffectLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic);

    void RemoveDestructibleDefense(IAbility? ability = null, IActiveEffect? activeEffect = null);

    void AdditionalSubtractHealthLogic(int toSubtract, IChampion victim, AppliedAdditionalLogic appliedAdditionalLogic);

    void AfterSubtractHealthLogic();

    void AdditionalProcessDeathLogic();

    void DealReaction(IAbility ability);

    void ReceiveReaction(IAbility ability);

    bool CounterOnEnemy(IAbility ability);

    bool ReflectOnEnemy(IAbility ability);

    bool CounterOnMe(IAbility ability);

    bool ActiveEffectChecks();

    void StartTurnChecks();

    void EndTurnChecks();

    void ModifyTargetsOnReflect(IAbility reflectedAbility, int[] targets);
}
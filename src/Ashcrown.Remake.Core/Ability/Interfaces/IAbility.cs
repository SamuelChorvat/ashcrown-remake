using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ai.Models;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Battle.Models.Dtos.Outbound;

namespace Ashcrown.Remake.Core.Ability.Interfaces;

public interface IAbility
{
	IChampion Owner { get; set; }
	string Name { get; set; }
	string Description { get; set; }
	int OriginalCooldown { get; set; }
	int[] OriginalCost { get; set; }
	AbilityClass[] AbilityClasses { get; set; }
	bool Active { get; set; }
	bool IgnoreInvulnerability { get; set; }
	bool Counterable { get; set; }
	bool Reflectable { get; set; }
	bool JustUsed { get; set; }
	bool Harmful { get; set; }
	bool Helpful { get; set; }
	bool Debuff { get; set; }
	bool Buff { get; set; }
	bool CannotBeRemoved { get; set; }
	bool Healing { get; set; }
	bool Damaging { get; set; }
	int EnergyAmount { get; set; }
	bool UniqueActiveEffect { get; set; }
	bool Invisible { get; set; }
	bool CostIncrease { get; set; }
	string[]? CostIncreaseClasses { get; set; }
	string[]? CostDecreaseClasses { get; set; }
	string[]? CooldownDecreaseClasses { get; set; }
	string[]? CounterClasses { get; set; }
	bool CannotBeIgnored { get; set; }
	bool Invulnerability { get; set; }
	string[]? TypeOfInvulnerability { get; set; }
	bool Stun { get; set; }
	string[]? StunType { get; set; }
	bool DisableInvulnerability { get; set; }
	bool DisableDamageReceiveReduction { get; set; }
	bool IgnoreStuns { get; set; }
	bool IgnoreHarmful { get; set; }
	bool PiercingDamage { get; set; }
	bool AfflictionDamage { get; set; }
	bool MagicDamage { get; set; }
	bool PhysicalDamage { get; set; }
	int ToReady { get; set; }
	int CooldownModifier { set; }
	int RandomCostModifier { set; }
	AbilityTarget Target { get; set; }
	bool SelfDisplay { get; set; }
	bool SelfCast { get; set; }
	int TimesUsed { get; set; } 
	bool Copied { get; set; }
	int Damage1 { get; set; }
	int Damage2 { get; set; }
	int Damage3 { get; set; }
	int BonusDamage1 { get; set; }
	int BonusDamage2 { get; set; }
	int Heal1 { get; set; }
	int BonusHeal1 { get; set; }
	int ReceiveDamageReductionPoint1 { get; set; }
	int ReceiveDamageIncreasePoint1 { get; set; }
	int ReceiveDamageReductionPercent1 { get; set; }
	int DestructibleDefense1 { get; set; }
	int DealDamageReductionPoint1 { get; set; }
	int DealDmgIncreasePoint1 { get; set; }
	int DealHealIncreasePoint1 { get; set; }
	int DealHealReductionPercent1 { get; set; }
	int ReceiveHealReductionPercent1 { get; set; }
	int Duration1 { get; set; }
	int Duration2 { get; set; }
	bool EnergySteal { get; set; }
	bool EnergyRemove { get; set; }
	bool DoNotModifyOnDealDamage { get; set; }
	AbilityType AbilityType { get; set; }
	string? ActiveEffectOwner { get; set; }
	string? ActiveEffectName { get; set; }
	string? ActiveEffectSourceName { get; set; }
	string? ActiveEffectTargetName { get; set; }
	string? ActiveEffectAlliesName { get; set; }
	string? ActiveEffectEnemiesName { get; set; }
	string? ActiveEffectAllyName { get; set; }
	string? ActiveEffectEnemyName { get; set; }
	bool AiStandardSelfInvulnerability { get; set; }
	bool AiActive { set; get; }
	
	int GetCurrentCooldown();
	int[] GetCurrentCost();
	bool IsReady();
	void PutOnCooldown();
	void TickDownCooldown();
	bool AbilityClassesContains(AbilityClass abilityClass);
	int TotalCost(IEnumerable<int> cost);
	void RemoveCostModifier(int mod);
	bool IsFree();
	int GetTotalCurrentCost();
	void RemoveCooldownModifier(int mod);
	int ReceiveAbilityDamageModifier(IChampion target, int amount);
	int DealAbilityHealingModifier(IChampion target, int amount);
	bool CustomReceiveAbilityDamageLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic);
	void AdditionalReceiveAbilityHealingLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic);
	int DealAbilityDamageModifier(IChampion target, int amount, bool secondary);
	void AdditionalDealAbilityDamageLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic);
	void AdditionalReceiveAbilityEnergyStealLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic);
	void AdditionalSubtractHealthLogic(int toSubtract, IChampion victim, AppliedAdditionalLogic appliedAdditionalLogic);
	int SubtractHealthModifier(int toSubtract, IChampion victim);
	bool UseChecks();
	int[] TargetsModifier(int[] targets); // TODO Was List
	void StartTurnChecks();
	void EndTurnChecks();
	void OnUse();
	AiMaximizedAbility AiMaximizeAbility();
	int[] GetPossibleTargets(); // TODO Was List && int[] emptyTargets not needed as input param
	int CalculateTotalPointsForTarget(IChampion target);
	
	// Use this for abilities like Dura's Whirlwind
	// Where there is an effect that we are not applying for each target but just once
	int CalculateSingletonSelfEffectTotalPoints();
	AbilityUpdate GetAbilityUpdate(int abilityNo);
}
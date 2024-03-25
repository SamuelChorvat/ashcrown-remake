using Ashcrown.Remake.Core.V1.Ability.Enums;
using Ashcrown.Remake.Core.V1.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.V1.Ai.Interfaces;
using Ashcrown.Remake.Core.V1.Ai.Models;
using Ashcrown.Remake.Core.V1.Battle.Models;
using Ashcrown.Remake.Core.V1.Champion.Interfaces;

namespace Ashcrown.Remake.Core.V1.Ability.Interfaces;

public interface IAbility
{
	IChampion Owner { get; set; }
	string Name { get; set; }
	string Description { get; set; }
	int OriginalCooldown { get; set; }
	int[] OriginalCost { get; set; }
	string[] Classes { get; set; }
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
	bool UniqueAe { get; set; }
	bool Invisible { get; set; }
	bool CostIncrease { get; set; }
	string[] CostIncreaseClasses { get; set; }
	string[] CostDecreaseClasses { get; set; }
	string[] CooldownDecreaseClasses { get; set; }
	string[] CounterClasses { get; set; }
	bool CannotBeIgnored { get; set; }

	// Moved from AE
	bool Invulnerability { get; set; }
	string[] TypeOfInvulnerability { get; set; }
	bool Stun { get; set; }
	string[] StunType { get; set; }
	bool DisableInvulnerability { get; set; }
	bool DisableDamageReceiveReduction { get; set; }
	bool IgnoreStuns { get; set; }
	bool IgnoreHarmful { get; set; }
	
	// Damage Type
	bool PiercingDamage { get; set; }
	bool AfflictionDamage { get; set; }
	bool MagicDamage { get; set; }
	bool PhysicalDamage { get; set; }
	
	int ToReady { get; set; }
	
	//Client info
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
	
	int ReceiveDmgReductPoint1 { get; set; }
	int ReceiveDmgIncreasePoint1 { get; set; }
	int ReceiveDmgReductPercent1 { get; set; }
	
	int DestructDef1 { get; set; }
	int DealDmgReductPoint1 { get; set; }
	int DealDmgIncreasePoint1 { get; set; }
	int DealHealIncreasePoint1 { get; set; }
	int DealHealReductPercent1 { get; set; }
	int ReceiveHealReductPercent1 { get; set; }
	
	int Duration1 { get; set; }
	int Duration2 { get; set; }
	bool EnergySteal { get; set; }
	bool EnergyRemove { get; set; }

	//Character Controller Flags
	bool DoNotModifyOnDealDamage { get; set; }
	
	//Ability Controller
	AbilityType AbilityType { get; set; }
	string AeOwner { get; set; }
	string AeName { get; set; }
	string AeSourceName { get; set; }
	string AeTargetName { get; set; }
	string AeAlliesName { get; set; }
	string AeEnemiesName { get; set; }
	string AeAllyName { get; set; }
	string AeEnemyName { get; set; }

	bool AiStandardSelfInvulnerability { get; set; }
	bool AiActive { set; }
	IAiAbilityHelper AiAbilityHelper { get; init; }
	
	int GetCurrentCd();
	int[] GetCurrentCost();
	bool IsReady();
	void PutOnCooldown();
	void TickDownCooldown();
	bool ClassContains(string className);
	int TotalCost(int[] cost);
	void SetCostModifier(int mod);
	void RemoveCostModifier(int mod);
	bool IsFree();
	int GetTotalCurrentCost();
	void SetCooldownModifier(int mod);
	void RemoveCooldownModifier(int mod);
	IList<int> GetCostAsList(); // TODO Remove?
	IList<string> GetAbClassAsList(); // TODO Remove?
	string GetClientTarget();
	int ReceiveAbilityDamageModifier(IChampion target, int amount);
	int DealAbilityHealingModifier(IChampion target, int amount);

	bool CustomReceiveAbilityDamageLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic);
	void AdditionalReceiveAbilityHealingLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic);
	int DealAbilityDamageModifier(IChampion target, int amount, bool secondary);
	void AdditionalDealAbilityDamageLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic);
	void AdditionalReceiveAbilityEnergyStealLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic);
	void AdditionalSubtractHealthLogic(int toSubtract, IActiveEffect activeEffect, IChampion victim, AppliedAdditionalLogic appliedAdditionalLogic);
	int SubtractHealthModifier(int toSubtract, IChampion victim);
	bool UseChecks();
	List<int> TargetsModifier(List<int> targets);
	void StartTurnChecks();
	void EndTurnChecks();
	void OnUse();
	bool IsInvisible();
	bool IsAiActive();
	AiMaximizedAbility AiMaximizeAbility();
	List<int> GetPossibleTargets(List<int> emptyTargets);
	int CalculateTotalPointsForTarget(IChampion target);
	
	// Use this for abilities like Dura's Whirlwind
	// Where there is an effect that we are not applying for each target but just once
	int CalculateSingletonSelfEffectTotalPoints();
	
	IAiAbilityHelper GetAiAbilityHelper();
}
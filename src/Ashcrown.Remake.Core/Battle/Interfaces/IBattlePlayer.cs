using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Battle.Models.Dtos;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Battle.Interfaces;

public interface IBattlePlayer
{
    int PlayerNo { get; init; }
    IBattleLogic BattleLogic { get; init; }
	IChampion[] Champions { get; init; }
	int[] Energy { get; init; }
	bool AiOpponent { get; init; }
	bool IsDead();
	void AddEnergy(EnergyType energyType);
	bool SpendEnergy(int[] spentEnergy);
	int NoOfDead();
	IList<IChampion> GetAliveChampions();
	bool ChampionUseAbility(int championNo, int abilityNo, int[] targets);
	IAbility GetAbility(int championNo, int abilityNo);
	void GenerateEnergy();
	EnergyType LoseRandomEnergy(IChampion target, IAbility? ability = null, IActiveEffect? activeEffect = null);
	void GainRandomEnergy();
	void RemoveActiveEffectFromAll(string activeEffectName);
	IActiveEffect? CheckActiveEffectPresentOnAny(string activeEffectName, int championNo, int playerNo);
	void TriggerStartTurnMethods();
	void CheckResume();
	void TriggerEndTurnMethods();
	IChampion GetRandomMyChampion();
	IChampion GetRandomEnemyChampion();
	IChampion[]? GetOtherChampions(IChampion champion);
	PlayerUpdate GetPlayerUpdate();
	TargetsUpdate GetTargets(int championNo, int abilityNo);
	UsableAbilitiesUpdate GetUsableAbilities(int[] currentResources, int energyToSubtract);
	IBattlePlayer GetEnemyPlayer();
	int GetTotalEnergy();
	bool AiCanAnyoneTargetCounterAbility(IAbility ability);
	bool AiCanAnyoneTargetReflectAbility(IAbility ability);
}
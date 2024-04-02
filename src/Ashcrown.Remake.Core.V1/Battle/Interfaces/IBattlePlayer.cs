using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.V1.Ability.Interfaces;
using Ashcrown.Remake.Core.V1.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.V1.Champion.Interfaces;

namespace Ashcrown.Remake.Core.V1.Battle.Interfaces;

public interface IBattlePlayer
{
    int PlayerNo { get; init; }
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
	int LoseRandomEnergy(IChampion target, IAbility ability, IActiveEffect activeEffect);
	void GainRandomEnergy();
	void RemoveActiveEffectFromAll(string activeEffectName); //TODO should this check it from the same character?
	IActiveEffect checkAEPresentOnAny(string activeEffectName, int championNo, int playerNo);
	void TriggerStartTurnMethods();
	void CheckResume();
	void TriggerEndTurnMethods();
	IChampion GetRandomMyChampion();
	IChampion GetRandomEnemyChampion(); //TODO what about invulnerability, what if the target is invulnerable, Seth ignores invul for now
	IList<IChampion> GetOtherChampions(IChampion champion); //TODO does this behave correctly in reflect scenario?
	Object GetPlayerUpdate(); //TODO Model for player update
	Object GetTargets(int championNo, int abilityNo); //TODO Model
	Object GetUsableAbilities(int[] currentResources, int energyToSubtract);
	IBattlePlayer GetEnemyPlayer();
	int GetTotalEnergy();
	bool AiCanAnyoneTargetCounterAbility(IAbility ability);
	bool AiCanAnyoneTargetReflectAbility(IAbility ability);
}
using Ashcrown.Remake.Core.V1.Ability.Interfaces;
using Ashcrown.Remake.Core.V1.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.V1.Battle.Interfaces;

namespace Ashcrown.Remake.Core.V1.Champion.Interfaces;

public interface IChampion
{
    IBattleLogic BattleLogic { get; init; }
    IPlayerBattleInfo PlayerBattleInfo { get; init; }
    int ChampionNo { get; init; }
    string Name { get; set; }
    int Health { get; set; }
    bool Alive { get; set; }
    bool Died { get; set; }
    IList<IAbility> ReceivedReflectedAbilities { get; init; }
    bool[] EnergyUsage { get; set; }
    bool AiReady { get; set; }
    bool RobotSuitUsed { get; set; }
    bool SacrificialPactTriggered { get; set; }
    bool AiLethal { get; set; }
    int AiTotalDestructibleDefenseLeft { get; set; }
    int AiTotalDamageToReceiveAfterDestructible { get; set; }
    int AiTotalHealingToReceive { get; set; }
    IAbility CurrentAbility1 { get; set; }
    IList<IAbility> Abilities1 { get; init; }
    IAbility CurrentAbility2 { get; set; }
    IList<IAbility> Abilities2 { get; init; }
    IAbility CurrentAbility3 { get; set; }
    IList<IAbility> Abilities3 { get; init; }
    IAbility CurrentAbility4 { get; set; }
    IList<IAbility> Abilities4 { get; init; }
    IAbilityController AbilityController { get; init; }
    IActiveEffectController ActiveEffectController { get; init; }
    IChampionController ChampionController { get; init; }
    IChampionSpecificsController ChampionSpecificsController { get; init; }
    void StartTurnMethods();
    void EndTurnMethods();
    IList<IAbility> GetAbilitiesArray(int abilitySlot);
    bool AiCanCounterAbilitySelf(IAbility ability);

    bool AiCanCounterAbilityTarget(IAbility ability);

    bool AiCanReflectAbilitySelf(IAbility ability);

    bool AiCanReflectAbilityTarget(IAbility ability);

    int GetTargetNo(IChampion championTargeting);

    int AiApplyChampionSpecificPenalty(int totalPoints, IAbility ability);

    bool ReceivedReflectStun();

    bool[] GetEnergyUsage();
}
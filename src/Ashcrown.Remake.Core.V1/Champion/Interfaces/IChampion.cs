using Ashcrown.Remake.Core.V1.Ability.Interfaces;
using Ashcrown.Remake.Core.V1.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.V1.Battle.Interfaces;

namespace Ashcrown.Remake.Core.V1.Champion.Interfaces;

public interface IChampion
{
    IBattleLogic BattleLogic { get; init; }
    IBattlePlayer BattlePlayer { get; init; }
    int ChampionNo { get; init; }
    string Name { get; set; }
    int Health { get; set; }
    bool Alive { get; set; }
    bool Died { get; set; }
    IList<IAbility> ReceivedReflectedAbilities { get; init; }
    bool[]? EnergyUsage { get; }
    bool AiReady { get; set; }
    bool RobotSuitUsed { get; set; }
    bool SacrificialPactTriggered { get; set; }
    bool AiLethal { get; set; }
    int AiTotalDestructibleDefenseLeft { get; set; }
    int AiTotalDamageToReceiveAfterDestructible { get; set; }
    int AiTotalHealingToReceive { get; set; }
    IAbility[] CurrentAbilities { get; init; }
    IList<IAbility>[] Abilities { get; init; } 
    IAbilityController AbilityController { get; init; }
    IActiveEffectController ActiveEffectController { get; init; }
    IChampionController ChampionController { get; init; }
    IChampionSpecificsController ChampionSpecificsController { get; init; }
    void StartTurnMethods();
    void EndTurnMethods();
    bool AiCanCounterAbilitySelf(IAbility ability);
    bool AiCanCounterAbilityTarget(IAbility ability);
    bool AiCanReflectAbilitySelf(IAbility ability);
    bool AiCanReflectAbilityTarget(IAbility ability);
    int GetTargetNo(IChampion championTargeting);
    int AiApplyChampionSpecificPenalty(int totalPoints, IAbility ability);
    bool ReceivedReflectStun();
}
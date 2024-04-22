using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Battle.Interfaces;

namespace Ashcrown.Remake.Core.Champion.Interfaces;

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
    IAbility[] CurrentAbilities { get; set; }
    IList<IAbility>[] Abilities { get; set; } 
    IList<IActiveEffect> ActiveEffects { get; init; }
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
    void SetStartAbilities(IAbility startAbility1, IAbility startAbility2, IAbility startAbility3, IAbility startAbility4);
}
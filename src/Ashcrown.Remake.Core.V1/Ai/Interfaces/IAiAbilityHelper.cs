using Ashcrown.Remake.Core.V1.Ability.Interfaces;
using Ashcrown.Remake.Core.V1.Ai.Models;

namespace Ashcrown.Remake.Core.V1.Ai.Interfaces;

public interface IAiAbilityHelper
{
    IAbility Ability { get; init; }
    int[] CreateEmptyTargets();
    AiMaximizedAbility StandardSelfInvulnerabilityMaximizer();
    IList<int[]> GetTargetsPermutations();
    AiMaximizedAbility GetHighestPointsTargetsPermutation(IList<int[]> targetsPermutations,
        IList<int> targetsPermutationsPoints);
    AiMaximizedAbility SelfTargetAbilityMaximizer(); //TODO Could these methods be converted to extensions?
    AiMaximizedAbility EnemyTargetAbilityMaximizer();
    AiMaximizedAbility EnemiesTargetAbilityMaximizer();
    AiMaximizedAbility AllyTargetAbilityMaximizer();
    AiMaximizedAbility AlliesTargetAbilityMaximizer();
    AiMaximizedAbility AllTargetAbilityMaximizer();
    AiMaximizedAbility AllyOrEnemyTargetAbilityMaximizer();
}
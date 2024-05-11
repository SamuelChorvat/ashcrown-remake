using Ashcrown.Remake.Core.Ai.Models;

namespace Ashcrown.Remake.Core.Ai.Interfaces;

public interface IAiAbilityHelper
{
    AiMaximizedAbility StandardSelfInvulnerabilityMaximizer<T>() where T : IAiPointsCalculator;
    AiMaximizedAbility SelfTargetAbilityMaximizer<T>() where T : IAiPointsCalculator;
    AiMaximizedAbility EnemyTargetAbilityMaximizer<T>() where T : IAiPointsCalculator;
    AiMaximizedAbility EnemiesTargetAbilityMaximizer<T>() where T : IAiPointsCalculator;
    AiMaximizedAbility AllyTargetAbilityMaximizer<T>() where T : IAiPointsCalculator;
    AiMaximizedAbility AlliesTargetAbilityMaximizer<T>() where T : IAiPointsCalculator;
    AiMaximizedAbility AllTargetAbilityMaximizer<T>() where T : IAiPointsCalculator;
    AiMaximizedAbility AllyOrEnemyTargetAbilityMaximizer<T>() where T : IAiPointsCalculator;
}
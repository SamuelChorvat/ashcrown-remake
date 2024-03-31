using Ashcrown.Remake.Core.V1.Ability.Interfaces;
using Ashcrown.Remake.Core.V1.Champion.Interfaces;

namespace Ashcrown.Remake.Core.V1.Ai.Interfaces;

public interface IAiPointsCalculator
{
    static abstract int GetInvulnerabilityPoints(int numberOfTurns, IAbility ability, IChampion target);
    static abstract int GetHealingPoints(int healAmount, int numberOfTurns, IAbility ability, IChampion target);
    static abstract int GetDamagePoints(int damageAmount, int numberOfTurns, IAbility ability, IChampion target);
    static abstract int GetDestructibleDefensePoints(int defenseAmount, IChampion target);
    static abstract int GetPointsReductionPoints(int pointsReductionAmount, int numberOfTurns, IChampion target);
    static abstract int GetPercentageReductionPoints(int percentageReductionAmount, int numberOfTurns, IChampion target);
    static abstract int GetStunPoints(IAbility ability, int stunDuration, IChampion target);
    static abstract int GetStealEnergyPoints(int energyAmount, int numberOfTurns, IAbility ability, IChampion target);
    static abstract int GetRemoveEnergyPoints(int energyAmount, int numberOfTurns, IAbility ability, IChampion target);
    static abstract int GetGainEnergyPoints(int energyAmount, int numberOfTurns, IAbility ability);
    static abstract int GetIgnoreStunEffectsPoints(int numberOfTurns, IAbility ability, IChampion target);
    static abstract int GetSpecialConditionPoints(int numberOfTurns);
    static abstract int GetDisableInvulnerabilityAndDamageReductionPoints(int numberOfTurns, IChampion target);
    static abstract int GetIgnoreHarmfulPoints(int numberOfTurns, IAbility ability, IChampion target);
    static abstract int GetRemoveAfflictionsPoints(IAbility ability, IChampion target);
    static abstract int GetRemoveHarmfulPoints(IAbility ability, IChampion target);
    static abstract int GetCostIncreasePoints(int numberOfTurns, IAbility ability, IChampion target);
    static abstract int GetCostDecreasePoints(int numberOfTurns, IAbility ability, IChampion target);
    static abstract int GetCooldownDecreasePoints(int numberOfTurns, IAbility ability, IChampion target);
    static abstract int GetCounterHarmfulPointsTargetAlly(int numberOfTurns, IAbility ability, IChampion target);
    static abstract int GetReflectHarmfulPointsTargetAlly(int numberOfTurns, IAbility ability, IChampion target);
    static abstract int GetCounterHarmfulPointsTargetEnemies(int numberOfTurns, IAbility ability, IChampion target);
    static abstract int GetCounterPointsTargetEnemy(int numberOfTurns, IAbility ability, IChampion target);
    static abstract int GetReduceStunDurationPoints(int numberOfTurns, IAbility ability, IChampion target);
    static abstract int GetReduceEnergyStealRemovePoints(int numberOfTurns, IAbility ability, IChampion target);
    static abstract int GetHealingReductionPoints(int numberOfTurns, IAbility ability, IChampion target);
    static abstract int ApplyPenalties(int abilityPoints, IAbility ability, IChampion target);
    static abstract int GetTotalDestructible(IChampion target); //TODO move to extension? or at least to different helper class?
}
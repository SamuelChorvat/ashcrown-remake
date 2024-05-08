using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ai.Interfaces;
using Ashcrown.Remake.Core.Ai.Models;

namespace Ashcrown.Remake.Core.Ai;

public class AiAbilityHelper(IAbility ability) : IAiAbilityHelper
{
    public AiMaximizedAbility StandardSelfInvulnerabilityMaximizer<T>() where T : IAiPointsCalculator
    {
        var totalPoints = T.GetInvulnerabilityPoints(ability.Duration1, ability, ability.Owner);
        totalPoints = T.ApplyPenalties(totalPoints, ability, ability.Owner);

        var targets = CreateEmptyTargets();
        targets[ability.Owner.GetTargetNo(ability.Owner)] = 1;
        return new AiMaximizedAbility(totalPoints, targets);
    }

    public AiMaximizedAbility SelfTargetAbilityMaximizer<T>() where T : IAiPointsCalculator
    {
        var totalPoints = ability.CalculateTotalPointsForTarget<T>(ability.Owner);
        totalPoints = T.ApplyPenalties(totalPoints, ability, ability.Owner);

        var targets = CreateEmptyTargets();
        targets[ability.Owner.GetTargetNo(ability.Owner)] = 1;

        return new AiMaximizedAbility(totalPoints, targets);
    }

    public AiMaximizedAbility EnemyTargetAbilityMaximizer<T>() where T : IAiPointsCalculator
    {
        var targetsPermutations = GetTargetsPermutations();
        var targetsPermutationsPoints = new List<int>();

        foreach(var targetsPermutation in targetsPermutations) {
            var totalPoints = 0;
            for (var i = 3; i < targetsPermutation.Length; i++)
            {
                if (targetsPermutation[i] != 1) continue;
                var target = ability.Owner.BattlePlayer.GetEnemyPlayer().Champions[i - 3];
                totalPoints = ability.CalculateTotalPointsForTarget<T>(target);
                totalPoints = T.ApplyPenalties(totalPoints, ability, target);
                break;
            }
            targetsPermutationsPoints.Add(totalPoints);
        }

        return GetHighestPointsTargetsPermutation(targetsPermutations, targetsPermutationsPoints);
    }

    public AiMaximizedAbility EnemiesTargetAbilityMaximizer<T>() where T : IAiPointsCalculator
    {
        var targetsPermutations = GetTargetsPermutations();
        var targetsPermutation = targetsPermutations[0];

        var totalPoints = 0;
        for (var i = 3; i < targetsPermutation.Length; i++)
        {
            if (targetsPermutation[i] != 1) continue;
            var target = ability.Owner.BattlePlayer.GetEnemyPlayer().Champions[i - 3];
            var currentTargetPoints = ability.CalculateTotalPointsForTarget<T>(target);
            currentTargetPoints = T.ApplyPenalties(currentTargetPoints, ability, target);
            totalPoints += currentTargetPoints;
        }
        totalPoints += ability.CalculateSingletonSelfEffectTotalPoints<T>();
        return new AiMaximizedAbility(totalPoints, targetsPermutation);
    }

    public AiMaximizedAbility AllyTargetAbilityMaximizer<T>() where T : IAiPointsCalculator
    {
        var targetsPermutations = GetTargetsPermutations();
        var targetsPermutationsPoints = new List<int>();

        foreach(var targetsPermutation in targetsPermutations) {
            var totalPoints = 0;
            for (var i = 0; i < 3; i++)
            {
                if (targetsPermutation[i] != 1) continue;
                var target = ability.Owner.BattlePlayer.Champions[i];
                totalPoints = ability.CalculateTotalPointsForTarget<T>(target);
                totalPoints = T.ApplyPenalties(totalPoints, ability, target);
                break;
            }
            targetsPermutationsPoints.Add(totalPoints);
        }

        return GetHighestPointsTargetsPermutation(targetsPermutations, targetsPermutationsPoints);
    }

    public AiMaximizedAbility AlliesTargetAbilityMaximizer<T>() where T : IAiPointsCalculator
    {
        var targetsPermutations = GetTargetsPermutations();
        var targetsPermutation = targetsPermutations[0];

        var totalPoints = 0;
        for (var i = 0; i < 3; i++)
        {
            if (targetsPermutation[i] != 1) continue;
            var target = ability.Owner.BattlePlayer.Champions[i];
            var currentTargetPoints = ability.CalculateTotalPointsForTarget<T>(target);
            currentTargetPoints = T.ApplyPenalties(currentTargetPoints, ability, target);
            totalPoints += currentTargetPoints;
        }
        totalPoints += ability.CalculateSingletonSelfEffectTotalPoints<T>();
        return new AiMaximizedAbility(totalPoints, targetsPermutation);
    }

    public AiMaximizedAbility AllTargetAbilityMaximizer<T>() where T : IAiPointsCalculator
    {
        var targetsPermutations = GetTargetsPermutations();
        var targetsPermutation = targetsPermutations[0];

        var totalPoints = 0;
        for (var i = 0; i < targetsPermutation.Length; i++)
        {
            if (targetsPermutation[i] != 1) continue;
            var target = i >= 3 ? 
                ability.Owner.BattlePlayer.GetEnemyPlayer().Champions[i - 3] 
                : ability.Owner.BattlePlayer.Champions[i];
            var currentTargetPoints = ability.CalculateTotalPointsForTarget<T>(target);
            currentTargetPoints = T.ApplyPenalties(currentTargetPoints, ability, target);
            totalPoints += currentTargetPoints;
        }
        totalPoints += ability.CalculateSingletonSelfEffectTotalPoints<T>();
        return new AiMaximizedAbility(totalPoints, targetsPermutation);
    }

    public AiMaximizedAbility AllyOrEnemyTargetAbilityMaximizer<T>() where T : IAiPointsCalculator
    {
        var targetsPermutations = GetTargetsPermutations();
        var targetsPermutationsPoints = new List<int>();

        foreach(var targetsPermutation in targetsPermutations) {
            var totalPoints = 0;
            for (var i = 0; i < targetsPermutation.Length; i++)
            {
                if (targetsPermutation[i] != 1) continue;
                var target = i >= 3 ? ability.Owner.BattlePlayer.GetEnemyPlayer().Champions[i - 3] 
                    : ability.Owner.BattlePlayer.Champions[i];

                totalPoints = ability.CalculateTotalPointsForTarget<T>(target);
                totalPoints = T.ApplyPenalties(totalPoints, ability, target);
                break;
            }
            targetsPermutationsPoints.Add(totalPoints);
        }

        return GetHighestPointsTargetsPermutation(targetsPermutations, targetsPermutationsPoints);
    }
    
    private static int[] CreateEmptyTargets()
    {
        return [0, 0, 0, 0, 0, 0];
    }
    
    private List<int[]> GetTargetsPermutations()
    {
        var possibleTargets = ability.GetPossibleTargets();
        var targetsPermutations = new List<int[]>();

        switch (ability.Target) {
            case AbilityTarget.Self:
            case AbilityTarget.All:
            case AbilityTarget.Enemies:
            case AbilityTarget.Allies:
                targetsPermutations.Add(possibleTargets);
                break;
            case AbilityTarget.Ally:
            case AbilityTarget.Enemy:
            case AbilityTarget.AllyOrEnemy:
                for(var i = 0; i < possibleTargets.Length; i++) {
                    if (possibleTargets[i] == 1) {
                        targetsPermutations.Add(GetTargetsWithOneTarget(i));
                    }
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return targetsPermutations;
    }

    private static int[] GetTargetsWithOneTarget(int targetIndex)
    {
        var targetsArray = CreateEmptyTargets();
        targetsArray[targetIndex] = 1;
        return targetsArray;
    }

    private static AiMaximizedAbility GetHighestPointsTargetsPermutation(IList<int[]> targetsPermutations,
        IList<int> targetsPermutationsPoints)
    {
        AiMaximizedAbility? toReturn = null;
        for (var i = 0; i < targetsPermutationsPoints.Count; i++) {
            if (toReturn == null) {
                toReturn = new AiMaximizedAbility(targetsPermutationsPoints[i], targetsPermutations[i]);
            } else if (targetsPermutationsPoints[i] > toReturn.Points
                       || (targetsPermutationsPoints[i] == toReturn.Points && new Random().Next(2) == 1)) {
                toReturn = new AiMaximizedAbility(targetsPermutationsPoints[i], targetsPermutations[i]);
            }
        }

        return toReturn!;
    }
}
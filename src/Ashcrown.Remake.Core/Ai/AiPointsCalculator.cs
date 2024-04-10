using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ai.Interfaces;
using Ashcrown.Remake.Core.Champion;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Ai;

public class AiPointsCalculator : IAiPointsCalculator
{
    public static int GetInvulnerabilityPoints(int numberOfTurns, IAbility ability, IChampion target)
    {
        if (target.ChampionController.IsInvulnerabilityDisabled()) {
            return 0;
        }
        
        if (target.ChampionController.IsInvulnerableTo(abilityClasses: ability.TypeOfInvulnerability)) {
            return 0;
        }

        var numberOfChampionsInvulnerableTo = 0;
        var numberOfChampionsAlive = 0;
        var opponentChampions = target.BattlePlayer.GetEnemyPlayer().Champions;
        foreach (var opponentChampion in opponentChampions) {
            if (!opponentChampion.Alive) {
                continue;
            }

            if (GetNumberOfAbilitiesInvulnerableTo(target, ability.TypeOfInvulnerability, opponentChampion) > 0) {
                numberOfChampionsInvulnerableTo += 1;
            }
            numberOfChampionsAlive += 1;
        }

        return (int) Math.Round(AiCalculatorConstants.InvulnerabilityPoints
                                * GetMissingHealthMultiplier(target)
                                * CalculateNumberOfTurnsMultiplier(numberOfTurns)
                                * ((double) numberOfChampionsInvulnerableTo/numberOfChampionsAlive));
    }

    public static int GetHealingPoints(int healAmount, int numberOfTurns, IAbility ability, IChampion target)
    {
        if (target.Health + target.AiTotalHealingToReceive >= 100) {
            return 0;
        }

        var caster = ability.Owner;
        var missingHealth = Math.Max(ChampionConstants.ChampionMaxHealth - target.Health - target.AiTotalHealingToReceive, 0);

        var numberOfTurnsMultiplier = CalculateNumberOfTurnsMultiplier(numberOfTurns);
        var adjustedHealAmount = Math.Max(healAmount
                                          + caster.ChampionController.TotalHealingDealIncrease.Points
                                          - (caster.ChampionController.IsIgnoringHarmful() ? 0 : caster.ChampionController.TotalHealingDealReduce.Points)
                                          + target.ChampionController.TotalHealingReceiveIncrease.Points
                                          - (target.ChampionController.IsIgnoringHarmful() ? 0 : target.ChampionController.TotalHealingReceiveReduce.Points), 0);
        adjustedHealAmount = (int) Math.Round(adjustedHealAmount * Math.Max(1d + caster.ChampionController.TotalHealingDealIncrease.Percentage/100d
                                                                      - (caster.ChampionController.IsIgnoringHarmful() ? 0 : caster.ChampionController.TotalHealingDealReduce.Percentage/100d)
                                                                      + target.ChampionController.TotalHealingReceiveIncrease.Percentage/100d
                                                                      - (target.ChampionController.IsIgnoringHarmful() ? 0 : target.ChampionController.TotalHealingReceiveReduce.Percentage/100d), 0));

        adjustedHealAmount = Math.Max(0, Math.Min(adjustedHealAmount, ChampionConstants.ChampionMaxHealth - (target.Health + target.AiTotalHealingToReceive)));
        target.AiTotalHealingToReceive += adjustedHealAmount;
        return (int) Math.Round((AiCalculatorConstants.BaseHealingPoints + adjustedHealAmount * numberOfTurnsMultiplier) * (missingHealth/(double)ChampionConstants.ChampionMaxHealth));
    }

    public static int GetDamagePoints(int damageAmount, int numberOfTurns, IAbility ability, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetDestructibleDefensePoints(int defenseAmount, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetPointsReductionPoints(int pointsReductionAmount, int numberOfTurns, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetPercentageReductionPoints(int percentageReductionAmount, int numberOfTurns, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetStunPoints(IAbility ability, int stunDuration, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetStealEnergyPoints(int energyAmount, int numberOfTurns, IAbility ability, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetRemoveEnergyPoints(int energyAmount, int numberOfTurns, IAbility ability, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetGainEnergyPoints(int energyAmount, int numberOfTurns, IAbility ability)
    {
        throw new NotImplementedException();
    }

    public static int GetIgnoreStunEffectsPoints(int numberOfTurns, IAbility ability, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetSpecialConditionPoints(int numberOfTurns)
    {
        throw new NotImplementedException();
    }

    public static int GetDisableInvulnerabilityAndDamageReductionPoints(int numberOfTurns, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetIgnoreHarmfulPoints(int numberOfTurns, IAbility ability, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetRemoveAfflictionsPoints(IAbility ability, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetRemoveHarmfulPoints(IAbility ability, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetCostIncreasePoints(int numberOfTurns, IAbility ability, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetCostDecreasePoints(int numberOfTurns, IAbility ability, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetCooldownDecreasePoints(int numberOfTurns, IAbility ability, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetCounterHarmfulPointsTargetAlly(int numberOfTurns, IAbility ability, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetReflectHarmfulPointsTargetAlly(int numberOfTurns, IAbility ability, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetCounterHarmfulPointsTargetEnemies(int numberOfTurns, IAbility ability, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetCounterPointsTargetEnemy(int numberOfTurns, IAbility ability, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetReduceStunDurationPoints(int numberOfTurns, IAbility ability, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetReduceEnergyStealRemovePoints(int numberOfTurns, IAbility ability, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetHealingReductionPoints(int numberOfTurns, IAbility ability, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int ApplyPenalties(int abilityPoints, IAbility ability, IChampion target)
    {
        throw new NotImplementedException();
    }

    public static int GetTotalDestructible(IChampion target)
    {
        throw new NotImplementedException();
    }
    
    private static double CalculateNumberOfTurnsMultiplier(int numberOfTurns)
    {
        if (numberOfTurns == AiCalculatorConstants.InfiniteNumberOfTurns) {
            return 2;
        }

        var numberOfTurnsMultiplier = 1d;

        var tempNumberOfTurns = numberOfTurns;
        while (tempNumberOfTurns > 1) {
            numberOfTurnsMultiplier += 1d/tempNumberOfTurns;
            tempNumberOfTurns -= 1;
        }

        return numberOfTurnsMultiplier;
    }

    private static double GetMissingHealthMultiplier(IChampion target)
    {
        return (ChampionConstants.ChampionMaxHealth - target.Health)/(double)ChampionConstants.ChampionMaxHealth;
    }

    private static int GetNumberOfAbilitiesInvulnerableTo(IChampion caster, AbilityClass[]? typesOfInvulnerability, IChampion attacker)
    {
        if (caster.ChampionController.IsInvulnerabilityDisabled()
            || caster.ChampionController.IsIgnoringHarmful()) {
            return 0;
        }

        var invulnerableAbilityCount = 0;
        for (var i = 1; i <= 4; i++) {
            if (!attacker.AbilityController.GetCurrentAbility(i).Active
                && attacker.AbilityController.GetCurrentAbility(i).ToReady > 1) {
                continue;
            }

            if (!attacker.AbilityController.GetCurrentAbility(i).Harmful
                || attacker.AbilityController.GetCurrentAbility(i).IgnoreInvulnerability) {
                continue;
            }

            if (typesOfInvulnerability!.Any(typeOfInvulnerability => typeOfInvulnerability.Equals(AbilityClass.All)
                                                                     || attacker.AbilityController.GetCurrentAbility(i).AbilityClassesContains(typeOfInvulnerability)))
            {
                invulnerableAbilityCount += 1;
            }
        }
        return invulnerableAbilityCount;
    }
}
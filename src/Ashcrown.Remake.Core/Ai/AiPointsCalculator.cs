using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ai.Interfaces;
using Ashcrown.Remake.Core.Champion;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Jane.Champion;
using Ashcrown.Remake.Core.Champions.Luther.ActiveEffects;

namespace Ashcrown.Remake.Core.Ai;

// TODO AI Tests with random champions where player just passes until loss
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
        if (target.AiLethal) {
            return 0;
        }

        if (target.ChampionController.IsInvulnerableTo(ability) || target.ChampionController.IsIgnoringDamage()) {
            return 0;
        }

        var caster = ability.Owner;
        var missingHealth = Math.Max(ChampionConstants.ChampionMaxHealth - target.Health + target.AiTotalDamageToReceiveAfterDestructible, ChampionConstants.ChampionMaxHealth);
        
        var missingHealthMultiplier =  missingHealth/(double)ChampionConstants.ChampionMaxHealth;
        var numberOfTurnsMultiplier = Math.Max(CalculateNumberOfTurnsMultiplier(numberOfTurns) - 1d, 0);

        var totalMultiplier = 1d;
        var adjustedDamageAmountBeforeDestructible = damageAmount;
        adjustedDamageAmountBeforeDestructible = ForgeSpiritActiveEffect.AbilityDamageModifier(caster, adjustedDamageAmountBeforeDestructible, ability);

        if (ability.PiercingDamage) {
            totalMultiplier += AiCalculatorConstants.PiercingDamageMultiplier;
        }

        if (ability.AfflictionDamage) {
            totalMultiplier += AiCalculatorConstants.AfflictionDamageMultiplier;
        } else {
            adjustedDamageAmountBeforeDestructible = Math.Max(adjustedDamageAmountBeforeDestructible
                            + caster.ChampionController.TotalAllDamageDealIncrease.Points
                            - (caster.ChampionController.IsIgnoringHarmful() || ability.PiercingDamage ? 0 : caster.ChampionController.TotalAllDamageDealReduce.Points)
                            + (target.ChampionController.IsIgnoringHarmful() ? 0 : target.ChampionController.TotalAllDamageReceiveIncrease.Points)
                            - (target.ChampionController.IsIgnoringReceivedDamageReduction() || ability.PiercingDamage ? 0 : target.ChampionController.TotalAllDamageReceiveReduce.Points)
                    , 0);
            adjustedDamageAmountBeforeDestructible = (int) Math.Round(adjustedDamageAmountBeforeDestructible * Math.Max(1d + caster.ChampionController.TotalAllDamageDealIncrease.Percentage/100d
                - (caster.ChampionController.IsIgnoringHarmful() || ability.PiercingDamage ? 0 : caster.ChampionController.TotalAllDamageDealReduce.Percentage/100d)
                + (target.ChampionController.IsIgnoringHarmful() ? 0 : target.ChampionController.TotalAllDamageReceiveIncrease.Percentage/100d)
                - (target.ChampionController.IsIgnoringReceivedDamageReduction() || ability.PiercingDamage ? 0 : target.ChampionController.TotalAllDamageReceiveReduce.Percentage/100d)
                , 0));

            if (ability.MagicDamage) {
                adjustedDamageAmountBeforeDestructible = Math.Max(adjustedDamageAmountBeforeDestructible
                                + caster.ChampionController.TotalMagicDamageDealIncrease.Points
                                - (caster.ChampionController.IsIgnoringHarmful() || ability.PiercingDamage ? 0 : caster.ChampionController.TotalMagicDamageDealReduce.Points)
                                + (target.ChampionController.IsIgnoringHarmful() ? 0 : target.ChampionController.TotalMagicDamageReceiveIncrease.Points)
                                - (target.ChampionController.IsIgnoringReceivedDamageReduction() || ability.PiercingDamage ? 0 : target.ChampionController.TotalMagicDamageReceiveReduce.Points)
                                , 0);
                adjustedDamageAmountBeforeDestructible = (int) Math.Round(adjustedDamageAmountBeforeDestructible * Math.Max(1f + caster.ChampionController.TotalMagicDamageDealIncrease.Percentage/100d
                    - (caster.ChampionController.IsIgnoringHarmful() || ability.PiercingDamage ? 0 : caster.ChampionController.TotalMagicDamageDealReduce.Percentage/100d)
                    + (target.ChampionController.IsIgnoringHarmful() ? 0 : target.ChampionController.TotalMagicDamageReceiveIncrease.Percentage/100d)
                    - (target.ChampionController.IsIgnoringReceivedDamageReduction() || ability.PiercingDamage ? 0 : target.ChampionController.TotalMagicDamageReceiveReduce.Percentage/100d)
                    , 0));
            } else if ( ability.PhysicalDamage) {
                adjustedDamageAmountBeforeDestructible = Math.Max(adjustedDamageAmountBeforeDestructible
                                + caster.ChampionController.TotalPhysicalDamageDealIncrease.Points
                                - (caster.ChampionController.IsIgnoringHarmful() || ability.PiercingDamage ? 0 : caster.ChampionController.TotalPhysicalDamageDealReduce.Points)
                                + (target.ChampionController.IsIgnoringHarmful() ? 0 : target.ChampionController.TotalPhysicalDamageReceiveIncrease.Points)
                                - (target.ChampionController.IsIgnoringReceivedDamageReduction() || ability.PiercingDamage ? 0 : target.ChampionController.TotalPhysicalDamageReceiveReduce.Points)
                                , 0);
                adjustedDamageAmountBeforeDestructible = (int) Math.Round(adjustedDamageAmountBeforeDestructible * (Math.Max(1f + caster.ChampionController.TotalPhysicalDamageDealIncrease.Percentage/100d
                    - (caster.ChampionController.IsIgnoringHarmful() || ability.PiercingDamage ? 0 : caster.ChampionController.TotalPhysicalDamageDealReduce.Percentage/100d)
                    + (target.ChampionController.IsIgnoringHarmful() ? 0 : target.ChampionController.TotalPhysicalDamageReceiveIncrease.Percentage/100d)
                    - (target.ChampionController.IsIgnoringReceivedDamageReduction() || ability.PiercingDamage ? 0 : target.ChampionController.TotalPhysicalDamageReceiveReduce.Percentage/100d)
                    , 0)));
            }
        }

        totalMultiplier += missingHealthMultiplier;
        totalMultiplier += numberOfTurnsMultiplier;

        var effectiveDamage = Math.Max(0, !ability.AfflictionDamage ? 
            Math.Min(adjustedDamageAmountBeforeDestructible, target.Health + target.AiTotalDestructibleDefenseLeft - target.AiTotalDamageToReceiveAfterDestructible) 
            : Math.Min(adjustedDamageAmountBeforeDestructible, target.Health - target.AiTotalDamageToReceiveAfterDestructible));
        var damagePoints = (int) (Math.Round(effectiveDamage * totalMultiplier) + AiCalculatorConstants.BaseDamagePoints);

        int damageAfterDestructible;
        if(!ability.AfflictionDamage) {
            if (adjustedDamageAmountBeforeDestructible > target.AiTotalDestructibleDefenseLeft) {
                damageAfterDestructible = adjustedDamageAmountBeforeDestructible - target.AiTotalDestructibleDefenseLeft;
                target.AiTotalDestructibleDefenseLeft = 0;
            } else if (adjustedDamageAmountBeforeDestructible == target.AiTotalDestructibleDefenseLeft) {
                damageAfterDestructible = 0;
                target.AiTotalDestructibleDefenseLeft = 0;
            } else {
                target.AiTotalDestructibleDefenseLeft -= adjustedDamageAmountBeforeDestructible;
                damageAfterDestructible = 0;
            }
        } else {
            damageAfterDestructible = adjustedDamageAmountBeforeDestructible;
        }

        if (ability.AfflictionDamage || target.AiTotalDestructibleDefenseLeft == 0) {
            if (target.Health <= damageAfterDestructible + target.AiTotalDamageToReceiveAfterDestructible) {
                damagePoints += AiCalculatorConstants.LethalDamagePoints;
                target.AiLethal = true;
            }
        }

        target.AiTotalDamageToReceiveAfterDestructible += damageAfterDestructible;

        return damagePoints;
    }

    public static int GetDestructibleDefensePoints(int defenseAmount, IChampion target)
    {
        return (int) Math.Round((AiCalculatorConstants.BaseDestructDefensePoints + defenseAmount) 
                                * GetMissingHealthMultiplier(target));
    }

    public static int GetPointsReductionPoints(int pointsReductionAmount, int numberOfTurns, IChampion target)
    {
        var numberOfTurnsMultiplier = CalculateNumberOfTurnsMultiplier(numberOfTurns);
        if (target.ChampionController.IsIgnoringReceivedDamageReduction()) {
            return 0;
        }

        return (int) Math.Round((AiCalculatorConstants.BasePointsReductionPoints + numberOfTurnsMultiplier * pointsReductionAmount) 
                                * GetMissingHealthMultiplier(target));
    }

    public static int GetPercentageReductionPoints(int percentageReductionAmount, int numberOfTurns, IChampion target)
    {
        var numberOfTurnsMultiplier = CalculateNumberOfTurnsMultiplier(numberOfTurns);
        if (target.ChampionController.IsIgnoringReceivedDamageReduction()) {
            return 0;
        }

        return (int) Math.Round(AiCalculatorConstants.BasePercentageReductionPoints
                                * (1 + numberOfTurnsMultiplier * ((double)percentageReductionAmount/100))
                                * GetMissingHealthMultiplier(target));
    }

    public static int GetStunPoints(IAbility ability, int stunDuration, IChampion target)
    {
        if (target.ChampionController.IsIgnoringStuns()
            || target.ChampionController.IsInvulnerableTo(ability)) {
            return 0;
        }

        var caster = ability.Owner;
        var adjustedDuration = stunDuration;
        if (!caster.ChampionController.IsIgnoringHarmful()) {
            adjustedDuration = Math.Max(adjustedDuration - caster.ActiveEffectController.GetActiveEffectCountByName(JaneNames.CounterShotActiveEffect) * 2, 0);
        }

        return (int) (Math.Round(AiCalculatorConstants.StunPoints * ((double) GetNumberOfStunnedAbilities(adjustedDuration, ability.StunType!, target)/4) * CalculateNumberOfTurnsMultiplier(adjustedDuration))
                      + GetNumberOfStunnableActions(target, ability.StunType!) * AiCalculatorConstants.PointsPerStunnedAction
                      + GetNumberOfStunnableControls(target, ability.StunType!) * AiCalculatorConstants.PointsPerStunnedControl);
    }

    public static int GetStealEnergyPoints(int energyAmount, int numberOfTurns, IAbility ability, IChampion target)
    {
        if (StealOrRemoveEnergyChecks(ability, target)) {
            return (int) Math.Round(AiCalculatorConstants.StealEnergyPoints * energyAmount * CalculateNumberOfTurnsMultiplier(numberOfTurns));
        }

        return 0;
    }

    public static int GetRemoveEnergyPoints(int energyAmount, int numberOfTurns, IAbility ability, IChampion target)
    {
        if (StealOrRemoveEnergyChecks(ability, target)) {
            return (int) Math.Round(AiCalculatorConstants.RemoveEnergyPoints * energyAmount * CalculateNumberOfTurnsMultiplier(numberOfTurns));
        }

        return 0;
    }

    public static int GetGainEnergyPoints(int energyAmount, int numberOfTurns, IAbility ability)
    {
        return (int) Math.Round(AiCalculatorConstants.GainEnergyPoints 
                                * energyAmount * CalculateNumberOfTurnsMultiplier(numberOfTurns));
    }

    public static int GetIgnoreStunEffectsPoints(int numberOfTurns, IAbility ability, IChampion target)
    {
        var caster = ability.Owner;
        var enemyChampions = caster.BattlePlayer.GetEnemyPlayer().Champions;

        var numberOfChampionsAlive = enemyChampions[0].BattlePlayer.GetAliveChampions().Count;

        var numberOfChampionsThatCanStun = enemyChampions.Count(enemyChampion => enemyChampion.Alive 
            && ChampionHasStunAbilityToStunTarget(enemyChampion, target));

        return (int) Math.Round(AiCalculatorConstants.IgnoreStunEffectsPoints
                                * ((double) numberOfChampionsThatCanStun/numberOfChampionsAlive)
                                * CalculateNumberOfTurnsMultiplier(numberOfTurns));
    }

    public static int GetSpecialConditionPoints(int numberOfTurns)
    {
        return (int) Math.Round(AiCalculatorConstants.SpecialConditionPoints * CalculateNumberOfTurnsMultiplier(numberOfTurns));
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
    
    private static int GetNumberOfStunnableControls(IChampion target, AbilityClass[] abilityStunType)
    {
        return target.ActiveEffects.Where(activeEffect => activeEffect is {EndsOnCasterStun: true, Source: true})
            .Count(activeEffect => 
                abilityStunType.Any(stunClass => stunClass == AbilityClass.All 
                                                 || activeEffect.OriginAbility.AbilityClassesContains(stunClass)));
    }

    private static int GetNumberOfStunnableActions(IChampion target, AbilityClass[] abilityStunType)
    {
        return target.ActiveEffects.Where(activeEffect => activeEffect is {PauseOnCasterStun: true, Source: true})
            .Count(activeEffect => 
                abilityStunType.Any(stunClass => stunClass == AbilityClass.All 
                                                 || activeEffect.OriginAbility.AbilityClassesContains(stunClass)));
    }

    private static int GetNumberOfStunnedAbilities(int adjustedDuration, AbilityClass[] abilityStunType, IChampion target)
    {
        return target.ChampionController.IsIgnoringStuns() ? 
            0 : GetNumberOfAbilitiesContainingClasses(adjustedDuration, abilityStunType, target);
    }

    private static int GetNumberOfAbilitiesContainingClasses(int adjustedDuration, AbilityClass[] abilityStunType, IChampion target)
    {
        var abilityCount = 0;
        for (var i = 1; i <= 4; i++)
        {
            if (!target.AbilityController.GetCurrentAbility(i).Active && target.AbilityController.GetCurrentAbility(i).ToReady > adjustedDuration) {
                continue;
            }

            if (abilityStunType.Any(increaseClass => increaseClass == AbilityClass.All 
                                                     || target.AbilityController.GetCurrentAbility(i).AbilityClassesContains(increaseClass)))
            {
                abilityCount += 1;
            }
        }
        return abilityCount;
    }
    
    private static bool StealOrRemoveEnergyChecks(IAbility ability, IChampion target)
    {
        if (target.ChampionController.IsIgnoringHarmful()
            || target.ChampionController.IsInvulnerableTo(ability)) {
            return false;
        }

        return target.BattlePlayer.GetTotalEnergy() > 0;
    }
    
    private static bool ChampionHasStunAbilityToStunTarget(IChampion champion, IChampion target)
    {
        if (!champion.Alive) {
            return false;
        }

        for (var i = 1; i <=4; i++) {
            if (champion.AbilityController.GetCurrentAbility(i).Stun
                && GetNumberOfStunnedAbilities(1, champion.AbilityController.GetCurrentAbility(i).StunType!, target) > 0) {
                return true;
            }
        }
        return false;
    }
}
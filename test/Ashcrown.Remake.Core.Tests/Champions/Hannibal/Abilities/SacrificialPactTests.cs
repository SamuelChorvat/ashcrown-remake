using Ashcrown.Remake.Core.Champions.Branley.Champion;
using Ashcrown.Remake.Core.Champions.Hannibal.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Hannibal.Abilities;

public class SacrificialPactTests
{
    [Fact]
    public void SacrificialPactAppliesCorrectAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(HannibalConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,1,0,0,0,0], [1,1,0,0]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();

        // Assert
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(1).Champions[1].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(HannibalConstants.SacrificialPactActiveEffect).Should().BeTrue();
        var activeEffect = battleLogic.GetBattlePlayer(1).Champions[1].ActiveEffectController
            .GetActiveEffectByName(HannibalConstants.SacrificialPactActiveEffect);
        activeEffect!.Hidden.Should().BeTrue();
        activeEffect.Infinite.Should().BeTrue();
    }

    [Fact]
    public void SacrificialPactCannotBeUsedWhileActive()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(HannibalConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,1,0,0,0,0], [1,1,0,0]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        var act = () => battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);

        // Assert
        act.Should().Throw<Exception>().WithMessage("Ability not ready(True) or active(False)");
    }

    [Fact]
    public void SacrificialPactDoesntTriggerWhileHannibalIsAlive()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(HannibalConstants.Name, 
            BranleyConstants.Name);
        var useSacrificialPact = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,1,0,0,0,0], [1,1,0,0]);
        var usePlunder = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,0,1,0], [0,1,1,0]);
        battleLogic.GetBattlePlayer(1).Champions[1].Health = 30;

        // Act
        battleLogic.AbilitiesUsed(1, useSacrificialPact, useSacrificialPact.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        var plunderResult = battleLogic.AbilitiesUsed(2, usePlunder, [0,1,1,0]);

        // Assert
        plunderResult.Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[1].Health.Should().Be(0);
        battleLogic.GetBattlePlayer(1).Champions[1].Alive.Should().BeFalse();
    }

    [Fact]
    public void SacrificialPactDoesTriggerWhenHannibalIsDead()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(HannibalConstants.Name);
        var useSacrificialPact = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,1,0,0,0,0], [1,1,0,0]);
        var usePlunder = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,0,1,0], [0,1,1,0]);
        battleLogic.GetBattlePlayer(1).Champions[1].Health = 30;

        // Act
        battleLogic.AbilitiesUsed(1, useSacrificialPact, useSacrificialPact.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(1).Champions[0].ChampionController.OnDeath();
        battleLogic.AbilitiesUsed(2, usePlunder, [0,1,1,0]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Alive.Should().BeFalse();
        battleLogic.GetBattlePlayer(1).Champions[1].Health.Should().Be(75);
        battleLogic.GetBattlePlayer(1).Champions[1].Alive.Should().BeTrue();
        var champion = battleLogic.GetBattlePlayer(1).Champions[1];
        champion.Name.Should().Be(HannibalConstants.Name);
        champion.AbilityController.GetCurrentAbility(1).Name.Should().Be(HannibalConstants.TasteForBlood);
        champion.AbilityController.GetCurrentAbility(2).Name.Should().Be(HannibalConstants.HealthFunnel);
        champion.AbilityController.GetCurrentAbility(3).Name.Should().Be(HannibalConstants.SacrificialPact);
        champion.AbilityController.GetCurrentAbility(4).Name.Should().Be(HannibalConstants.DemonicSkin);
    }
}
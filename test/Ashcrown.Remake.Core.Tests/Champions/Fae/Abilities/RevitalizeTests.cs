using Ashcrown.Remake.Core.Champions.Fae.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Fae.Abilities;

public class RevitalizeTests
{
    [Fact]
    public void RevitalizeHealsCorrectAmount()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(FaeConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 1, 0, 0, 0, 0], [0, 0, 1, 1]);
        battleLogic.GetBattlePlayer(1).Champions[1].Health = 10;

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[1].Health.Should().Be(45);
    }
    
    [Fact]
    public void RevitalizeRemovesAfflictions()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(FaeConstants.Name);
        var useCorruption = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(2, 2,
            [0, 0, 0, 0, 1, 0], [0, 0, 1, 0]);
        var usedRevitalize = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 1, 0, 0, 0, 0], [0, 0, 1, 1]);

        // Act
        battleLogic.AbilitiesUsed(2, useCorruption, useCorruption.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        battleLogic.GetBattlePlayer(1).Champions[1].Health.Should().Be(90);
        battleLogic.GetBattlePlayer(1).Champions[1].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(FaeConstants.CorruptionActiveEffect).Should().BeTrue();

        battleLogic.AbilitiesUsed(1, usedRevitalize, usedRevitalize.SpentEnergy!).Should().BeTrue();

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[1].Health.Should().Be(100);
        battleLogic.GetBattlePlayer(1).Champions[1].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(FaeConstants.CorruptionActiveEffect).Should().BeFalse();
    }
}
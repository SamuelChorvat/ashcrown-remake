using Ashcrown.Remake.Core.Champions.Hrom.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Hrom.Abilities;

public class LightningStormTests
{
    [Fact]
    public void LightningStormCannotBeUsedCurrently()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(HromConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,0,0,1,1,1], [1,1,1,0]);
        
        // Act
        var act = () => battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);

        // Assert
        act.Should().Throw<Exception>().WithMessage("Ability not ready(True) or active(False)");
    }

    [Fact]
    public void LightningStormDealsCorrectDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(HromConstants.Name);
        var useLightningStrike = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [1,0,0,0]);
        var useLightningStorm = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,0,0,1,1,1], [1,1,1,0]);

        // Act
        var strikeResult = battleLogic.AbilitiesUsed(1, useLightningStrike, useLightningStrike.SpentEnergy!);
        strikeResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        var stormResult = battleLogic.AbilitiesUsed(1, useLightningStorm, useLightningStorm.SpentEnergy!);
        stormResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(30);
        battleLogic.GetBattlePlayer(2).Champions[1].Health.Should().Be(55);
        battleLogic.GetBattlePlayer(2).Champions[2].Health.Should().Be(55);
    }
}
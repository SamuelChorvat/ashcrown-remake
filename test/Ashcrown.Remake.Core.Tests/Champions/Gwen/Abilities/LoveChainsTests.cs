using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Gwen.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Gwen.Abilities;

public class LoveChainsTests
{
    [Fact]
    public void LoveChainsApplyCorrectAes()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(GwenConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 0, 0, 1, 1, 1], [0, 1, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        for (var i = 0; i < 3; i++)
        {
            battleLogic.GetBattlePlayer(2).Champions[i].ActiveEffectController
                .ActiveEffectPresentByActiveEffectName(GwenConstants.LoveChainsActiveEffect).Should().BeTrue();
            battleLogic.GetBattlePlayer(2).Champions[i].ActiveEffectController
                .GetActiveEffectByName(GwenConstants.LoveChainsActiveEffect)!.TimeLeft.Should().Be(1);
            battleLogic.GetBattlePlayer(2).Champions[i].ActiveEffectController
                .GetActiveEffectByName(GwenConstants.LoveChainsActiveEffect)!.Stun.Should().BeTrue();
            battleLogic.GetBattlePlayer(2).Champions[i].ActiveEffectController
                .GetActiveEffectByName(GwenConstants.LoveChainsActiveEffect)!.StunType![0].Should().Be(AbilityClass.All);
        }
    }

}
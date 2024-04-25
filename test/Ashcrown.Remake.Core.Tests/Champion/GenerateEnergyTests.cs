using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champion;

public class GenerateEnergyTests
{
    [Fact]
    public void GenerateEnergyIsAbleToGenerateAllEnergiesAtLeastOnceIn1000()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AlthalosConstants.Name);
        var energyGenerated = new[] {false, false, false, false};
        
        // Act
        for (var i = 0; i < 1000; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                battleLogic.GetBattlePlayer(1).Energy[j] = 0;
            }
            
            battleLogic.GetBattlePlayer(1).GenerateEnergy();
            battleLogic.GetBattlePlayer(1).GetTotalEnergy().Should().Be(3);
            for (var j = 0; j < 4; j++)
            {
                if (!energyGenerated[j])
                {
                    energyGenerated[j] = battleLogic.GetBattlePlayer(1).Energy[j] > 0;
                }
            }
        }
        
        // Assert
        for (var i = 0; i < 4; i++)
        {
            energyGenerated[i].Should().BeTrue();
        }
    }
    
    [Fact]
    public void GenerateEnergyIsAbleToGenerateAllEnergiesTwiceIn10000()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AlthalosConstants.Name);
        var energyGenerated = new[] {false, false, false, false};
        
        // Act
        for (var i = 0; i < 1000; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                battleLogic.GetBattlePlayer(1).Energy[j] = 0;
            }
            
            battleLogic.GetBattlePlayer(1).GenerateEnergy();
            battleLogic.GetBattlePlayer(1).GetTotalEnergy().Should().Be(3);
            for (var j = 0; j < 4; j++)
            {
                if (!energyGenerated[j])
                {
                    energyGenerated[j] = battleLogic.GetBattlePlayer(1).Energy[j] == 2;
                }
            }
        }
        
        // Assert
        for (var i = 0; i < 4; i++)
        {
            energyGenerated[i].Should().BeTrue();
        }
    }

    [Fact]
    public void GenerateEnergyNeverGeneratesMoreThan2OfSameEnergiesIn10_000()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AlthalosConstants.Name);
        
        // Act && Assert
        for (var i = 0; i < 1000; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                battleLogic.GetBattlePlayer(1).Energy[j] = 0;
            }
            
            battleLogic.GetBattlePlayer(1).GenerateEnergy();
            battleLogic.GetBattlePlayer(1).GetTotalEnergy().Should().Be(3);
            for (var j = 0; j < 4; j++)
            {
                battleLogic.GetBattlePlayer(1).Energy[j].Should().BeLessThan(3);
            }
        }
    }

    [Theory]
    [InlineData(EnergyType.Blue,EnergyType.Red,EnergyType.Green)]
    [InlineData(EnergyType.Red,EnergyType.Green, EnergyType.Purple)]
    [InlineData(EnergyType.Blue,EnergyType.Green, EnergyType.Purple)]
    [InlineData(EnergyType.Blue,EnergyType.Red,EnergyType.Purple)]
    public void GenerateEnergyIsAbleToGenerate3UniqueEnergy(EnergyType energyType1, EnergyType energyType2, EnergyType energyType3)
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AlthalosConstants.Name);
        var threeUniqueGenerated = false;
        
        // Act
        for (var i = 0; i < 1000; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                battleLogic.GetBattlePlayer(1).Energy[j] = 0;
            }
            
            battleLogic.GetBattlePlayer(1).GenerateEnergy();
            battleLogic.GetBattlePlayer(1).GetTotalEnergy().Should().Be(3);
            threeUniqueGenerated =
                battleLogic.GetBattlePlayer(1).Energy[(int) energyType1] == 1 &&
                battleLogic.GetBattlePlayer(1).Energy[(int) energyType2] == 1 &&
                battleLogic.GetBattlePlayer(1).Energy[(int) energyType3] == 1;
            
            if (threeUniqueGenerated) break;
        }
        
        // Assert
        threeUniqueGenerated.Should().BeTrue();
    }

    [Fact]
    public void GenerateEnergyGeneratesCorrectAmountWhenOneChampionIsDead()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AlthalosConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Alive = false;
        var energyGenerated = new[] {false, false, false, false};
        
        // Act
        for (var i = 0; i < 1000; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                battleLogic.GetBattlePlayer(1).Energy[j] = 0;
            }
            
            battleLogic.GetBattlePlayer(1).GenerateEnergy();
            battleLogic.GetBattlePlayer(1).GetTotalEnergy().Should().Be(2);
            for (var j = 0; j < 4; j++)
            {
                if (!energyGenerated[j])
                {
                    energyGenerated[j] = battleLogic.GetBattlePlayer(1).Energy[j] > 0;
                }
            }
        }
        
        // Assert
        for (var i = 0; i < 4; i++)
        {
            energyGenerated[i].Should().BeTrue();
        }
    }

    [Fact]
    public void GenerateEnergyGeneratesCorrectAmountWhenTwoChampionsAreDead()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AlthalosConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Alive = false;
        battleLogic.GetBattlePlayer(1).Champions[1].Alive = false;
        var energyGenerated = new[] {false, false, false, false};
        
        // Act
        for (var i = 0; i < 1000; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                battleLogic.GetBattlePlayer(1).Energy[j] = 0;
            }
            
            battleLogic.GetBattlePlayer(1).GenerateEnergy();
            battleLogic.GetBattlePlayer(1).GetTotalEnergy().Should().Be(1);
            for (var j = 0; j < 4; j++)
            {
                if (!energyGenerated[j])
                {
                    energyGenerated[j] = battleLogic.GetBattlePlayer(1).Energy[j] > 0;
                }
            }
        }
        
        // Assert
        for (var i = 0; i < 4; i++)
        {
            energyGenerated[i].Should().BeTrue();
        }
    }
}
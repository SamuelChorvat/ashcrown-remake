using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Akio.Champion;
using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Champions.Aniel.Champion;
using Ashcrown.Remake.Core.Champions.Arabela.Champion;
using Ashcrown.Remake.Core.Champions.Ash.Champion;
using Ashcrown.Remake.Core.Champions.Azrael.Champion;
using Ashcrown.Remake.Core.Champions.Branley.Champion;
using Ashcrown.Remake.Core.Champions.Braya.Champion;
using Ashcrown.Remake.Core.Champions.Cedric.Champion;
using Ashcrown.Remake.Core.Champions.Cleo.Champion;
using Ashcrown.Remake.Core.Champions.Cronos.Champion;
using Ashcrown.Remake.Core.Champions.Dex.Champion;
using Ashcrown.Remake.Core.Champions.Dura.Champion;
using Ashcrown.Remake.Core.Champions.Eluard.Champion;
using Ashcrown.Remake.Core.Champions.Evanore.Champion;
using Ashcrown.Remake.Core.Champions.Fae.Champion;
using Ashcrown.Remake.Core.Champions.Garr.Champion;
using Ashcrown.Remake.Core.Champions.Gruber.Champion;
using Ashcrown.Remake.Core.Champions.Gwen.Champion;
using Ashcrown.Remake.Core.Champions.Hannibal.Champion;
using Ashcrown.Remake.Core.Champions.Hrom.Champion;
using Ashcrown.Remake.Core.Champions.Izrin.Champion;
using Ashcrown.Remake.Core.Champions.Jafali.Champion;
using Ashcrown.Remake.Core.Champions.Jane.Champion;
using Ashcrown.Remake.Core.Champions.Khan.Champion;
using Ashcrown.Remake.Core.Champions.Sarfu.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Ability;

public class StandardInvulnerabiltyTests
{
    [Theory]
    [InlineData(AkioConstants.TestName, AkioConstants.FlowDisruptionActiveEffect)]
    [InlineData(AlthalosConstants.TestName, AlthalosConstants.DivineShieldActiveEffect)]
    [InlineData(AnielConstants.TestName, AnielConstants.AdrenalineRushActiveEffect)]
    [InlineData(ArabelaConstants.TestName, ArabelaConstants.HolyShieldActiveEffect)]
    [InlineData(AshConstants.TestName, AshConstants.FireBlockActiveEffect)]
    [InlineData(AzraelConstants.TestName, AzraelConstants.DisappearActiveEffect)]
    [InlineData(BranleyConstants.TestName, BranleyConstants.DefensiveManeuverActiveEffect)]
    [InlineData(BrayaConstants.TestName, BrayaConstants.DisengageActiveEffect)]
    [InlineData(CedricConstants.TestName, CedricConstants.NetherSlipActiveEffect)]
    [InlineData(CleoConstants.TestName, CleoConstants.SwarmSummoningActiveEffect)]
    [InlineData(CronosConstants.TestName, CronosConstants.MagitechCircuitryActiveEffect)]
    [InlineData(DexConstants.TestName, DexConstants.RatPackActiveEffect)]
    [InlineData(DuraConstants.TestName, DuraConstants.MeditateActiveEffect)]
    [InlineData(EluardConstants.TestName, EluardConstants.EvadeActiveEffect)]
    [InlineData(EvanoreConstants.TestName, EvanoreConstants.HoundDefenseActiveEffect)]
    [InlineData(FaeConstants.TestName, FaeConstants.FlashActiveEffect)]
    [InlineData(GarrConstants.TestName, GarrConstants.IntimidatingShoutActiveEffect)]
    [InlineData(GruberConstants.TestName, GruberConstants.DNAEnhancementActiveEffect)]
    [InlineData(GwenConstants.TestName, GwenConstants.LightsOutActiveEffect)]
    [InlineData(HannibalConstants.TestName, HannibalConstants.DemonicSkinActiveEffect)]
    [InlineData(HromConstants.TestName, HromConstants.StormShieldActiveEffect)]
    [InlineData(IzrinConstants.TestName, IzrinConstants.WillOfTheUndeadActiveEffect)]
    [InlineData(JafaliConstants.TestName, JafaliConstants.DevilsGameActiveEffect)]
    [InlineData(JaneConstants.TestName, JaneConstants.MisdirectionActiveEffect)]
    [InlineData(KhanConstants.TestName, KhanConstants.PerceptionActiveEffect)]
    [InlineData(SarfuConstants.TestName, SarfuConstants.DeflectActiveEffect)]
    public void InvulnerabilityShouldCorrectlyApplyActiveEffects(string championName, string activeEffectName)
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(championName);
        var useInvulnerability = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,4, 
            [1,0,0,0,0,0], [0,1,0,0]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useInvulnerability, useInvulnerability.SpentEnergy!).Should().BeTrue();
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(activeEffectName).Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(activeEffectName)!.Invulnerability.Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(activeEffectName)!.TimeLeft.Should().Be(1);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(activeEffectName)!.TypeOfInvulnerability![0].Should().Be(AbilityClass.All);
    }

    [Theory]
    [InlineData(AkioConstants.TestName, AkioConstants.FlowDisruptionActiveEffect)]
    [InlineData(AlthalosConstants.TestName, AlthalosConstants.DivineShieldActiveEffect)]
    [InlineData(AnielConstants.TestName, AnielConstants.AdrenalineRushActiveEffect)]
    [InlineData(ArabelaConstants.TestName, ArabelaConstants.HolyShieldActiveEffect)]
    [InlineData(AshConstants.TestName, AshConstants.FireBlockActiveEffect)]
    [InlineData(AzraelConstants.TestName, AzraelConstants.DisappearActiveEffect)]
    [InlineData(BranleyConstants.TestName, BranleyConstants.DefensiveManeuverActiveEffect)]
    [InlineData(BrayaConstants.TestName, BrayaConstants.DisengageActiveEffect)]
    [InlineData(CedricConstants.TestName, CedricConstants.NetherSlipActiveEffect)]
    [InlineData(CleoConstants.TestName, CleoConstants.SwarmSummoningActiveEffect)]
    [InlineData(CronosConstants.TestName, CronosConstants.MagitechCircuitryActiveEffect)]
    [InlineData(DexConstants.TestName, DexConstants.RatPackActiveEffect)]
    [InlineData(DuraConstants.TestName, DuraConstants.MeditateActiveEffect)]
    [InlineData(EluardConstants.TestName, EluardConstants.EvadeActiveEffect)]
    [InlineData(EvanoreConstants.TestName, EvanoreConstants.HoundDefenseActiveEffect)]
    [InlineData(FaeConstants.TestName, FaeConstants.FlashActiveEffect)]
    [InlineData(GarrConstants.TestName, GarrConstants.IntimidatingShoutActiveEffect)]
    [InlineData(GruberConstants.TestName, GruberConstants.DNAEnhancementActiveEffect)]
    [InlineData(GwenConstants.TestName, GwenConstants.LightsOutActiveEffect)]
    [InlineData(HannibalConstants.TestName, HannibalConstants.DemonicSkinActiveEffect)]
    [InlineData(HromConstants.TestName, HromConstants.StormShieldActiveEffect)]
    [InlineData(IzrinConstants.TestName, IzrinConstants.WillOfTheUndeadActiveEffect)]
    [InlineData(JafaliConstants.TestName, JafaliConstants.DevilsGameActiveEffect)]
    [InlineData(JaneConstants.TestName, JaneConstants.MisdirectionActiveEffect)]
    [InlineData(KhanConstants.TestName, KhanConstants.PerceptionActiveEffect)]
    [InlineData(SarfuConstants.TestName, SarfuConstants.DeflectActiveEffect)]
    public void InvulnerabilityShouldBeInvulnerability(string championName, string activeEffectName)
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(championName, 
            AlthalosConstants.TestName);
        var useInvulnerability = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,4, 
            [1,0,0,0,0,0], [0,1,0,0]);
        var useDamage = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,1, 
            [0,0,0,1,0,0], [0,0,1,0]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useInvulnerability, useInvulnerability.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, useDamage, useDamage.SpentEnergy!).Should().BeFalse();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(100);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(activeEffectName).Should().BeFalse();
    }
}
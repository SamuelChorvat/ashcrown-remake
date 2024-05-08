using Ashcrown.Remake.Core.Battle;
using Ashcrown.Remake.Core.Battle.Enums;
using Ashcrown.Remake.Core.Champion;
using Ashcrown.Remake.Core.Champions.Arabela.Champion;
using Ashcrown.Remake.Core.Champions.Branley.Champion;
using Ashcrown.Remake.Core.Champions.Cedric.Champion;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Ai;

public class ChampionsAiTests
{
    [Theory]
    [MemberData(nameof(UniqueTeamsData))]
    public void AllAiTeamCombinations_ShouldAlwaysWin_VersusInactivePlayer(string[] uniqueTeam)
    {
        // Arrange
        var battleLogic = SetupAiBattle(uniqueTeam);
        if (battleLogic.GetBattlePlayer(1).Champions.Any(x => !x.AiReady))
        {
            // Skip teams that contain champions not setup to work with AI
            return;
        }
        
        // Act
        while (battleLogic.TurnCount < BattleConstants.TurnLimit)
        {
            if (battleLogic.BattleEndTime == null) battleLogic.EndAiTurn();
            
            // Non-AI player just passes
            if (battleLogic.BattleEndTime == null) battleLogic.EndTurnProcesses(2);
            
            if (battleLogic.BattleEndTime != null) break;
        }
        
        // Assert
        battleLogic.BattleEndedUpdates.Should().NotBeNull();
        battleLogic.BattleEndedUpdates![0].Should().Be(BattleStatus.Victory);
    }
    
    [Fact]
    public void DebugProblematicTeam() //Used for team debugging if they fail the inactivity test
    {
        for (var i = 0; i < 100; i++)
        {
            // Arrange
            var battleLogic = SetupAiBattle([ArabelaConstants.TestName, BranleyConstants.TestName, CedricConstants.Name]);
        
            // Act
            while (battleLogic.TurnCount < BattleConstants.TurnLimit)
            {
                if (battleLogic.TurnCount > 90)
                {
                    //Something is likely going wrong
                    var breakPoint = 1;
                }
            
                if (battleLogic.BattleEndTime == null) battleLogic.EndAiTurn();
                if (battleLogic.BattleEndTime == null) battleLogic.EndTurnProcesses(2);
                if (battleLogic.BattleEndTime != null) break;
            }
        
            // Assert
            battleLogic.BattleEndedUpdates.Should().NotBeNull();
            battleLogic.BattleEndedUpdates![0].Should().Be(BattleStatus.Victory);
        }
    }
    
    private static BattleLogic SetupAiBattle(string[] championNames)
    {
        var battleLogic = new BattleLogic(false,
            LoggerFactory.Create(builder =>
            {
                builder
                    .AddDebug()
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Trace);
            }));
        
        battleLogic.SetBattlePlayer(1, "AshcrownNET", 
            championNames, true);
        battleLogic.SetBattlePlayer(2, "Player2", 
            championNames, false);
        battleLogic.InitializePlayers();

        return battleLogic;
    }
    
    public static IEnumerable<object[]> UniqueTeamsData()
    {
        var n = ChampionConstants.AllChampionsNames.Length;
        for (var i = 0; i < n - 2; i++)
        {
            for (var j = i + 1; j < n - 1; j++)
            {
                for (var k = j + 1; k < n; k++)
                {
                    yield return
                    [
                        new[] {
                            ChampionConstants.AllChampionsNames[i],
                            ChampionConstants.AllChampionsNames[j],
                            ChampionConstants.AllChampionsNames[k]
                        }
                    ];
                }
            }
        }
    }
}
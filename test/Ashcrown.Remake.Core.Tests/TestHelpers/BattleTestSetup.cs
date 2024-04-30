using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Battle;
using Ashcrown.Remake.Core.Battle.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Tests.TestHelpers;

public static class BattleTestSetup
{
    public static IBattleLogic StandardMockedSetupWithSingleChampion(string championName)
    {
        var battleLogic = new BattleLogic(false,
            LoggerFactory.Create(builder =>
            {
                builder
                    .AddDebug()
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Trace);
            }));
        
        battleLogic.SetBattlePlayer(1, "Player1", 
            [championName, championName, championName], false);
        battleLogic.SetBattlePlayer(2, "Player2", 
            [championName, championName, championName], false);

        battleLogic.GetBattlePlayer(1).Energy[(int) EnergyType.Blue] = 10;
        battleLogic.GetBattlePlayer(1).Energy[(int) EnergyType.Red] = 10;
        battleLogic.GetBattlePlayer(1).Energy[(int) EnergyType.Green] = 10;
        battleLogic.GetBattlePlayer(1).Energy[(int) EnergyType.Purple] = 10;
        
        battleLogic.GetBattlePlayer(2).Energy[(int) EnergyType.Blue] = 10;
        battleLogic.GetBattlePlayer(2).Energy[(int) EnergyType.Red] = 10;
        battleLogic.GetBattlePlayer(2).Energy[(int) EnergyType.Green] = 10;
        battleLogic.GetBattlePlayer(2).Energy[(int) EnergyType.Purple] = 10;

        return battleLogic;
    }
    
    public static IBattleLogic StandardMockedSetupWithTwoDifferentChampions(string player1ChampionName, 
        string player2ChampionName)
    {
        var battleLogic = new BattleLogic(false,
            LoggerFactory.Create(builder =>
            {
                builder
                    .AddDebug()
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Trace);
            }));
        
        battleLogic.SetBattlePlayer(1, "Player1", 
            [player1ChampionName, player1ChampionName, player1ChampionName], false);
        battleLogic.SetBattlePlayer(2, "Player2", 
            [player2ChampionName, player2ChampionName, player2ChampionName], false);
        
        battleLogic.GetBattlePlayer(1).Energy[(int) EnergyType.Blue] = 99;
        battleLogic.GetBattlePlayer(1).Energy[(int) EnergyType.Red] = 99;
        battleLogic.GetBattlePlayer(1).Energy[(int) EnergyType.Green] = 99;
        battleLogic.GetBattlePlayer(1).Energy[(int) EnergyType.Purple] = 99;

        return battleLogic;
    }
}
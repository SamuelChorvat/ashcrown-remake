using Ashcrown.Remake.Api.Extensions;
using Ashcrown.Remake.Api.Models.Enums;
using Ashcrown.Remake.Core.Battle;
using Microsoft.Extensions.Logging.Debug;

namespace Ashcrown.Remake.Api.Models;

public class StartedMatch
{
    public AcceptedMatch AcceptedMatch { get; init; }
    public BattleLogic? BattleLogic { get; set; }
    public DateTime CreatedAt = DateTime.UtcNow;

    public StartedMatch(AcceptedMatch acceptedMatch)
    {
        AcceptedMatch = acceptedMatch;
        
        var firstPlayerIndex = new Random().Next(2);
        var secondPlayerIndex = 1 - firstPlayerIndex;
        
        if (acceptedMatch.FoundMatch.FindMatchType.IsDraft())
        {
            firstPlayerIndex = acceptedMatch.FoundMatch.PlayerNames[0]
                .Equals(acceptedMatch.DraftMatch!.DraftLogic!.Players[0])
                ? 0
                : 1;
            secondPlayerIndex = 1 - firstPlayerIndex;
        }
        
        BattleLogic =
            new BattleLogic(acceptedMatch.FoundMatch.FindMatchType == FindMatchType.BlindAi,
                new LoggerFactory(new[] {new DebugLoggerProvider()}));
            
        BattleLogic.SetBattlePlayer(1, 
            acceptedMatch.FoundMatch.PlayerNames[firstPlayerIndex], 
            acceptedMatch.FoundMatch.PlayerBlindChampions[firstPlayerIndex], 
            acceptedMatch.FoundMatch.PlayerNames[firstPlayerIndex].Equals("AshcrownNET"));

        BattleLogic.SetBattlePlayer(2, 
            acceptedMatch.FoundMatch.PlayerNames[secondPlayerIndex], 
            acceptedMatch.FoundMatch.PlayerBlindChampions[secondPlayerIndex], 
            acceptedMatch.FoundMatch.PlayerNames[secondPlayerIndex].Equals("AshcrownNET"));
            
        BattleLogic.InitializePlayers();
    }
}
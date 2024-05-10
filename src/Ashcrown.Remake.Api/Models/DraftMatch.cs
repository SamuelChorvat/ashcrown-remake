using Ashcrown.Remake.Core.Draft;

namespace Ashcrown.Remake.Api.Models;

public class DraftMatch
{
    public FoundMatch FoundMatch { get; init; }
    public DraftLogic? DraftLogic { get; init; }
    public DateTime CreatedAt = DateTime.UtcNow;

    public DraftMatch(FoundMatch foundMatch)
    {
        FoundMatch = foundMatch;
        var firstPlayerIndex = new Random().Next(2);
        var secondPlayerIndex = 1 - firstPlayerIndex;
        DraftLogic = new DraftLogic
        {
            Players =
            {
                [0] = FoundMatch.PlayerNames[firstPlayerIndex],
                [1] = FoundMatch.PlayerNames[secondPlayerIndex]
            }
        };
        DraftLogic.WhoseTurn = DraftLogic.Players[0];
    }
}
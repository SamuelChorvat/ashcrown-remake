using Ashcrown.Remake.Core.Champion;

namespace Ashcrown.Remake.Api.Models;

public class PlayerSession
{
    private readonly string _secret = Guid.NewGuid().ToString();
    private bool _secretReturned;
    
    public string Secret 
    {
        get
        {
            if (_secretReturned) return string.Empty;
            _secretReturned = true;
            return _secret;
        }
    }
    
    public required string PlayerName { get; init; }
    public DateTime LastRequestDateTime { get; set; } = DateTime.UtcNow;
    public string IconName { get; set; } = AshcrownApiConstants.IconNames.OrderBy(x => Guid.NewGuid()).First();
    public string CrownName { get; set; } = AshcrownApiConstants.CrownNames.OrderBy(x => Guid.NewGuid()).First();
    public string[] BlindChampions { get; set; } = ChampionConstants.GetRandomChampionNames(3);
   
    public int DraftWins { get; set; }
    public int DraftLosses { get; set; }
    public int BlindWins { get; set; }
    public int BlindLosses { get; set; }
    public List<MatchRecord> MatchHistory { get; set; } = [];

    public bool ValidateSecret(string secret)
    {
        return _secret.Equals(secret);
    }
}
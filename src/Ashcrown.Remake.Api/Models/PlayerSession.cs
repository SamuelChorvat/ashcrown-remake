using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Champions.Eluard.Champion;
using Ashcrown.Remake.Core.Champions.Sarfu.Champion;

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
    
    public DateTime LastRequestDateTime { get; set; } = DateTime.UtcNow;
    public string IconName { get; set; } = AshcrownApiConstants.IconNames.OrderBy(x => Guid.NewGuid()).First();
    public string CrownName { get; set; } = AshcrownApiConstants.CrownNames.OrderBy(x => Guid.NewGuid()).First();
    public string[] BlindChampions { get; set; } = 
        [AlthalosConstants.Name, EluardConstants.Name, SarfuConstants.Name];

    public bool ValidateSecret(string secret)
    {
        return _secret.Equals(secret);
    }
}
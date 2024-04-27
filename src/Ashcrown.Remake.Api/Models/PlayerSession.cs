using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Champions.Eluard.Champion;
using Ashcrown.Remake.Core.Champions.Sarfu.Champion;

namespace Ashcrown.Remake.Api.Models;

public class PlayerSession
{
    public DateTime LastRequestDateTime { get; set; } = DateTime.UtcNow;
    public string IconName { get; set; } = AshcrownApiConstants.IconNames.OrderBy(x => Guid.NewGuid()).First();
    public string CrownName { get; set; } = AshcrownApiConstants.CrownNames.OrderBy(x => Guid.NewGuid()).First();
    public string[] BlindChampions { get; set; } = 
        [AlthalosConstants.Name, EluardConstants.Name, SarfuConstants.Name];
}
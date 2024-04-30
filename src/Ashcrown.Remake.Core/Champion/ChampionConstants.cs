using Ashcrown.Remake.Core.Champions.Akio.Champion;
using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Champions.Aniel.Champion;
using Ashcrown.Remake.Core.Champions.Eluard.Champion;
using Ashcrown.Remake.Core.Champions.Sarfu.Champion;

namespace Ashcrown.Remake.Core.Champion;

public static class ChampionConstants
{
    public const int ChampionMaxHealth = 100;
    public const int MaxNumberOfCurrentAbilities = 4;
    public static readonly string[] AllChampionsNames = [AkioConstants.Name,
        AlthalosConstants.Name, AnielConstants.Name, EluardConstants.Name, SarfuConstants.Name];
}
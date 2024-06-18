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
using Ashcrown.Remake.Core.Champions.Lexi.Champion;
using Ashcrown.Remake.Core.Champions.Lucifer.Champion;
using Ashcrown.Remake.Core.Champions.Luther.Champion;
using Ashcrown.Remake.Core.Champions.Moroz.Champion;
using Ashcrown.Remake.Core.Champions.Nazuc.Champion;
using Ashcrown.Remake.Core.Champions.Sarfu.Champion;

namespace Ashcrown.Remake.Core.Champion;

public static class ChampionConstants
{
    public const int ChampionMaxHealth = 100;
    public const int MaxNumberOfCurrentAbilities = 4;
    public static readonly string[] AllChampionsNames = [AkioConstants.Name,
        AlthalosConstants.Name, AnielConstants.Name, ArabelaConstants.Name,
        AshConstants.Name, AzraelConstants.Name, BranleyConstants.Name, 
        BrayaConstants.Name, CedricConstants.Name, CleoConstants.Name,
        CronosConstants.Name, DexConstants.Name, DuraConstants.Name, 
        EluardConstants.Name, EvanoreConstants.Name, FaeConstants.Name,
        GarrConstants.Name, GruberConstants.Name, GwenConstants.Name,
        HannibalConstants.Name, HromConstants.Name, IzrinConstants.Name,
        JafaliConstants.Name, JaneConstants.Name, KhanConstants.Name,
        LexiConstants.Name, LuciferConstants.Name, LutherConstants.Name,
        MorozConstants.Name, NazucConstants.Name, SarfuConstants.Name];
    
    public static string[] GetRandomChampionNames(int count)
    {
        var random = new Random();
        return AllChampionsNames.OrderBy(_ => random.Next()).Take(count).ToArray();
    }
}
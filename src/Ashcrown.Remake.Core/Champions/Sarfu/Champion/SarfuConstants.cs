using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Sarfu.Champion;

public abstract class SarfuConstants : IChampionConstants
{
    public static string Name => "Sarfu";
    public static string Title => "The Barbarian";

    public static string Bio => "Expelled from this tribe, Sarfu spent years wandering the Ashland wastes. " +
                                "He survived off of the rotten flesh of animal carcasses and the fresh hot blood of the traders he found along the way. " +
                                "Stronger than three oxen combined and faster than his size would seem to allow, " +
                                "Sarfu was born to rip and tear through his enemies. " +
                                "His former tribe now mere corpses and with their blood in Sarfu’s belly, he seeks a new set of mortals to wreak his havoc upon.";
    public static string Artist => "WolfsBane";
    public static int[] Attributes => [2, 2, 0, 2];

    public const string TestName = "Sarfu";
    
    public const string Deflect = "Deflect";
    public const string Duel = "Duel";
    public const string Overpower = "Overpower";
    public const string Charge = "Charge";

    public const string DeflectActiveEffect = "DeflectActiveEffect";
    public const string DuelMeActiveEffect = "DuelMeActiveEffect";
    public const string DuelTargetActiveEffect = "DuelTargetActiveEffect";
}
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Cedric.Champion;

public abstract class CedricConstants : IChampionConstants
{
    public static string Name => "Cedric";
    public static string Title => "The Black Mage";

    public static string Bio => "One would think the practising of black magic would lead to one’s banishment from the mage’s guild, " +
                                "but Cedric is a more than esteemed member of the community. " +
                                "Who better to learn about the dark arts than a respected mage? " +
                                "Who better to practise it, deep into the night, than an upstanding citizen like Cedric? " +
                                "Why, who’s better at all?";

    public static string Artist => "Yann Trolong";
    public static int[] Attributes => [1, 2, 1, 0];

    public const string TestName = "Cedric";
    
    public const string DarkSoul = "Dark Soul";
    public const string MirrorImage = "Mirror Image";
    public const string NetherSlip = "Nether Slip";
    public const string TimeWarp = "Time Warp";

    public const string DarkSoulKillActiveEffect = "DarkSoulKillActiveEffect";
    public const string DarkSoulMeActiveEffect = "DarkSoulMeActiveEffect";
    public const string DarkSoulTargetActiveEffect = "DarkSoulTargetActiveEffect";
    public const string MirrorImageActiveEffect = "MirrorImageActiveEffect";
    public const string NetherSlipActiveEffect = "NetherSlipActiveEffect";
    public const string TimeWarpActiveEffect = "TimeWarpActiveEffect";
}
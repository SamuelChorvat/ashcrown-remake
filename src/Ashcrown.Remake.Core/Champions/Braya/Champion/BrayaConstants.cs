using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Braya.Champion;

public abstract class BrayaConstants : IChampionConstants
{
    public static string Name => "Braya";
    public static string Title => "The Hunter";

    public static string Bio => "Shanks of venison, rumps of boar and bolts of bear furs are a necessity for many of the elite of Coela, and they have to come from somewhere. " +
                                "Light footed and keen eyed, Braya is one with the forest, the mountains and with land of Coela itself. " +
                                "Now the elites fiend for something a little more unsavoury than what is usually served at their typical banquet dinners. " +
                                "If the price is good enough, Braya doesn't mind moving her hunting grounds onto the battlefield.";

    public static string Artist => "WolfsBane";
    public static int[] Attributes => [2, 2, 0, 0];

    public const string TestName = "Braya";
    
    public const string Disengage = "Disengage";
    public const string HuntersFocus = "Hunter's Focus";
    public const string KillShot = "Kill Shot";
    public const string QuickShot = "Quick Shot";

    public const string DisengageActiveEffect = "DisengageActiveEffect";
    public const string HuntersFocusIgnoreActiveEffect = "HuntersFocusIgnoreActiveEffect";
    public const string HuntersFocusStacksActiveEffect = "HuntersFocusStacksActiveEffect";
    public const string QuickShotActiveEffect = "QuickShotActiveEffect";
}
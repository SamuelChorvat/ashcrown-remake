using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Azrael.Champion;

public abstract class AzraelConstants : IChampionConstants
{
    public static string Name => "Azrael";
    public static string Title => "The Reaper";

    public static string Bio => "Nothing is known of Azrael’s past except that it was once damned to roam Coela, living on the boundary of between life and death. " +
                                "It’s present is filled with the reaping of souls, some of which are due on time to go to the great beyond, many of which are not. " +
                                "Azrael sees that his future holds a great deal more than floating on his solitary plane, and is desperate to escape. " +
                                "He is secure in the fact that he will one day manage to leave his current existence, as long as he reaps enough souls.";

    public static string Artist => "Yann Trolong";
    public static int[] Attributes => [2, 2, 0, 0];
    
    public const string TestName = "Azrael";
    
    public const string CursedMark = "Cursed Mark";
    public const string Disappear = "Disappear";
    public const string Reap = "Reap";
    public const string SoulRealm = "Soul Realm";

    public const string CursedMarkMeActiveEffect = "CursedMarkMeActiveEffect";
    public const string CursedMarkTargetActiveEffect = "CursedMarkTargetActiveEffect";
    public const string DisappearActiveEffect = "DisappearActiveEffect";
    public const string ReapMeActiveEffect = "ReapMeActiveEffect";
    public const string ReapTargetActiveEffect = "ReapTargetActiveEffect";
    public const string ReapTriggeredTargetActiveEffect = "ReapTriggeredTargetActiveEffect";
    public const string SoulRealmActiveEffect = "SoulRealmActiveEffect";
}
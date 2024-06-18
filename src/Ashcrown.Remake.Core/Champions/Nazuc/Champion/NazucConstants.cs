using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Nazuc.Champion;

public class NazucConstants : IChampionConstants
{
    public static string Name => "Nazuc";
    public static string Title => "The Stalker";

    public static string Bio =>
        "Many travellers go to the jungles of Arikem with the goal of mapping out new sections of the unforgiving rainforest. " +
        "With heads full of the promise of fame, and their royal contracts promising even more on the discovery of new resources, " +
        "they never take a moment to take a true look through the foliage of the jungle. " +
        "It’s only when the leaves rustle, or a branch breaks that they realise where they truly are. By then, it’s too late.";

    public static string Artist => "WolfsBane";
    public static int[] Attributes => [2, 2, 0, 1];
    
    public const string TestName = "Nazuc";
    
    public const string HuntersMark = "Hunter's Mark";
    public const string SpearBarrage = "Spear Barrage";
    public const string SpearThrow = "Spear Throw";
    public const string SeasonedStalker = "Seasoned Stalker";

    public const string HuntersMarkActiveEffect = "HuntersMarkActiveEffect";
    public const string SpearBarrageBuffActiveEffect = "SpearBarrageBuffActiveEffect";
    public const string SpearBarrageMeActiveEffect = "SpearBarrageMeActiveEffect";
    public const string SpearBarrageTargetActiveEffect = "SpearBarrageTargetActiveEffect";
    public const string SeasonedStalkerActiveEffect = "SeasonedStalkerActiveEffect";
}
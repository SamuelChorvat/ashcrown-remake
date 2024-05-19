using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Khan.Champion;

public abstract class KhanConstants : IChampionConstants
{
    public static string Name => "Khan";
    public static string Title => "The Protector";

    public static string Bio => "What god he follows no one knows. What he protects anyone from, if anything at all, is still unknown. " +
                                "Who he protects seems to depend on his passing fancies. Khan fashioned himself the title seemingly out of nowhere, " +
                                "introducing himself with it appended to his name to anyone who would listen. " +
                                "Whatever the state of his mind may be, everyone agrees that they’d rather have him on their side than against it.";

    public static string Artist => "Yann Trolong";
    public static int[] Attributes => [2, 2, 0, 1];

    public const string TestName = "Khan";
    
    public const string Bladestorm = "Bladestorm";
    public const string HandOfTheProtector = "Hand of the Protector";
    public const string MortalStrike = "Mortal Strike";
    public const string Perception = "Perception";

    public const string BladestormMeActiveEffect = "BladestormMeActiveEffect";
    public const string BladestormTargetActiveEffect = "BladestormTargetActiveEffect";
    public const string HandOfTheProtectorActiveEffect = "HandOfTheProtectorActiveEffect";
    public const string MortalStrikeActiveEffect = "MortalStrikeActiveEffect";
    public const string PerceptionActiveEffect = "PerceptionActiveEffect";
}
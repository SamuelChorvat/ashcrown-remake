using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Dex.Champion;

public abstract class DexConstants : IChampionConstants
{
    public static string Name => "Dex";
    public static string Title => "The Nightblade";

    public static string Bio => "Sewer filth. Gutter scum. Rotten grime. " +
                                "These are all words that have been used to describe Dex, and all things he himself enjoys in plenty. " +
                                "Dex may not run anything significant in Ingbridge, but the people who do often call upon his services to keep things moving. " +
                                "In the rank streets of the capital, it is easy to find that Dex is far from the most disgusting thing there.";

    public static string Artist => "WolfsBane";
    public static int[] Attributes => [2, 1, 0, 1];

    public const string TestName = "Dex";
    
    public const string Garrote = "Garrote";
    public const string Nightblade = "Nightblade";
    public const string RatPack = "Rat Pack";
    public const string ShurikenThrow = "Shuriken Throw";

    public const string GarroteActiveEffect = "GarroteActiveEffect";
    public const string NightbladeActiveEffect = "NightbladeActiveEffect";
    public const string RatPackActiveEffect = "RatPackActiveEffect";
}
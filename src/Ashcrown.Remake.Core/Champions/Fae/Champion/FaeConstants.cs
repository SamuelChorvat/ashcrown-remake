using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Fae.Champion;

public abstract class FaeConstants : IChampionConstants
{
    public static string Name => "Fae";
    public static string Title => "The Witch";
    public static string Bio => "Deep in the Waldvollur forest, along the river and close to a certain peculiar cliff side a traveller can find a deep tunnel carved into the rocky wall. " +
                                "If they were to enter this tunnel and squeeze their way through the narrow passages, they would eventually find themselves in a large and homely cavern kept by a unique and powerful woman. " +
                                "Fae, the legendary witch of the Waldvollur forest is solitary and mysterious. Why she has come out of her home and travelled far past the Waldvollur treeline, know one knows yet.";

    public static string Artist => "WolfsBane";
    public static int[] Attributes => [1, 1, 1, 0];

    public const string TestName = "Fae";
    
    public const string Corruption = "Corruption";
    public const string DaggerStab = "Dagger Stab";
    public const string Flash = "Flash";
    public const string Revitalize = "Revitalize";

    public const string CorruptionActiveEffect = "CorruptionActiveEffect";
    public const string FlashActiveEffect = "FlashActiveEffect";
}
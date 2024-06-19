using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Nerissa.Champion;

public class NerissaConstants : IChampionConstants
{
    public static string Name => "Nerissa";
    public static string Title => "The Mermaid";

    public static string Bio =>
        "When a sailor crosses the Axeina sea, they may be able to hear a shimmering melody emanating from beneath the ripples of the water. " +
        "They find that the voice becomes clearer and louder as they spend more and more time looking over the taffrail. " +
        "Eventually they all reach the same conclusion. The only way to really hear the music in its purest form is to dive underneath surface of the sea itself. " +
        "It’s only until they’ve jumped in and swam down to it’s source, way past the point of no return, that they find the horror of Nerissa’s true form.";

    public static string Artist => "PiotrTekien's Games";
    public static int[] Attributes => [1, 2, 0, 1];

    public const string TestName = "Nerissa";
    
    public const string Overflow = "Overflow";
    public const string Drown = "Drown";
    public const string AncientSpirits = "Ancient Spirits";
    public const string MesmerizingWater = "Mesmerizing Water";

    public const string DrownActiveEffect = "DrownActiveEffect";
    public const string AncientSpiritsActiveEffect = "AncientSpiritsActiveEffect";
    public const string MesmerizingWaterActiveEffect = "MesmerizingWaterActiveEffect";
}
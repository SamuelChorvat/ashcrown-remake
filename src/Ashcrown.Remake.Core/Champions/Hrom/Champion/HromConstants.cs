using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Hrom.Champion;

public abstract class HromConstants : IChampionConstants
{
    public static string Name => "Hrom";
    public static string Title => "The Thunder King";
    public static string Bio => "Worshipped as a god in Waldvollur, Hrom rules over the north from the frigid skies through a combination of force and fear. " +
                                "Legend says of how aeons ago his mother was courted by the heavens themselves, leading to the conception of Hrom and his many siblings. " +
                                "Coddled by his superstitious mother who believed in a late prophecy describing how Hrom would be usurped by his own brothers and sisters, " +
                                "she led him to murder his family in a twisted quest to rule over Waldvollur’s sky. " +
                                "Warped by power and trampled by the guilt of what he has done, Hrom wanders the stars casting down his misdirected anger on anyone he sees fit to do so.";

    public static string Artist => "Yann Trolong";
    public static int[] Attributes => [3, 2, 0, 0];

    public const string TestName = "Hrom";
    
    public const string LightningBarrier = "Lightning Barrier";
    public const string LightningStorm = "Lightning Storm";
    public const string LightningStrike = "Lightning Strike";
    public const string StormShield = "Storm Shield";

    public const string LightningBarrierActiveEffect = "LightningBarrierActiveEffect";
    public const string LightningBarrierCounterActiveEffect = "LightningBarrierCounterActiveEffect";
    public const string LightningBarrierEndActiveEffect = "LightningBarrierEndActiveEffect";
    public const string LightningStrikeActiveEffect = "LightningStrikeActiveEffect";
    public const string LightningStrikeHelperActiveEffect = "LightningStrikeHelperActiveEffect";
    public const string StormShieldActiveEffect = "StormShieldActiveEffect";
}
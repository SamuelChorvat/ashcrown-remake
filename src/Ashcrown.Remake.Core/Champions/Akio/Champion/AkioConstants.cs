using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Akio.Champion;

public abstract class AkioConstants : IChampionConstants
{
    public static string Name => "Akio";
    public static string Title  => "The Ronin";
    public static string Bio => "Once the greatest student of Ukyo, Akio left the dojo in disgrace after the accidental killing of a fellow classmate. " +
                                "Wandering in shame, many may be led to believe that his total banishment was too harsh of a punishment for something that could so easily happen in everyday martial training. " +
                                "Only Akio and his former master know that he was lucky to receive a sentence so light. " +
                                "Murdering a student to see how far he could push his ability was the first unholy transgression that Akio committed. " +
                                "Now he roams Coela looking to test his steel and commit his second.";
    public static string Artist => "Yann Trolong";
    public static int[] Attributes => [2, 2, 0, 0];

    public const string TestName = "Akio";
    
    public const string DragonRage = "Dragon Rage";
    public const string FlowDisruption = "Flow Disruption";
    public const string MasterfulSlice = "Masterful Slice";
    public const string LightningSpeed = "Lightning Speed";

    public const string DragonRageActiveEffect = "DragonRageActiveEffect";
    public const string FlowDisruptionActiveEffect = "FlowDisruptionActiveEffect";
    public const string MasterfulSliceActiveEffect = "MasterfulSliceActiveEffect";
    public const string LightningSpeedActiveEffect = "LightningSpeedActiveEffect";
    public const string LightningSpeedEndActiveEffect = "LightningSpeedEndActiveEffect";
}
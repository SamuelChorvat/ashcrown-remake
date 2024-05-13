using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Gwen.Champion;

public abstract class GwenConstants : IChampionConstants
{
    public static string Name => "Gwen";
    public static string Title => "The Succubus";

    public static string Bio => "Whether meticulously trained in the art of seduction or simply born with a gift, Gwen has made a successful living entertaining some of the most prestigious clients in Coela. " +
                                "Duchesses, priests and lords alike have previously fallen under her spell, but now Gwen has her sights set on a much more ambitious client. " +
                                "The King himself. What better way to reach the king than working her way up the hierarchy of officers on the battlefield?";

    public static string Artist => "Yann Trolong";
    public static int[] Attributes => [1, 1, 0, 2];

    public const string TestName = "Gwen";
    
    public const string Charm = "Charm";
    public const string Kiss = "Kiss";
    public const string LightsOut = "Lights Out";
    public const string LoveChains = "Love Chains";

    public const string CharmBuffActiveEffect = "CharmBuffActiveEffect";
    public const string CharmMeActiveEffect = "CharmMeActiveEffect";
    public const string CharmTargetActiveEffect = "CharmTargetActiveEffect";
    public const string LightsOutActiveEffect = "LightsOutActiveEffect";
    public const string LoveChainsActiveEffect = "LoveChainsActiveEffect";
}
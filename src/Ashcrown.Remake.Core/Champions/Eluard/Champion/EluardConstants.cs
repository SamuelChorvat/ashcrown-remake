using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Eluard.Champion;

public abstract class EluardConstants : IChampionConstants
{
    public static string Name => "Eluard";
    public static string Title => "The Dragon Knight";
    public static string Bio => "Once a long time ago, the Order of the Dragon Knights flew high above the clouds keeping the chaos of Coela at bay. " +
                                "After openly supporting the side which was fated to lose in the royal rebellion, they were banished and forced into hiding throughout the country. " +
                                "Growing up in the secret sect, Eluard spent long days living indoors and longer nights taking to the skies. " +
                                "A man now, he wishes to see the Order restored to their pre-war glory and will not let anyone get in his way.";

    public static string Artist => "PiotrTekien's Games";
    public static int[] Attributes => [2, 2, 0, 1];
    
    public const string TestName = "Eluard";

    public const string Devastate = "Devastate";
    public const string Evade = "Evade";
    public const string SwordStrike = "Sword Strike";
    public const string UnyieldingWill = "Unyielding Will";

    public const string DevastateActiveEffect = "DevastateActiveEffect";
    public const string EvadeActiveEffect = "EvadeActiveEffect";
    public const string UnyieldingWillActiveEffect = "UnyieldingWillActiveEffect";
}
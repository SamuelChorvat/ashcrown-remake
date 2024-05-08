using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Branley.Champion;

public abstract class BranleyConstants : IChampionConstants
{
    public static string Name => "Branley";
    public static string Title => "The Outlaw";

    public static string Bio =>
        "A former merchant sailor who found that bartering went a lot smoother when he was waving a loaded gun in the air, " +
        "Branley has taken to a more aggressive branch of mercantilism. " +
        "The inland sea of Axeina holds host to many a pirate who can lay claim to the tides, but Branley knows he need not boast. " +
        "Only walking down the streets of Ingbridge is advertisement enough of his services to his less than savoury clientele, " +
        "and a warning to any honest merchants who plan on making the crossing. Often paid by clients on the brink of bankruptcy to rob their ships to spur on dubious insurance claims, " +
        "Branley seems to remain a necessary evil to many of the elite. Disappearing amongst the sail high waves when pursued by the King’s navy, " +
        "he has avoided capture thus far through his cunning, charisma, and most importantly of all, his ruthlessness.";
    public static string Artist => "Yann Trolong";
    public static int[] Attributes => [2, 1, 0, 2];

    public const string TestName = "Branley";
    
    public const string DefensiveManeuver = "Defensive Maneuver";
    public const string FireTheCannons = "Fire the Cannons";
    public const string Plunder = "Plunder";
    public const string RaiseTheFlag = "Raise the Flag";

    public const string DefensiveManeuverActiveEffect = "DefensiveManeuverActiveEffect";
    public const string FireTheCannonsActiveEffect = "FireTheCannonsActiveEffect";
    public const string RaiseTheFlagMeActiveEffect = "RaiseTheFlagMeActiveEffect";
    public const string RaiseTheFlagTargetActiveEffect = "RaiseTheFlagTargetActiveEffect";
}
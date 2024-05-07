using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Ash.Champion;

public class AshConstants : IChampionConstants
{
    public static string Name => "Ash";
    public static string Title => "The Pyromancer";

    public static string Bio => "Expelled from her local guild of mages for burning down the library in a fit of uncontrollable rage, " +
                                "Ash has become a lone sorceress for hire. " +
                                "Unruly and completely without discipline, the soldiers put up with her thanks only to her ability to create utter carnage on the battlefield. " +
                                "The ones who dare to speak up against her attitude often find themselves walking away without any eyebrows; or not at all.";

    public static string Artist => "WolfsBane";
    public static int[] Attributes => [3, 1, 0, 1];
    
    public const string TestName = "Ash";
    
    public const string Fireball = "Fireball";
    public const string FireBlock = "Fire Block";
    public const string FireShield = "Fire Shield";
    public const string FireWhirl = "Fire Whirl";
    public const string Inferno = "Inferno";
    public const string PhoenixFlames = "Phoenix Flames";

    public const string FireballActiveEffect = "FireballActiveEffect";
    public const string FireBlockActiveEffect = "FireBlockActiveEffect";
    public const string FireShieldMeActiveEffect = "FireShieldMeActiveEffect";
    public const string FireShieldTargetActiveEffect = "FireShieldTargetActiveEffect";
    public const string FireWhirlActiveEffect = "FireWhirlActiveEffect";
    public const string PhoenixFlamesActiveEffect = "PhoenixFlamesActiveEffect";
}
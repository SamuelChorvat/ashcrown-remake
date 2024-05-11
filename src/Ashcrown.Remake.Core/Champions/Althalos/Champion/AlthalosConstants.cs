using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Althalos.Champion;

public abstract class AlthalosConstants : IChampionConstants
{
    public static string Name => "Althalos";
    public static string Title => "The Righteous Crusader";
    public static string Bio => "Once a lowly mercenary, Althalos claims that a divine revelation from an obscure Eastern desert god led to his salvation. " +
                    "Highly disciplined and pious to the point of obsession, " +
                    "Althalos can be found anywhere on the trade routes that are scattered throughout the East enforcing his holy brand of morality on the brigands that mar the path. " +
                    "Loved by some and feared by all, the roads are most certainly safer for travellers. " +
                    "That is, as long as they make sure not to offend Althalos’ new desert sensibilities.";
    public static string Artist => "Dean Spencer";
    public static int[] Attributes => [1, 2, 1, 1];
    
    public const string TestName = "Althalos";

    public const string CrusaderOfLight = "Crusader of Light";
    public const string DivineShield = "Divine Shield";
    public const string HammerOfJustice = "Hammer of Justice";
    public const string HolyLight = "Holy Light";

    public const string CrusaderOfLightActiveEffect = "CrusaderOfLightActiveEffect";
    public const string DivineShieldActiveEffect = "DivineShieldActiveEffect";
    public const string HammerOfJusticeActiveEffect = "HammerOfJusticeActiveEffect";
}
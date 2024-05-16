using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Hannibal.Champion;

public abstract class HannibalConstants : IChampionConstants
{
    public static string Name => "Hannibal";
    public static string Title => "The Blood Lord";

    public static string Bio => "A once proud lord ruling over a vast estate tended by swathes of hypnotized peasants, " +
                                "Hannibal was suddenly forced into hiding after the first purging of all vampiric elements in Coela. " +
                                "Living quietly and yet feasting often, Hannibal has managed to survive a succession of several purges since then. " +
                                "Now he has been yanked from is simple life as a lowly vampire slayer of the name Aniel harangues him in her quest to find her father. " +
                                "Tired of a life on the run and his patience far past tested, " +
                                "Hannibal has decided that the next person who seeks to disturb him will not live to regret their decision.";

    public static string Artist => "Yann Trolong";
    public static int[] Attributes => [2, 2, 0, 1];

    public const string TestName = "Hannibal";
    
    public const string DemonicSkin = "Demonic Skin";
    public const string HealthFunnel = "Health Funnel";
    public const string SacrificialPact = "Sacrificial Pact";
    public const string TasteForBlood = "Taste for Blood";

    public const string DemonicSkinActiveEffect = "DemonicSkinActiveEffect";
    public const string HealthFunnelMeActiveEffect = "HealthFunnelMeActiveEffect";
    public const string HealthFunnelTargetActiveEffect = "HealthFunnelTargetActiveEffect";
    public const string SacrificialPactActiveEffect = "SacrificialPactActiveEffect";
}
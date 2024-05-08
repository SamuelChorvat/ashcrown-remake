using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Arabela.Champion;

public abstract class ArabelaConstants : IChampionConstants
{
    public static string Name => "Arabela";
    public static string Title => "The Faithful Cleric";

    public static string Bio =>
        "Picked up as an orphan by the local parish, Arabela spent her youth piously learning the arts of prayer, worship and healing. " +
        "Known for her near miraculous ability to heal the sick, the damned and the insane, she has travelled Coela helping anyone she can along the way. " +
        "Believing that she could make more of a difference on the battlefield than in any rustic village, she now accompanies soldiers, mercenaries and paladins on their gruesome adventures throughout the country. " +
        "Where better to find the damned than littered amongst the bloody sand of the battlefield?";
    public static string Artist => "WolfsBane";
    public static int[] Attributes => [1, 1, 3, 1];

    public const string TestName = "Arabela";
    
    public const string DivineTrap = "Divine Trap";
    public const string FlashHeal = "Flash Heal";
    public const string HolyShield = "Holy Shield";
    public const string PrayerOfHealing = "Prayer of Healing";

    public const string DivineTrapEndActiveEffect = "DivineTrapEndActiveEffect";
    public const string DivineTrapMeActiveEffect = "DivineTrapMeActiveEffect";
    public const string DivineTrapTargetActiveEffect = "DivineTrapTargetActiveEffect";
    public const string HolyShieldActiveEffect = "HolyShieldActiveEffect";
    public const string PrayerOfHealingMeActiveEffect = "PrayerOfHealingMeActiveEffect";
    public const string PrayerOfHealingTargetActiveEffect = "PrayerOfHealingTargetActiveEffect";
}
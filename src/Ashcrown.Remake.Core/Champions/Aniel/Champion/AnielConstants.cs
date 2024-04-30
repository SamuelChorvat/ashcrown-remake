using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Aniel.Champion;

public class AnielConstants : IChampionConstants
{
    public static string Name => "Aniel";
    public static string Title => "The Slayer";

    public static string Bio =>
        "Ever since she could remember, Aniel knew that there was something strange about her father. " +
        "One day she found her mother dead, blood dripping from the two holes in her neck, and her father nowhere to be found. " +
        "On that day, Aniel swore an oath of vengeance and left her home that very night. " +
        "In her travels around Coela in search of her mother’s killer, she discovered the truth about the scourge of vampirism that plagued her country. " +
        "Sickened with the knowledge that the cold blood of the creatures she hated the most ran through her veins, " +
        "Aniel has turned her anger and disgust on anyone foolish enough to bring up the plague of Coela in front of her.";

    public static string Artist => "WolfsBane";
    public static int[] Attributes => [1, 1, 0, 2];

    public const string TestName = "Aniel";
    
    public const string Condemn = "Condemn";
    public const string EnchantedGarlicBomb = "Enchanted Garlic Bomb";
    public const string BladeOfGluttony = "Blade of Gluttony";
    public const string AdrenalineRush = "Adrenaline Rush";

    public const string CondemnInvulnerabilityActiveEffect = "CondemnInvulnerabilityActiveEffect";
    public const string CondemnUsedActiveEffect = "CondemnUsedActiveEffect";
    public const string EnchantedGarlicBombUsedMeActiveEffect = "EnchantedGarlicBombUsedMeActiveEffect";
    public const string EnchantedGarlicBombUsedTargetCantActiveEffect = "EnchantedGarlicBombUsedTargetCantActiveEffect";
    public const string EnchantedGarlicBombUsedTargetStunActiveEffect = "EnchantedGarlicBombUsedTargetStunActiveEffect";
    public const string BladeOfGluttonyUsedActiveEffect = "BladeOfGluttonyUsedActiveEffect";
    public const string BladeOfGluttonyUsedMagicActiveEffect = "BladeOfGluttonyUsedMagicActiveEffect";
    public const string BladeOfGluttonyUsedPhysicalActiveEffect = "BladeOfGluttonyUsedPhysicalActiveEffect";
    public const string AdrenalineRushActiveEffect = "AdrenalineRushActiveEffect";
}
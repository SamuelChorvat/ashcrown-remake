using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Lucifer.Champion;

public class LuciferConstants : IChampionConstants
{
    public static string Name => "Lucifer";
    public static string Title => "The Mysterious Traveler";

    public static string Bio => "In the dead of night, a local saloon may hear a rapping on their door. " +
                                "Upon answering, a lone stranger will charm his way in, receiving any food and drink he asks for. " +
                                "He’ll spend the whole night making the landlords laugh and sing while he consumes their pantry, " +
                                "their coffers and their soul. If any landlord attempts to wake up from the spell, " +
                                "he’ll find that Lucifer can take on a very different form than the handsome young stranger they let in.";

    public static string Artist => "Yann Trolong";
    public static int[] Attributes => [3, 1, 0, 0];

    public const string TestName = "Lucifer";
    
    public const string DemonForm = "Demon Form";
    public const string ShadowBolts = "Shadow Bolts";
    public const string CursedCrow = "Cursed Crow";
    public const string DarkChalice = "Dark Chalice";
    public const string HeartOfDarkness = "Heart of Darkness";

    public const string DemonFormActiveEffect = "DemonFormActiveEffect";
    public const string CursedCrowActiveEffect = "CursedCrowActiveEffect";
    public const string DarkChaliceActiveEffect = "DarkChaliceActiveEffect";
    public const string HeartOfDarknessActiveEffect = "HeartOfDarknessActiveEffect";
}
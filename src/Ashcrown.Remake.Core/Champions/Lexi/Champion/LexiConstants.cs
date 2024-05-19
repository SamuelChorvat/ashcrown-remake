using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Lexi.Champion;

public abstract class LexiConstants : IChampionConstants
{
    public static string Name => "Lexi";
    public static string Title => "The Wood Elf";

    public static string Bio => "Part of a dying race of people living along the Arikem river system, " +
                                "Lexi and her kin fled deep in the jungle after the Reconstruction era. Despite dreams of becoming a world renowned healer, " +
                                "Lexi found that she would have to adapt to a life of hunting on the fringes of Coelan society. " +
                                "As the trees that protect her people recede to the power of buzzing machines and hacking axes, " +
                                "Lexi has come out from the safety of the forest with the aim of wreaking justice to any outsiders she deems as a threat to her way of life.";

    public static string Artist => "WolfsBane";
    public static int[] Attributes => [1, 2, 1, 0];

    public const string TestName = "Lexi";
    
    public const string Nourish = "Nourish";
    public const string Shadowmeld = "Shadowmeld";
    public const string ThornWhip = "Thorn Whip";
    public const string Tranquility = "Tranquility";

    public const string ShadowmeldActiveEffect = "ShadowmeldActiveEffect";
    public const string TranquilityActiveEffect = "TranquilityActiveEffect";
}
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Izrin.Champion;

public abstract class IzrinConstants : IChampionConstants
{
    public static string Name => "Izrin";
    public static string Title => "The Undead Warrior";

    public static string Bio => "A less than average warrior in life and death, Izrin’s fate should have been to fade out of the forgetful memories of her close kin after dying at the start of a minor battle during the royal rebellion. " +
                                "It was not until after her death, when she was resurrected to be part of Nezhum’s undead army that Izrin’s life truly began. " +
                                "In a moment of wilful determination, Izrin broke free of the necromancer’s spell and escaped the undead horde. " +
                                "Now wandering the country, she seeks to find her purpose beyond the twilight realm in which she now resides.";

    public static string Artist => "WolfsBane";
    public static int[] Attributes => [2, 2, 0, 1];

    public const string TestName = "Izrin";
    
    public const string Bite = "Bite";
    public const string BloodyStrike = "Bloody Strike";
    public const string Rake = "Rake";
    public const string WillOfTheUndead = "Will of the Undead";

    public const string BloodyStrikeActiveEffect = "BloodyStrikeActiveEffect";
    public const string BloodyStrikeHelperActiveEffect = "BloodyStrikeHelperActiveEffect";
    public const string RakeActiveEffect = "RakeActiveEffect";
    public const string WillOfTheUndeadActiveEffect = "WillOfTheUndeadActiveEffect";
}
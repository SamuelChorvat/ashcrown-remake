using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Jane.Champion;

public abstract class JaneConstants : IChampionConstants
{
    public static string Name => "Jane";
    public static string Title => "The Gunslinger";

    public static string Bio => "Most of the saloon taverns in the Western provinces hold whispers of a lone gunslinger. " +
                                "Some call her crooked, others fair, but everyone can agree that she’s a dead shot with nothing to lose. " +
                                "Wandering with her wolf dog Benji, Jane makes a living in anyway her guns can for her, including taking out any “varmint” she meets on the battlefield.";

    public static string Artist => "Yann Trolong";
    public static int[] Attributes => [2, 2, 1, 1];

    public const string TestName = "Jane";
    
    public const string Benji = "Benji";
    public const string CounterShot = "Counter Shot";
    public const string Flashbang = "Flashbang";
    public const string GoForTheThroat = "Go for the Throat";
    public const string Misdirection = "Misdirection";

    public const string BenjiActiveEffect = "BenjiActiveEffect";
    public const string CounterShotActiveEffect = "CounterShotActiveEffect";
    public const string FlashbangActiveEffect = "FlashbangActiveEffect";
    public const string MisdirectionActiveEffect = "MisdirectionActiveEffect";
}
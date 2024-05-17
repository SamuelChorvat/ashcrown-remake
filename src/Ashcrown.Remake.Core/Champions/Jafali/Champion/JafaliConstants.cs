using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Jafali.Champion;

public abstract class JafaliConstants : IChampionConstants
{
    public static string Name => "Jafali";
    public static string Title => "The Ifrit";

    public static string Bio => "Hidden deep in the stones of the Tainowak palace walls, many find that it is possible to summon Jafali through a simple ritual. " +
                                "After coming out, the startled summoner may ask him one favour, which Jafali will grant with great pleasure. " +
                                "Many beggars of this dark art have found that the wording of their wish is often ... open to interpretation. " +
                                "The last victim of Jafali’s mischief spoke in such a way as to accidentally let him free, leading to the unfortunate possession of his body. " +
                                "Now at large, Jafali feels a pull towards a mass of people congregating in Coela. Just because he is free does not mean that he’s lost his mischievous streak.";

    public static string Artist => "PiotrTekien's Games";
    public static int[] Attributes => [1, 2, 0, 2];

    public const string TestName = "Jafali";
    
    public const string Anger = "Anger";
    public const string DecayingSoul = "Decaying Soul";
    public const string Envy = "Envy";
    public const string Avarice = "Avarice";
    public const string Pride = "Pride";
    public const string DevilsGame = "Devil's Game";

    public const string AngerActiveEffect = "AngerActiveEffect";
    public const string DecayingSoulActiveEffect = "DecayingSoulActiveEffect";
    public const string EnvyActiveEffect = "EnvyActiveEffect";
    public const string DevilsGameActiveEffect = "DevilsGameActiveEffect";
}
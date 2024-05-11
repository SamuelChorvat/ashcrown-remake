using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Cleo.Champion;

public abstract class CleoConstants : IChampionConstants
{
    public static string Name => "Cleo";
    public static string Title => "The Hive Empress";
    public static string Bio => "Billions. The sheer number of her hive is unimaginable to the human mind. " +
                                "How many have entered through the mouth of the dark funnel that leads to her underground lair in search of her eggs, " +
                                "only to find it empty? It is not until the walls begin to shift around them, revealing the pulsating mass of brood that they have awoken. " +
                                "There are no walls here. Only the hive.";

    public static string Artist => "WolfsBane";
    public static int[] Attributes => [1, 2, 2, 1];

    public const string TestName = "Cleo";
    
    public const string VenomousSting = "Venomous Sting";
    public const string ChitinRegeneration = "Chitin Regeneration";
    public const string Hive = "Hive";
    public const string HealingSalve = "Healing Salve";
    public const string SwarmSummoning = "Swarm Summoning";

    public const string VenomousStingActiveEffect = "VenomousStingActiveEffect";
    public const string HiveActiveEffect = "HiveActiveEffect";
    public const string HealingSalveActiveEffect = "HealingSalveActiveEffect";
    public const string SwarmSummoningActiveEffect = "SwarmSummoningActiveEffect";
    
}
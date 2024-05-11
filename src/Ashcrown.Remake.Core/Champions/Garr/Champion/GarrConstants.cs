using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Garr.Champion;

public abstract class GarrConstants : IChampionConstants
{
    public static string Name => "Garr";
    public static string Title => "The Raging Berserker";

    public static string Bio => "Filled to the brim with a rage that can not be contained, " +
                                "Garr lives only to crush his enemies into dust and send them to the ground where they belong. " +
                                "A lost soul, Garr has no interest in any higher luxuries, stopping to eat and rest only as a means to fuel himself for his existence as a whirlwind wreaking destruction throughout Coela. " +
                                "Wherever the clamour of steel is mixed with the screams of the dying, " +
                                "Garr can be found doing what he does best and what he enjoys the most.";

    public static string Artist => "Dean Spencer";
    public static int[] Attributes => [3, 2, 0, 0];

    public const string TestName = "Garr";
    
    public const string IntimidatingShout = "Intimidating Shout";
    public const string BarbedChain = "Barbed Chain";
    public const string Recklessness = "Recklessness";
    public const string Slam = "Slam";

    public const string IntimidatingShoutActiveEffect = "IntimidatingShoutActiveEffect";
    public const string BarbedChainInvulnerabilityActiveEffect = "BarbedChainInvulnerabilityActiveEffect";
    public const string BarbedChainMeActiveEffect = "BarbedChainMeActiveEffect";
    public const string BarbedChainTargetActiveEffect = "BarbedChainTargetActiveEffect";
    public const string RecklessnessActiveEffect = "RecklessnessActiveEffect";
}
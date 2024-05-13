using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Gruber.Champion;
// ReSharper disable twice InconsistentNaming
public abstract class GruberConstants : IChampionConstants
{
    public static string Name => "Gruber";
    public static string Title => "The Mad Scientist";

    public static string Bio => "Within the discretion of the royal family, " +
                                "Gruber is allowed to practise a variety of his experiments under the guise of helping the crown’s interests. " +
                                "Paid handsomely to research and develop cures to some of the most common diseases that blight the land, " +
                                "the King is known to turn a blind eye to some of Gruber’s more unsavoury practises. " +
                                "Such as when a few children in Ingbridge who had gone missing were found in an alley, " +
                                "indescribably conjoined and twisted into something inhuman. " +
                                "At what point does the means of progress become too twisted to justify the ends? " +
                                "Gruber seeks to find the answer to this question.";

    public static string Artist => "Yann Trolong";
    public static int[] Attributes => [2, 1, 1, 1];

    public const string TestName = "Gruber";
    
    public const string AdaptiveVirus = "Adaptive Virus";
    public const string DNAEnhancement = "DNA Enhancement";
    public const string ExplosiveLeech = "Explosive Leech";
    public const string PoisonInjection = "Poison Injection";

    public const string AdaptiveVirusAllyActiveEffect = "AdaptiveVirusAllyActiveEffect";
    public const string AdaptiveVirusEnemyActiveEffect = "AdaptiveVirusEnemyActiveEffect";
    public const string DNAEnhancementActiveEffect = "DNAEnhancementActiveEffect";
    public const string ExplosiveLeechActiveEffect = "ExplosiveLeechActiveEffect";
    public const string ExplosiveLeechEndActiveEffect = "ExplosiveLeechEndActiveEffect";
    public const string PoisonInjectionPartOneActiveEffect = "PoisonInjectionPartOneActiveEffect";
    public const string PoisonInjectionPartTwoActiveEffect = "PoisonInjectionPartTwoActiveEffect";
    public const string PoisonInjectionStacksActiveEffect = "PoisonInjectionStacksActiveEffect";
}
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Evanore.Champion;

public abstract class EvanoreConstants : IChampionConstants
{
    public static string Name => "Evanore";
    public static string Title => "The Summoner";
    public static string Bio => "Surrounded by a veil of fur and feathers, Evanore has launched herself onto the scene through her unique magical methods and intimidating aura. " +
                                "Receiving her training from the Guild and continuing her own studies deep in the Waldvollur forest, " +
                                "Evanore has rediscovered an old brand of mysticism and brought it back in full force. " +
                                "With a chip on her shoulder and desire to prove the power of her new style, " +
                                "Evanore is ready to showcase her fantastical art on the battlefield and show everyone what she can do.";

    public static string Artist => "WolfsBane";
    public static int[] Attributes => [1, 1, 1, 0];

    public const string TestName = "Evanore";
    
    public const string HoundDefense = "Hound Defense";
    public const string MonstrousBear = "Monstrous Bear";
    public const string MurderOfCrows = "Murder of Crows";
    public const string UmbraWolf = "Umbra Wolf";

    public const string HoundDefenseActiveEffect = "HoundDefenseActiveEffect";
    public const string MonstrousBearActiveEffect = "MonstrousBearActiveEffect";
    public const string MurderOfCrowsMeActiveEffect = "MurderOfCrowsMeActiveEffect";
    public const string MurderOfCrowsTargetActiveEffect = "MurderOfCrowsTargetActiveEffect";
    public const string UmbraWolfActiveEffect = "UmbraWolfActiveEffect";
}
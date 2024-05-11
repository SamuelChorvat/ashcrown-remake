using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Cronos.Champion;

public abstract class CronosConstants : IChampionConstants
{
    public static string Name => "Cronos";
    public static string Title => "The Magitech Juggernaut";

    public static string Bio => "More machine than woman, Cronos belongs to an exalted order of mechanist high priests in charge of protecting the Time Stones, a collection of ancient geological artifacts. " +
                                "Her wholehearted belief in their role in the fate of Coela has led her down a path of vigorous study and experimentation." +
                                " What began as a studious spirit has turned into obsession, as slowly she has become more dependent on the magi-mechanical exoskeleton gifted to her by her order. " +
                                "Now, waiting in the arcane temples to protect the stones is no longer enough. After all, the best defense is a good offense.";

    public static string Artist => "Tan Ho Sim";
    public static int[] Attributes => [2, 1, 1, 1];

    public const string TestName = "Cronos";
    
    public const string GravityWell = "Gravity Well";
    public const string BattlefieldBulwark = "Battlefield Bulwark";
    // ReSharper disable once InconsistentNaming
    public const string EMPBurst = "EMP Burst";
    public const string PulseCannon = "Pulse Cannon";
    public const string MagitechCircuitry = "Magitech Circuitry";

    public const string BattlefieldBulwarkAllyActiveEffect = "BattlefieldBulwarkAllyActiveEffect";
    public const string BattlefieldBulwarkEnemyActiveEffect = "BattlefieldBulwarkEnemyActiveEffect";
    public const string BattlefieldBulwarkInvulnerableActiveEffect = "BattlefieldBulwarkInvulnerableActiveEffect";
    // ReSharper disable once InconsistentNaming
    public const string EMPBurstActiveEffect = "EMPBurstActiveEffect";
    public const string PulseCannonMeActiveEffect = "PulseCannonMeActiveEffect";
    public const string PulseCannonTargetActiveEffect = "PulseCannonTargetActiveEffect";
    public const string MagitechCircuitryActiveEffect = "MagitechCircuitryActiveEffect";
}
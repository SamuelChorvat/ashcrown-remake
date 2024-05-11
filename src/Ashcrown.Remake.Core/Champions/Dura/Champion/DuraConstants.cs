using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Dura.Champion;

public abstract class DuraConstants : IChampionConstants
{
    public static string Name => "Dura";
    public static string Title => "The Monk";
    public static string Bio => "High in the mountains lies a well kept temple with a golden pavilion. " +
                                "The long periods of meditative silence are punctuated only by the unified shouts of the monks who train there, day in and day out. " +
                                "Here Dura came as a confused young man many years ago. " +
                                "Through the rigorous training the temple provided him, he honed his emotion into discipline, and his discipline into a sharp martial tool. " +
                                "Now, with the temple in the sights of people who wish to destroy it, he is ready to unleash the full spectrum of his abilities.";

    public static string Artist => "Christof Grobielsky";
    public static int[] Attributes => [2, 2, 1, 1];

    public const string TestName = "Dura";
    
    public const string Meditate = "Meditate";
    public const string SonicWaves = "Sonic Waves";
    public const string Tempest = "Tempest";
    public const string Whirlwind = "Whirlwind";

    public const string MeditateActiveEffect = "MeditateActiveEffect";
    public const string SonicWavesMeActiveEffect = "SonicWavesMeActiveEffect";
    public const string SonicWavesTargetActiveEffect = "SonicWavesTargetActiveEffect";
    public const string WhirlwindActiveEffect = "WhirlwindActiveEffect";
}
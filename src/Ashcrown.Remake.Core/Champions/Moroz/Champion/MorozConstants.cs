using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Moroz.Champion;

public abstract class MorozConstants : IChampionConstants
{
    public static string Name => "Moroz";
    public static string Title => "The Cryomancer";

    public static string Bio =>
        "Hailing from the fiery lands of Iskandar, Moroz started learning his craft to keep cool underneath the desert sun. " +
        "When the guild of mages took notice of his talent, they forcibly moved him to the frigid North to hone his craft despite his protests. " +
        "Now one of the most powerful Cryomancers in Coela, Moroz finds that the only way he can heat his icy veins is through the blood pumping action of combat.";
    public static string Artist => "WolfsBane";
    public static int[] Attributes => [1, 2, 0, 1];

    public const string TestName = "Moroz";
    
    public const string Freeze = "Freeze";
    public const string FrozenArmor = "Frozen Armor";
    public const string IceBarrier = "Ice Barrier";
    public const string IceBlock = "Ice Block";
    public const string Shatter = "Shatter";

    public const string FreezeMeActiveEffect = "FreezeMeActiveEffect";
    public const string FreezeTargetActiveEffect = "FreezeTargetActiveEffect";
    public const string FrozenArmorActiveEffect = "FrozenArmorActiveEffect";
    public const string IceBarrierActiveEffect = "IceBarrierActiveEffect";
    public const string IceBlockActiveEffect = "IceBlockActiveEffect";
    
    
}
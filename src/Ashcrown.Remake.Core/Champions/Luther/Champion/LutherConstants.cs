using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Luther.Champion;

public abstract class LutherConstants : IChampionConstants
{
    public static string Name => "Luther";
    public static string Title => "The Blacksmith";

    public static string Bio => "Deep in the pits of Fa'lmur the ringing of steel on steel would echo off the rock walls, " +
                                "the sound mixing with the stifling heat of the volcanic crater. Cursed by the mountain for his greed, " +
                                "Luther toiled away forging the war hammer Hrau'stal, his sweat hissing into steam as it rolled over his branded head. " +
                                "When the thundering of metal and rock finally ended and the cavern walls tore themselves apart, " +
                                "he felt the pull of the mountain spirits lead him into the cool outside air. " +
                                "Now, he fights fiercely on the battlefield with Hrau'stal by his side." +
                                " For what purpose, only the mountain spirits seem to know.";

    public static string Artist => "Yann Trolong";
    public static int[] Attributes => [2, 1, 0, 2];

    public const string TestName = "Luther";
    
    public const string FieryBrand = "Fiery Brand";
    public const string Flamestrike = "Flamestrike";
    public const string ForgeSpirit = "Forge Spirit";
    public const string LivingForge = "Living Forge";
    public const string MoltenArmor = "Molten Armor";

    public const string FieryBrandCounterActiveEffect = "FieryBrandCounterActiveEffect";
    public const string FieryBrandEndActiveEffect = "FieryBrandEndActiveEffect";
    public const string FieryBrandTargetActiveEffect = "FieryBrandTargetActiveEffect";
    public const string FlamestrikeMeActiveEffect = "FlamestrikeMeActiveEffect";
    public const string FlamestrikeTargetActiveEffect = "FlamestrikeTargetActiveEffect";
    public const string ForgeSpiritActiveEffect = "ForgeSpiritActiveEffect";
    public const string LivingForgeBuffActiveEffect = "LivingForgeBuffActiveEffect";
    public const string LivingForgeMeActiveEffect = "LivingForgeMeActiveEffect";
    public const string LivingForgeTargetActiveEffect = "LivingForgeTargetActiveEffect";
    public const string MoltenArmorActiveEffect = "MoltenArmorActiveEffect";
}
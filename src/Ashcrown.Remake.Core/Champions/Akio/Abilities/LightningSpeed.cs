using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Akio.Champion;

namespace Ashcrown.Remake.Core.Champions.Akio.Abilities;

public class LightningSpeed : AbilityBase
{
    public LightningSpeed(IChampion champion) : 
        base(champion, 
            AkioConstants.LightningSpeed, 
            2,
            [0,0,0,0,1], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Self, 
            AbilityType.AllyBuff, 
            3)
    {
        BonusDamage1 = 5;
        Duration1 = 1;
        Description = $"{AkioConstants.Name} prepares for an attack. For {Duration1} turn, " +
                      $"if a new enemy harmful ability is used on {AkioConstants.Name}, {AkioConstants.DragonRage.HighlightInGold()} will permanently deal an additional {$"{BonusDamage1} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()} (can be triggered multiple times). " +
                      $"During this time, {AkioConstants.Name} {"ignores".HighlightInPurple()} all harmful effects except energy cost changes. " +
                      $"This effect is {"invisible".HighlightInPurple()} to the enemy.";
        SelfCast = true;
        Helpful = true;
        Buff = true;
        IgnoreHarmful = true;
        Invisible = true;
        ActiveEffectOwner = AkioConstants.Name;
        ActiveEffectName = AkioConstants.LightningSpeedActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetIgnoreHarmfulPoints(Duration1, this, target);
        totalPoints += 10;
        return totalPoints;
    }
}
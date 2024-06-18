using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Nazuc.Champion;

namespace Ashcrown.Remake.Core.Champions.Nazuc.Abilities;

public class HuntersMark : AbilityBase
{
    public HuntersMark(IChampion champion) 
        : base(champion, 
            NazucConstants.HuntersMark, 
            1,
            [0,0,0,0,1], 
            [AbilityClass.Strategic, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDebuff, 
            3)
    {
        Duration1 = 3;
        BonusDamage1 = 5;
        
        Description = $"{NazucConstants.Name} mark one enemy who cannot {"reduce damage".HighlightInPurple()} or become {"invulnerable".HighlightInPurple()} for {Duration1} turns. " +
                      $"During this time {NazucConstants.SpearThrow.HighlightInGold()} and {NazucConstants.SpearBarrage.HighlightInGold()} will deal an additional {$"{BonusDamage1} physical damage".HighlightInOrange()} to them. " +
                      $"{NazucConstants.HuntersMark.HighlightInGold()} cannot be used on enemy already affected by it.";
        Harmful = true;
        Debuff = true;
        DisableDamageReceiveReduction = true;
        DisableInvulnerability = true;
        UniqueActiveEffect = true;

        ActiveEffectOwner = NazucConstants.Name;
        ActiveEffectName = NazucConstants.HuntersMarkActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDisableInvulnerabilityAndDamageReductionPoints(Duration1, target);
        totalPoints += BonusDamage1 * 2;
        return totalPoints;
    }
}
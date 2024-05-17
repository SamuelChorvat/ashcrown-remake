using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Izrin.Champion;

namespace Ashcrown.Remake.Core.Champions.Izrin.Abilities;

public class Rake : AbilityBase
{
    public Rake(IChampion champion) 
        : base(champion, 
            IzrinConstants.Rake, 
            0,
            [0,1,0,0,0], 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamageAndDebuff, 
            1)
    {
        Duration1 = 2;
        DealDamageReductionPoint1 = 15;
        BonusDamage1 = 10;
        Damage1 = 15;
        
        Description = $"{IzrinConstants.Name} rakes an enemy, dealing {$"{Damage1} physical damage".HighlightInOrange()} to them and lowering their non-affliction damage by 15 for {Duration1} turns. " +
                      $"{IzrinConstants.Rake.HighlightInGold()} will deal an additional {$"{BonusDamage1} physical damage".HighlightInOrange()}, and the damage reduction effect will last 1 additional turn for each ally that is dead.";
        Harmful = true;
        Debuff = true;
        Damaging = true;
        PhysicalDamage = true;

        ActiveEffectOwner = IzrinConstants.Name;
        ActiveEffectName = IzrinConstants.RakeActiveEffect;
    }

    public override int DealAbilityDamageModifier(IChampion target, int amount, bool secondary)
    {
        return amount + BonusDamage1 * Owner.BattlePlayer.NoOfDead();
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(DealAbilityDamageModifier(target, Damage1, false), 1, this, target);
        totalPoints += DealDamageReductionPoint1 * (Duration1 + Owner.BattlePlayer.NoOfDead());
        return totalPoints;
    }
}
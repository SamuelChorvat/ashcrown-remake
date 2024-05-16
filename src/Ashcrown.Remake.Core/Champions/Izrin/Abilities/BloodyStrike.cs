using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Izrin.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Izrin.Champion;

namespace Ashcrown.Remake.Core.Champions.Izrin.Abilities;

public class BloodyStrike : AbilityBase
{
    public BloodyStrike(IChampion champion) 
        : base(champion, 
            IzrinConstants.BloodyStrike, 
            0,
            [0,1,0,0,0], 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            2)
    {
        Duration1 = 1;
        ReceiveDamageReductionPoint1 = 10;
        BonusDamage1 = 10;
        Damage1 = 20;
        
        Description = $"{IzrinConstants.Name} deals {$"{Damage1} physical damage".HighlightInOrange()} to one enemy " +
                      $"and gains {$"{ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()} for {Duration1} turn. " +
                      $"The following turn this ability will deal and additional {$"{BonusDamage1} physical damage".HighlightInOrange()} " +
                      $"and grant {$"{ReceiveDamageReductionPoint1} more damage reduction".HighlightInYellow()}. " +
                      $"{IzrinConstants.BloodyStrike.HighlightInGold()} will deal an additional {$"{BonusDamage1} physical damage".HighlightInOrange()} for every ally that is dead.";
        Harmful = true;
        Helpful = true;
        Buff = true;
        Damaging = true;
        PhysicalDamage = true;
    }

    public override int DealAbilityDamageModifier(IChampion target, int amount, bool secondary)
    {
        var newAmount = amount;

        if (Owner.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(IzrinConstants.BloodyStrikeHelperActiveEffect)) {
            newAmount += BonusDamage1;
        }

        newAmount += BonusDamage1 * Owner.BattlePlayer.NoOfDead();

        return newAmount;
    }

    public override void AdditionalDealAbilityDamageLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        var bloodyAe = new BloodyStrikerActiveEffect(this, Owner);

        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(IzrinConstants.BloodyStrikeHelperActiveEffect)) {
            bloodyAe.AllDamageReceiveModifier = new PointsPercentageModifier(-(bloodyAe.OriginAbility.ReceiveDamageReductionPoint1 + 10));
        }

        Owner.ChampionController.DealActiveEffect(Owner, this, bloodyAe, true, appliedAdditionalLogic);
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(DealAbilityDamageModifier(target, Damage1, false), 1, this, target);
        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(IzrinConstants.BloodyStrikeHelperActiveEffect)) {
            totalPoints += T.GetPointsReductionPoints(ReceiveDamageReductionPoint1 + 10, Duration1, Owner);
        } else {
            totalPoints += T.GetPointsReductionPoints(ReceiveDamageReductionPoint1, Duration1, Owner);
        }
        return totalPoints;
    }
}
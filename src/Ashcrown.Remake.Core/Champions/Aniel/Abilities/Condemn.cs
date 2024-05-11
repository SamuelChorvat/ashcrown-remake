using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Aniel.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Aniel.Champion;

namespace Ashcrown.Remake.Core.Champions.Aniel.Abilities;

public class Condemn : AbilityBase
{
    public Condemn(IChampion champion) 
        : base(champion, 
               AnielConstants.Condemn, 
               0,
               [0,0,0,0,1],
               [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Ranged], 
               AbilityTarget.Enemy, 
               AbilityType.EnemyDamageAndDebuff,
               1)
    {
        Damage1 = 15;
        BonusDamage1 = 25;
        Duration1 = 1;
        
        Description = $"{AnielConstants.Name} deals {$"{Damage1} physical damage".HighlightInOrange()} to one enemy. " +
                      $"If used on the same enemy one turn after {AnielConstants.BladeOfGluttony.HighlightInGold()}, " +
                      $"that enemy will receive an additional {$"{BonusDamage1} physical damage".HighlightInOrange()}. " +
                      $"If used one turn after {AnielConstants.EnchantedGarlicBomb.HighlightInGold()}, " +
                      $"{AnielConstants.Name} will become {"invulnerable".HighlightInPurple()} for {Duration1} turn.";
        Harmful = true;
        Debuff = true;
        PhysicalDamage = true;
        Damaging = true;
        Invulnerability = true;
        TypeOfInvulnerability = [AbilityClass.All];
        
        ActiveEffectOwner = AnielConstants.Name;
        ActiveEffectName = AnielConstants.CondemnUsedActiveEffect;
    }

    public override int ReceiveAbilityDamageModifier(IChampion target, int amount)
    {
        var newAmount = amount;
        newAmount += BonusDamage1 * target.ActiveEffectController.GetActiveEffectCountByName(AnielConstants.BladeOfGluttonyUsedActiveEffect);
        newAmount += BonusDamage1 * target.ActiveEffectController.GetActiveEffectCountByName(AnielConstants.BladeOfGluttonyUsedMagicActiveEffect);
        newAmount += BonusDamage1 * target.ActiveEffectController.GetActiveEffectCountByName(AnielConstants.BladeOfGluttonyUsedPhysicalActiveEffect);
        return newAmount;
    }

    public override void AdditionalDealAbilityDamageLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (Owner.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AnielConstants.EnchantedGarlicBombUsedMeActiveEffect))
        {
            Owner.ChampionController.DealActiveEffect(Owner,
                                                      this, 
                                                      new CondemnInvulnerabilityActiveEffect(this, Owner), 
                                                      true, 
                                                      appliedAdditionalLogic);
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(
            ReceiveAbilityDamageModifier(target, Damage1), 1, this, target);
        if (Owner.ActiveEffectController.
            ActiveEffectPresentByActiveEffectName(AnielConstants.EnchantedGarlicBombUsedMeActiveEffect))
        {
            totalPoints += T.GetInvulnerabilityPoints(Duration1, this, Owner);
        }
        return totalPoints;
    }
}
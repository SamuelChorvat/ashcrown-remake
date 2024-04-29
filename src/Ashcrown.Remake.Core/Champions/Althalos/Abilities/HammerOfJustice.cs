using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Althalos.Champion;

namespace Ashcrown.Remake.Core.Champions.Althalos.Abilities;

public class HammerOfJustice : AbilityBase
{
    public HammerOfJustice(IChampion champion) 
        : base(champion, 
            AlthalosConstants.HammerOfJustice, 
            0,
            [0,0,1,0,0],
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamageAndDebuff,
            1)
    {
        Damage1 = 20;
        BonusDamage1 = 10;
        Duration1 = 1;
        Description = $"{AlthalosConstants.Name} hits one enemy with his hammer dealing {$"{Damage1} physical damage".HighlightInOrange()} to them " +
                      $"and {"stunning".HighlightInPurple()} their Physical and Strategic abilities for {Duration1} turn. " +
                      $"While {AlthalosConstants.CrusaderOfLight.HighlightInGold()} is active " +
                      $"this will deal an additional {$"{BonusDamage1} physical damage".HighlightInOrange()}.";
        Stun = true;
        StunType = [AbilityClass.Physical, AbilityClass.Strategic];
        Harmful = true;
        Debuff = true;
        PhysicalDamage = true;
        Damaging = true;
        ActiveEffectOwner = AlthalosConstants.Name;
        ActiveEffectName = AlthalosConstants.HammerOfJusticeActiveEffect;
    }

    public override int DealAbilityDamageModifier(IChampion target, int amount, bool secondary)
    {
        var newAmount = amount;

        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AlthalosConstants.CrusaderOfLightActiveEffect)) {
            newAmount += BonusDamage1 * 
                         Owner.ActiveEffectController.GetActiveEffectCountByName(AlthalosConstants.CrusaderOfLightActiveEffect);
        }

        return newAmount;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(DealAbilityDamageModifier(target, Damage1, false), 
            1,this, target);
        totalPoints += T.GetStunPoints(this, Duration1, target);
        return  totalPoints;
    }
}
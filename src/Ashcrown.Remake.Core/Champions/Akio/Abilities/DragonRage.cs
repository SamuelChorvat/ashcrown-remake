using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Akio.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Akio.Champion;

namespace Ashcrown.Remake.Core.Champions.Akio.Abilities;

public class DragonRage : AbilityBase
{
    public DragonRage(IChampion champion) 
        : base(champion, 
            AkioConstants.DragonRage, 
            0,
            [0,2,0,0,0], 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            2)
    {
        Damage1 = 40;
        BonusDamage1 = 5;
        Description = $"{AkioConstants.Name} deals {$"{Damage1} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()} that {"ignore invulnerability".HighlightInPurple()} to one enemy. " +
                      $"{AkioConstants.DragonRage.HighlightInGold()} will deal an additional {$"{BonusDamage1} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()} each time it is used.";
        Harmful = true;
        Damaging = true;
        PhysicalDamage = true;
        PiercingDamage = true;
        IgnoreInvulnerability = true;
        Helpful = true;
        Buff = true;
    }

    public override void AdditionalDealAbilityDamageLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        Owner.ChampionController.DealActiveEffect(Owner, this, new DragonRageActiveEffect(this, Owner), true, appliedAdditionalLogic);
    }

    public override int DealAbilityDamageModifier(IChampion target, int amount, bool secondary)
    {
        var newAmount = amount;

        var activeEffect = Owner.ActiveEffectController.GetActiveEffectByName(AkioConstants.DragonRageActiveEffect);
        if (activeEffect != null) {
            newAmount += BonusDamage1 * activeEffect.Stacks;
        }

        return newAmount;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(DealAbilityDamageModifier(target, Damage1, false), 
            1, this, target);
        return totalPoints;
    }
}
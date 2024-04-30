using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Aniel.Champion;

namespace Ashcrown.Remake.Core.Champions.Aniel.Abilities;

public class EnchantedGarlicBomb : AbilityBase
{
        public EnchantedGarlicBomb(IChampion champion) 
        : base(champion,
               AnielConstants.EnchantedGarlicBomb,
               1,
               [0,0,0,0,1],
               [AbilityClass.Magic, AbilityClass.Instant, AbilityClass.Ranged],
               AbilityTarget.Enemy,
               AbilityType.EnemyDebuff,
               2)
    {
        Damage1 = 10;
        Duration1 = 2;
        Duration2 = 1;
        Description = $"One enemy cannot {"reduce damage".HighlightInPurple()} or become {"invulnerable".HighlightInPurple()} for {Duration1} turns. " +
                      $"If used on the same enemy one turn after {AnielConstants.BladeOfGluttony.HighlightInGold()} that enemy will also be stunned for {Duration2} turn. " +
                      $"If used on the same enemy one turn after {AnielConstants.Condemn.HighlightInGold()} that enemy will also receive {$"{Damage1} magic damage".HighlightInBlue()}.";
        Harmful = true;
        Debuff = true;
        DisableDamageReceiveReduction = true;
        DisableInvulnerability = true;
        Stun = true;
        StunType = [AbilityClass.All];
        MagicDamage = true;
        Damaging = true;
        ActiveEffectOwner = AnielConstants.Name;
        ActiveEffectName = AnielConstants.EnchantedGarlicBombUsedTargetCantActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDisableInvulnerabilityAndDamageReductionPoints(Duration1, target);
        if (target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AnielConstants.CondemnUsedActiveEffect))
        {
            totalPoints += T.GetDamagePoints(
                Damage1 * target.ActiveEffectController.GetActiveEffectCountByName(AnielConstants.CondemnUsedActiveEffect), 1, this, target);
        }
        if (target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AnielConstants.BladeOfGluttonyUsedActiveEffect) ||
            target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AnielConstants.BladeOfGluttonyUsedMagicActiveEffect) ||
            target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AnielConstants.BladeOfGluttonyUsedPhysicalActiveEffect))
        {
            totalPoints += T.GetStunPoints(this, Duration2, target);
        }
        return totalPoints;
    }
}
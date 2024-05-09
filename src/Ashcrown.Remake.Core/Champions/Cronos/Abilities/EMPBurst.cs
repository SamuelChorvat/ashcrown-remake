using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cronos.Champion;

namespace Ashcrown.Remake.Core.Champions.Cronos.Abilities;

// ReSharper disable once InconsistentNaming
public class EMPBurst : AbilityBase
{
    public EMPBurst(IChampion champion) 
        : base(champion, 
            CronosConstants.EMPBurst, 
            0, 
            new int[] {0,1,1,1,0}, 
            [AbilityClass.Affliction, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemies, 
            AbilityType.EnemiesDamageAndDebuff, 
            3)
    {
        Damage1 = 25;
        
        Description = $"Massive electromagnetic pulse radiates from {CronosConstants.Name} " +
                      $"and deals {$"{Damage1} affliction damage".HighlightInRed()} to all enemies. " +
                      $"All affected enemies will take 5 additional damage from Affliction abilities for the rest of the game. " +
                      $"After this ability is used, it will be replaced by {CronosConstants.PulseCannon.HighlightInGold()}.";
        Harmful = true;
        Debuff = true;
        AfflictionDamage = true;
        Damaging = true;
        ActiveEffectOwner = CronosConstants.Name;
        ActiveEffectName = CronosConstants.EMPBurstActiveEffect;
    }

    public override void OnUse()
    {
        if (Owner.AbilityController.GetMyAbilityByName(CronosConstants.PulseCannon) != null) {
            Owner.CurrentAbilities[2] = Owner.AbilityController.GetMyAbilityByName(CronosConstants.PulseCannon)!;
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        totalPoints += 5;
        return totalPoints;
    }
}
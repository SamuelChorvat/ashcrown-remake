using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Dex.Champion;

namespace Ashcrown.Remake.Core.Champions.Dex.Abilities;

public class Garrote : AbilityBase
{
    private AbilityType _abilityType;
    
    public Garrote(IChampion champion) 
        : base(champion, 
            DexConstants.Garrote, 
            0,
            [0,1,0,0,1], 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee],
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamageAndDebuff, 
            2)
    {
        Damage1 = 20;
        Duration1 = 1;
        
        Description = $"{DexConstants.Name} attacks one enemy from behind, dealing {$"{Damage1} physical damage".HighlightInOrange()} and " +
                      $"{"stunning".HighlightInPurple()} their Physical and Strategic abilities for {Duration1} turn. " +
                      $"During {DexConstants.Nightblade.HighlightInGold()} this ability is improved and targets all enemies and also its cost is reduced by <sprite=4>.";
        Stun = true;
        StunType = [AbilityClass.Physical, AbilityClass.Strategic];
        Harmful = true;
        Debuff = true;
        PhysicalDamage = true;
        Damaging = true;
        ActiveEffectOwner = DexConstants.Name;
        ActiveEffectName = DexConstants.GarroteActiveEffect;
    }
    
    public override AbilityType AbilityType
    {
        get =>
            Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(DexConstants.NightbladeActiveEffect) 
                ? AbilityType.EnemiesDamageAndDebuff 
                : AbilityType.EnemyDamageAndDebuff;
        set => _abilityType = value;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        totalPoints += T.GetStunPoints(this, Duration1, target);
        return totalPoints;
    }
}
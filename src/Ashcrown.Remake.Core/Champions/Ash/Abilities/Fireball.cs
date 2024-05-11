using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Ash.Champion;

namespace Ashcrown.Remake.Core.Champions.Ash.Abilities;

public class Fireball : AbilityBase
{
    public Fireball(IChampion champion) 
        : base(champion, 
            AshConstants.Fireball, 
            0,
            [1,0,0,1,0],
            [AbilityClass.Magic, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamageAndDebuff, 
            2)
    {
        Duration1 = 1;
        DealDamageReductionPoint1 = 20;
        Damage1 = 35;
        Description = $"{AshConstants.Name} hurls a fireball at one enemy, dealing {$"{Damage1} magic damage".HighlightInBlue()} to them " +
                      $"and reducing all non-Affliction ability damage they deal by {DealDamageReductionPoint1} damage for {Duration1} turn.";
        Harmful = true;
        Debuff = true;
        Damaging = true;
        MagicDamage = true;
        ActiveEffectOwner = AshConstants.Name;
        ActiveEffectName = AshConstants.FireballActiveEffect;
    }

    public override void StartTurnChecks()
    {
        if (!IsReady()) return;
        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AshConstants.PhoenixFlamesActiveEffect) 
            && Owner.ActiveEffectController.GetActiveEffectByName(AshConstants.PhoenixFlamesActiveEffect)!.Stacks >= 5) {
            Active = false;
        } else {
            Active = true;
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        totalPoints += 50;
        return totalPoints;
    }
}
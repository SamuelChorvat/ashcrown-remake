using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Gwen.Champion;

namespace Ashcrown.Remake.Core.Champions.Gwen.Abilities;

public class Kiss : AbilityBase
{
    public Kiss(IChampion champion) 
        : base(champion, 
            GwenConstants.Kiss, 
            0,
            [0,0,0,0,1], 
            [AbilityClass.Affliction, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            2)
    {
        Damage1 = 10;
        
        Description = $"{GwenConstants.Name} kisses one enemy, dealing {$"{Damage1} affliction damage".HighlightInRed()} to them " +
                      $"and {"removing".HighlightInPurple()} <sprite=4> from them. " +
                      $"This ability can only be used while {GwenConstants.Charm.HighlightInGold()} is active.";
        Harmful = true;
        AfflictionDamage= true;
        Damaging = true;
        EnergyRemove = true;
        EnergyAmount = 1;
        Active = false;
    }

    public override void StartTurnChecks()
    {
        if (!IsReady()) return;
        Active = Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GwenConstants.CharmBuffActiveEffect);
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        totalPoints += T.GetRemoveEnergyPoints(EnergyAmount, 1, this, target);
        return totalPoints;
    }
}
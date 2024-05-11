using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Ash.Champion;

namespace Ashcrown.Remake.Core.Champions.Ash.Abilities;

public class Inferno : AbilityBase
{
    public Inferno(IChampion champion) 
        : base(champion, 
            AshConstants.Inferno, 
            0,
            [1,0,0,1,0],
            [AbilityClass.Magic, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            2)
    {
        Damage1 = 1000;
        
        Description = $"{AshConstants.Name} deals {$"{Damage1} magic damage".HighlightInBlue()} to one enemy.";
        Harmful = true;
        MagicDamage = true;
        Damaging = true;
    }

    public override void StartTurnChecks()
    {
        if (!IsReady()) return;
        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AshConstants.PhoenixFlamesActiveEffect) 
            && Owner.ActiveEffectController.GetActiveEffectByName(AshConstants.PhoenixFlamesActiveEffect)!.Stacks < 8) {
            Active = false;
        } else {
            Active = true;
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        return totalPoints;
    }
}
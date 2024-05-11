using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Ash.Champion;

namespace Ashcrown.Remake.Core.Champions.Ash.Abilities;

public class FireWhirl : AbilityBase
{
    public FireWhirl(IChampion champion) 
        : base(champion, 
            AshConstants.FireWhirl, 
            0,
            [1,0,0,1,0],
            [AbilityClass.Magic, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamageAndDebuff, 
            2)
    {
        Damage1 = 60;
        Duration1 = 1;
        
        Description = $"{AshConstants.Name} unleashes a fire whirl on one enemy, " +
                      $"dealing {$"{Damage1} magic damage".HighlightInBlue()} to them and {"stunning".HighlightInPurple()} all their abilities for {Duration1} turn.";
        Stun = true;
        StunType = [AbilityClass.All];
        Harmful = true;
        Debuff = true;
        Damaging = true;
        MagicDamage = true;

        ActiveEffectOwner = AshConstants.Name;
        ActiveEffectName = AshConstants.FireWhirlActiveEffect;
    }

    public override void StartTurnChecks()
    {
        if (!IsReady()) return;
        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AshConstants.PhoenixFlamesActiveEffect) 
            && (Owner.ActiveEffectController.GetActiveEffectByName(AshConstants.PhoenixFlamesActiveEffect)!.Stacks < 5 
                || Owner.ActiveEffectController.GetActiveEffectByName(AshConstants.PhoenixFlamesActiveEffect)!.Stacks >= 8)) {
            Active = false;
        } else {
            Active = true;
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, Duration1, this, target);
        totalPoints += T.GetStunPoints(this, Duration1, target);
        return totalPoints;
    }
}
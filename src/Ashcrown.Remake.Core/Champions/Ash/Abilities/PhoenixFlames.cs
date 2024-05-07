using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Ash.Champion;

namespace Ashcrown.Remake.Core.Champions.Ash.Abilities;

public class PhoenixFlames : AbilityBase
{
    public PhoenixFlames(IChampion champion) 
        : base(champion, 
            AshConstants.PhoenixFlames, 
            0,
            [0,0,0,0,0],
            [AbilityClass.Affliction, AbilityClass.Instant], 
            AbilityTarget.Self, 
            AbilityType.AllyBuff, 
            3)
    {
        Damage1 = 5;
        ReceiveDamageReductionPoint1 = 5;
        DealDamageIncreasePoint1 = 5;
        
        Description = $"{AshConstants.Name} receives 5 {"affliction damage".HighlightInRed()}, " +
                      $"but permanently gains {$"{ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()} " +
                      $"and increases the {"magic damage".HighlightInBlue()} of all her abilities by {DealDamageIncreasePoint1}. " +
                      $"At 5 stacks, {AshConstants.Fireball.HighlightInGold()} will be replaced by {AshConstants.FireWhirl.HighlightInGold()} and at 8 stacks, " +
                      $"it will be replaced by {AshConstants.Inferno.HighlightInGold()}. This ability can only be used 10 times.";
        SelfCast = true;
        Helpful = true;
        Harmful = true;
        Buff = true;
        Damaging = true;
        AfflictionDamage = true;
        CannotBeRemoved = true;
        DoNotModifyOnDealDamage = true;
        ActiveEffectOwner = AshConstants.Name;
        ActiveEffectName = AshConstants.PhoenixFlamesActiveEffect;
    }

    public override void StartTurnChecks()
    {
        if (!IsReady()) return;
        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AshConstants.PhoenixFlamesActiveEffect) 
            && Owner.ActiveEffectController.GetActiveEffectByName(AshConstants.PhoenixFlamesActiveEffect)!.Stacks == 10) {
            Active = false;
        } else {
            Active = true;
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        if (Owner.Health <= 25) {
            return 0;
        }
        var totalPoints = T.GetPointsReductionPoints(ReceiveDamageReductionPoint1,
            AiCalculatorConstants.InfiniteNumberOfTurns, target);
        totalPoints += T.GetSpecialConditionPoints(AiCalculatorConstants.InfiniteNumberOfTurns);
        totalPoints -= (100 - Owner.Health) * 2;
        return totalPoints;
    }
}
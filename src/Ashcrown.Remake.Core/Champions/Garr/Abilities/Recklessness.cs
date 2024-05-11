using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Garr.Champion;

namespace Ashcrown.Remake.Core.Champions.Garr.Abilities;

public class Recklessness : AbilityBase
{
    public Recklessness(IChampion champion) 
        : base(champion, 
            GarrConstants.Recklessness, 
            0,
            [0,0,0,0,0], 
            [AbilityClass.Affliction,AbilityClass.Instant], 
            AbilityTarget.Self, 
            AbilityType.AllyBuff, 
            3)
    {
        Damage1 = 20;
        
        Description = $"{GarrConstants.Name} becomes more reckless, " +
                      $"{GarrConstants.Slam.HighlightInGold()} will deal an additional {"20 physical damage".HighlightInOrange()} " +
                      $"and {GarrConstants.BarbedChain.HighlightInGold()} will deal an additional {"10 physical damage".HighlightInOrange()} per turn. " +
                      $"Garr will receive {$"{Damage1} affliction damage".HighlightInRed()} when this ability is used. " +
                      $"This ability can only be used 3 times and the effect is permanent.";
        SelfCast = true;
        Helpful = true;
        Harmful = true;
        Buff = true;
        Damaging = true;
        AfflictionDamage = true;

        DoNotModifyOnDealDamage = true;
        ActiveEffectOwner = GarrConstants.Name;
        ActiveEffectName = GarrConstants.RecklessnessActiveEffect;
    }

    public override void StartTurnChecks()
    {
        if (Active && Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GarrConstants.RecklessnessActiveEffect) 
                   && Owner.ActiveEffectController.GetActiveEffectByName(GarrConstants.RecklessnessActiveEffect)!.Stacks == 3) {
            Active = false;
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        if (Owner.Health <= 25) {
            return 0;
        }

        var totalPoints = T.GetSpecialConditionPoints(AiCalculatorConstants.InfiniteNumberOfTurns);
        totalPoints -= (100 - Owner.Health) * 2;
        return totalPoints;
    }
}
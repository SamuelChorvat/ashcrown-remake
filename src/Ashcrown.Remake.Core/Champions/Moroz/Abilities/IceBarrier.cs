using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Moroz.Champion;

namespace Ashcrown.Remake.Core.Champions.Moroz.Abilities;

public class IceBarrier : AbilityBase
{
    public IceBarrier(IChampion champion) 
        : base(champion, 
            MorozConstants.IceBarrier, 
            2,
            [0,0,0,0,0], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Self, 
            AbilityType.AllyBuff, 
            2)
    {
        Description = $"{MorozConstants.Name} creates an ice barrier around himself. " +
                      $"Until {MorozConstants.Name} is targeted by new enemy Strategic ability, he will {"ignore".HighlightInPurple()} all {"stun".HighlightInPurple()} effects. " +
                      $"This ability cannot be used while active.";
        SelfCast = true;
        IgnoreStuns = true;
        Helpful = true;
        Buff = true;
        UniqueActiveEffect = true;

        ActiveEffectOwner = MorozConstants.Name;
        ActiveEffectName = MorozConstants.IceBarrierActiveEffect;
    }

    public override void StartTurnChecks()
    {
        if (!IsReady()) return;
        Active = !Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(MorozConstants.IceBarrierActiveEffect);
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetIgnoreStunEffectsPoints(AiCalculatorConstants.InfiniteNumberOfTurns, this, target);
        return totalPoints;
    }
}
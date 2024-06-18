using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Moroz.Champion;

namespace Ashcrown.Remake.Core.Champions.Moroz.Abilities;

public class FrozenArmor : AbilityBase
{
    public FrozenArmor(IChampion champion) 
        : base(champion, 
            MorozConstants.FrozenArmor, 
            4,
            [0,0,0,0,1], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Self, 
            AbilityType.AllyBuff, 
            3)
    {
        DestructibleDefense1 = 40;
        
        Description = $"By covering himself with ice, {MorozConstants.Name} permanently gains {$"{DestructibleDefense1} points of destructible defense".HighlightInYellow()}. " +
                      $"This ability cannot stack and will re-apply itself.";
        SelfCast = true;
        Helpful = true;
        Buff = true;

        ActiveEffectOwner = MorozConstants.Name;
        ActiveEffectName = MorozConstants.FrozenArmorActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        int totalPoints;
        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(MorozConstants.FrozenArmorActiveEffect)) {
            totalPoints = T.GetDestructibleDefensePoints(Math.Max(0, DestructibleDefense1 
                                                                     - Owner.ActiveEffectController.GetActiveEffectByName(MorozConstants.FrozenArmorActiveEffect)!.DestructibleDefense), target);
        } else {
            totalPoints = T.GetDestructibleDefensePoints(DestructibleDefense1, target);
        }
        return totalPoints;
    }
}
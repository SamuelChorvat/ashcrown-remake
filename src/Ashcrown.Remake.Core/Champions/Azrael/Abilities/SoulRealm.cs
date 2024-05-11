using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Azrael.Champion;

namespace Ashcrown.Remake.Core.Champions.Azrael.Abilities;

public class SoulRealm : AbilityBase
{
    public SoulRealm(IChampion champion) 
        : base(champion, 
            AzraelConstants.SoulRealm, 
            2,
            [0,0,0,0,1], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Self, 
            AbilityType.AllyBuff, 
            1)
    {
        
        ReceiveDamageReductionPercent1 = 25;
        Duration2 = 1;
        BonusDamage1 = 20;
        
        Description = $"{AzraelConstants.Name} slips into the soul realm gaining {$"{ReceiveDamageReductionPercent1}% damage reduction".HighlightInYellow()} permanently. " +
                      $"This effect stacks. {AzraelConstants.Reap.HighlightInGold()} will deal an additional {$"{BonusDamage1} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()} for {Duration2} turn when this ability is used. " +
                      $"This ability can only be used 3 times.";
        Helpful = true;
        Buff = true;
        SelfCast = true;
        ActiveEffectOwner = AzraelConstants.Name;
        ActiveEffectName = AzraelConstants.SoulRealmActiveEffect;
    }

    public override void StartTurnChecks()
    {
        if (!IsReady()) return;
        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(AzraelConstants.SoulRealmActiveEffect) 
            && Owner.ActiveEffectController.GetActiveEffectByName(AzraelConstants.SoulRealmActiveEffect)!.Stacks == 3) {
            Active = false;
        } else {
            Active = true;
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetPercentageReductionPoints(ReceiveDamageReductionPercent1,
            AiCalculatorConstants.InfiniteNumberOfTurns, Owner);
        totalPoints += 20;
        return totalPoints;
    }
}
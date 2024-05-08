using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Braya.Champion;

namespace Ashcrown.Remake.Core.Champions.Braya.Abilities;

public class HuntersFocus : AbilityBase
{
    public HuntersFocus(IChampion champion) 
        : base(champion, 
            BrayaConstants.HuntersFocus, 
            1,
            [0,0,0,0,0], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Self, 
            AbilityType.AllyBuff, 
            1)
    {
        Duration2 = 1;
        
        Description = $"{BrayaConstants.Name} focuses her mind and assesses the battlefield. " +
                      $"Braya gains 4 {BrayaConstants.HuntersFocus.HighlightInGold()} stacks. " +
                      $"During this time, Braya can use {BrayaConstants.HuntersFocus.HighlightInGold()} again " +
                      $"to {"ignore".HighlightInPurple()} all harmful effects except energy cost changes for {Duration2} turn " +
                      $"while using up 1 {BrayaConstants.HuntersFocus.HighlightInGold()} stack."; ;
        SelfCast = true;
        Helpful = true;
        Buff = true;
        IgnoreHarmful = true;
        ActiveEffectOwner = BrayaConstants.Name;
        ActiveEffectName = BrayaConstants.HuntersFocusStacksActiveEffect;
    }

    public override bool UseChecks()
    {
        var aeRef = Owner.ActiveEffectController.GetActiveEffectByName(BrayaConstants.HuntersFocusStacksActiveEffect);
        if (aeRef is { Stacks: > 0 }) {
            aeRef.Stacks -= 1;
        }

        return true;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = Owner.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(BrayaConstants.HuntersFocusStacksActiveEffect) 
            ? T.GetIgnoreHarmfulPoints(Duration2, this, target) 
            : T.GetSpecialConditionPoints(AiCalculatorConstants.InfiniteNumberOfTurns);
        return totalPoints;
    }
}
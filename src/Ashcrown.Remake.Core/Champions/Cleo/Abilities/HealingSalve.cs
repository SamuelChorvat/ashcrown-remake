using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cleo.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Cleo.Champion;

namespace Ashcrown.Remake.Core.Champions.Cleo.Abilities;

public class HealingSalve : AbilityBase
{
    public HealingSalve(IChampion champion) 
        : base(champion, 
            CleoConstants.HealingSalve, 
            2,
            [1,0,0,0,0], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Allies, 
            AbilityType.AlliesHeal, 
            3)
    {
        Heal1 = 10;
        DestructibleDefense1 = 10;
        
        Description = $"{CleoConstants.Name} heals herself and her allies for {$"{Heal1} health".HighlightInGreen()} " +
                      $"and gives them {$"{DestructibleDefense1} points of destructible defense".HighlightInYellow()}.";
        Helpful = true;
        SelfCast = true;
        Healing = true;
        Buff = true;
    }

    public override void AdditionalReceiveAbilityHealingLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        target.ChampionController.ReceiveActiveEffect(new HealingSalveActiveEffect(this, target), 
            true, new AppliedAdditionalLogic());
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetHealingPoints(Heal1, 1, this, target);
        totalPoints += T.GetDestructibleDefensePoints(DestructibleDefense1, target);
        return totalPoints;
    }
}
using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Nerissa.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Nerissa.Champion;

namespace Ashcrown.Remake.Core.Champions.Nerissa.Abilities;

public class AncientSpirits : AbilityBase
{
    public AncientSpirits(IChampion champion) 
        : base(champion, 
            NerissaConstants.AncientSpirits, 
            3,
            [0,0,0,1,1], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Self, 
            AbilityType.AllyHeal, 
            3)
    {
        Heal1 = 25;
        DestructibleDefense1 = 25;
        
        Description = $"{NerissaConstants.Name} heals herself for {$"{Heal1} health".HighlightInGreen()} " +
                      $"and gains {$"{DestructibleDefense1} destructible defense".HighlightInYellow()}.";
        Helpful = true;
        SelfCast = true;
        Healing = true;
        Buff = true;
    }

    public override void AdditionalReceiveAbilityHealingLogic(IChampion target, 
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        Owner.ChampionController.DealActiveEffect(target, this, 
            new AncientSpiritsActiveEffect(this, target), true, appliedAdditionalLogic);
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetHealingPoints(Heal1, 1, this, target);
        totalPoints += T.GetDestructibleDefensePoints(DestructibleDefense1, target);
        return totalPoints;
    }
}
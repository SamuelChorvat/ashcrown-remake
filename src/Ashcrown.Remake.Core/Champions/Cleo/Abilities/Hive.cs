using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cleo.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Cleo.Champion;

namespace Ashcrown.Remake.Core.Champions.Cleo.Abilities;

public class Hive : AbilityBase
{
    public Hive(IChampion champion) 
        : base(champion, 
            CleoConstants.Hive, 
            0,
            [1,1,0,1,0], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Allies, 
            AbilityType.AlliesHeal, 
            3)
    {
        Heal1 = 40;
        BonusHeal1 = 5;
        
        Description = $"{CleoConstants.Name} heals herself and her allies for {$"{Heal1} health".HighlightInGreen()}. " +
                      $"For the rest of the game, {CleoConstants.Name}'s team will heal {$"{BonusHeal1} health".HighlightInGreen()} at the start of their turn. " +
                      $"This ability will be replaced by {CleoConstants.HealingSalve.HighlightInGold()}. " +
                      $"This ability will end if {CleoConstants.Name} dies.";
        Helpful = true;
        SelfCast = true;
        Healing = true;
        Buff = true;
    }

    public override void AdditionalReceiveAbilityHealingLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        target.ChampionController.ReceiveActiveEffect(new HiveActiveEffect(this, target), 
            true, new AppliedAdditionalLogic());
    }

    public override void OnUse()
    {
        if (Owner.AbilityController.GetMyAbilityByName(CleoConstants.HealingSalve) != null) {
            Owner.CurrentAbilities[2] = Owner.AbilityController.GetMyAbilityByName(CleoConstants.HealingSalve)!;
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetHealingPoints(Heal1, 1, this, target);
        totalPoints += T.GetHealingPoints(BonusHeal1, AiCalculatorConstants.InfiniteNumberOfTurns, this, target);
        return totalPoints;
    }
}
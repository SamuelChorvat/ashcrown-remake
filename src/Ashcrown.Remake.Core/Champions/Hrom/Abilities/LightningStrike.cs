using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Hrom.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Hrom.Champion;

namespace Ashcrown.Remake.Core.Champions.Hrom.Abilities;

public class LightningStrike : AbilityBase
{
    public LightningStrike(IChampion champion) 
        : base(champion, 
            HromConstants.LightningStrike, 
            0,
            [1,0,0,0,0], 
            [AbilityClass.Magic, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            1)
    {
        Duration1 = 1;
        Damage1 = 25;
        
        Description = $"{HromConstants.Name} deals {$"{Damage1} magic damage".HighlightInBlue()} to one enemy. " +
                      $"{HromConstants.LightningStorm.HighlightInGold()} may be used the following turn.";
        Harmful = true;
        Helpful = true;
        Buff = true;
        Damaging = true;
        MagicDamage = true;
    }

    public override void AdditionalDealAbilityDamageLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        var lightAe = new LightningStrikeActiveEffect(this, Owner);
        Owner.ChampionController.DealActiveEffect(Owner, this, lightAe, true, appliedAdditionalLogic);
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        totalPoints += T.GetSpecialConditionPoints(Duration1);
        return totalPoints;
    }
}
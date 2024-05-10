using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Evanore.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Evanore.Champion;

namespace Ashcrown.Remake.Core.Champions.Evanore.Abilities;

public class MonstrousBear : AbilityBase
{
    public MonstrousBear(IChampion champion) 
        : base(champion, 
            EvanoreConstants.MonstrousBear,
            1,
            new int[] {0,1,0,1,0}, 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            1)
    {
        Damage1 = 30;
        Damage2 = 15;
        Duration1 = 2;
        
        Description = $"{EvanoreConstants.Name} summons a giant double headed bear that deals {$"{Damage1} physical damage".HighlightInOrange()} to one enemy. " +
                      $"The following {Duration1} turns, any enemy that uses a new ability will be dealt {$"{Damage2} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()}.";
        Harmful = true;
        Debuff = true;
        PhysicalDamage = true;
        Damaging = true;
    }

    public override void AdditionalDealAbilityDamageLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        foreach (var champion in target.BattlePlayer.Champions)
        {
            Owner.ChampionController.DealActiveEffect(champion,
                this, new MonstrousBearActiveEffect(this, champion), true, appliedAdditionalLogic);
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        totalPoints += 30;
        return totalPoints;
    }
}
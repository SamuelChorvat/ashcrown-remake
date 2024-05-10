using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Dura.Champion;

namespace Ashcrown.Remake.Core.Champions.Dura.Abilities;

public class Whirlwind : AbilityBase
{
    public Whirlwind(IChampion champion) 
        : base(champion, 
            DuraConstants.Whirlwind, 
            1,
            [0,0,1,0,0], 
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemies, 
            AbilityType.EnemiesDamage, 
            2)
    {
        Damage1 = 15;
        Duration1 = 1;
        
        Description = $"{DuraConstants.Name} becomes {"invulnerable".HighlightInPurple()} for {Duration1} turn " +
                      $"while dealing {$"{Damage1} physical damage".HighlightInOrange()} to all enemies.";
        Harmful = true;
        PhysicalDamage = true;
        Damaging = true;
        Invulnerability = true;
        TypeOfInvulnerability = [AbilityClass.All];
        Helpful = true;
        Buff = true;
        SelfDisplay = true;
    }

    public override void AdditionalDealAbilityDamageLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        Owner.ChampionController.EnemyDamageMyBuff(this, DuraConstants.Whirlwind, 
            DuraConstants.Name, DuraConstants.WhirlwindActiveEffect, appliedAdditionalLogic);
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        return totalPoints;
    }

    public override int CalculateSingletonSelfEffectTotalPoints<T>()
    {
        var totalPoints = T.GetInvulnerabilityPoints(Duration1, this, Owner);
        return totalPoints;
    }
}
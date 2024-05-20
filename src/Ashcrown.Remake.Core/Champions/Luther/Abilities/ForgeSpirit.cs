using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Luther.ActiveEffects;
using Ashcrown.Remake.Core.Champions.Luther.Champion;

namespace Ashcrown.Remake.Core.Champions.Luther.Abilities;

public class ForgeSpirit : AbilityBase
{
    public ForgeSpirit(IChampion champion) 
        : base(champion, 
            LutherConstants.ForgeSpirit, 
            0,
            [0,0,0,1,1], 
            [AbilityClass.Magic, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamageAndDebuff, 
            3)
    {
        Damage1 = 30;
        Duration1 = 3;
        
        Description = $"{LutherConstants.Name} deals {$"{Damage1} {"piercing".HighlightInBold()} magic damage".HighlightInBlue()} to one enemy and for {Duration1} turns, " +
                      $"all original non-affliction damage they deal will be reduced to a maximum of 30. " +
                      $"{LutherConstants.FieryBrand.HighlightInGold()} will also be applied to one random enemy.";
        Harmful = true;
        Debuff = true;
        PiercingDamage = true;
        MagicDamage = true;
        Damaging = true;

        ActiveEffectOwner = LutherConstants.Name;
        ActiveEffectName = LutherConstants.ForgeSpiritActiveEffect;
    }

    public override void AdditionalDealAbilityDamageLogic(IChampion target, AppliedAdditionalLogic appliedAdditionalLogic)
    {
        var fiery = Owner.AbilityController.GetMyAbilityByName(LutherConstants.FieryBrand);
        if (fiery == null) return;
        
        //TODO what if the ability is copied, should make case where the ability is first added to the character that copied it, and do this for all cases like this
        var randTarget =  Owner.BattlePlayer.GetRandomEnemyChampion();
        Owner.ChampionController.DealActiveEffect(randTarget, fiery, new FieryBrandTargetActiveEffect(fiery, randTarget), 
            true, appliedAdditionalLogic);
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        totalPoints += 100;
        return totalPoints;
    }
}
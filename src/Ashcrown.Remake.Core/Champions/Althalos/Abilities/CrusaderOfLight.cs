﻿using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Althalos.Champion;

namespace Ashcrown.Remake.Core.Champions.Althalos.Abilities;

public class CrusaderOfLight : Ability.Abstract.Ability
{
    public CrusaderOfLight(IChampion champion) : base(champion, 
        AlthalosConstants.CrusaderOfLight,
        4, 
        [0,0,0,0,1], 
        [AbilityClass.Strategic, AbilityClass.Instant], 
        AbilityTarget.Self, 
        AbilityType.AllyBuff)
    {
        Duration1 = 4;
        BonusDamage1 = 10;
        ReceiveDamageReductionPoint1 = 10;
        Description =
            $"{AlthalosConstants.Althalos} empowers himself with holy energy. " +
            $"For {Duration1} turns, {AlthalosConstants.Althalos} will gain " +
            $"{$"{ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()} " +
            $"and will {"ignore".HighlightInPurple()} all stun effects. " +
            $"During this time {AlthalosConstants.HammerOfJustice.HighlightInGold()} " +
            $"will deal an additional {$"{BonusDamage1} physical damage".HighlightInOrange()}.";
        SelfCast = true;
        IgnoreStuns = true;
        Helpful = true;
        Buff = true;
        ActiveEffectOwner = AlthalosConstants.Althalos;
        ActiveEffectName = AlthalosConstants.CrusaderOfLightActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetPointsReductionPoints(ReceiveDamageReductionPoint1, Duration1, target);
        totalPoints += T.GetIgnoreStunEffectsPoints(Duration1,this, target);
        totalPoints += T.GetSpecialConditionPoints(Duration1);
        return totalPoints;
    }
}
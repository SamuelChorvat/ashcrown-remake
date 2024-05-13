using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Gwen.Champion;

namespace Ashcrown.Remake.Core.Champions.Gwen.Abilities;

public class Charm : AbilityBase
{
    public Charm(IChampion champion) 
        : base(champion, 
            GwenConstants.Charm, 
            1,
            [0,0,0,1,1], 
            [AbilityClass.Magic, AbilityClass.Action, AbilityClass.Ranged, AbilityClass.Instant], 
            AbilityTarget.Enemies, 
            AbilityType.EnemiesActionControl, 
            1)
    {
        Duration1 = 2;
        Damage1 = 15;
        ReceiveDamageReductionPoint1 = 15;
        
        Description = $"{GwenConstants.Name} charms all enemies and deals {$"{Damage1} magic damage".HighlightInBlue()} to them each turn for {Duration1} turns. " +
                      $"The following {Duration1} turns, {GwenConstants.Name} gains {$"{ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()} " +
                      $"and {GwenConstants.Kiss.HighlightInGold()} can be used.";
        SelfDisplay = true;
        Harmful = true;
        Helpful = true;
        Debuff = true;
        Buff = true;
        Damaging = true;
        MagicDamage = true;

        ActiveEffectOwner = GwenConstants.Name;
        ActiveEffectSourceName = GwenConstants.CharmMeActiveEffect;
        ActiveEffectTargetName = GwenConstants.CharmTargetActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, Duration1, this, target);
        return totalPoints;
    }

    public override int CalculateSingletonSelfEffectTotalPoints<T>()
    {
        var totalPoints = T.GetPointsReductionPoints(ReceiveDamageReductionPoint1, Duration1, Owner);
        totalPoints += 50;
        return totalPoints;
    }
}
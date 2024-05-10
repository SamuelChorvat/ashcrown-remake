using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Dex.Champion;

namespace Ashcrown.Remake.Core.Champions.Dex.Abilities;

public class Nightblade : AbilityBase
{
    public Nightblade(IChampion champion) 
        : base(champion, 
            DexConstants.Nightblade, 
            4,
            [0,1,0,0,1], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Self, 
            AbilityType.AllyBuff, 
            3)
    {
        Duration1 = 3;
        DestructibleDefense1 = 30;
        
        Description = $"The following {Duration1} turns {DexConstants.ShurikenThrow.HighlightInGold()} and {DexConstants.Garrote.HighlightInGold()} will have their costs reduced by <sprite=4> and {DexConstants.Garrote.HighlightInGold()} will also affect all enemies. " +
                      $"During this time Dex gains {$"{DestructibleDefense1} destructible defense".HighlightInYellow()} and is {"invulnerable".HighlightInPurple()} to Physical and Strategic abilities.";
        SelfCast = true;
        Invulnerability = true;
        TypeOfInvulnerability = [AbilityClass.Physical, AbilityClass.Strategic];
        Helpful = true;
        Buff = true;
        ActiveEffectOwner = DexConstants.Name;
        ActiveEffectName = DexConstants.NightbladeActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDestructibleDefensePoints(DestructibleDefense1, target);
        totalPoints += T.GetInvulnerabilityPoints(Duration1, this, target);
        totalPoints += T.GetSpecialConditionPoints(Duration1);
        return totalPoints;
    }
}
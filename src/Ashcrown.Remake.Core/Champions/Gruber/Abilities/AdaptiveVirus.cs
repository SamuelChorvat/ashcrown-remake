using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Gruber.Champion;

namespace Ashcrown.Remake.Core.Champions.Gruber.Abilities;

public class AdaptiveVirus : AbilityBase
{
    public AdaptiveVirus(IChampion champion) 
        : base(champion, 
            GruberConstants.AdaptiveVirus,
            3,
            [1,0,0,1,0], 
            [AbilityClass.Affliction, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.All, 
            AbilityType.AlliesBuffEnemiesDebuff, 
            2)
    {
        Damage1 = 5;
        DestructibleDefense1 = 10;
        
        Description = $"{GruberConstants.Name} releases a virus giving his allies {$"{DestructibleDefense1} destructible defense".HighlightInYellow()} " +
                      $"and dealing {$"{Damage1} affliction damage".HighlightInRed()} each turn permanently to the enemy team. This effect can stack.";
        SelfCast = true;
        Harmful = true;
        Debuff = true;
        Damaging = true;
        Helpful = true;
        Buff = true;
        ActiveEffectOwner = GruberConstants.Name;
        ActiveEffectAlliesName = GruberConstants.AdaptiveVirusAllyActiveEffect;
        ActiveEffectEnemiesName = GruberConstants.AdaptiveVirusEnemyActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = Owner.BattlePlayer.PlayerNo == target.BattlePlayer.PlayerNo 
            ? T.GetDestructibleDefensePoints(DestructibleDefense1, target) 
            : T.GetDamagePoints(Damage1, AiCalculatorConstants.InfiniteNumberOfTurns, this, target);
        return totalPoints;
    }
}
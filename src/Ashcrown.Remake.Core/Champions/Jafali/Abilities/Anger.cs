using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Jafali.Champion;

namespace Ashcrown.Remake.Core.Champions.Jafali.Abilities;

public class Anger : AbilityBase
{
    public Anger(IChampion champion) 
        : base(champion, 
            JafaliConstants.Anger, 
            0,
            [0,1,0,0,0], 
            [AbilityClass.Affliction, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDebuff, 
            1)
    {
        DestructibleDefense1 = 15;
        
        Description = $"{JafaliConstants.Name} applies {JafaliConstants.DecayingSoul.HighlightInGold()} to one enemy and gains {$"{DestructibleDefense1} destructible defense".HighlightInYellow()}. " +
                      $"Enemy that destroys the destructible defense will have {JafaliConstants.DecayingSoul.HighlightInGold()} applied to them. " +
                      $"While the destructible defense is active this ability will be replaced by {JafaliConstants.DecayingSoul.HighlightInGold()}.";
        Harmful = true;
        Debuff = true;
        
        ActiveEffectOwner = JafaliConstants.Name;
        ActiveEffectName = JafaliConstants.DecayingSoulActiveEffect;
    }

    public override void StartTurnChecks()
    {
        if (!IsReady()) return;
        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(JafaliConstants.AngerActiveEffect)) {
            Active = false;
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(5, 5, this, target);
        totalPoints += T.GetDestructibleDefensePoints(DestructibleDefense1, Owner);
        return totalPoints;
    }
}
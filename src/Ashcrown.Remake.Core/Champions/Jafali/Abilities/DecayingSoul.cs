using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Jafali.Champion;

namespace Ashcrown.Remake.Core.Champions.Jafali.Abilities;

public class DecayingSoul : AbilityBase
{
    public DecayingSoul(IChampion champion) 
        : base(champion, 
            JafaliConstants.DecayingSoul, 
            0,
            [0,0,0,0,1], 
            [AbilityClass.Affliction, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDebuff, 
            1)
    {
        Damage1 = 5;
        Duration1 = 5;
        
        Description = $"{JafaliConstants.Name} targets an enemy. They will receive {$"{Damage1} affliction damage".HighlightInRed()} for {Duration1} turns.";
        Harmful = true;
        Damaging = true;
        AfflictionDamage = true;
        Debuff = true;

        ActiveEffectOwner = JafaliConstants.Name;
        ActiveEffectName = JafaliConstants.DecayingSoulActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, Duration1, this, target);
        return totalPoints;
    }
}
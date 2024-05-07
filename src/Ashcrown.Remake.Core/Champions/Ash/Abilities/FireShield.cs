using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Ash.Champion;

namespace Ashcrown.Remake.Core.Champions.Ash.Abilities;

public class FireShield : AbilityBase
{
    public FireShield(IChampion champion) 
        : base(champion, 
            AshConstants.FireShield, 
            2,
            [1,0,0,0,0],
            [AbilityClass.Magic, AbilityClass.Action,AbilityClass.Ranged],
            AbilityTarget.Enemy, 
            AbilityType.EnemiesActionControl, 
            1)
    {
        Duration1 = 3;
        Damage1 = 10;
        
        Description = $"{AshConstants.Name} deals {$"{Damage1} magic damage".HighlightInBlue()} to one enemy for {Duration1} turns. " +
                      $"During this time, if an enemy uses a new harmful Physical ability on {AshConstants.Name}, " +
                      $"she will deal {$"{Damage1} magic damage".HighlightInBlue()} to them. " +
                      $"This ability will end if the target or {AshConstants.Name} dies.";
        SelfDisplay = true;
        Debuff = true;
        Buff = true;
        Harmful = true;
        Damaging = true;
        MagicDamage = true;
        ActiveEffectOwner = AshConstants.Name;
        ActiveEffectSourceName = AshConstants.FireShieldMeActiveEffect;
        ActiveEffectTargetName = AshConstants.FireShieldTargetActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, Duration1, this, target);
        totalPoints += 30;
        return totalPoints;
    }
}
using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Evanore.Champion;

namespace Ashcrown.Remake.Core.Champions.Evanore.Abilities;

public class MurderOfCrows : AbilityBase
{
    public MurderOfCrows(IChampion champion) 
        : base(champion, 
            EvanoreConstants.MurderOfCrows, 
            3,
            [0,0,0,1,1], 
            [AbilityClass.Physical,AbilityClass.Action,AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyActionControl, 
            2)
    {
        Duration1 = 3;
        Damage1 = 15;
        
        Description = $"{EvanoreConstants.Name} summons a murder of crows to assault one enemy, " +
                      $"dealing {$"{Damage1} physical damage".HighlightInOrange()} to them for {Duration1} turns.";
        SelfDisplay = true;
        Debuff = true;
        Harmful = true;
        Damaging = true;
        PhysicalDamage = true;

        ActiveEffectOwner = EvanoreConstants.Name;
        ActiveEffectSourceName = EvanoreConstants.MurderOfCrowsMeActiveEffect;
        ActiveEffectTargetName = EvanoreConstants.MurderOfCrowsTargetActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, Duration1, this, target);
        return totalPoints;
    }
}
using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Jafali.Champion;

namespace Ashcrown.Remake.Core.Champions.Jafali.Abilities;

public class Avarice : AbilityBase
{
    public Avarice(IChampion champion) 
        : base(champion, 
            JafaliConstants.Avarice, 
            0,
            [0,0,0,0,2], 
            [AbilityClass.Affliction, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemies, 
            AbilityType.EnemiesDebuff, 
            2)
    {
        Description = $"{JafaliConstants.Name} applies {JafaliConstants.DecayingSoul.HighlightInGold()} to all enemies.";
        AfflictionDamage = true;
        Damaging = true;
        Harmful = true;
        Debuff = true;

        ActiveEffectOwner = JafaliConstants.Name;
        ActiveEffectName = JafaliConstants.DecayingSoulActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(5, 5, this, target);
        return totalPoints;
    }
}
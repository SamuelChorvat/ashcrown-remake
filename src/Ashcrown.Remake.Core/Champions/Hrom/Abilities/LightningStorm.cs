using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Hrom.Champion;

namespace Ashcrown.Remake.Core.Champions.Hrom.Abilities;

public class LightningStorm : AbilityBase
{
    public LightningStorm(IChampion champion) 
        : base(champion, 
            HromConstants.LightningStorm,
            0,
            [1,0,0,0,2], 
            [AbilityClass.Magic, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemies, 
            AbilityType.EnemiesDamage, 
            3)
    {
        Damage1 = 45;
        
        Description = $"{HromConstants.Name} conjures a lightning storm, dealing {$"{Damage1} magic damage".HighlightInBlue()} to all enemies. " +
                      $"This ability requires {HromConstants.LightningStrike.HighlightInGold()} to be used in the previous turn.";
        Harmful = true;
        Damaging = true;
        MagicDamage = true;
        Active = false;
    }

    public override void StartTurnChecks()
    {
        if(IsReady())
        {
            Active = Owner.ActiveEffectController
                .ActiveEffectPresentByActiveEffectName(HromConstants.LightningStrikeHelperActiveEffect);
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        return totalPoints;
    }
}
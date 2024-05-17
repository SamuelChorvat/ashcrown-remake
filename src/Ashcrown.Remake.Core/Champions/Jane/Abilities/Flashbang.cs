using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Jane.Champion;

namespace Ashcrown.Remake.Core.Champions.Jane.Abilities;

public class Flashbang : AbilityBase
{
    public Flashbang(IChampion champion) 
        : base(champion, 
            JaneConstants.Flashbang, 
            3,
            [0,0,0,0,1], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Allies, 
            AbilityType.AlliesBuff, 
            3)
    {
        Duration1 = 1;
        
        Description = $"{JaneConstants.Name} covers herself and her allies, making them {"invulnerable".HighlightInPurple()} to all non-Affliction abilities for {Duration1} turn. " +
                      $"{JaneConstants.Name} only carries 3 of these grenades at all times, so this ability can only be used 3 times.";
        Invulnerability = true;
        TypeOfInvulnerability = [AbilityClass.Strategic, AbilityClass.Magic, AbilityClass.Physical];
        Helpful = true;
        Buff = true;
        SelfCast = true;

        ActiveEffectOwner = JaneConstants.Name;
        ActiveEffectName = JaneConstants.FlashbangActiveEffect;
    }

    public override void StartTurnChecks()
    {
        if (!IsReady()) return;
        Active = TimesUsed < 3;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetInvulnerabilityPoints(Duration1, this, target);
        return totalPoints;
    }
}
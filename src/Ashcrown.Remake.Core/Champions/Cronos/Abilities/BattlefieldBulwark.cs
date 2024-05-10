using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cronos.Champion;

namespace Ashcrown.Remake.Core.Champions.Cronos.Abilities;

public class BattlefieldBulwark : AbilityBase
{
    public BattlefieldBulwark(IChampion champion) 
        : base(champion, 
            CronosConstants.BattlefieldBulwark, 
            3,
            [0,0,0,1,0], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.All, 
            AbilityType.AlliesBuffEnemiesDebuff, 
            2)
    {
        Duration1 = 2;
        
        Description = $"For {Duration1} turns, all enemies will be unable to {"reduce damage".HighlightInPurple()} or " +
                      $"become {"invulnerable".HighlightInPurple()}. During this time if {CronosConstants.Name} or one of his allies is targeted by a new harmful non-Affliction ability, " +
                      $"they will become {"invulnerable".HighlightInPurple()} for 1 turn.";
        SelfCast = true;
        Harmful = true;
        Debuff = true;
        Helpful = true;
        Buff = true;
        DisableDamageReceiveReduction = true;
        DisableInvulnerability = true;
        ActiveEffectOwner = CronosConstants.Name;
        ActiveEffectAlliesName = CronosConstants.BattlefieldBulwarkAllyActiveEffect;
        ActiveEffectEnemiesName = CronosConstants.BattlefieldBulwarkEnemyActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = Owner.BattlePlayer.PlayerNo == target.BattlePlayer.PlayerNo 
            ? 15 : T.GetDisableInvulnerabilityAndDamageReductionPoints(Duration1, target);
        return totalPoints;
    }
}
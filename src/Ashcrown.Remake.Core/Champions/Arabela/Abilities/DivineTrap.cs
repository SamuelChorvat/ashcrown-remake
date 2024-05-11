using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Arabela.Champion;

namespace Ashcrown.Remake.Core.Champions.Arabela.Abilities;

public class DivineTrap : AbilityBase
{
    public DivineTrap(IChampion champion) 
        : base(champion, 
            ArabelaConstants.DivineTrap, 
            1,
            [0,0,0,1,0],
            [AbilityClass.Magic, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDebuff,
            1)
    {
        Duration1 = 1;
        Damage1 = 15;
        ReceiveDamageReductionPoint1 = 15;
        BonusDamage1 = 15;
        Description = $"{ArabelaConstants.Name} traps one enemy. For {Duration1} turn, {ArabelaConstants.Name} will gain {$"{ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()}" +
                      $", and that enemy will take {$"{Damage1} {"piercing".HighlightInBold()} magic damage".HighlightInBlue()}" + 
                      $" that {"ignores invulnerability".HighlightInPurple()} at the end of their turn. " +
                      $"If that enemy uses a new ability, they will take another {$"{BonusDamage1} {"piercing".HighlightInBold()} magic damage".HighlightInBlue()}" +
                      $". This ability {"ignores invulnerability".HighlightInPurple()}. The target of this ability is {"invisible".HighlightInPurple()}.";
        Helpful = true;
        Harmful = true;
        Debuff = true;
        Buff = true;
        Damaging = true;
        MagicDamage = true;
        PiercingDamage = true;
        IgnoreInvulnerability = true;
        Invisible = true;
        SelfDisplay = true;
        ActiveEffectOwner = ArabelaConstants.Name;
        ActiveEffectName = ArabelaConstants.DivineTrapTargetActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetPointsReductionPoints(ReceiveDamageReductionPoint1, 1, Owner);
        totalPoints += T.GetDamagePoints(Damage1, 1, this, target);
        return totalPoints;
    }
}
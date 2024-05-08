using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cedric.Champion;

namespace Ashcrown.Remake.Core.Champions.Cedric.Abilities;

public class DarkSoul : AbilityBase
{
    public DarkSoul(IChampion champion) 
        : base(champion, 
            CedricConstants.DarkSoul,
            1, 
            new int[] {0,0,0,1,0}, 
            [AbilityClass.Magic, AbilityClass.Ranged,AbilityClass.Action], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyActionControl, 
            1)
    {
        Duration1 = 2;
        Damage1 = 15;
        Heal1 = 30;
        
        Description = $"{CedricConstants.Name} deals {$"{Damage1} magic damage".HighlightInBlue()} to one enemy for {Duration1} turns. " +
                      $"If that enemy dies while affected by this ability, {CedricConstants.Name} will heal {$"{Heal1} health".HighlightInGreen()}, " +
                      $"permanently gain {"5 points of damage reduction".HighlightInYellow()} and will permanently " +
                      $"deal an additional {"5 magic damage".HighlightInBlue()}.";
        SelfDisplay = true;
        Debuff = true;
        Harmful = true;
        Damaging = true;
        MagicDamage = true;

        ActiveEffectOwner = CedricConstants.Name;
        ActiveEffectSourceName = CedricConstants.DarkSoulMeActiveEffect;
        ActiveEffectTargetName = CedricConstants.DarkSoulTargetActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        int totalPoints;
        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(CedricConstants.MirrorImageActiveEffect)) {
            totalPoints = T.GetDamagePoints(Damage1,
                Duration1 + Owner.ActiveEffectController.
                    GetActiveEffectByName(CedricConstants.MirrorImageActiveEffect)!.Stacks,
                this, target);
        } else {
            totalPoints = T.GetDamagePoints(Damage1, Duration1, this, target);
        }
        totalPoints += 25;
        return totalPoints;
    }
}
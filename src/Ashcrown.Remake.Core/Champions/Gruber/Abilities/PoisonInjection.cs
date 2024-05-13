using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Garr.Champion;
using Ashcrown.Remake.Core.Champions.Gruber.Champion;

namespace Ashcrown.Remake.Core.Champions.Gruber.Abilities;

public class PoisonInjection : AbilityBase
{
    public PoisonInjection(IChampion champion) 
        : base(champion, 
            GruberConstants.PoisonInjection, 
            1,
            [0,0,0,1,0], 
            [AbilityClass.Physical, AbilityClass.Affliction, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamageAndDebuff, 
            1)
    {
        Damage1 = 10;
        Damage2 = 20;
        Duration1 = 1;
        
        Description = $"{GarrConstants.Name} deals {$"{Damage1} physical damage".HighlightInOrange()} to one enemy. " +
                      $"The following turn, that enemy will be {"stunned".HighlightInPurple()} for {Duration1} turn " +
                      $"and will take {$"{Damage2} affliction damage".HighlightInRed()}; that enemy will also be " +
                      $"{"stunned".HighlightInPurple()} for one extra turn by this ability for the rest of the game. " +
                      $"This ability cannot be used on a champion already affected by it.";
        Stun = true;
        StunType = [AbilityClass.All];
        Harmful = true;
        Debuff = true;
        PhysicalDamage = true;
        Damaging = true;
        ActiveEffectOwner = GruberConstants.Name;
        ActiveEffectName = GruberConstants.PoisonInjectionPartOneActiveEffect;
    }

    public override int[] TargetsModifier(int[] targets)
    {
        for (var i = 0; i < 3; i++) {
            if (Owner.BattlePlayer.GetEnemyPlayer().Champions[i].ActiveEffectController
                    .ActiveEffectPresentByActiveEffectName(GruberConstants.PoisonInjectionPartOneActiveEffect) ||
                Owner.BattlePlayer.GetEnemyPlayer().Champions[i].ActiveEffectController
                    .ActiveEffectPresentByActiveEffectName(GruberConstants.PoisonInjectionPartTwoActiveEffect)) {
                targets[i + 3] = 0;
            }
        }
        return targets;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        if(target.ActiveEffectController
           .ActiveEffectPresentByActiveEffectName(GruberConstants.PoisonInjectionStacksActiveEffect)) {
            totalPoints += T.GetStunPoints(this,
                Duration1 + target.ActiveEffectController
                    .GetActiveEffectByName(GruberConstants.PoisonInjectionStacksActiveEffect)!.Stacks, target);
        } else {
            totalPoints += T.GetStunPoints(this, Duration1, target);
        }
        totalPoints += Damage2 * 2;
        return totalPoints;
    }
}
using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Garr.Champion;

namespace Ashcrown.Remake.Core.Champions.Garr.Abilities;

public class BarbedChain : AbilityBase
{
    public BarbedChain(IChampion champion) 
        : base(champion, 
            GarrConstants.BarbedChain, 
            2,
            [0,1,0,0,0], 
            [AbilityClass.Physical, AbilityClass.Action, AbilityClass.Melee , AbilityClass.Instant], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyActionControl, 
            2)
    {
        Duration1 = 2;
        Duration2 = 1;
        Damage1 = 10;
        
        Description = $"{GarrConstants.Name} slashes one enemy with his chain. " +
                      $"They will receive {$"{Damage1} physical damage".HighlightInOrange()} for {Duration1} turns. " +
                      $"Garr will also become {"invulnerable".HighlightInPurple()} for {Duration2} turn.";
        SelfDisplay = true;
        Invulnerability = true;
        TypeOfInvulnerability = [AbilityClass.All];
        Harmful = true;
        Helpful = true;
        Debuff = true;
        Buff = true;
        Damaging = true;
        PhysicalDamage = true;

        ActiveEffectOwner = GarrConstants.Name;
        ActiveEffectSourceName = GarrConstants.BarbedChainMeActiveEffect;
        ActiveEffectTargetName = GarrConstants.BarbedChainTargetActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetInvulnerabilityPoints(Duration1, this, Owner);
        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GarrConstants.RecklessnessActiveEffect)) {
            totalPoints += T.GetDamagePoints(
                Damage1 + Owner.ActiveEffectController.GetActiveEffectByName(GarrConstants.RecklessnessActiveEffect)!.Stacks * 10,
                Duration1, this, target);
        } else {
            totalPoints += T.GetDamagePoints(Damage1, Duration1, this, target);
        }
        return totalPoints;
    }
}
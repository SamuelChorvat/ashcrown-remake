using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Hannibal.Champion;

namespace Ashcrown.Remake.Core.Champions.Hannibal.Abilities;

public class HealthFunnel : AbilityBase
{
    public HealthFunnel(IChampion champion) 
        : base(champion, 
            HannibalConstants.HealthFunnel, 
            3,
            [1,0,0,1,0], 
            [AbilityClass.Affliction,AbilityClass.Ranged,AbilityClass.Control], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyActionControl, 
            2)
    {
        Duration1 = 3;
        Damage1 = 15;
        
        Description = $"{HannibalConstants.Name} targets one enemy for {Duration1} turns, each turn {"stunning".HighlightInPurple()} their non-Strategic abilities, " +
                      $"and {"stealing".HighlightInPurple()} {$"{Damage1} health".HighlightInGreen()} from them. This ability will end if {HannibalConstants.Name} dies.";
        SelfDisplay = true;
        Harmful = true;
        Debuff = true;
        Stun = true;
        StunType = [AbilityClass.Magic,AbilityClass.Physical,AbilityClass.Affliction];
        Damaging = true;
        AfflictionDamage = true;

        ActiveEffectOwner = HannibalConstants.Name;
        ActiveEffectSourceName = HannibalConstants.HealthFunnelMeActiveEffect;
        ActiveEffectTargetName = HannibalConstants.HealthFunnelTargetActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, Duration1, this, target);
        totalPoints += T.GetHealingPoints(Math.Min(Damage1, target.Health), 
            Duration1, this, Owner);
        return totalPoints;
    }
}
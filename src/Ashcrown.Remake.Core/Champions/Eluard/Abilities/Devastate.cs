using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Eluard.Champion;

namespace Ashcrown.Remake.Core.Champions.Eluard.Abilities;

public class Devastate : Ability.Abstract.Ability
{
    public Devastate(IChampion champion) 
        : base(champion, 
            EluardConstants.Devastate, 
            1,
            [0,1,0,0,1],
            [AbilityClass.Physical, AbilityClass.Instant, AbilityClass.Melee], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamageAndDebuff)
    {
        Damage1 = 45;
        Duration1 = 1;
        Description = $"{EluardConstants.Eluard} hits one enemy using all his strength dealing {$"{Damage1} physical damage".HighlightInOrange()} " +
                      $"to them and {"stunning".HighlightInPurple()} all their abilities for {Duration1} turn. " +
                      $"This ability requires {EluardConstants.UnyieldingWill.HighlightInGold()}.";
        Stun = true;
        StunType = [AbilityClass.All];
        Harmful = true;
        Debuff = true;
        PhysicalDamage = true;
        Damaging = true;
        Active = false;
        ActiveEffectOwner = EluardConstants.Eluard;
        ActiveEffectName = EluardConstants.DevastateActiveEffect;
    }

    public override void StartTurnChecks()
    {
        if(IsReady())
        {
            Active = Owner.ActiveEffectController
                .ActiveEffectPresentByActiveEffectName(EluardConstants.UnyieldingWillActiveEffect);
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        totalPoints += T.GetStunPoints(this, Duration1, target);
        return totalPoints;
    }
}
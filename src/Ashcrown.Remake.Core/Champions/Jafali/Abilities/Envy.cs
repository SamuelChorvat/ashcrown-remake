using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Jafali.Champion;

namespace Ashcrown.Remake.Core.Champions.Jafali.Abilities;

public class Envy : AbilityBase
{
    public Envy(IChampion champion) 
        : base(champion,
            JafaliConstants.Envy, 
            0,
            [0,2,0,0,0], 
            [AbilityClass.Affliction, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemies, 
            AbilityType.EnemiesDebuff, 
            2)
    {
        DestructibleDefense1 = 30;
        
        Description = "Jafali applies <color=#EAB65B>Decaying Soul</color> to all enemies and gains <color=yellow>30 destructible defense</color>. Enemy that destroys the destructible defense will have <color=#EAB65B>Decaying Soul</color> applied to them. While the destructible defense is active this ability will be replaced by <color=#EAB65B>Avarice</color>.";
        Harmful = true;
        Debuff = true;

        ActiveEffectOwner = JafaliConstants.Name;
        ActiveEffectName = JafaliConstants.DecayingSoulActiveEffect;
    }

    public override void StartTurnChecks()
    {
        if (!IsReady()) return;
        if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(JafaliConstants.EnvyActiveEffect)) {
            Active = false;
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(5, 5, this, target);
        return totalPoints;
    }

    public override int CalculateSingletonSelfEffectTotalPoints<T>()
    {
        var totalPoints = T.GetDestructibleDefensePoints(DestructibleDefense1, Owner);
        return totalPoints;
    }
}
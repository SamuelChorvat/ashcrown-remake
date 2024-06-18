using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Nazuc.Champion;

namespace Ashcrown.Remake.Core.Champions.Nazuc.Abilities;

public class SpearBarrage : AbilityBase
{
    public SpearBarrage(IChampion champion) 
        : base(champion, 
            NazucConstants.SpearBarrage, 
            3, 
            new int[] {0,1,1,0,0}, 
            [AbilityClass.Physical, AbilityClass.Action, AbilityClass.Ranged , AbilityClass.Instant], 
            AbilityTarget.Enemies, 
            AbilityType.EnemiesActionControl, 
            2)
    {
        Duration1 = 3;
        Damage1 = 15;
        BonusDamage1 = 5;
        ReceiveDamageReductionPoint1 = 15;
        
        Description = "Nazuc attacks all enemies dealing <color=#FF5C04>15 physical damage</color> to them each turn for 3 turns. The following 3 turns <color=#EAB65B>Spear Throw</color> cost will be reduced by <sprite=4>. During this time Nazuc gains <color=yellow>15 points of damage reduction</color>.";
        SelfDisplay = true;
        Harmful = true;
        Helpful = true;
        Debuff = true;
        Buff = true;
        Damaging = true;
        PhysicalDamage = true;

        ActiveEffectOwner = NazucConstants.Name;
        ActiveEffectSourceName = NazucConstants.SpearBarrageMeActiveEffect;
        ActiveEffectTargetName = NazucConstants.SpearBarrageTargetActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(NazucConstants.HuntersMarkActiveEffect) 
            ? T.GetDamagePoints(Damage1 + BonusDamage1, Duration1, this, target) 
            : T.GetDamagePoints(Damage1, Duration1, this, target);
        return totalPoints;
    }

    public override int CalculateSingletonSelfEffectTotalPoints<T>()
    {
        var totalPoints = T.GetPointsReductionPoints(ReceiveDamageReductionPoint1, Duration1, Owner);
        totalPoints += AiCalculatorConstants.PenaltyPerEnergy;
        return totalPoints;
    }
}
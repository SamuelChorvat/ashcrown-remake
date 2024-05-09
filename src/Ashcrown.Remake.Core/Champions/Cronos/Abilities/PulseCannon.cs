using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cronos.Champion;

namespace Ashcrown.Remake.Core.Champions.Cronos.Abilities;

public class PulseCannon : AbilityBase
{
    public PulseCannon(IChampion champion) 
        : base(champion, 
            CronosConstants.PulseCannon, 
            1,
            [1,0,0,0,1], 
            [AbilityClass.Affliction, AbilityClass.Action, AbilityClass.Ranged], 
            AbilityTarget.Enemies, 
            AbilityType.EnemiesActionControl, 
            3)
    {
        Duration1 = 2;
        Damage1 = 15;
        
        Description = $"{CronosConstants.Name} deals {$"{Damage1} affliction damage".HighlightInRed()} to all enemies for {Duration1} turns.";
        SelfDisplay = true;
        Harmful = true;
        Debuff = true;
        Damaging = true;
        AfflictionDamage = true;

        ActiveEffectOwner = CronosConstants.Name;
        ActiveEffectSourceName = CronosConstants.PulseCannonMeActiveEffect;
        ActiveEffectTargetName = CronosConstants.PulseCannonTargetActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = target.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(CronosConstants.EMPBurstActiveEffect) 
            ? T.GetDamagePoints(Damage1 + 5, Duration1, this, target) 
            : T.GetDamagePoints(Damage1, Duration1, this, target);
        return totalPoints;
    }
}
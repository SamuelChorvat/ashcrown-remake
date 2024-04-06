using Ashcrown.Remake.Core.V1.Ability.Interfaces;
using Ashcrown.Remake.Core.V1.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.V1.Champion.Interfaces;

namespace Ashcrown.Remake.Core.V1.Champions.Cronos.ActiveEffects;

//TODO Check active effect creator works with EMP as Emp
public class EmpBurstActiveEffect
{
    //TODO Refactor this?
    public static int ApplyEmpBurstAfflictionDamageIncrease(IChampion victim, int damage, IAbility? ability = null,
        IActiveEffect? activeEffect = null)
    {
        return damage;
    }
}
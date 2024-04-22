using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Cronos.ActiveEffects;

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
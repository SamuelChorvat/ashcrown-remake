using Ashcrown.Remake.Core.V1.Ability.Interfaces;
using Ashcrown.Remake.Core.V1.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.V1.Champions.Samson.ActiveEffects;

namespace Ashcrown.Remake.Core.V1.Ability.Models;

public class AbilityReflected
{
    private readonly bool _reflected;

    public bool Reflected
    {
        get => !KingdomGuardianAllyActiveEffect.IsKingdomGuardianAllyActiveEffect(ReflectActiveEffect) && _reflected;
        private init => _reflected = value;
    }

    public IAbility ReflectedAbility { get; init; }
    private IActiveEffect? ReflectActiveEffect { get; init; }
    public int[] Targets { get; init; }

    public AbilityReflected(bool reflected, IAbility reflectedAbility, IActiveEffect? reflectActiveEffect, int[] targets)
    {
        Reflected = reflected;
        ReflectedAbility = reflectedAbility;
        ReflectActiveEffect = reflectActiveEffect;
        Targets = targets;

        if (reflected)
        {
            reflectActiveEffect?.ModifyTargetsOnReflect(reflectedAbility, targets);
        }
    }

    public bool IsSelfReflected()
    {
        return KingdomGuardianAllyActiveEffect.IsKingdomGuardianAllyActiveEffect(ReflectActiveEffect);
    }
}
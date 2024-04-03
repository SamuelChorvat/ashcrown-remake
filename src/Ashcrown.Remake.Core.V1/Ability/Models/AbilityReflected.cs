using Ashcrown.Remake.Core.V1.Ability.Interfaces;
using Ashcrown.Remake.Core.V1.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.V1.Champions.Samson.ActiveEffects;

namespace Ashcrown.Remake.Core.V1.Ability.Models;

public class AbilityReflected
{
    private readonly bool _reflected;

    public required bool Reflected
    {
        get => !KingdomGuardianAllyAE.IsKingdomGuardianAllyActiveEffect(ReflectActiveEffect) && _reflected;
        init => _reflected = value;
    }

    public required IAbility ReflectedAbility { get; init; }
    public required IActiveEffect ReflectActiveEffect { get; init; }
    public required int[] Targets { get; init; }

    public AbilityReflected(bool reflected, IAbility reflectedAbility, IActiveEffect reflectActiveEffect, int[] targets)
    {
        Reflected = reflected;
        ReflectedAbility = reflectedAbility;
        ReflectActiveEffect = reflectActiveEffect;
        Targets = targets;

        if (reflected)
        {
            reflectActiveEffect.ModifyTargetsOnReflect(reflectedAbility, targets);
        }
    }

    public bool IsSelfReflected()
    {
        return KingdomGuardianAllyAE.IsKingdomGuardianAllyActiveEffect(ReflectActiveEffect);
    }
}
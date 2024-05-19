using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Lexi.Champion;

namespace Ashcrown.Remake.Core.Champions.Lexi.ActiveEffects;

public class ShadowmeldActiveEffect(IAbility originAbility, IChampion championTarget)
    : StandardInvulnerabilityActiveEffect(originAbility,
        championTarget,
        LexiConstants.ShadowmeldActiveEffect);
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Branley.Champion;

namespace Ashcrown.Remake.Core.Champions.Branley.ActiveEffects;

public class DefensiveManeuverActiveEffect(IAbility originAbility, IChampion championTarget)
    : StandardInvulnerabilityActiveEffect(originAbility,
        championTarget,
        BranleyConstants.DefensiveManeuverActiveEffect);
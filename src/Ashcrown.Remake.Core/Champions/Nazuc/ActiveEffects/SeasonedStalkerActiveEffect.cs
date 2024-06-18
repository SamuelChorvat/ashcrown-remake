using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Nazuc.Champion;

namespace Ashcrown.Remake.Core.Champions.Nazuc.ActiveEffects;

public class SeasonedStalkerActiveEffect(IAbility originAbility, IChampion championTarget)
    : StandardInvulnerabilityActiveEffect(originAbility,
        championTarget,
        NazucConstants.SeasonedStalkerActiveEffect);
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Abstract;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Akio.Champion;

namespace Ashcrown.Remake.Core.Champions.Akio.ActiveEffects;

public class FlowDisruptionActiveEffect(IAbility originAbility, IChampion championTarget)
    : StandardInvulnerabilityActiveEffect(originAbility,
        championTarget,
        AkioConstants.FlowDisruptionActiveEffect);
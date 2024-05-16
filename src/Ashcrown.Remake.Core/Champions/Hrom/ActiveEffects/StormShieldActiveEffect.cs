using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Hrom.Champion;

namespace Ashcrown.Remake.Core.Champions.Hrom.ActiveEffects;

public class StormShieldActiveEffect(IAbility originAbility, IChampion championTarget)
    : StandardInvulnerabilityActiveEffect(originAbility,
        championTarget,
        HromConstants.StormShieldActiveEffect);
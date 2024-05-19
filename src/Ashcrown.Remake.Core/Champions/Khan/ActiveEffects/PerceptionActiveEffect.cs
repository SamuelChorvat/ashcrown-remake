using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Khan.Champion;

namespace Ashcrown.Remake.Core.Champions.Khan.ActiveEffects;

public class PerceptionActiveEffect(IAbility originAbility, IChampion championTarget)
    : StandardInvulnerabilityActiveEffect(originAbility,
        championTarget,
        KhanConstants.PerceptionActiveEffect);
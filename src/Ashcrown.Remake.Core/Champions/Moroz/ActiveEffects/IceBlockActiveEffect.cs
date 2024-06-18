using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Moroz.Champion;

namespace Ashcrown.Remake.Core.Champions.Moroz.ActiveEffects;

public class IceBlockActiveEffect(IAbility originAbility, IChampion championTarget)
    : StandardInvulnerabilityActiveEffect(originAbility,
        championTarget,
        MorozConstants.IceBlockActiveEffect);
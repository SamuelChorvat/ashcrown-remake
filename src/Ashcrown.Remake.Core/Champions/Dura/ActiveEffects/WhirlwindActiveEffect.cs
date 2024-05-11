using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Dura.Champion;

namespace Ashcrown.Remake.Core.Champions.Dura.ActiveEffects;

public class WhirlwindActiveEffect(IAbility originAbility, IChampion championTarget)
    : StandardInvulnerabilityActiveEffect(originAbility,
        championTarget,
        DuraConstants.WhirlwindActiveEffect);
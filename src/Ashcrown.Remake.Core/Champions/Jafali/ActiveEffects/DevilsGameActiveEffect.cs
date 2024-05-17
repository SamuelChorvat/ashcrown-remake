using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Jafali.Champion;

namespace Ashcrown.Remake.Core.Champions.Jafali.ActiveEffects;

public class DevilsGameActiveEffect(IAbility originAbility, IChampion championTarget)
    : StandardInvulnerabilityActiveEffect(originAbility,
        championTarget,
        JafaliConstants.DevilsGameActiveEffect);
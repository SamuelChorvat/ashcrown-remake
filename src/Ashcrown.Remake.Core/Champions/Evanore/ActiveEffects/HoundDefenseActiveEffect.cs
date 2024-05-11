using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Evanore.Champion;

namespace Ashcrown.Remake.Core.Champions.Evanore.ActiveEffects;

public class HoundDefenseActiveEffect(IAbility originAbility, IChampion championTarget)
    : StandardInvulnerabilityActiveEffect(originAbility,
        championTarget,
        EvanoreConstants.HoundDefenseActiveEffect);
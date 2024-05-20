using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Luther.Champion;

namespace Ashcrown.Remake.Core.Champions.Luther.ActiveEffects;

public class MoltenArmorActiveEffect(IAbility originAbility, IChampion championTarget)
    : StandardInvulnerabilityActiveEffect(originAbility,
        championTarget,
        LutherConstants.MoltenArmorActiveEffect);
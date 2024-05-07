using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Azrael.Champion;

namespace Ashcrown.Remake.Core.Champions.Azrael.ActiveEffects;

public class DisappearActiveEffect(IAbility originAbility, IChampion championTarget)
    : StandardInvulnerabilityActiveEffect(originAbility,
        championTarget,
        AzraelConstants.DisappearActiveEffect);
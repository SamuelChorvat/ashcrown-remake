using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Hannibal.Champion;

namespace Ashcrown.Remake.Core.Champions.Hannibal.ActiveEffects;

public class DemonicSkinActiveEffect(IAbility originAbility, IChampion championTarget)
    : StandardInvulnerabilityActiveEffect(originAbility,
        championTarget,
        HannibalConstants.DemonicSkinActiveEffect);
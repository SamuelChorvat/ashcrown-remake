using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Hannibal.Champion;

namespace Ashcrown.Remake.Core.Champions.Hannibal.Abilities;

public class DemonicSkin(IChampion champion) : StandardInvulnerability(champion,
    HannibalConstants.Name,
    HannibalConstants.DemonicSkin,
    HannibalConstants.DemonicSkinActiveEffect);
using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Moroz.Champion;

namespace Ashcrown.Remake.Core.Champions.Moroz.Abilities;

public class IceBlock(IChampion champion) : StandardInvulnerability(champion,
    MorozConstants.Name,
    MorozConstants.IceBlock,
    MorozConstants.IceBlockActiveEffect);
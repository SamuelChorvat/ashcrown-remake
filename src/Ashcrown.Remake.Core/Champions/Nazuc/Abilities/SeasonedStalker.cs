using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Nazuc.Champion;

namespace Ashcrown.Remake.Core.Champions.Nazuc.Abilities;

public class SeasonedStalker(IChampion champion) : StandardInvulnerability(champion,
    NazucConstants.Name,
    NazucConstants.SeasonedStalker,
    NazucConstants.SeasonedStalkerActiveEffect);
using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Branley.Champion;

namespace Ashcrown.Remake.Core.Champions.Branley.Abilities;

public class DefensiveManeuver(IChampion champion) : StandardInvulnerability(champion,
    BranleyConstants.Name,
    BranleyConstants.DefensiveManeuver,
    BranleyConstants.DefensiveManeuverActiveEffect);
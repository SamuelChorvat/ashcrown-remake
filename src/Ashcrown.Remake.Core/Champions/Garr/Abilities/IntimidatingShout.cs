using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Garr.Champion;

namespace Ashcrown.Remake.Core.Champions.Garr.Abilities;

public class IntimidatingShout(IChampion champion) : StandardInvulnerability(champion,
    GarrConstants.Name,
    GarrConstants.IntimidatingShout,
    GarrConstants.IntimidatingShoutActiveEffect);
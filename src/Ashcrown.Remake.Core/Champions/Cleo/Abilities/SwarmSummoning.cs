using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cleo.Champion;

namespace Ashcrown.Remake.Core.Champions.Cleo.Abilities;

public class SwarmSummoning(IChampion champion) : StandardInvulnerability(champion,
    CleoConstants.Name,
    CleoConstants.SwarmSummoning,
    CleoConstants.SwarmSummoningActiveEffect);
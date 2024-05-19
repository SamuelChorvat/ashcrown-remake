using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Khan.Champion;

namespace Ashcrown.Remake.Core.Champions.Khan.Abilities;

public class Perception(IChampion champion) : StandardInvulnerability(champion,
    KhanConstants.Name,
    KhanConstants.Perception,
    KhanConstants.PerceptionActiveEffect);
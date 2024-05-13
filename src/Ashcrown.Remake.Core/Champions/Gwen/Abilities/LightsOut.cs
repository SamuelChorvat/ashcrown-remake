using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Gwen.Champion;

namespace Ashcrown.Remake.Core.Champions.Gwen.Abilities;

public class LightsOut(IChampion champion) : StandardInvulnerability(champion,
    GwenConstants.Name,
    GwenConstants.LightsOut,
    GwenConstants.LightsOutActiveEffect);
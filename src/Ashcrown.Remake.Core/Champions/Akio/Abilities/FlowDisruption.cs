using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Akio.Champion;

namespace Ashcrown.Remake.Core.Champions.Akio.Abilities;

public class FlowDisruption(IChampion champion) : StandardInvulnerability(champion,
    AkioConstants.Name,
    AkioConstants.FlowDisruption,
    AkioConstants.FlowDisruptionActiveEffect);
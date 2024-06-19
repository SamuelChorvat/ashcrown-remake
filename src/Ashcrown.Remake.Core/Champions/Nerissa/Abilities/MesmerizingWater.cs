using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Nerissa.Champion;

namespace Ashcrown.Remake.Core.Champions.Nerissa.Abilities;

public class MesmerizingWater(IChampion champion) : StandardInvulnerability(champion,
    NerissaConstants.Name,
    NerissaConstants.MesmerizingWater,
    NerissaConstants.MesmerizingWaterActiveEffect);
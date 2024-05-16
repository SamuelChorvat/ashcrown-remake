using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Izrin.Champion;

namespace Ashcrown.Remake.Core.Champions.Izrin.Abilities;

public class WillOfTheUndead(IChampion champion) : StandardInvulnerability(champion,
    IzrinConstants.Name,
    IzrinConstants.WillOfTheUndead,
    IzrinConstants.WillOfTheUndeadActiveEffect);
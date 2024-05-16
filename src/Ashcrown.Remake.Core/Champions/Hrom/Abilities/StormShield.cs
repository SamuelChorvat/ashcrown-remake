using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Hrom.Champion;

namespace Ashcrown.Remake.Core.Champions.Hrom.Abilities;

public class StormShield(IChampion champion) : StandardInvulnerability(champion,
    HromConstants.Name,
    HromConstants.StormShield,
    HromConstants.StormShieldActiveEffect);
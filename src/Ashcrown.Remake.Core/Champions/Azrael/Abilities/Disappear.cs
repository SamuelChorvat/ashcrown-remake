using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Azrael.Champion;

namespace Ashcrown.Remake.Core.Champions.Azrael.Abilities;

public class Disappear(IChampion champion) : StandardInvulnerability(champion,
    AzraelConstants.Name,
    AzraelConstants.Disappear,
    AzraelConstants.DisappearActiveEffect);
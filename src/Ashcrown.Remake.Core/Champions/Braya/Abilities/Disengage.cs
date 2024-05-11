using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Braya.Champion;

namespace Ashcrown.Remake.Core.Champions.Braya.Abilities;

public class Disengage(IChampion champion) : StandardInvulnerability(champion,
    BrayaConstants.Name,
    BrayaConstants.Disengage,
    BrayaConstants.DisengageActiveEffect);
    
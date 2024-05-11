using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Dura.Champion;

namespace Ashcrown.Remake.Core.Champions.Dura.Abilities;

public class Meditate(IChampion champion) : StandardInvulnerability(champion,
    DuraConstants.Name,
    DuraConstants.Meditate,
    DuraConstants.MeditateActiveEffect);
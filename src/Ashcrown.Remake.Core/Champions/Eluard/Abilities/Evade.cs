using Ashcrown.Remake.Core.Ability.Abstract;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Eluard.Champion;

namespace Ashcrown.Remake.Core.Champions.Eluard.Abilities;

public class Evade(IChampion champion) : StandardInvulnerability(champion,
    EluardConstants.Eluard,
    EluardConstants.Evade,
    EluardConstants.EvadeActiveEffect);
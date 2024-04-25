using Ashcrown.Remake.Core.Ability.Abstract;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Sarfu.Champion;

namespace Ashcrown.Remake.Core.Champions.Sarfu.Abilities;

public class Deflect(IChampion champion) : StandardInvulnerability(champion,
    SarfuConstants.Name,
    SarfuConstants.Deflect,
    SarfuConstants.DeflectActiveEffect);
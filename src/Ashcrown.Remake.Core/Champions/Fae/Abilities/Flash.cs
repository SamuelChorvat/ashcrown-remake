using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Fae.Champion;

namespace Ashcrown.Remake.Core.Champions.Fae.Abilities;

public class Flash(IChampion champion) : StandardInvulnerability(champion,
    FaeConstants.Name,
    FaeConstants.Flash,
    FaeConstants.FlashActiveEffect);
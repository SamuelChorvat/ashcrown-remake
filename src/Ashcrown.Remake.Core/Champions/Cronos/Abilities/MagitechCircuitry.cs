using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cronos.Champion;

namespace Ashcrown.Remake.Core.Champions.Cronos.Abilities;

public class MagitechCircuitry(IChampion champion) : StandardInvulnerability(champion,
    CronosConstants.Name,
    CronosConstants.MagitechCircuitry,
    CronosConstants.MagitechCircuitryActiveEffect);
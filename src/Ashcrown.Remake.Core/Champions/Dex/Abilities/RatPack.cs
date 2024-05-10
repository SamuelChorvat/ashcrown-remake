using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Dex.Champion;

namespace Ashcrown.Remake.Core.Champions.Dex.Abilities;

public class RatPack(IChampion champion) : StandardInvulnerability(champion,
    DexConstants.Name,
    DexConstants.RatPack,
    DexConstants.RatPackActiveEffect);
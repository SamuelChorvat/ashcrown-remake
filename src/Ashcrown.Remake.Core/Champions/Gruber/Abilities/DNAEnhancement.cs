using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Gruber.Champion;

namespace Ashcrown.Remake.Core.Champions.Gruber.Abilities;

// ReSharper disable once InconsistentNaming
public class DNAEnhancement(IChampion champion) : StandardInvulnerability(champion,
    GruberConstants.Name,
    GruberConstants.DNAEnhancement,
    GruberConstants.DNAEnhancementActiveEffect);
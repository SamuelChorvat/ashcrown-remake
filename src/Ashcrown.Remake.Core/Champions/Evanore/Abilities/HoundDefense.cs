using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Evanore.Champion;

namespace Ashcrown.Remake.Core.Champions.Evanore.Abilities;

public class HoundDefense(IChampion champion) : StandardInvulnerability(champion,
    EvanoreConstants.Name,
    EvanoreConstants.HoundDefense,
    EvanoreConstants.HoundDefenseActiveEffect);
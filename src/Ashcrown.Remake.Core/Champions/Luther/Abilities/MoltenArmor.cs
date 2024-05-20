using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Luther.Champion;

namespace Ashcrown.Remake.Core.Champions.Luther.Abilities;

public class MoltenArmor(IChampion champion) : StandardInvulnerability(champion,
    LutherConstants.Name,
    LutherConstants.MoltenArmor,
    LutherConstants.MoltenArmorActiveEffect);
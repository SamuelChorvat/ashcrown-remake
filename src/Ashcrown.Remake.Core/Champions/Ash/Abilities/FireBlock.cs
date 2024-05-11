using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Ash.Champion;

namespace Ashcrown.Remake.Core.Champions.Ash.Abilities;

public class FireBlock(IChampion champion) : StandardInvulnerability(champion,
    AshConstants.Name,
    AshConstants.FireBlock,
    AshConstants.FireBlockActiveEffect);
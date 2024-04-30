using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Aniel.Champion;

namespace Ashcrown.Remake.Core.Champions.Aniel.Abilities;

public class AdrenalineRush(IChampion champion) : StandardInvulnerability(champion,
    AnielConstants.Name,
    AnielConstants.AdrenalineRush,
    AnielConstants.AdrenalineRushActiveEffect);
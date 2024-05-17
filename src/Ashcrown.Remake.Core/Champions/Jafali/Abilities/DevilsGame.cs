using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Jafali.Champion;

namespace Ashcrown.Remake.Core.Champions.Jafali.Abilities;

public class DevilsGame(IChampion champion) : StandardInvulnerability(champion,
    JafaliConstants.Name,
    JafaliConstants.DevilsGame,
    JafaliConstants.DevilsGameActiveEffect);
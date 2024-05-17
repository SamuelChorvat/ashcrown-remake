using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Jane.Champion;

namespace Ashcrown.Remake.Core.Champions.Jane.Abilities;

public class Misdirection(IChampion champion) : StandardInvulnerability(champion,
    JaneConstants.Name,
    JaneConstants.Misdirection,
    JaneConstants.MisdirectionActiveEffect);
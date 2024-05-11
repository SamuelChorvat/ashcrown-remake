using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Arabela.Champion;

namespace Ashcrown.Remake.Core.Champions.Arabela.Abilities;

public class HolyShield(IChampion champion) : StandardInvulnerability(champion,
    ArabelaConstants.Name,
    ArabelaConstants.HolyShield,
    ArabelaConstants.HolyShieldActiveEffect);
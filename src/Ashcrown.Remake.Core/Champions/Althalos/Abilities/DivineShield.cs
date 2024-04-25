using Ashcrown.Remake.Core.Ability.Abstract;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Althalos.Champion;

namespace Ashcrown.Remake.Core.Champions.Althalos.Abilities;

public class DivineShield(IChampion champion) : StandardInvulnerability(champion,
    AlthalosConstants.Name,
    AlthalosConstants.DivineShield,
    AlthalosConstants.DivineShieldActiveEffect);
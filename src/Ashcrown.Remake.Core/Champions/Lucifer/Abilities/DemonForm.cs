using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Lucifer.Champion;

namespace Ashcrown.Remake.Core.Champions.Lucifer.Abilities;

public class DemonForm(IChampion champion) : StandardInvulnerability(champion,
    LuciferConstants.Name,
    LuciferConstants.DemonForm,
    LuciferConstants.DemonFormActiveEffect);
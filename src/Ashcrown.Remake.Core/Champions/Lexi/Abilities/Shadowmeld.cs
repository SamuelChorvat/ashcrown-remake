using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Lexi.Champion;

namespace Ashcrown.Remake.Core.Champions.Lexi.Abilities;

public class Shadowmeld(IChampion champion) : StandardInvulnerability(champion,
    LexiConstants.Name,
    LexiConstants.Shadowmeld,
    LexiConstants.ShadowmeldActiveEffect);
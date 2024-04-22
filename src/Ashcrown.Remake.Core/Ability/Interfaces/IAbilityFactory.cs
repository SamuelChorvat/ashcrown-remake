using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Ability.Interfaces;

public interface IAbilityFactory
{
    IAbility CreateAbility(string championName, string abilityName, IChampion target);
}
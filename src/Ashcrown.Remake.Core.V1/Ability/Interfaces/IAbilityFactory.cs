using Ashcrown.Remake.Core.V1.Champion.Interfaces;

namespace Ashcrown.Remake.Core.V1.Ability.Interfaces;

public interface IAbilityFactory
{
    IAbility CreateAbility(string championName, string abilityName, IChampion target);
}
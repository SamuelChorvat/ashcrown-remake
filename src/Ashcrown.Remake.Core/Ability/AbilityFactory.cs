using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Ability;

public class AbilityFactory : IAbilityFactory
{
    public IAbility CreateAbility(string championName, string abilityName, IChampion target)
    {
        var formattedChampionName = championName.Replace("'","");
        var formattedAbilityName = string.Join(" ",
                abilityName.Split(' ').Select(word => char.ToUpper(word[0]) + word[1..].ToLower()))
            .Replace("'", "")
            .Replace(" ", "");
        var className = $"Ashcrown.Remake.Core.Champions.{formattedChampionName}.Abilities.{formattedAbilityName}";
        var type = Type.GetType(className);

        if (type == null) throw new Exception($"Ability class not found {championName}.{abilityName}");
        
        object[] constructorArgs = [target];
        var instance = Activator.CreateInstance(type, constructorArgs);

        if (instance == null) throw new Exception($"Ability instance creation failed {championName}.{abilityName}");
        return (IAbility) instance;
    }
}
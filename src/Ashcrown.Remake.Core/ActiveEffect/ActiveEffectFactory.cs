using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.ActiveEffect;

public class ActiveEffectFactory : IActiveEffectFactory
{
    public IActiveEffect CreateActiveEffect(string activeEffectOwner, string activeEffectName, IAbility ability, IChampion target)
    {
        var formattedChampionName = activeEffectOwner.Replace("'","");
        var className = $"Ashcrown.Remake.Core.Champions.{formattedChampionName}.ActiveEffects.{activeEffectName}";
        var type = Type.GetType(className);

        if (type == null) throw new Exception($"ActiveEffect class not found {activeEffectOwner}.{activeEffectName}, , {className}");
        
        object[] constructorArgs = [ability, target];
        var instance = Activator.CreateInstance(type, constructorArgs);

        if (instance == null) throw new Exception($"ActiveEffect instance creation failed {activeEffectOwner}.{activeEffectName}, {className}");
        return (IActiveEffect) instance;
    }
}
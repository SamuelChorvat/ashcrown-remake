using Ashcrown.Remake.Core.V1.Ability.Interfaces;
using Ashcrown.Remake.Core.V1.Champion.Interfaces;

namespace Ashcrown.Remake.Core.V1.ActiveEffect.Interfaces;

public interface IActiveEffectFactory
{
    IActiveEffect CreateActiveEffect(string activeEffectOwner, string activeEffectName, IAbility ability,
        IChampion target);
}
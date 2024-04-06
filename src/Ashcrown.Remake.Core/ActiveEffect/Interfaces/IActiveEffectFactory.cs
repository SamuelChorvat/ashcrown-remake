using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.ActiveEffect.Interfaces;

public interface IActiveEffectFactory
{
    IActiveEffect CreateActiveEffect(string activeEffectOwner, string activeEffectName, IAbility ability,
        IChampion target);
}
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Jafali.Champion;

namespace Ashcrown.Remake.Core.Champions.Jafali.ActiveEffects;

public class EnvyActiveEffect : ActiveEffectBase
{
    public EnvyActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(JafaliConstants.EnvyActiveEffect, originAbility, championTarget)
    {
    }
}
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Luther.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Luther.Champion;

public class Luther : ChampionBase<LutherConstants>
{
    public Luther(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new Flamestrike(this),
            new FieryBrand(this),
            new LivingForge(this),
            new MoltenArmor(this));
        
        Abilities[2].Add(new ForgeSpirit(this));
    }

    public override bool AiCanCounterAbilitySelf(IAbility ability)
    {
        return ability.Owner.BattlePlayer.PlayerNo != BattlePlayer.PlayerNo
               && ability.AbilityClassesContains(AbilityClass.Strategic)
               && !AbilityController.IsStunnedToUseAbility(CurrentAbilities[1])
               && (CurrentAbilities[1].Active || CurrentAbilities[1].JustUsed);
    }

    public override bool AiCanCounterAbilityTarget(IAbility ability)
    {
        return AiCanCounterAbilitySelf(ability);
    }
}
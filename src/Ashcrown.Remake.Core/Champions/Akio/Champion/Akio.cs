using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Akio.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Akio.Champion;

public class Akio : ChampionBase<AkioConstants>
{
    public Akio(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new MasterfulSlice(this),
            new DragonRage(this),
            new LightningSpeed(this),
            new FlowDisruption(this));
    }

    public override bool AiCanCounterAbilitySelf(IAbility ability)
    {
        if (!ability.Harmful
            || ability.Owner.BattlePlayer.PlayerNo == BattlePlayer.PlayerNo
            || AbilityController.IsStunnedToUseAbility(CurrentAbilities[2])) {
            return false;
        }

        return CurrentAbilities[2].Active 
               || CurrentAbilities[2].ToReady >= CurrentAbilities[2].GetCurrentCooldown();
    }
}
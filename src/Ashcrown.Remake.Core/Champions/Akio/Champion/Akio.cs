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
}
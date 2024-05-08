using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Cedric.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Cedric.Champion;

public class Cedric : ChampionBase<CedricConstants>
{
    public Cedric(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new DarkSoul(this),
            new MirrorImage(this),
            new TimeWarp(this),
            new NetherSlip(this));
    }
}
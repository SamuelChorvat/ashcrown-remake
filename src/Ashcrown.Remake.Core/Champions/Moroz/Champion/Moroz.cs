using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Moroz.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Moroz.Champion;

public class Moroz : ChampionBase<MorozConstants>
{
    public Moroz(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new Freeze(this),
            new IceBarrier(this),
            new FrozenArmor(this),
            new IceBlock(this));
        
        Abilities[0].Add(new Shatter(this));
    }
}
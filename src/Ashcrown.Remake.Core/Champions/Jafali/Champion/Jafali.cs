using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Jafali.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Jafali.Champion;

public class Jafali : ChampionBase<JafaliConstants>
{
    public Jafali(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new Anger(this),
            new Envy(this),
            new Pride(this),
            new DevilsGame(this));
        
        Abilities[0].Add(new DecayingSoul(this));
        Abilities[1].Add(new Avarice(this));
    }
}
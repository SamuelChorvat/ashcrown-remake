using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Cronos.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Cronos.Champion;

public class Cronos : ChampionBase<CronosConstants>
{
    public Cronos(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new GravityWell(this),
            new BattlefieldBulwark(this),
            new EMPBurst(this),
            new MagitechCircuitry(this));
        
        Abilities[2].Add(new PulseCannon(this));
    }
}
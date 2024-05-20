using Ashcrown.Remake.Core.Battle.Interfaces;
using Ashcrown.Remake.Core.Champion.Base;
using Ashcrown.Remake.Core.Champions.Lucifer.Abilities;
using Microsoft.Extensions.Logging;

namespace Ashcrown.Remake.Core.Champions.Lucifer.Champion;

public class Lucifer : ChampionBase<LuciferConstants>
{
    public Lucifer(IBattleLogic battleLogic, IBattlePlayer battlePlayer, int championNo, ILoggerFactory loggerFactory) 
        : base(battleLogic, battlePlayer, championNo, loggerFactory)
    {
        SetStartAbilities(
            new HeartOfDarkness(this),
            new ShadowBolts(this),
            new DarkChalice(this),
            new DemonForm(this));
        
        Abilities[2].Add(new CursedCrow(this));
    }
}
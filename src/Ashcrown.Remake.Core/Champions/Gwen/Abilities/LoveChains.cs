using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Gwen.Champion;

namespace Ashcrown.Remake.Core.Champions.Gwen.Abilities;

public class LoveChains : AbilityBase
{
    public LoveChains(IChampion champion) 
        : base(champion, 
            GwenConstants.LoveChains, 
            4,
            [0,0,0,1,1], 
            [AbilityClass.Strategic, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemies, 
            AbilityType.EnemiesDebuff, 
            3)
    {
        Duration1 = 1;
        
        Description = $"{GwenConstants.Name} {"stuns".HighlightInPurple()} all enemies for {Duration1} turn.";
        Stun = true;
        StunType = [AbilityClass.All];
        Harmful = true;
        Debuff = true;

        ActiveEffectOwner = GwenConstants.Name;
        ActiveEffectName = GwenConstants.LoveChainsActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetStunPoints(this, Duration1, target);
        return totalPoints;
    }
}
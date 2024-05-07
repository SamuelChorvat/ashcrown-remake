using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Ash.Champion;
using Ashcrown.Remake.Core.Champions.Azrael.Champion;

namespace Ashcrown.Remake.Core.Champions.Azrael.Abilities;

public class CursedMark : AbilityBase
{
    public CursedMark(IChampion champion) 
        : base(champion, 
            AzraelConstants.CursedMark, 
            1,
            [0,1,0,0,0],
            [AbilityClass.Magic, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Self, 
            AbilityType.AllyBuff, 
            3)
    {
        
        Duration1 = 1;
        Damage1 = 5;
        Damage2 = 20;
        
        Description = $"{AshConstants.Name} uses a curse to protect himself. For {Duration1} turn {AshConstants.Name} will be {"invulnerable".HighlightInPurple()} to enemy Strategic abilities and {AzraelConstants.Reap.HighlightInGold()} will deal " +
                      $"an additional {$"{Damage2} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()}. During this time, any enemy that uses a new ability on {AzraelConstants.Name} will be dealt " +
                      $"{$"{Damage1} {"piercing".HighlightInBold()} magic damage".HighlightInBlue()} each turn for the rest of the game. This effect stacks.";
        Invulnerability = true;
        TypeOfInvulnerability = [AbilityClass.Strategic];
        Helpful = true;
        Buff = true;
        SelfCast = true;
        Harmful = true;
        Debuff = true;
        PiercingDamage = true;
        MagicDamage = true;

        ActiveEffectOwner = AzraelConstants.Name;
        ActiveEffectName = AzraelConstants.CursedMarkMeActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetInvulnerabilityPoints(Duration1, this, Owner);
        totalPoints += 25;
        return  totalPoints;
    }
}
using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Lucifer.Champion;

namespace Ashcrown.Remake.Core.Champions.Lucifer.Abilities;

public class ShadowBolts : AbilityBase
{
    private AbilityType _abilityType;
    
    public ShadowBolts(IChampion champion) 
        : base(champion, 
            LuciferConstants.ShadowBolts, 
            0,
            [1,0,0,0,0], 
            [AbilityClass.Magic, AbilityClass.Instant, AbilityClass.Ranged], 
            AbilityTarget.Enemy, 
            AbilityType.EnemyDamage, 
            2)
    {
        Damage1 = 15;
        
        Description = $"1) Rank 1 deals {$"15 {"piercing".HighlightInBold()} magic damage".HighlightInBlue()} to one enemy and {"ignores invulnerability".HighlightInPurple()}." +
                      $"\n2) Rank 2 deals {"25 magic damage".HighlightInBlue()} to one enemy." +
                      $"\n3) Rank 3 deals {"20 magic damage".HighlightInBlue()} to all enemies.";
        Harmful = true;
        Damaging = true;
        MagicDamage = true;
        PiercingDamage = true;
        IgnoreInvulnerability = true;
    }
    
    public override AbilityType AbilityType
    {
        get
        {
            if (Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(LuciferConstants.HeartOfDarknessActiveEffect)
                && Owner.ActiveEffectController
                    .GetActiveEffectByName(LuciferConstants.HeartOfDarknessActiveEffect)!.Description.Contains("Rank 3")) {
                return AbilityType.EnemiesDamage;
            }

            return AbilityType.EnemyDamage;
        }
        set => _abilityType = value;
    }

    public override void StartTurnChecks()
    {
        if (!Owner.ActiveEffectController.ActiveEffectPresentByActiveEffectName(LuciferConstants.HeartOfDarknessActiveEffect) 
            || Owner.ActiveEffectController.GetActiveEffectByName(LuciferConstants.HeartOfDarknessActiveEffect)!.Description.Contains("Rank 1")) {
            Damage1 = 15;
            Target = AbilityTarget.Enemy;
            PiercingDamage = true;
            IgnoreInvulnerability = true;
        } else if (Owner.ActiveEffectController.GetActiveEffectByName(LuciferConstants.HeartOfDarknessActiveEffect)!.Description.Contains("Rank 2")) {
            Damage1 = 25;
            Target = AbilityTarget.Enemy;
            PiercingDamage = false;
            IgnoreInvulnerability = false;
        } else if (Owner.ActiveEffectController.GetActiveEffectByName(LuciferConstants.HeartOfDarknessActiveEffect)!.Description.Contains("Rank 3")) {
            Damage1 = 20;
            Target = AbilityTarget.Enemies;
            PiercingDamage = false;
            IgnoreInvulnerability = false;
        }
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetDamagePoints(Damage1, 1, this, target);
        return totalPoints;
    }
}
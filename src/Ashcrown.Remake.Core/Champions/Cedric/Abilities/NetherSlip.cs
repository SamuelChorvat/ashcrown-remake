using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cedric.Champion;

namespace Ashcrown.Remake.Core.Champions.Cedric.Abilities;

public class NetherSlip : AbilityBase
{
    public NetherSlip(IChampion champion) 
        : base(champion, 
            CedricConstants.NetherSlip, 
            4,
            [0,0,0,0,1], 
            [AbilityClass.Strategic, AbilityClass.Instant], 
            AbilityTarget.Self, 
            AbilityType.AllyBuff, 
            4)
    {
        Duration1 = 1;
        
        Description = $"This ability makes {CedricConstants.Name} {"invulnerable".HighlightInPurple()} for {Duration1} turn. " +
                      $"This ability is not affected by {CedricConstants.MirrorImage.HighlightInGold()}.";
        Invulnerability = true;
        TypeOfInvulnerability = [AbilityClass.All];
        Helpful = true;
        Buff = true;
        SelfCast = true;
        ActiveEffectOwner = CedricConstants.Name;
        ActiveEffectName = CedricConstants.NetherSlipActiveEffect;
        AiStandardSelfInvulnerability = true;
    }
}
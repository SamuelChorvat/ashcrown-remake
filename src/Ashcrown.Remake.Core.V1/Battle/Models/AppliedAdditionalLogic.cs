using Ashcrown.Remake.Core.V1.Battle.Enums;

namespace Ashcrown.Remake.Core.V1.Battle.Models;

public class AppliedAdditionalLogic
{
    private readonly List<AdditionalLogicPair> _additionalLogicPairs = [];
    
    public bool CheckIfAlreadyApplied(AdditionalLogic additionalLogic, string appliedTo)
    {
        return _additionalLogicPairs.Any(additionalLogicPair => 
            additionalLogicPair.AppliedAdditionalLogic.Equals(additionalLogic)
            && additionalLogicPair.AppliedToName.Equals(appliedTo));
    }

    public void AddAppliedAdditionalLogic(AdditionalLogic additionalLogic, string appliedTo) {
        _additionalLogicPairs.Add(new AdditionalLogicPair
        {
            AppliedAdditionalLogic = additionalLogic,
            AppliedToName = appliedTo
        });
    }
}
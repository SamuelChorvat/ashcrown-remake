using Ashcrown.Remake.Core.V1.Battle.Enums;

namespace Ashcrown.Remake.Core.V1.Battle.Models;

public class AdditionalLogicPair
{
    public AdditionalLogic AppliedAdditionalLogic { get; set; }
    public required string AppliedToName { get; set; }
}
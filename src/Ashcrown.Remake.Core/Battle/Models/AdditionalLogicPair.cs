using Ashcrown.Remake.Core.Battle.Enums;

namespace Ashcrown.Remake.Core.Battle.Models;

public class AdditionalLogicPair
{
    public AdditionalLogic AppliedAdditionalLogic { get; set; }
    public required string AppliedToName { get; set; }
}